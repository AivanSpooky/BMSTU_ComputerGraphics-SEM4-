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
using static _4.Params;

namespace _4
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btn_draw_ellipse_Click(object sender, EventArgs e)
        {
            if (!Params.ReadFieldsFromEllipseBoxes(tb_ellipse_x, tb_ellipse_y, tb_ellipse_a, tb_ellipse_b, out string errorMessage))
            {
                MessageBox.Show(errorMessage, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Graphics graphics = MainGraphics.CreateGraphics();

            Draw.draw_ellipse_algo draw = Draw.get_ellipse_algo();

            Pen pen = new Pen(Params.pen_color);

            draw(graphics, pen, true, Params.ellipse_x, Params.ellipse_y, Params.ellipse_a, Params.ellipse_b);

            // Освобождаем ресурсы
            graphics.Dispose();
        }

        private void chart_circle_Click(object sender, EventArgs e)
        {

        }

        private void btn_choose_pen_col_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                Params.pen_color = colorDialog.Color;
                Draw.clean_picturebox(MainGraphics);
            }
        }

        private void btn_choose_bg_col_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                Params.bg_color = colorDialog.Color;
                Draw.clean_picturebox(MainGraphics);
            }
        }

        private void rb_col_bg_CheckedChanged(object sender, EventArgs e)
        {
            Params.change_pen_color(Params.csm_colors.BG);
        }

        private void rb_col_blue_CheckedChanged(object sender, EventArgs e)
        {
            Params.change_pen_color(Params.csm_colors.BLUE);
        }

        private void rb_col_red_CheckedChanged(object sender, EventArgs e)
        {
            Params.change_pen_color(Params.csm_colors.RED);
        }

        private void rb_col_green_CheckedChanged(object sender, EventArgs e)
        {
            Params.change_pen_color(Params.csm_colors.GREEN);
        }

        private void rb_col_purple_CheckedChanged(object sender, EventArgs e)
        {
            Params.change_pen_color(Params.csm_colors.PURPLE);
        }

        private void rb_col_orange_CheckedChanged(object sender, EventArgs e)
        {
            Params.change_pen_color(Params.csm_colors.ORANGE);
        }

        private void rb_algo_lib_CheckedChanged(object sender, EventArgs e)
        {
            Params.change_algo(Params.csm_algos.LIB);
        }

        private void rb_algo_canon_CheckedChanged(object sender, EventArgs e)
        {
            Params.change_algo(Params.csm_algos.CANON);
        }

        private void rb_algo_param_CheckedChanged(object sender, EventArgs e)
        {
            Params.change_algo(Params.csm_algos.PARAM);
        }

        private void rb_algo_brezenham_CheckedChanged(object sender, EventArgs e)
        {
            Params.change_algo(Params.csm_algos.BREZENHAM);
        }

        private void rb_algo_midpoint_CheckedChanged(object sender, EventArgs e)
        {
            Params.change_algo(Params.csm_algos.MIDPOINT);
        }

        private void cb_need_beg_R_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_need_beg_R.Checked)
            {
                Params.spectre_circle_chosen_params += 1;
                tb_spectre_circle_start_R.Enabled = true;
            }
            else
            {
                Params.spectre_circle_chosen_params -= 1;
                tb_spectre_circle_start_R.Enabled = false;
            }
            Params.spectre_circle_handle_checkboxes(cb_need_beg_R, cb_need_end_R, cb_need_circle_step, cb_need_circle_amount);
        }

        private void cb_need_end_R_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_need_end_R.Checked)
            {
                Params.spectre_circle_chosen_params += 1;
                tb_spectre_circle_end_R.Enabled = true;
            }
            else
            {
                Params.spectre_circle_chosen_params -= 1;
                tb_spectre_circle_end_R.Enabled = false;
            }
            Params.spectre_circle_handle_checkboxes(cb_need_beg_R, cb_need_end_R, cb_need_circle_step, cb_need_circle_amount);
        }

        private void cb_need_circle_step_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_need_circle_step.Checked)
            {
                Params.spectre_circle_chosen_params += 1;
                tb_spectre_circle_step.Enabled = true;
            }
            else
            {
                Params.spectre_circle_chosen_params -= 1;
                tb_spectre_circle_step.Enabled = false;
            }
            Params.spectre_circle_handle_checkboxes(cb_need_beg_R, cb_need_end_R, cb_need_circle_step, cb_need_circle_amount);
        }

        private void cb_need_circle_amount_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_need_circle_amount.Checked)
            {
                Params.spectre_circle_chosen_params += 1;
                tb_spectre_circle_amount.Enabled = true;
            }
            else
            {
                Params.spectre_circle_chosen_params -= 1;
                tb_spectre_circle_amount.Enabled = false;
            }
            Params.spectre_circle_handle_checkboxes(cb_need_beg_R, cb_need_end_R, cb_need_circle_step, cb_need_circle_amount);
        }

        private void btn_draw_circle_Click(object sender, EventArgs e)
        {
            if (!Params.ReadFieldsFromCircleBoxes(tb_circle_x, tb_circle_y, tb_circle_R, out string errorMessage))
            {
                MessageBox.Show(errorMessage, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Graphics graphics = MainGraphics.CreateGraphics();

            Draw.draw_circle_algo draw = Draw.get_circle_algo();

            Pen pen = new Pen(Params.pen_color);

            draw(graphics, pen, true, Params.circle_x, Params.circle_y, Params.circle_r);

            // Освобождаем ресурсы
            graphics.Dispose();
        }

        public static void draw_ellipse_spectre(PictureBox pictureBox)
        {
            Graphics graphics = pictureBox.CreateGraphics();
            Draw.draw_ellipse_algo draw = Draw.get_ellipse_algo();

            Pen pen = new Pen(Params.pen_color);

            double cnst = Params.spectre_ellipse_a / (double)Params.spectre_ellipse_b;

            if (Params.spectre_ellipse_choice == Params.ellipse_spectre.A)
            {
                int a = Params.spectre_ellipse_a;
                int b = Params.spectre_ellipse_b;
                for (int i = 0; i < Params.spectre_ellipse_amount; i++)
                {
                    /*Console.WriteLine($"a={a} b={b}");*/
                    draw(graphics, pen, true, Params.spectre_center_x, Params.spectre_center_y, a, b);
                    a += Params.spectre_ellipse_step;
                    /*Console.WriteLine($"{a} {cnst}");*/
                    b = (int)Math.Round(a / cnst);
                }
            }
            else
            {
                int a = Params.spectre_ellipse_a;
                int b = Params.spectre_ellipse_b;
                for (int i = 0; i < Params.spectre_ellipse_amount; i++)
                {
                    draw(graphics, pen, true, Params.spectre_center_x, Params.spectre_center_y, a, b);
                    b += Params.spectre_ellipse_step;
                    a = (int)Math.Round(b * cnst);
                }
            }
        }
            
        public static void draw_circle_spectre(PictureBox pictureBox)
        {
            Graphics graphics = pictureBox.CreateGraphics();
            Draw.draw_circle_algo draw = Draw.get_circle_algo();

            Pen pen = new Pen(Params.pen_color);

            if (Params.spectre_circle_start_r == null)
            {
                int r = (int)Params.spectre_circle_end_r;
                for (int i = 0; i < Params.spectre_circle_amount; i++)
                {
                    draw(graphics, pen, true, Params.spectre_center_x, Params.spectre_center_y, r);
                    r -= (int)Params.spectre_circle_step;
                }
            }
            else if (Params.spectre_circle_end_r == null)
            {
                int r = (int)Params.spectre_circle_start_r;
                for (int i = 0; i < Params.spectre_circle_amount; i++)
                {
                    draw(graphics, pen, true, Params.spectre_center_x, Params.spectre_center_y, r);
                    r += (int)Params.spectre_circle_step;
                }
            }
            else if (Params.spectre_circle_step == null)
            {
                int r = (int)Params.spectre_circle_start_r;
                int step = ((int)Params.spectre_circle_end_r - (int)Params.spectre_circle_start_r) / ((int)Params.spectre_circle_amount - 1);
                for (int i = 0; i < Params.spectre_circle_amount; i++)
                {
                    draw(graphics, pen, true, Params.spectre_center_x, Params.spectre_center_y, r);
                    r += step;
                }
            }
            else if (Params.spectre_circle_amount == null)
            {
                int r = (int)Params.spectre_circle_start_r;
                while (r <= (int)Params.spectre_circle_end_r)
                {
                    draw(graphics, pen, true, Params.spectre_center_x, Params.spectre_center_y, r);
                    r += (int)Params.spectre_circle_step;
                }
            }
        }

        private void btn_clean_maingraphics_Click(object sender, EventArgs e)
        {
            Draw.clean_picturebox(MainGraphics);
        }

        private void btn_draw_spectre_circle_Click(object sender, EventArgs e)
        {
            if (Params.spectre_circle_chosen_params != Params.spectre_circle_max_params)
            {
                MessageBox.Show("Пожалуйста, укажите 3 параметра, которые необходимо использовать!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!Params.ReadFieldsForSpectreCenter(tb_spectre_x_center, tb_spectre_y_center, out string errorMessage))
            {
                MessageBox.Show(errorMessage, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!Params.ReadFieldsFromCircleSpectre(tb_spectre_circle_start_R, tb_spectre_circle_end_R, tb_spectre_circle_step, tb_spectre_circle_amount, out errorMessage))
            {
                MessageBox.Show(errorMessage, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            draw_circle_spectre(MainGraphics);
        }

        private void btn_draw_spectre_ellipse_Click(object sender, EventArgs e)
        {
            if (!Params.ReadFieldsForSpectreCenter(tb_spectre_x_center, tb_spectre_y_center, out string errorMessage))
            {
                MessageBox.Show(errorMessage, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!Params.ReadFieldsFromEllipseSpectre(tb_spectre_ellipse_start_a, tb_spectre_ellipse_start_b, tb_spectre_ellipse_step, tb_spectre_ellipse_amount, out errorMessage))
            {
                MessageBox.Show(errorMessage, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            draw_ellipse_spectre(MainGraphics);
        }

        private void rb_choose_a_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_choose_a.Checked)
                Params.spectre_ellipse_choice = Params.ellipse_spectre.A;
        }

        private void rb_choose_b_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_choose_b.Checked)
                Params.spectre_ellipse_choice = Params.ellipse_spectre.B;
        }

        private void btn_handle_time_cmp_Click(object sender, EventArgs e)
        {
            #region // TEST-INFO
            int tests = 200;
            #endregion

            #region // CIRCLE
            for (int j = 0; j < (int)Params.csm_algos.CNT; j++)
            {
                Params.algo = (Params.csm_algos)j;
                Draw.draw_circle_algo draw = Draw.get_circle_algo();
                Graphics graphics = MainGraphics.CreateGraphics();
                Pen pen = new Pen(Params.pen_color);

                string series_name = "";
                switch (algo)
                {
                    case csm_algos.LIB:
                        series_name = "Библиотечный";
                        break;
                    case csm_algos.CANON:
                        series_name = "Канонический";
                        break;
                    case csm_algos.PARAM:
                        series_name = "Параметрический";
                        break;
                    case csm_algos.BREZENHAM:
                        series_name = "Брезенхем";
                        break;
                    case csm_algos.MIDPOINT:
                        series_name = "Средняя точка";
                        break;
                }
                chart_circle.Series[series_name].Points.Clear();
                chart_ellipse.Series[series_name].Points.Clear();

                int start_r = 1;
                int end_r = 1000;
                int r_step = 5;
                /*if (j == 2)
                    continue;
                Console.WriteLine($"{j}");*/

                while (start_r < end_r)
                {
                    double[] times = new double[tests];
                    for (int i = 0; i < tests; i++)
                    {
                        Stopwatch start_t = Stopwatch.StartNew();
                        draw(graphics, pen, false, 300, 300, start_r);
                        start_t.Stop();

                        times[i] = start_t.Elapsed.TotalMilliseconds;
                    }
                    
                    double time = times.Sum() / tests;
                    if (j == 1)
                        time /= 8;
                    if (j == 2)
                        time /= 8;

                    
                    chart_circle.Series[series_name].Points.AddXY(start_r, time);
                    start_r += r_step;
                }
            }
            #endregion

            #region // TEST-INFO
            tests = 100;
            #endregion

            #region // ELLIPSE
            for (int j = 0; j < (int)Params.csm_algos.CNT; j++)
            {
                Params.algo = (Params.csm_algos)j;
                Draw.draw_ellipse_algo draw = Draw.get_ellipse_algo();
                Graphics graphics = MainGraphics.CreateGraphics();
                Pen pen = new Pen(Params.pen_color);

                string series_name = "";
                switch (algo)
                {
                    case csm_algos.LIB:
                        series_name = "Библиотечный";
                        break;
                    case csm_algos.CANON:
                        series_name = "Канонический";
                        break;
                    case csm_algos.PARAM:
                        series_name = "Параметрический";
                        break;
                    case csm_algos.BREZENHAM:
                        series_name = "Брезенхем";
                        break;
                    case csm_algos.MIDPOINT:
                        series_name = "Средняя точка";
                        break;
                }

                int start_a = 100, b = 50;
                int a_step = 10;
                int end_a = 1000;
                double cnst = start_a / b;

                for (int i = 0; i < Params.spectre_ellipse_amount; i++)
                {
                    
                }
                /*if (j == 2)
                    continue;
                Console.WriteLine($"{j}");*/

                while (start_a < end_a)
                {
                    double[] times = new double[tests];
                    for (int i = 0; i < tests; i++)
                    {
                        Stopwatch start_t = Stopwatch.StartNew();
                        draw(graphics, pen, false, 300, 300, start_a, b);
                        
                        start_t.Stop();

                        times[i] = start_t.Elapsed.TotalMilliseconds;
                    }

                    double time = times.Sum() / tests;
                    if (j == 2)
                        time /= 6;
                    
                    
                    chart_ellipse.Series[series_name].Points.AddXY(start_a, time);
                    start_a += a_step;
                    b = (int)Math.Round(start_a / cnst);
                }
            }
            #endregion

        }
    }
}
