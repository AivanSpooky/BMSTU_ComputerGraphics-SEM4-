using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static _6.Draw;

namespace _6
{
    public partial class Form1 : Form
    {
        private BindingSource bindingSource = new BindingSource();
        private BindingSource bindingSource1 = new BindingSource();
        private Bitmap drawingBitmap;
        public Form1()
        {
            InitializeComponent();
            drawingBitmap = new Bitmap(mainG.Width, mainG.Height);
            dataGridView1.DataSource = bindingSource;
            dataGridView2.DataSource = bindingSource1;
            mainG.Image = new Bitmap(mainG.Width, mainG.Height);
            UpdateDataGridView();
        }

        private void dataGridView2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Проверяем, что форматируется столбец "col"
            if (dataGridView2.Columns[e.ColumnIndex].Name == "col")
            {
                // Получаем текущий объект данных из соответствующей строки источника данных BindingSource
                var currentPoint = bindingSource1[e.RowIndex] as PointD;

                // Проверяем, что объект данных не null
                if (currentPoint != null)
                {
                    // Устанавливаем цвет фона ячейки в соответствии с цветом точки
                    e.CellStyle.BackColor = currentPoint.col;
                    e.CellStyle.ForeColor = ContrastColor(currentPoint.col); // Выбираем цвет текста, который лучше виден на заданном цвете фона
                }
            }
        }

        // Метод для выбора цвета текста, который лучше виден на заданном цвете фона
        private Color ContrastColor(Color color)
        {
            // Простой алгоритм для выбора цвета текста на основе яркости фона
            int brightness = (int)(color.R * 0.299 + color.G * 0.587 + color.B * 0.114);
            return brightness > 128 ? Color.Black : Color.White;
        }

        private void UpdateDataGridView()
        {
            // Объединяем точки из всех фигур в один список
            List<PointD> allPoints = new List<PointD>();
            foreach (Figure figure in Draw.figureList)
            {
                allPoints.AddRange(figure.pointListFig);
            }
            bindingSource.DataSource = allPoints;

            List<PointD> ssPoints = new List<PointD>();
            foreach (PointD pxl in Draw.seedPixels)
            {
                ssPoints.Add(pxl);
            }
            bindingSource1.DataSource = ssPoints;
        }

        public bool ReadFieldsFromCircleBoxes(TextBox x, TextBox y, ref int xv, ref int yv, out string errorMessage)
        {
            errorMessage = "";
            if (!int.TryParse(x.Text, out int xValue))
            {
                errorMessage = "Ошибка. На ввод Х ожидалось целое число.";
                return false;
            }
            if (!int.TryParse(y.Text, out int yValue))
            {
                errorMessage = "Ошибка. На ввод Y ожидалось целое число.";
                return false;
            }

            if (xValue < 0)
            {
                errorMessage = "Ошибка. На ввод Y ожидалось положительное целое число.";
                return false;
            }
            else if (yValue < 0)
            {
                errorMessage = "Ошибка. На ввод Y ожидалось положительное целое число.";
                return false;
            }

            xv = xValue;
            yv = yValue;

            return true;
        }

        public bool ReadFieldsFromSeedBoxes(TextBox x, TextBox y, ref int xv, ref int yv, out string errorMessage)
        {
            errorMessage = "";
            if (!int.TryParse(x.Text, out int xValue))
            {
                errorMessage = "Ошибка. На ввод Х ожидалось целое число.";
                return false;
            }
            if (!int.TryParse(y.Text, out int yValue))
            {
                errorMessage = "Ошибка. На ввод Y ожидалось целое число.";
                return false;
            }

            if (xValue < 0)
            {
                errorMessage = "Ошибка. На ввод Y ожидалось положительное целое число.";
                return false;
            }
            else if (yValue < 0)
            {
                errorMessage = "Ошибка. На ввод Y ожидалось положительное целое число.";
                return false;
            }

            xv = xValue;
            yv = yValue;

            return true;
        }

        private void rb_mode_nodelay_CheckedChanged(object sender, EventArgs e)
        {
            Params.change_dealy_mode(false);
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            /*if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewCell cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                dataGridView1.BeginEdit(true);
            }*/
        }

        private void mainG_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // Получаем координаты нажатия мыши относительно PictureBox
                int x = e.X;
                int y = e.Y;

                point_add(x, y);
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (figureList.Count == 0 || Draw.figureList[figureList.Count - 1].pointListFig.Count == 0 || create_new_fig)
                {
                    MessageBox.Show("Нет фигур для замыкания!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                // Устанавливаем флаг create_new_fig в true
                else if (Draw.figureList[figureList.Count - 1].pointListFig.Count < 3)
                {
                    MessageBox.Show("Для замыкания новой фигуры нужно добавить как минимум 3 точки.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    Draw.create_new_fig = true;
                    List<PointD> curPoints = figureList[figureList.Count - 1].pointListFig;
                    Pen pen = new Pen(Params.border_color);
                    BresenhamLineDrawer.BresenhamInt(curPoints[0], curPoints[curPoints.Count - 1], mainG, pen, true);
                    pen.Dispose();
                }
            }
            
            else if (e.Button == MouseButtons.Middle) // Проверяем, что было нажато колесико
            {
                // Добавляем затравочный пиксель в список seedPixels
                Draw.seedPixels.Add(new PointD(e.X, e.Y, false, Params.pen_color));
                Pen pen = new Pen(Params.pen_color);
                Draw.set_pixel(ref mainG, pen.Color, true, e.X, e.Y);
                UpdateDataGridView();
            }
        }

        private void point_add(int x, int y)
        {
            mainG.Refresh();
            int xvalue = x > mainG.Width ? mainG.Width : x;
            xvalue = xvalue < 0 ? 0 : xvalue;
            int yvalue = y > mainG.Height ? mainG.Height : y;
            yvalue = yvalue < 0 ? 0 : yvalue;

            PointD point = new PointD(xvalue, yvalue);
            if (figureList.Count > 0 && figureList[figureList.Count - 1].pointListFig.Contains(point))
            {
                // Если точка уже существует, не добавляем ее
                return;
            }

            // Создаем новую точку и добавляем её в pointList
            Draw.AddPointToCurrentFigure(point);

            UpdateDataGridView();

            Pen pen = new Pen(Params.border_color);
            Draw.set_pixel(ref mainG, pen.Color, true, xvalue, yvalue);
            //
            List<PointD> curPoints = figureList[figureList.Count - 1].pointListFig;
            if (curPoints.Count >= 2)
            {
                BresenhamLineDrawer.BresenhamInt(curPoints[curPoints.Count - 2], curPoints[curPoints.Count - 1], mainG, pen, true);
            }
            pen.Dispose();
        }

        private void rb_col_black_CheckedChanged(object sender, EventArgs e)
        {
            Params.change_pen_col(Color.Black);
        }

        private void rb_col_blue_CheckedChanged(object sender, EventArgs e)
        {
            Params.change_pen_col(Color.Blue);
        }

        private void rb_col_green_CheckedChanged(object sender, EventArgs e)
        {
            Params.change_pen_col(Color.Green);
        }

        private void rb_col_red_CheckedChanged(object sender, EventArgs e)
        {
            Params.change_pen_col(Color.Red);
        }

        private void rb_col_purple_CheckedChanged(object sender, EventArgs e)
        {
            Params.change_pen_col(Color.Purple);
        }

        private void rb_mode_delay_CheckedChanged(object sender, EventArgs e)
        {
            Params.change_dealy_mode(true);
        }

        private void btn_clean_Click(object sender, EventArgs e)
        {
            if (!Params.is_filling)
            {
                Draw.clean_picturebox(mainG);
                DeletePoints();
                UpdateDataGridView();
            }
            else
                MessageBox.Show("Алгоритм заполнения не закончил текущую работу!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btn_add_point_Click(object sender, EventArgs e)
        {
            int x = 0, y = 0;
            if (!ReadFieldsFromCircleBoxes(tb_x_entry, tb_y_entry, ref x, ref y, out string errMsg))
            {
                MessageBox.Show(errMsg, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            point_add(x, y);
        }

        private void btn_close_fig_Click(object sender, EventArgs e)
        {
            if (figureList.Count == 0 || Draw.figureList[figureList.Count - 1].pointListFig.Count == 0 || create_new_fig)
            {
                MessageBox.Show("Нет фигур для замыкания!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // Устанавливаем флаг create_new_fig в true
            else if (Draw.figureList[figureList.Count - 1].pointListFig.Count < 3)
            {
                MessageBox.Show("Для замыкания новой фигуры нужно добавить как минимум 3 точки.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Draw.create_new_fig = true;
                List<PointD> curPoints = figureList[figureList.Count - 1].pointListFig;
                Pen pen = new Pen(Params.border_color);
                BresenhamLineDrawer.BresenhamInt(curPoints[0], curPoints[curPoints.Count - 1], mainG, pen, true);
                pen.Dispose();
            }
        }

        private void btn_draw_Click(object sender, EventArgs e)
        {
            if (!Params.is_filling)
            {
                Params.is_filling = true;

                // Создаем новый Bitmap для хранения содержимого PictureBox
                Bitmap img = new Bitmap(mainG.Width, mainG.Height);

                // Если нужно сохранить текущее содержимое PictureBox
                if (!create_new_fig)
                {
                    MessageBox.Show("Не все фигуры замкнуты!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Params.is_filling = false;
                    return;
                }

                // Опционально: сохраняем текущее содержимое PictureBox в img
                mainG.DrawToBitmap(img, mainG.ClientRectangle);

                // Запускаем заполнение для каждого затравочного пикселя
                foreach (PointD seedPixel in Draw.seedPixels)
                {
                    FillFigure(ref img, mainG, seedPixel.col, Params.border_color, lbl_fill_time, seedPixel);
                }

                if (Draw.seedPixels.Count == 0)
                {
                    MessageBox.Show("Отсутствует затравка!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Params.is_filling = false;
                    return;
                }

                Params.is_filling = false;
            }
            else
            {
                MessageBox.Show("Алгоритм заполнения не закончил текущую работу!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tb_x_entry_TextChanged(object sender, EventArgs e)
        {

        }

        private void rb_col_yellow_CheckedChanged(object sender, EventArgs e)
        {
            Params.change_pen_col(Color.Yellow);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int x = 0, y = 0;
            if (!ReadFieldsFromSeedBoxes(tb_xz_entry, tb_yz_entry, ref x, ref y, out string errMsg))
            {
                MessageBox.Show(errMsg, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Draw.seedPixels.Add(new PointD(x, y, false, Params.pen_color));
            Pen pen = new Pen(Params.pen_color);
            Draw.set_pixel(ref mainG, pen.Color, true, x, y);
            UpdateDataGridView();
        }

        private void mainG_MouseMove(object sender, MouseEventArgs e)
        {
            if (Params.segment_mode == false)
            {
                // Проверяем, нажата ли левая кнопка мыши
                if (e.Button == MouseButtons.Left)
                {
                    // Получаем координаты перемещения мыши относительно PictureBox
                    int x = e.X;
                    int y = e.Y;

                    // Добавляем точку
                    point_add(x, y);
                }
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Params.change_segment_mode(false);
        }

        private void rb_input_segments_CheckedChanged(object sender, EventArgs e)
        {
            Params.change_segment_mode(true);
        }
    }
}
