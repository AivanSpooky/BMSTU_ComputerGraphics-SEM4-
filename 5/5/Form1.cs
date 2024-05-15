using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static _5.Draw;

namespace _5
{
    public partial class Form1 : Form
    {
        private BindingSource bindingSource = new BindingSource();
        public Form1()
        {
            InitializeComponent();
            dataGridView1.DataSource = bindingSource;
            UpdateDataGridView();
        }

        private void UpdateDataGridView()
        {
            // Объединяем точки из всех фигур в один список
            List<PointD> allPoints = new List<PointD>();
            foreach (Figure figure in Draw.figureList)
            {
                allPoints.AddRange(figure.pointListFig);
            }
            // Устанавливаем этот список в качестве источника данных для DataGridView
            bindingSource.DataSource = allPoints;
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
            if (e.Button == MouseButtons.Right)
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
                    Pen pen = new Pen(Params.pen_color);
                    BresenhamLineDrawer.BresenhamInt(curPoints[0], curPoints[curPoints.Count - 1], mainG.CreateGraphics(), pen, true);
                    pen.Dispose();
                }
            }
            else if (e.Button == MouseButtons.Left)
            {
                // Получаем координаты нажатия мыши относительно PictureBox
                int x = e.X;
                int y = e.Y;

                point_add(x, y);
            }
        }

        private void point_add(int x, int y)
        {
            int xvalue = x >= mainG.Width ? mainG.Width - 1 : x;
            xvalue = xvalue < 0 ? 0 : xvalue;
            int yvalue = y >= mainG.Height ? mainG.Height - 1 : y;
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

            Pen pen = new Pen(Params.pen_color);
            Draw.set_pixel(mainG.CreateGraphics(), pen, true, xvalue, yvalue);
            //
            List<PointD> curPoints = figureList[figureList.Count - 1].pointListFig;
            if (curPoints.Count >= 2)
            {
                BresenhamLineDrawer.BresenhamInt(curPoints[curPoints.Count - 2], curPoints[curPoints.Count - 1], mainG.CreateGraphics(), pen, true);
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
                Pen pen = new Pen(Params.pen_color);
                BresenhamLineDrawer.BresenhamInt(curPoints[0], curPoints[curPoints.Count - 1], mainG.CreateGraphics(), pen, true);
                pen.Dispose();
            }
        }

        private void btn_draw_Click(object sender, EventArgs e)
        {
            if (!Params.is_filling)
            {
                Params.is_filling = true;
                Bitmap img = new Bitmap(mainG.Width, mainG.Height);

                // Заполняем изображение нужным фоновым цветом, если это необходимо
                using (Graphics g = Graphics.FromImage(img))
                {
                    g.Clear(Color.White); // Заполняем белым цветом, например
                }

                List<PointD> allPoints = new List<PointD>();
                foreach (Figure figure in Draw.figureList)
                {
                    allPoints.AddRange(figure.pointListFig);
                }
                int minX = allPoints.Min(point => point.X);
                int maxX = allPoints.Max(point => point.X);
                int minY = allPoints.Min(point => point.Y);
                int maxY = allPoints.Max(point => point.Y);

                DateTime startTime = DateTime.Now;
                //FillAlgorithm.Fill(ref img, mainG, Color.Orange, Color.White, Params.pen_color, new PointD(0, 0, false), new PointD(mainG.Width - 1, mainG.Height - 1, false), Params.delay_mode);
                FillAlgorithm.Fill(ref img, mainG, Color.Orange, Color.White, Params.pen_color, new PointD(0, minY - 1, false), new PointD(mainG.Width - 1, maxY + 1, false), Params.delay_mode);
                TimeSpan duration = DateTime.Now - startTime;

                string timeStr = duration.TotalSeconds.ToString("0.00") + "s";
                lbl_fill_time.Text = $"Время закраски области: {timeStr}";

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

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Params.change_segment_mode(false);
        }

        private void rb_input_segments_CheckedChanged(object sender, EventArgs e)
        {
            Params.change_segment_mode(true);
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
    }
}
