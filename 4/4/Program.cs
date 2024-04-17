using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _4
{
    public static class Params
    {
        public static Color bg_color = Color.White;
        public static Color pen_color = Color.Blue;

        public static csm_algos algo = csm_algos.LIB;

        public static int spectre_center_x = 0;
        public static int spectre_center_y = 0;

        public static int circle_x = 0;
        public static int circle_y = 0;
        public static int circle_r = 0;

        public static int ellipse_x = 0;
        public static int ellipse_y = 0;
        public static int ellipse_a = 0;
        public static int ellipse_b = 0;

        public const uint spectre_circle_max_params = 3;
        public static uint spectre_circle_chosen_params = 0;
        public static int? spectre_circle_start_r = 0;
        public static int? spectre_circle_end_r = 0;
        public static int? spectre_circle_step = 0;
        public static int? spectre_circle_amount = 0;

        public enum ellipse_spectre
        {
            A = 0,
            B = 1,
        };
        public static ellipse_spectre spectre_ellipse_choice;
        public static int spectre_ellipse_a = 0;
        public static int spectre_ellipse_b = 0;
        public static int spectre_ellipse_step = 0;
        public static int spectre_ellipse_amount = 0;

        public enum csm_colors
        {
            BG = 0,
            BLUE = 1,
            RED = 2,
            GREEN = 3,
            PURPLE = 4,
            ORANGE = 5,
        };

        public enum csm_algos
        {
            LIB = 0,
            CANON = 1,
            PARAM = 2,
            BREZENHAM = 3,
            MIDPOINT = 4,
            CNT = 5,
        };

        public static bool ReadFieldsFromCircleBoxes(TextBox x, TextBox y, TextBox r, out string errorMessage)
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
            if (!int.TryParse(r.Text, out int rValue))
            {
                errorMessage = "Ошибка. На ввод Х ожидалось целое число.";
                return false;
            }

            circle_x = xValue;
            circle_y = yValue;
            circle_r = rValue;

            return true;
        }

        public static bool ReadFieldsFromEllipseSpectre(TextBox a, TextBox b, TextBox step, TextBox amount, out string errorMessage)
        {
            errorMessage = "";
            if (!int.TryParse(a.Text, out int aval))
            {
                errorMessage = "Ошибка. Проверьте, что A - целое число.";
                return false;
            }
            if (!int.TryParse(b.Text, out int bval))
            {
                errorMessage = "Ошибка. Проверьте, что B - целое число.";
                return false;
            }
            if (!int.TryParse(step.Text, out int stepval))
            {
                errorMessage = "Ошибка. Проверьте, что шаг - целое число.";
                return false;
            }
            if (!int.TryParse(amount.Text, out int amval))
            {
                errorMessage = "Ошибка. Проверьте, что количество эллипсов - целое число.";
                return false;
            }

            spectre_ellipse_a = aval;
            spectre_ellipse_b = bval;
            spectre_ellipse_step = stepval;
            spectre_ellipse_amount = amval;

            return true;
        }

        public static bool ReadFieldsFromCircleSpectre(TextBox start_r, TextBox end_r, TextBox step, TextBox amount, out string errorMessage)
        {
            errorMessage = "";
            int readen_wrong = 0;
            int val1 = 0, val2 = 0, val3 = 0, val4 = 0;
            if (start_r.Enabled && !int.TryParse(start_r.Text, out val1))
            {
                readen_wrong++;
                spectre_circle_start_r = null;
            }
            else
            {
                if (start_r.Enabled)
                    spectre_circle_start_r = val1;
                else
                    spectre_circle_start_r = null;
            }
            if (end_r.Enabled && !int.TryParse(end_r.Text, out val2))
            {
                readen_wrong++;
                spectre_circle_end_r = null;
            }
            else
            {
                if (end_r.Enabled)
                    spectre_circle_end_r = val2;
                else
                    spectre_circle_end_r = null;
            }
            if (step.Enabled && !int.TryParse(step.Text, out val3))
            {
                readen_wrong++;
                spectre_circle_step = null;
            }
            else
            {
                if (step.Enabled)
                    spectre_circle_step = val3;
                else
                    spectre_circle_step = null;
            }
            if (amount.Enabled && !int.TryParse(amount.Text, out val4))
            {
                readen_wrong++;
                spectre_circle_amount = null;
            }
            else
            {
                if (amount.Enabled)
                    spectre_circle_amount = val4;
                else
                    spectre_circle_amount = null;
            }

            if (readen_wrong > 0)
            {
                errorMessage = "Ошибка. Проверьте, что все введенные параметры - целые числа.";
                return false;
            }
            if (spectre_circle_start_r > spectre_circle_end_r)
            {
                errorMessage = "Ошибка. Стартовый радиус должен быть меньше конечного!";
                return false;
            }

            return true;
        }

        public static bool ReadFieldsForSpectreCenter(TextBox x, TextBox y, out string errorMessage)
        {
            errorMessage = "";
            if (!int.TryParse(x.Text, out int xval))
            {
                errorMessage = "Ошибка. Координата X центра спектра должна быть целым числом!";
                return false;
            }
            if (!int.TryParse(y.Text, out int yval))
            {
                errorMessage = "Ошибка. Координата Y центра спектра должна быть целым числом!";
                return false;
            }

            spectre_center_x = xval;
            spectre_center_y = yval;

            return true;
        }

        public static bool ReadFieldsFromEllipseBoxes(TextBox x, TextBox y, TextBox a, TextBox b, out string errorMessage)
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
            if (!int.TryParse(a.Text, out int aValue))
            {
                errorMessage = "Ошибка. На ввод A ожидалось целое число.";
                return false;
            }
            if (!int.TryParse(b.Text, out int bValue))
            {
                errorMessage = "Ошибка. На ввод B ожидалось целое число.";
                return false;
            }

            ellipse_x = xValue;
            ellipse_y = yValue;
            ellipse_a = aValue;
            ellipse_b = bValue;

            return true;
        }

        public static void spectre_circle_handle_checkboxes(CheckBox cb1, CheckBox cb2, CheckBox cb3, CheckBox cb4)
        {
            if (spectre_circle_chosen_params < spectre_circle_max_params)
            {
                if (!cb1.Checked)
                    cb1.Enabled = true;
                if (!cb2.Checked)
                    cb2.Enabled = true;
                if (!cb3.Checked)
                    cb3.Enabled = true;
                if (!cb4.Checked)
                    cb4.Enabled = true;
            }
            else
            {
                if (!cb1.Checked)
                    cb1.Enabled = false;
                if (!cb2.Checked)
                    cb2.Enabled = false;
                if (!cb3.Checked)
                    cb3.Enabled = false;
                if (!cb4.Checked)
                    cb4.Enabled = false;
            }
        }
        public static void change_pen_color(csm_colors col_id)
        {
            switch (col_id)
            {
                case csm_colors.BG:
                    pen_color = bg_color;
                    break;
                case csm_colors.BLUE:
                    pen_color = Color.Blue;
                    break;
                case csm_colors.RED:
                    pen_color = Color.Red;
                    break;
                case csm_colors.GREEN:
                    pen_color = Color.Green;
                    break;
                case csm_colors.PURPLE:
                    pen_color = Color.Purple;
                    break;
                case csm_colors.ORANGE:
                    pen_color = Color.Orange;
                    break;
            }
        }

        public static void change_algo(csm_algos algo_id)
        {
            algo = algo_id;
        }
    }


    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
