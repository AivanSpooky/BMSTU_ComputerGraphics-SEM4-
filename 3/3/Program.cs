using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace _3
{
    delegate void DrawSegment(Graphics graphics, Pen pen, float x1, float y1, float x2, float y2, bool draw, bool stepCount, out uint steps);
    public static class CsmAlgoNames
    {
        public const string ALGO_LIB = "lib";
        public const string ALGO_DDA = "dda";
        public const string ALGO_BR_FLOAT = "br-float";
        public const string ALGO_BR_INT = "br-int";
        public const string ALGO_BR_LADDER = "br-ladder";
        public const string ALGO_WU = "wu";
    }
    public static class CsmAlgoRusNames
    {
        public const string ALGO_LIB = "Библиотечный";
        public const string ALGO_DDA = "ЦДА";
        public const string ALGO_BR_FLOAT = "Брезенхем (float)";
        public const string ALGO_BR_INT = "Брезенхем (int)";
        public const string ALGO_BR_LADDER = "Брезенхем (ступ.)";
        public const string ALGO_WU = "Ву";

        public static string GetRusAlgoName(uint algo)
        {
            string name = "";
            switch (algo)
            {
                case (uint)csm_algo.ALGO_LIB:
                    name = CsmAlgoRusNames.ALGO_LIB;
                    break;
                case (uint)csm_algo.ALGO_DDA:
                    name = CsmAlgoRusNames.ALGO_DDA;
                    break;
                case (uint)csm_algo.ALGO_BR_FLOAT:
                    name = CsmAlgoRusNames.ALGO_BR_FLOAT;
                    break;
                case (uint)csm_algo.ALGO_BR_INT:
                    name = CsmAlgoRusNames.ALGO_BR_INT;
                    break;
                case (uint)csm_algo.ALGO_BR_LADDER:
                    name = CsmAlgoRusNames.ALGO_BR_LADDER;
                    break;
                case (uint)csm_algo.ALGO_WU:
                    name = CsmAlgoRusNames.ALGO_WU;
                    break;
            }
            return name;
        }
    }
    class Params
    {
        public static Color bg_color = Color.White;
        public static csm_color cur_color = csm_color.NOT_CHOSEN;
        public static csm_algo cur_algo = csm_algo.NOT_CHOSEN;
        public static int? x1 = null;
        public static int? y1 = null;
        public static int? x2 = null;
        public static int? y2 = null;

        public static float? angle = null;
        public static int? distance = null;
        

        public static void SetColor(out Color color)
        {
            switch (cur_color)
            {
                case csm_color.BG:
                    color = bg_color;
                    Console.WriteLine("Background Color: " + bg_color.ToString());
                    break;
                case csm_color.BG_BLUE:
                    color = Color.Blue;
                    break;
                case csm_color.BG_RED:
                    color = Color.Red;
                    break;
                case csm_color.BG_GREEN:
                    color = Color.Green;
                    break;
                case csm_color.BG_ORANGE:
                    color = Color.Orange;
                    break;
                case csm_color.BG_VIOLET:
                    color = Color.Purple;
                    break;
                default:
                    color = Color.Black;
                    break;
            }
        }

        public static void SetAlgo(out DrawSegment draw)
        {
            switch (cur_algo)
            {
                case csm_algo.ALGO_LIB:
                    draw = Draw.draw_algo_lib;
                    break;
                case csm_algo.ALGO_DDA:
                    draw = Draw.draw_algo_dda;
                    break;
                case csm_algo.ALGO_BR_FLOAT:
                    draw = Draw.draw_algo_br_float;
                    break;
                case csm_algo.ALGO_BR_INT:
                    draw = Draw.draw_algo_br_int;
                    break;
                case csm_algo.ALGO_BR_LADDER:
                    draw = Draw.draw_algo_br_ladder;
                    break;
                case csm_algo.ALGO_WU:
                    draw = Draw.draw_algo_wu;
                    break;
                default:
                    draw = Draw.draw_algo_lib;
                    break;
            }
        }

        public static float DegreesToRadians(int degrees)
        {
            return degrees * (float)Math.PI / 180;
        }

        public static bool DrawSegmentCheck(out string errorMessage)
        {
            errorMessage = "";
            if (cur_color == csm_color.NOT_CHOSEN)
            {
                errorMessage = "Ошибка: Цвет не выбран!";
                return false;
            } else if (cur_algo == csm_algo.NOT_CHOSEN)
            {
                errorMessage = "Ошибка: Алгоритм не выбран!";
                return false;
            } else if (x1 == null || x2 == null || y1 == null || y2 == null)
            {
                errorMessage = "Ошибка: Координаты точек не заданы!";
                return false;
            }
            return true;
        }

        public static bool DrawSpectreCheck(out string errorMessage)
        {
            errorMessage = "";
            if (cur_color == csm_color.NOT_CHOSEN)
            {
                errorMessage = "Ошибка: Цвет не выбран!";
                return false;
            }
            else if (cur_algo == csm_algo.NOT_CHOSEN)
            {
                errorMessage = "Ошибка: Алгоритм не выбран!";
                return false;
            } else if (angle == null)
            {
                errorMessage = "Ошибка: Угол не указан!";
                return false;
            } else if (distance == null)
            {
                errorMessage = "Ошибка: Расстояние отрезка не указано!";
                return false;
            }
            return true;
        }

        public static bool ReadFieldsFromTextBoxes(TextBox x1TextBox, TextBox y1TextBox, TextBox x2TextBox, TextBox y2TextBox, out string errorMessage)
        {
            errorMessage = "";

            if (!int.TryParse(x1TextBox.Text, out int x1Value))
            {
                errorMessage = "Ошибка: Некорректное целочисленное значение для x1";
                return false;
            }
            x1 = x1Value;

            if (!int.TryParse(y1TextBox.Text, out int y1Value))
            {
                errorMessage = "Ошибка: Некорректное целочисленное значение для y1";
                return false;
            }
            y1 = y1Value;

            if (!int.TryParse(x2TextBox.Text, out int x2Value))
            {
                errorMessage = "Ошибка: Некорректное целочисленное значение для x2";
                return false;
            }
            x2 = x2Value;

            if (!int.TryParse(y2TextBox.Text, out int y2Value))
            {
                errorMessage = "Ошибка: Некорректное целочисленное значение для y2";
                return false;
            }
            y2 = y2Value;

            return true;
        }

        public static bool ReadFielsForSpectre(TextBox angleTextBox, TextBox distanceTextBox, out string errorMessage)
        {
            errorMessage = "";
            // Парсим и проверяем угол
            if (!int.TryParse(angleTextBox.Text, out int angleValue))
            {
                errorMessage = "Ошибка: Некорректное целочисленное значение для угла";
                return false;
            }
            if (angleValue <= 0)
            {
                errorMessage = "Ошибка: Угол должен быть положительным";
                return false;
            }
            else if (angleValue > 360)
            {
                errorMessage = "Ошибка: Угол должен быть меньше 360 градусов";
                return false;
            }
            //angle = DegreesToRadians(angleValue);
            angle = angleValue;

            // Парсим и проверяем расстояние
            if (!int.TryParse(distanceTextBox.Text, out int distanceValue))
            {
                errorMessage = "Ошибка: Некорректное целочисленное значение для расстояния";
                return false;
            }
            if (distanceValue < 0)
            {
                errorMessage = "Ошибка: Расстояние не может быть отрицательным";
                return false;
            }
            distance = distanceValue;
            return true;
        }

        public static bool ReadFielsForStep(TextBox distanceTextBox, out string errorMessage)
        {
            errorMessage = "";
            // Парсим и проверяем расстояние
            if (!int.TryParse(distanceTextBox.Text, out int distanceValue))
            {
                errorMessage = "Ошибка: Некорректное целочисленное значение для расстояния";
                return false;
            }
            if (distanceValue < 0)
            {
                errorMessage = "Ошибка: Расстояние не может быть отрицательным";
                return false;
            }
            distance = distanceValue;
            return true;
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
            Application.Run(new Form1());
        }
    }
}
