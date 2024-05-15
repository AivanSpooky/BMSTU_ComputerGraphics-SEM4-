using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static _5.Draw;

namespace _5
{
    public static class Params
    {
        public static Color pen_color = Color.Black;
        public static Color bg_color = Color.White;
        public static bool segment_mode = true;
        public static bool delay_mode = false;
        public static bool is_filling = false;

        public static void change_pen_col(Color col)
        {
            pen_color = col;
        }
        public static void change_segment_mode(bool mode)
        {
            segment_mode = mode;
        }
        public static void change_dealy_mode(bool cond)
        {
            delay_mode = cond;
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
