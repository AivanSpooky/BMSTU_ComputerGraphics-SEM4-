using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _3
{
    internal class Draw
    {
        public static int I = 255;
        public static List<Color> fill = new List<Color>();
        public static List<Color> GetRgbIntensity(Color color, Color bgColor, int intensity)
        {
            List<Color> grad = new List<Color>();

            float r1 = color.R;
            float g1 = color.G;
            float b1 = color.B;

            float r2 = bgColor.R;
            float g2 = bgColor.G;
            float b2 = bgColor.B;

            float rRatio = (r2 - r1) / intensity;
            float gRatio = (g2 - g1) / intensity;
            float bRatio = (b2 - b1) / intensity;

            for (int i = 0; i < intensity; i++)
            {
                int nr = (int)(r1 + (rRatio * i));
                int ng = (int)(g1 + (gRatio * i));
                int nb = (int)(b1 + (bRatio * i));

                nr = Math.Min(255, Math.Max(0, nr)); // Ограничиваем значение в диапазоне [0, 255]
                ng = Math.Min(255, Math.Max(0, ng));
                nb = Math.Min(255, Math.Max(0, nb));

                grad.Add(Color.FromArgb(nr, ng, nb));
            }

            grad.Reverse();
            return grad;
        }

        public static Color GetColor(Color color, int light)
        {
            int r = Math.Min(255, color.R + light);
            int g = Math.Min(255, color.G + light);
            int b = Math.Min(255, color.B + light);
            return Color.FromArgb(r, g, b);
        }
        public static void DrawPointOnGraphics(Graphics graphics, Brush brush, Point point)
        {
            graphics.FillRectangle(brush, point.X, point.Y, 1, 1);
        }
        public static void DrawPointsOnGraphics(Graphics graphics, Pen pen, List<Point> points)
        {
            foreach (Point point in points)
            {
                DrawPointOnGraphics(graphics, pen.Brush, point);
            }
        }

        public static void draw_algo_lib(Graphics graphics, Pen pen, float x1, float y1, float x2, float y2, bool draw, bool stepCount, out uint steps)
        {
            steps = 0;
            graphics.DrawLine(pen, x1, y1, x2, y2);
            if (!draw)
                graphics.Clear(Color.White);
        }

        public static void draw_algo_dda(Graphics graphics, Pen pen, float x1, float y1, float x2, float y2, bool draw, bool stepCount, out uint steps)
        {
            List<Point> points = DDA.DDAAlgorithm(x1, y1, x2, y2, out steps, stepCount);
            if (draw)
                DrawPointsOnGraphics(graphics, pen, points);
        }

        public static void draw_algo_br_float(Graphics graphics, Pen pen, float x1, float y1, float x2, float y2, bool draw, bool stepCount, out uint steps)
        {
            List<Point> points = Bresenham.BresenhamFloat(x1, y1, x2, y2, out steps, stepCount);
            if (draw)
                DrawPointsOnGraphics(graphics, pen, points);
        }

        public static void draw_algo_br_int(Graphics graphics, Pen pen, float x1, float y1, float x2, float y2, bool draw, bool stepCount, out uint steps)
        {
            List<Point> points = Bresenham.BresenhamInt((int)x1, (int)y1, (int)x2, (int)y2, out steps, stepCount);
            if (draw)
                DrawPointsOnGraphics(graphics, pen, points);
        }

        public static void draw_algo_br_ladder(Graphics graphics, Pen pen, float x1, float y1, float x2, float y2, bool draw, bool stepCount, out uint steps)
        {
            List<Point> points = Bresenham.BresenhamAntialiased((int)x1, (int)y1, (int)x2, (int)y2, out steps, graphics, pen, draw, stepCount);
            /*if (draw)
                DrawPointsOnGraphics(graphics, pen, points);*/
        }

        public static void draw_algo_wu(Graphics graphics, Pen pen, float x1, float y1, float x2, float y2, bool draw, bool stepCount, out uint steps)
        {
            List<Point> points = Wu.WuAlgorithm(x1, y1, x2, y2, graphics, ref pen, out steps, draw, stepCount);
        }

        public class DDA
        {
            public static List<Point> DDAAlgorithm(float x1, float y1, float x2, float y2, out uint steps, bool stepCount = false)
            {
                float dx = x2 - x1;
                float dy = y2 - y1;
                steps = 0;

                if (dx == 0 && dy == 0)
                    return new List<Point> { new Point(Convert.ToInt32(Math.Round(x1)), Convert.ToInt32(Math.Round(y1))) };

                float l = Math.Abs(dx) >= Math.Abs(dy) ? Math.Abs(dx) : Math.Abs(dy);

                float deltaX = dx / l;
                float deltaY = dy / l;

                float x = x1;
                float y = y1;

                List<Point> points = new List<Point>
                {
                    new Point(Convert.ToInt32(Math.Round(x)), Convert.ToInt32(Math.Round(y)))
                };

                for (int i = 1; i <= l; i++)
                {
                    if (stepCount)
                    {
                        float xBuf = x;
                        float yBuf = y;

                        x += deltaX;
                        y += deltaY;

                        if (Math.Round(xBuf) != Math.Round(x) && Math.Round(yBuf) != Math.Round(y))
                            steps++;
                    }
                    else
                    {
                        x += deltaX;
                        y += deltaY;

                        points.Add(new Point(Convert.ToInt32(Math.Round(x)), Convert.ToInt32(Math.Round(y))));
                    }
                }
                return points;
            }
        }

        public class Bresenham
        {
            public static int Sign(int x)
            {
                if (x > 0)
                    return 1;
                else if (x < 0)
                    return -1;
                else
                    return 0;
            }
            public static int Sign(float x)
            {
                if (x > 0)
                    return 1;
                else if (x < 0)
                    return -1;
                else
                    return 0;
            }

            public static List<Point> BresenhamFloat(float x1, float y1, float x2, float y2, out uint steps, bool stepCount = false)
            {
                float dx = x2 - x1;
                float dy = y2 - y1;
                steps = 0;

                if (dx == 0 && dy == 0)
                    return new List<Point> { new Point((int)x1, (int)y1) };

                int xSign = Sign(dx);
                int ySign = Sign(dy);

                dx = Math.Abs(dx);
                dy = Math.Abs(dy);

                bool exchange;
                if (dy - dx > 0)
                {
                    float temp = dx;
                    dx = dy;
                    dy = temp;
                    exchange = true;
                }
                else
                {
                    exchange = false;
                }

                float e = dy / (float)dx - 0.5f;

                float x = x1;
                float y = y1;
                List<Point> points = new List<Point>();
                
                float xBuf = x;
                float yBuf = y;
                for (int i = 0; i <= dx; i++)
                {
                    if (!stepCount)
                    {
                        points.Add(new Point((int)x, (int)y));
                    }

                    if (e >= 0)
                    {
                        if (exchange)
                            x += xSign;
                        else
                            y += ySign;
                        e -= 1;
                    }

                    if (exchange)
                        y += ySign;
                    else
                        x += xSign;

                    //Debug.Write($"ex: {exchange}, x:{x}, y:{y}, e:{e}\n");

                    e += dy / (float)dx;

                    if (stepCount)
                    {
                        if (xBuf != x && yBuf != y)
                            steps++;
                        xBuf = x;
                        yBuf = y;
                    }
                }
                return points;
            }

            public static List<Point> BresenhamInt(int x1, int y1, int x2, int y2, out uint steps, bool stepCount = false)
            {
                int dx = x2 - x1;
                int dy = y2 - y1;
                steps = 0;

                if (dx == 0 && dy == 0)
                    return new List<Point> { new Point(x1, y1) };

                int xSign = Sign(dx);
                int ySign = Sign(dy);

                dx = Math.Abs(dx);
                dy = Math.Abs(dy);

                bool exchange;
                if (dy > dx)
                {
                    int temp = dx;
                    dx = dy;
                    dy = temp;
                    exchange = true;
                }
                else
                {
                    exchange = false;
                }

                int twoDy = 2 * dy;
                int twoDx = 2 * dx;

                int e = twoDy - dx;

                int x = x1;
                int y = y1;
                List<Point> points = new List<Point>();

                int xBuf = x;
                int yBuf = y;

                for (int i = 0; i <= dx; i++)
                {
                    if (!stepCount)
                    {
                        points.Add(new Point(x, y));
                    }

                    if (e >= 0)
                    {
                        if (exchange)
                            x += xSign;
                        else
                            y += ySign;
                        e -= twoDx;
                    }

                    if (exchange)
                        y += ySign;
                    else
                        x += xSign;


                    e += twoDy;

                    if (stepCount)
                    {
                        if (xBuf != x && yBuf != y)
                            steps++;
                        xBuf = x;
                        yBuf = y;
                    }
                }

                return points;
            }

            public static List<Point> BresenhamAntialiased(int x1, int y1, int x2, int y2, out uint steps, Graphics graphics, Pen pen, bool draw, bool stepCount = false)
            {
                int dx = x2 - x1;
                int dy = y2 - y1;
                steps = 0;

                if (dx == 0 && dy == 0)
                    return new List<Point> { new Point(x1, y1) };

                int xSign = Sign(dx);
                int ySign = Sign(dy);

                dx = Math.Abs(dx);
                dy = Math.Abs(dy);

                bool exchange;
                if (dy > dx)
                {
                    int temp = dx;
                    dx = dy;
                    dy = temp;
                    exchange = true;
                }
                else
                {
                    exchange = false;
                }

                float m = dy / (float)dx * I;
                float w = I - m;
                float e = I * 0.5f;

                int x = x1;
                int y = y1;
                List<Point> points = new List<Point>();

                int xBuf = x;
                int yBuf = y;

                for (int i = 0; i <= dx; i++)
                {
                    if (!stepCount)
                    {
                        int cf = (int)Math.Round(e);
                        Color cur_color = fill[0];
                        if (cf == 0)
                            cur_color = fill[cf];
                        else
                            cur_color = fill[cf - 1];
                        points.Add(new Point(x, y));
                        if (draw)
                            DrawPointOnGraphics(graphics, new SolidBrush(cur_color), new Point(x, y));
                    }

                    if (e < w)
                    {
                        if (!exchange)
                            x += xSign;
                        else
                            y += ySign;
                        e += m;
                    }
                    else
                    {
                        x += xSign;
                        y += ySign;
                        e -= w;
                    }

                    if (stepCount)
                    {
                        if (xBuf != x && yBuf != y)
                            steps++;
                        xBuf = x;
                        yBuf = y;
                    }
                }

                return points;
            }
        }

        public class Wu
        {
            public static List<Point> WuAlgorithm(float x1, float y1, float x2, float y2, Graphics graphics, ref Pen pen, out uint steps, bool draw, bool stepCount = false)
            {
                float dx = x2 - x1;
                float dy = y2 - y1;
                steps = 0;

                if (dx == 0 && dy == 0)
                    return new List<Point> { new Point((int)x1, (int)y1) };

                float m = 1;
                int step = 1;


                List<Point> points = new List<Point>();

                Color cur_color = pen.Color;

                if (Math.Abs(dy) >= Math.Abs(dx))
                {
                    if (dy != 0)
                        m = dx / dy;

                    float m1 = m;

                    if (y1 > y2)
                    {
                        m1 *= -1;
                        step *= -1;
                    }

                    int bord = dy < dx ? (int)Math.Round(y2) - 1 : (int)Math.Round(y2) + 1;

                    for (int y = (int)Math.Round(y1); y != bord; y += step)
                    {
                        double d1 = x1 - Math.Floor(x1);
                        double d2 = 1 - d1;

                        if (!stepCount)
                        {
                            cur_color = GetColor(pen.Color, (int)Math.Round(Math.Abs(d2) * I));
                            if (draw)
                                DrawPointOnGraphics(graphics, new SolidBrush(cur_color), new Point((int)x1 + 1, y));
                            points.Add(new Point((int)x1 + 1, y));
                            cur_color = GetColor(pen.Color, (int)Math.Round(Math.Abs(d1) * I));
                            if (draw)
                                DrawPointOnGraphics(graphics, new SolidBrush(cur_color), new Point((int)x1, y));
                            points.Add(new Point((int)x1, y));
                        }
                        else if (y < Math.Round(y2) && (int)x1 != (int)(x1 + m))
                        {
                            steps++;
                        }

                        x1 += m1;
                    }
                }
                else
                {
                    if (dx != 0)
                        m = dy / dx;

                    float m1 = m;

                    if (x1 > x2)
                    {
                        step *= -1;
                        m1 *= -1;
                    }

                    int bord = dy > dx ? (int)Math.Round(x2) - 1 : (int)Math.Round(x2) + 1;

                    for (int x = (int)Math.Round(x1); x != bord; x += step)
                    {
                        double d1 = y1 - Math.Floor(y1);
                        double d2 = 1 - d1;

                        if (!stepCount)
                        {
                            cur_color = GetColor(pen.Color, (int)Math.Round(Math.Abs(d2) * I));
                            if (draw)
                                DrawPointOnGraphics(graphics, new SolidBrush(cur_color), new Point(x, (int)y1 + 1));
                            points.Add(new Point(x, (int)y1 + 1));
                            cur_color = GetColor(pen.Color, (int)Math.Round(Math.Abs(d1) * I));
                            if (draw)
                                DrawPointOnGraphics(graphics, new SolidBrush(cur_color), new Point(x, (int)y1));
                            points.Add(new Point(x, (int)y1));
                        }
                        else if (x < Math.Round(x2) && (int)y1 != (int)(y1 + m))
                        {
                            steps++;
                        }

                        y1 += m1;
                    }
                }
                return points;
            }
        }
    }
}
