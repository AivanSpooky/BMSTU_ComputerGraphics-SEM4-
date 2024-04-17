using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static _4.Params;

namespace _4
{
    internal class Draw
    {
        public delegate void draw_circle_algo(Graphics graphics, Pen pen, bool draw, int x, int y, int r);
        public delegate void draw_ellipse_algo(Graphics graphics, Pen pen, bool draw, int x, int y, int a, int b);

        public static draw_circle_algo get_circle_algo()
        {
            draw_circle_algo draw_algo = draw_circle_lib;
            switch (Params.algo)
            {
                case csm_algos.LIB:
                    draw_algo = draw_circle_lib;
                    break;
                case csm_algos.CANON:
                    draw_algo = draw_canonical_circle;
                    break;
                case csm_algos.PARAM:
                    draw_algo = draw_parameter_circle;
                    break;
                case csm_algos.BREZENHAM:
                    draw_algo = draw_bresenham_circle;
                    break;
                case csm_algos.MIDPOINT:
                    draw_algo = draw_midpoint_circle;
                    break;
                default:
                    draw_algo = draw_circle_lib;
                    break;
            }
            return draw_algo;
        }

        public static draw_ellipse_algo get_ellipse_algo()
        {
            draw_ellipse_algo draw_algo = draw_ellipse_lib;
            switch (Params.algo)
            {
                case csm_algos.LIB:
                    draw_algo = draw_ellipse_lib;
                    break;
                case csm_algos.CANON:
                    draw_algo = draw_canonical_ellipse;
                    break;
                case csm_algos.PARAM:
                    draw_algo = draw_parameter_ellipse;
                    break;
                case csm_algos.BREZENHAM:
                    draw_algo = draw_bresenham_ellipse;
                    break;
                case csm_algos.MIDPOINT:
                    draw_algo = draw_midpoint_ellipse;
                    break;
                default:
                    draw_algo = draw_ellipse_lib;
                    break;
            }
            return draw_algo;
        }

        public static void draw_pixels(Graphics graphics, Pen pen, bool draw, int x, int y, int xc, int yc, bool circle = true)
        {
            if (circle)
            {
                set_pixel(graphics, pen, draw, y - yc + xc, x - xc + yc);
                set_pixel(graphics, pen, draw, -y + yc + xc, x - xc + yc);
                set_pixel(graphics, pen, draw, y - yc + xc, -x + xc + yc);
                set_pixel(graphics, pen, draw, -y + yc + xc, -x + xc + yc);
            }

            set_pixel(graphics, pen, draw, x, y);
            set_pixel(graphics, pen, draw, -x + 2 * xc, y);
            set_pixel(graphics, pen, draw, x, -y + 2 * yc);
            set_pixel(graphics, pen, draw, -x + 2 * xc, -y + 2 * yc);
        }

        public static void set_pixel(Graphics graphics, Pen pen, bool draw, int x, int y)
        {
            if (draw)
            {
                graphics.FillRectangle(pen.Brush, x, y, 1, 1);
            }
        }



        public static void draw_circle_lib(Graphics graphics, Pen pen, bool draw, int x, int y, int r)
        {
            Rectangle rect = new Rectangle(x - r, y - r, 2*r, 2*r);
            if (draw)
                graphics.DrawEllipse(pen, rect);
        }

        public static void draw_ellipse_lib(Graphics graphics, Pen pen, bool draw, int x, int y, int a, int b)
        {
            Rectangle rect = new Rectangle(x - a, y - b, 2*a, 2*b);
            if (draw)
                graphics.DrawEllipse(pen, rect);
        }

        public static void draw_canonical_circle(Graphics graphics, Pen pen, bool draw, int x, int y, int r)
        {
            double sqr_r = Math.Pow(r, 2);

            double border = Math.Round(x + r / Math.Sqrt(2));

            for (int x_i = x; x_i < border + 1; x_i++)
            {
                int y_i = y + (int)Math.Round(Math.Sqrt(sqr_r - Math.Pow((x_i - x), 2)));
                if (draw)
                    draw_pixels(graphics, pen, draw, x_i, y_i, x, y);
            }
        }

        public static void draw_canonical_ellipse(Graphics graphics, Pen pen, bool draw, int x, int y, int a, int b)
        {
            double aa = a * a;
            double bb = b * b;
            double x0 = 0;
            double y0 = b;
            double d = bb - aa * b + 0.25 * aa;

            double dx = 2 * bb * x0;
            double dy = 2 * aa * y0;

            while (dx < dy)
            {
                if (draw)
                {
                    graphics.FillRectangle(pen.Brush, (float)(x + x0), (float)(y + y0), 1, 1);
                    graphics.FillRectangle(pen.Brush, (float)(x - x0), (float)(y + y0), 1, 1);
                    graphics.FillRectangle(pen.Brush, (float)(x + x0), (float)(y - y0), 1, 1);
                    graphics.FillRectangle(pen.Brush, (float)(x - x0), (float)(y - y0), 1, 1);
                }

                if (d < 0)
                {
                    x0++;
                    dx += 2 * bb;
                    d += bb + dx;
                }
                else
                {
                    x0++;
                    y0--;
                    dx += 2 * bb;
                    dy -= 2 * aa;
                    d += bb + dx - dy;
                }
            }

            d = bb * (x0 + 0.5) * (x0 + 0.5) + aa * (y0 - 1) * (y0 - 1) - aa * bb;

            while (y0 >= 0)
            {
                if (draw)
                {
                    graphics.FillRectangle(pen.Brush, (float)(x + x0), (float)(y + y0), 1, 1);
                    graphics.FillRectangle(pen.Brush, (float)(x - x0), (float)(y + y0), 1, 1);
                    graphics.FillRectangle(pen.Brush, (float)(x + x0), (float)(y - y0), 1, 1);
                    graphics.FillRectangle(pen.Brush, (float)(x - x0), (float)(y - y0), 1, 1);
                }

                if (d > 0)
                {
                    y0--;
                    dy -= 2 * aa;
                    d += aa - dy;
                }
                else
                {
                    y0--;
                    x0++;
                    dx += 2 * bb;
                    dy -= 2 * aa;
                    d += aa - dy + dx;
                }
            }
        }

        public static void draw_bresenham_circle(Graphics graphics, Pen pen, bool draw, int x, int y, int r)
        {
            int x_i = 0;
            int y_i = r;

            if (draw)
                draw_pixels(graphics, pen, draw, x + x_i, y + y_i, x, y);
                /*graphics.FillRectangle(pen.Brush, x + x_i, y + y_i, 1, 1);*/

            int delta = 2 * (1 - r);

            while (x_i < y_i)
            {
                int d = 2 * (delta + y_i) - 1;
                x_i += 1;

                if (d >= 0)
                {
                    y_i -= 1;
                    delta += 2 * (x_i - y_i + 1);
                }
                else
                {
                    delta += x_i + x_i + 1;
                }

                if (draw)
                    draw_pixels(graphics, pen, draw, x + x_i, y + y_i, x, y);
                /*graphics.FillRectangle(pen.Brush, x + x_i, y + y_i, 1, 1);*/
            }
        }

        public static void draw_bresenham_ellipse(Graphics graphics, Pen pen, bool draw, int x, int y, int a, int b)
        {
            int x_i = 0;
            int y_i = b;

            if (draw)
                draw_pixels(graphics, pen, draw, x + x_i, y + y_i, x, y, circle: false);

            double sqr_ra = Math.Pow(a, 2);
            double sqr_rb = Math.Pow(b, 2);
            double delta = sqr_rb - sqr_ra * (b + b + 1);

            while (y_i >= 0)
            {
                if (delta <= 0)
                {
                    double d = 2 * delta + sqr_ra * (y_i + y_i + 2);
                    x_i += 1;
                    delta += sqr_rb * (x_i + x_i + 1);

                    if (d >= 0)
                    {
                        y_i -= 1;
                        delta += sqr_ra * (-y_i - y_i + 1);
                    }
                }
                else
                {
                    double d = 2 * delta + sqr_rb * (-x_i - x_i + 2);
                    y_i -= 1;
                    delta += sqr_ra * (-y_i - y_i + 1);

                    if (d <= 0)
                    {
                        x_i += 1;
                        delta += sqr_rb * (x_i + x_i + 1);
                    }
                }

                if (draw)
                    draw_pixels(graphics, pen, draw, x + x_i, y + y_i, x, y, circle: false);
            }
        }

        public static void draw_parameter_circle(Graphics graphics, Pen pen, bool draw, int x, int y, int r)
        {
            double step = 1.0 / r;

            double i = 0;
            while (i <= Math.PI / 4 + step)
            {
                int x_i = x + (int)Math.Round(r * Math.Cos(i));
                int y_i = y + (int)Math.Round(r * Math.Sin(i));

                if (draw)
                    draw_pixels(graphics, pen, draw, x_i, y_i, x, y);

                i += step;
            }
        }

        /*public static void draw_parameter_ellipse(Graphics graphics, Pen pen, bool draw, int x, int y, int a, int b)
        {
            *//*double angle_step = Math.PI / 180; // Шаг изменения угла (в радианах)*//*
            double angle_step = (a > b) ? 1/ (double)a : 1/ (double)b;
            double angle = 0;

            while (angle <= Math.PI / 2 + angle_step)
            {
                *//*Console.WriteLine($"{angle}");*//*
                int x_i = x + (int)Math.Round(a * Math.Cos(angle));
                int y_i = y + (int)Math.Round(b * Math.Sin(angle));

                if (draw)
                {
                    draw_pixels(graphics, pen, draw, x_i, y_i, x, y, circle: false);
                }

                angle += angle_step;
            }
        }*/

        public static void draw_parameter_ellipse(Graphics graphics, Pen pen, bool draw, int x, int y, int a, int b)
        {
            double aa = a * a;
            double bb = b * b;
            double end_x = Math.Sqrt(aa * aa / (aa + bb));
            double end_y = Math.Sqrt(bb - bb / aa * end_x * end_x);

            double cur_x = 0;
            double cur_y = b;

            if (draw)
                draw_pixels(graphics, pen, draw, x + (int)Math.Round(cur_x), y + (int)Math.Round(cur_y), x, y, circle: false);

            while (cur_x <= end_x)
            {
                cur_x += 1;
                double t = Math.Acos(cur_x / a);
                cur_y = b * Math.Sin(t);

                if (draw)
                    draw_pixels(graphics, pen, draw, x + (int)Math.Round(cur_x), y + (int)Math.Round(cur_y), x, y, circle: false);
            }

            cur_y = end_y;
            while (cur_y > 0)
            {
                cur_y -= 1;
                double t = Math.Asin(cur_y / b);
                cur_x = a * Math.Cos(t);

                if (draw)
                    draw_pixels(graphics, pen, draw, x + (int)Math.Round(cur_x), y + (int)Math.Round(cur_y), x, y, circle: false);
            }
        }

        public static void draw_midpoint_circle(Graphics graphics, Pen pen, bool draw, int x, int y, int r)
        {
            int x_i = r;
            int y_i = 0;

            if (draw)
                draw_pixels(graphics, pen, draw, x + x_i, y + y_i, x, y);

            int delta = 1 - r;

            while (y_i <= x_i)
            {
                if (delta >= 0)
                {
                    x_i -= 1;
                    delta -= x_i + x_i;
                }

                y_i += 1;
                delta += y_i + y_i + 1;

                if (draw)
                    draw_pixels(graphics, pen, draw, x + x_i, y + y_i, x, y);
            }
        }


        public static void draw_midpoint_ellipse(Graphics graphics, Pen pen, bool draw, int x, int y, int a, int b)
        {
            int sqr_ra = a * a;
            int sqr_rb = b * b;

            int cur_x = 0;
            int cur_y = b;

            if (draw)
            {
                draw_pixels(graphics, pen, draw, x + cur_x, y + cur_y, x, y, circle: false);
            }

            int border_x = (int)(a / Math.Sqrt(1 + (double)sqr_rb / sqr_ra));
            int delta = sqr_rb - (int)Math.Round(sqr_ra * (b - 0.25));

            while (cur_x <= border_x)
            {
                if (delta > 0)
                {
                    cur_y--;
                    delta -= sqr_ra * 2 * cur_y;
                }

                cur_x++;
                delta += sqr_rb * (2 * cur_x + 1);

                if (draw)
                {
                    draw_pixels(graphics, pen, draw, x + cur_x, y + cur_y, x, y, circle: false);
                }
            }

            cur_x = a;
            cur_y = 0;

            if (draw)
            {
                draw_pixels(graphics, pen, draw, x + cur_x, y + cur_y, x, y, circle: false);
            }

            int border_y = (int)(b / Math.Sqrt(1 + (double)sqr_ra / sqr_rb));
            delta = sqr_ra - (int)Math.Round(sqr_rb * (a - 0.25));

            while (cur_y <= border_y)
            {
                if (delta > 0)
                {
                    cur_x--;
                    delta -= 2 * sqr_rb * cur_x;
                }

                cur_y++;
                delta += sqr_ra * (2 * cur_y + 1);

                if (draw)
                {
                    draw_pixels(graphics, pen, draw, x + cur_x, y + cur_y, x, y, circle: false);
                }
            }
        }

        public static void clean_picturebox(PictureBox pictureBox)
        {
            using (Graphics g = pictureBox.CreateGraphics())
            {
                g.Clear(Params.bg_color);
            }
        }
    }
}
