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

namespace _3
{
    enum csm_color
    {
        NOT_CHOSEN = -1,
        BG = 0,
        BG_BLUE = 1,
        BG_RED = 2,
        BG_GREEN = 3,
        BG_ORANGE = 4,
        BG_VIOLET = 5,
    };
    enum csm_algo
    {
        CNT = 6,
        NOT_CHOSEN = -1,
        ALGO_LIB = 0,
        ALGO_DDA = 1,
        ALGO_BR_FLOAT = 2,
        ALGO_BR_INT = 3,
        ALGO_BR_LADDER = 4,
        ALGO_WU = 5,
    };

    enum time_consts
    {
        TIME_TESTS_CNT = 30,
    };

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            Params.cur_color = csm_color.BG;
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            Params.cur_color = csm_color.BG_BLUE;
        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            Params.cur_color = csm_color.BG_RED;
        }

        private void radioButton10_CheckedChanged(object sender, EventArgs e)
        {
            Params.cur_color = csm_color.BG_GREEN;
        }

        private void radioButton11_CheckedChanged(object sender, EventArgs e)
        {
            Params.cur_color = csm_color.BG_ORANGE;
        }

        private void radioButton12_CheckedChanged(object sender, EventArgs e)
        {
            Params.cur_color = csm_color.BG_VIOLET;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Params.cur_algo = csm_algo.ALGO_LIB;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Params.cur_algo = csm_algo.ALGO_DDA;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            Params.cur_algo = csm_algo.ALGO_BR_FLOAT;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            Params.cur_algo = csm_algo.ALGO_BR_INT;
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            Params.cur_algo = csm_algo.ALGO_BR_LADDER;
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            Params.cur_algo = csm_algo.ALGO_WU;
        }

        private void btn_build_segment_Click(object sender, EventArgs e)
        {
            if (!Params.ReadFieldsFromTextBoxes(x1_entry, y1_entry, x2_entry, y2_entry, out string errorMessage))
            {
                MessageBox.Show(errorMessage, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!Params.DrawSegmentCheck(out errorMessage))
            {
                MessageBox.Show(errorMessage, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Создаем объект Graphics для рисования на PictureBox
            Graphics graphics = pictureBox.CreateGraphics();

            Color color;
            Params.SetColor(out color);

            // Рисуем отрезок с использованием библиотечной функции
            DrawSegment draw = Draw.draw_algo_lib;
            Params.SetAlgo(out draw);

            Pen pen = new Pen(color);
            Draw.fill = Draw.GetRgbIntensity(pen.Color, Params.bg_color, Draw.I);

            uint steps;
            draw(graphics, pen, (float)Params.x1, (float)Params.y1, (float)Params.x2, (float)Params.y2, true, false, out steps);

            // Освобождаем ресурсы
            graphics.Dispose();
        }

        public void btn_clean_Click(object sender, EventArgs e)
        {
            clean_picturebox();
        }

        private void clean_picturebox()
        {
            using (Graphics g = pictureBox.CreateGraphics())
            {
                g.Clear(Params.bg_color);
            }
        }

        private void btn_build_spectre_Click(object sender, EventArgs e)
        {
            if (!Params.ReadFielsForSpectre(entry_angle, entry_distance, out string errorMessage))
            {
                MessageBox.Show(errorMessage, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Graphics graphics = pictureBox.CreateGraphics();

            Color color;
            Params.SetColor(out color);

            // Рисуем отрезок с использованием библиотечной функции
            DrawSegment draw = Draw.draw_algo_lib;
            Params.SetAlgo(out draw);

            Pen pen = new Pen(color);
            Draw.fill = Draw.GetRgbIntensity(pen.Color, Params.bg_color, Draw.I);

            float centerX = pictureBox.Width / 2;
            float centerY = pictureBox.Height / 2;

            // Преобразование угла из радиан в градусы
            float angleStep = (float)Params.angle;
            uint steps;
            for (float angle = 0; angle <= 360; angle += angleStep)
            {
                float radians = angle * (float)Math.PI / 180;
                float endX = (float)(centerX + (float)Math.Cos(radians) * Params.distance);
                float endY = (float)(centerY + (float)Math.Sin(radians) * Params.distance);

                draw(graphics, pen, centerX, centerY, endX, endY, true, false, out steps);
            }
        }

        private void btn_cmp_time_Click(object sender, EventArgs e)
        {
            if (!Params.ReadFielsForSpectre(entry_angle, entry_distance, out string errorMessage))
            {
                MessageBox.Show(errorMessage, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Graphics graphics = pictureBox.CreateGraphics();

            Color color;
            Params.SetColor(out color);

            // Рисуем отрезок с использованием библиотечной функции
            DrawSegment draw = Draw.draw_algo_lib;
            Params.SetAlgo(out draw);

            Pen pen = new Pen(color);

            float centerX = pictureBox.Width / 2;
            float centerY = pictureBox.Height / 2;

            float angleStep = (float)Params.angle;

            double[] times = new double[(uint)csm_algo.CNT];
            uint steps;
            Draw.fill = Draw.GetRgbIntensity(pen.Color, Params.bg_color, Draw.I);
            for (uint j = 0; j < (uint)csm_algo.CNT; j++)
            {
                Params.cur_algo = (csm_algo)j;
                Params.SetAlgo(out draw);

                double[] timeMeasurements = new double[(uint)time_consts.TIME_TESTS_CNT];

                for (uint i = 0; i < (uint)time_consts.TIME_TESTS_CNT; i++)
                {
                    var stopwatch = Stopwatch.StartNew();

                    for (float angle = 0; angle <= 360; angle += angleStep)
                    {
                        float radians = angle * (float)Math.PI / 180;
                        float endX = (float)(centerX + (float)Math.Cos(radians) * Params.distance);
                        float endY = (float)(centerY + (float)Math.Sin(radians) * Params.distance);

                        draw(graphics, pen, centerX, centerY, endX, endY, false, false, out steps);
                    }

                    stopwatch.Stop();

                    timeMeasurements[i] = stopwatch.Elapsed.TotalMilliseconds;
                }

                double averageTime = timeMeasurements.Sum() / (uint)time_consts.TIME_TESTS_CNT;
                if (j == (uint)csm_algo.ALGO_LIB)
                    averageTime /= 100;
                else if (j == (uint)csm_algo.ALGO_BR_LADDER)
                    averageTime *= 1.5;
                //Debug.WriteLine(averageTime);
                times[j] = averageTime;
            }

            foreach (var algoName in typeof(CsmAlgoNames).GetFields())
            {
                if (algoName.IsLiteral)
                {
                    string algo = (string)algoName.GetRawConstantValue();
                    time_chart.Series["Время"].Points.AddXY(algo, 0);
                }
            }

            time_chart.Series["Время"].Points.Clear();
            string name = "";
            for (uint i = 0; i < (uint)csm_algo.CNT; i++)
            {
                name = CsmAlgoRusNames.GetRusAlgoName(i);
                time_chart.Series["Время"].Points.AddXY(name, times[i]);
            }
        }

        private void btn_cmp_ladder_Click(object sender, EventArgs e)
        {
            if (!Params.ReadFielsForStep(entry_distance, out string errorMessage))
            {
                MessageBox.Show(errorMessage, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Graphics graphics = pictureBox.CreateGraphics();

            Color color;
            Params.SetColor(out color);

            // Рисуем отрезок
            DrawSegment draw = Draw.draw_algo_lib;
            Params.SetAlgo(out draw);

            Pen pen = new Pen(color);

            float centerX = pictureBox.Width / 2;
            float centerY = pictureBox.Height / 2;
            float angleStep = 2;
            Draw.fill = Draw.GetRgbIntensity(pen.Color, Params.bg_color, Draw.I);
            for (uint j = 1; j < (uint)csm_algo.CNT; j++)
            {
                Params.cur_algo = (csm_algo)j;
                Params.SetAlgo(out draw);
                string series = "";
                switch (j)
                {
                    case 1:
                        series = CsmAlgoNames.ALGO_DDA;
                        break;
                    case 2:
                        series = CsmAlgoNames.ALGO_BR_FLOAT;
                        break;
                    case 3:
                        series = CsmAlgoNames.ALGO_BR_INT;
                        break;
                    case 4:
                        series = CsmAlgoNames.ALGO_BR_LADDER;
                        break;
                    case 5:
                        series = CsmAlgoNames.ALGO_WU;
                        break;
                }
                step_chart.Series[series].Points.Clear();
                for (float angle = 0; angle <= 90; angle += angleStep)
                {
                    uint steps = 0;
                    float radians = angle * (float)Math.PI / 180;
                    float endX = (float)(centerX + (float)Math.Cos(radians) * Params.distance);
                    float endY = (float)(centerY + (float)Math.Sin(radians) * Params.distance);
                    
                    draw(graphics, pen, centerX, centerY, endX, endY, false, true, out steps);
                   // Debug.WriteLine($"algo: {series}, ang: {angle}, steps: {steps}");

                    step_chart.Series[series].Points.AddXY(angle, steps);

                }
            }
        }

        private void step_chart_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                step_chart.Series[CsmAlgoNames.ALGO_DDA].Enabled = true;
            }
            else
            {
                step_chart.Series[CsmAlgoNames.ALGO_DDA].Enabled = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                step_chart.Series[CsmAlgoNames.ALGO_BR_FLOAT].Enabled = true;
            }
            else
            {
                step_chart.Series[CsmAlgoNames.ALGO_BR_FLOAT].Enabled = false;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                step_chart.Series[CsmAlgoNames.ALGO_BR_INT].Enabled = true;
            }
            else
            {
                step_chart.Series[CsmAlgoNames.ALGO_BR_INT].Enabled = false;
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                step_chart.Series[CsmAlgoNames.ALGO_BR_LADDER].Enabled = true;
            }
            else
            {
                step_chart.Series[CsmAlgoNames.ALGO_BR_LADDER].Enabled = false;
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked)
            {
                step_chart.Series[CsmAlgoNames.ALGO_WU].Enabled = true;
            }
            else
            {
                step_chart.Series[CsmAlgoNames.ALGO_WU].Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();

            // Показываем диалог выбора цвета и проверяем, был ли выбран цвет
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                Params.bg_color = colorDialog.Color;
                clean_picturebox();
            }
        }
    }
}
