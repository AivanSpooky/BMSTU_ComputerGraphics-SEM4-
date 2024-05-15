using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _5
{
    internal class Draw
    {
        public class Figure
        {
            // Поле для точек конкретной фигуры
            public List<PointD> pointListFig = new List<PointD>();

            // Добавление новой точки в список точек текущей фигуры
            public void AddPoint(PointD point)
            {
                pointListFig.Add(point);
            }

            // Статический метод для подсчета общего количества фигур
            public static int CountFigures()
            {
                // Поскольку каждая новая фигура создается при щелчке правой кнопкой мыши,
                // количество фигур равно количеству раз, когда мы нажимали правую кнопку мыши
                return figureList.Count;
            }
        }

        public static List<Figure> figureList = new List<Figure>();
        public static bool create_new_fig = false;

        // Статический метод для создания новой фигуры
        public static void CreateNewFigure()
        {
            // Создаем новую фигуру и добавляем её в список всех фигур
            Figure newFigure = new Figure();
            figureList.Add(newFigure);
        }

        // Добавление новой точки в список точек текущей фигуры
        public static void AddPointToCurrentFigure(PointD point)
        {
            if (figureList.Count == 0 || create_new_fig)
            {
                CreateNewFigure();
                create_new_fig = false;
            }
            // Получаем последнюю фигуру из списка всех фигур
            Figure currentFigure = figureList[figureList.Count - 1];
            // Добавляем точку к текущей фигуре
            currentFigure.AddPoint(point);
        }

        public static void DeletePoints()
        {
            reset_index();
            // Проходим по всем фигурам в списке
            foreach (Figure figure in figureList)
            {
                // Очищаем список точек текущей фигуры
                figure.pointListFig.Clear();
            }
            // Очищаем список всех фигур
            figureList.Clear();
        }

        public static void reset_index()
        {
            PointD.nextIndex = 1;
        }

        public class PointD
        {
            public static int nextIndex = 1;

            public int Index { get; private set; }
            public int X { get; set; }
            public int Y { get; set; }

            public PointD(int x, int y)
            {
                Index = nextIndex++;
                X = x;
                Y = y;
            }

            public PointD(int x, int y, bool f)
            {
                if (f)
                    Index = nextIndex++;
                X = x;
                Y = y;
            }

            public void reset_index()
            {
                nextIndex = 1;
            }
        }

        public static void set_pixel(Graphics graphics, Pen pen, bool draw, int x, int y)
        {
            if (draw)
            {
                graphics.FillRectangle(pen.Brush, x, y, 1, 1);
            }
        }

        public class BresenhamLineDrawer
        {
            private static int Sign(int x)
            {
                if (x > 0)
                    return 1;
                else if (x < 0)
                    return -1;
                else
                    return 0;
            }

            public static void BresenhamInt(PointD begPoint, PointD endPoint, Graphics graphics, Pen pen, bool draw)
            {
                int dx = endPoint.X - begPoint.X;
                int dy = endPoint.Y - begPoint.Y;

                if (dx == 0 && dy == 0)
                {
                    set_pixel(graphics, pen, draw, begPoint.X, begPoint.Y);
                    return;
                }

                int xSign = Sign(dx);
                int ySign = Sign(dy);

                dx = Math.Abs(dx);
                dy = Math.Abs(dy);
                int exchange;

                if (dy > dx)
                {
                    int temp = dx;
                    dx = dy;
                    dy = temp;
                    exchange = 1;
                }
                else
                {
                    exchange = 0;
                }

                int twoDy = 2 * dy;
                int twoDx = 2 * dx;

                int e = twoDy - dx;

                int x = begPoint.X;
                int y = begPoint.Y;

                for (int i = 0; i <= dx; i++)
                {
                    set_pixel(graphics, pen, draw, x, y);

                    if (e >= 0)
                    {
                        if (exchange == 1)
                            x += xSign;
                        else
                            y += ySign;

                        e -= twoDx;
                    }

                    if (exchange == 1)
                        y += ySign;
                    else
                        x += xSign;

                    e += twoDy;
                }
            }
        }

        public static class FillAlgorithm
        {
            public static void Fill(ref Bitmap img, PictureBox canvas, Color markColor, Color bgColor, Color figureColor, PointD pMin, PointD pMax, bool delay)
            {
                if (!create_new_fig)
                {
                    MessageBox.Show("Ошибка", "Не все фигуры замкнуты!");
                    return;
                }

                MarkDesiredPixels(ref img, figureList, markColor);
                bool flag = false;

                for (int y = pMax.Y; y >= pMin.Y; y--)
                {
                    for (int x = pMin.X; x <= pMax.X; x++)
                    {
                        if (img.GetPixel(x, y).R == markColor.R && img.GetPixel(x, y).G == markColor.G && img.GetPixel(x, y).B == markColor.B)
                        {
                            flag = !flag;
                        }

                        if (flag)
                        {
                            img.SetPixel(x, y, figureColor);
                        }
                        else
                        {
                            img.SetPixel(x, y, bgColor);
                        }
                    }

                    if (delay)
                    {
                        canvas.Image = img; 
                        Application.DoEvents(); // Обновляем форму для отображения изменений
                        System.Threading.Thread.Sleep(10); // Задержка для визуализации
                    }
                }
                canvas.Image = img;
            }

            public static void MarkDesiredPixels(ref Bitmap img, List<Figure> figures, Color markColor)
            {
                foreach (Figure figure in figures)
                {
                    for (int i = 0; i < figure.pointListFig.Count; i++)
                    {
                        PointD p1 = figure.pointListFig[i];
                        PointD p2 = figure.pointListFig[(i + 1) % figure.pointListFig.Count]; // Следующая точка (циклически)

                        if (p2.Y == p1.Y)
                            continue;

                        int yMin = Math.Min(p1.Y, p2.Y);
                        int yMax = Math.Max(p1.Y, p2.Y);

                        float dx = p2.X - p1.X;
                        float dy = p2.Y - p1.Y;

                        for (int y = yMin; y < yMax; y++)
                        {
                            float x = dx / dy * (y - p1.Y) + p1.X;

                            if (img.GetPixel((int)x + 1, y).R == markColor.R && img.GetPixel((int)x + 1, y).G == markColor.G && img.GetPixel((int)x + 1, y).B == markColor.B)
                            {
                                img.SetPixel((int)x + 2, y, markColor);
                            }
                            else
                            {
                                img.SetPixel((int)x + 1, y, markColor);
                            }
                        }
                    }
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
