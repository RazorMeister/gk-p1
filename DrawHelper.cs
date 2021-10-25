using System;
using System.Drawing;

namespace Projekt1
{
    public static class DrawHelper
    {
        public const int DISTANCE = 20;

        public enum DrawType
        {
            NORMAL,
            ANTYALIASING
        }

        public static void DrawLine(Bitmap bm, Point p1, Point p2, DrawType drawType, Color? colorParam = null)
        {
            Color color = colorParam ?? Color.Black;

            if (drawType == DrawType.NORMAL)
                DrawLineNormal(bm, p1, p2, color);
            else
                DrawLineAntyaliasing(bm, p1, p2, color);
        }

        private static int Ipart(double x) => (int)x;

        private static int Round(double x) => Ipart(x + 0.5);

        private static double Fpart(double x)
        {
            if (x < 0) return (1 - (x - Math.Floor(x)));
            return (x - Math.Floor(x));
        }

        private static double Rfpart(double x) => 1 - Fpart(x);

        private static void Plot(Bitmap bm, double x, double y, double c, Color colorParam)
        {
            int alpha = (int)(c * 255);
            if (alpha > 255) alpha = 255;
            if (alpha < 0) alpha = 0;
            Color color = Color.FromArgb(alpha, colorParam);
            SetPixel(bm, (int)x, (int)y, color);
        }

        public static void DrawLineAntyaliasing(Bitmap bm, Point p1, Point p2, Color color)
        {
            double x0 = p1.X;
            double y0 = p1.Y;

            double x1 = p2.X;
            double y1 = p2.Y;

            bool steep = Math.Abs(y1 - y0) > Math.Abs(x1 - x0);
            double temp;
            if (steep)
            {
                temp = x0; x0 = y0; y0 = temp;
                temp = x1; x1 = y1; y1 = temp;
            }
            if (x0 > x1)
            {
                temp = x0; x0 = x1; x1 = temp;
                temp = y0; y0 = y1; y1 = temp;
            }

            double dx = x1 - x0;
            double dy = y1 - y0;
            double gradient = dy / dx;

            double xEnd = Round(x0);
            double yEnd = y0 + gradient * (xEnd - x0);
            double xGap = Rfpart(x0 + 0.5);
            double xPixel1 = xEnd;
            double yPixel1 = Ipart(yEnd);

            if (steep)
            {
                Plot(bm, yPixel1, xPixel1, Rfpart(yEnd) * xGap, color);
                Plot(bm, yPixel1 + 1, xPixel1, Fpart(yEnd) * xGap, color);
            }
            else
            {
                Plot(bm, xPixel1, yPixel1, Rfpart(yEnd) * xGap, color);
                Plot(bm, xPixel1, yPixel1 + 1, Fpart(yEnd) * xGap, color);
            }
            double intery = yEnd + gradient;

            xEnd = Round(x1);
            yEnd = y1 + gradient * (xEnd - x1);
            xGap = Fpart(x1 + 0.5);
            double xPixel2 = xEnd;
            double yPixel2 = Ipart(yEnd);
            if (steep)
            {
                Plot(bm, yPixel2, xPixel2, Rfpart(yEnd) * xGap, color);
                Plot(bm, yPixel2 + 1, xPixel2, Fpart(yEnd) * xGap, color);
            }
            else
            {
                Plot(bm, xPixel2, yPixel2, Rfpart(yEnd) * xGap, color);
                Plot(bm, xPixel2, yPixel2 + 1, Fpart(yEnd) * xGap, color);
            }

            if (steep)
            {
                for (int x = (int)(xPixel1 + 1); x <= xPixel2 - 1; x++)
                {
                    Plot(bm, Ipart(intery), x, Rfpart(intery), color);
                    Plot(bm, Ipart(intery) + 1, x, Fpart(intery), color);
                    intery += gradient;
                }
            }
            else
            {
                for (int x = (int)(xPixel1 + 1); x <= xPixel2 - 1; x++)
                {
                    Plot(bm, x, Ipart(intery), Rfpart(intery), color);
                    Plot(bm, x, Ipart(intery) + 1, Fpart(intery), color);
                    intery += gradient;
                }
            }
        }

        public static void DrawLineNormal(Bitmap bm, Point p1, Point p2, Color color)
        {
            int x1 = p1.X;
            int y1 = p1.Y;

            int x2 = p2.X;
            int y2 = p2.Y;

            int d, dx, dy, ai, bi, xi, yi;
            int x = x1, y = y1;

            // Set direction of drawing
            if (x1 < x2)
            {
                xi = 1;
                dx = x2 - x1;
            }
            else
            {
                xi = -1;
                dx = x1 - x2;
            }

            // Set direction of drawing
            if (y1 < y2)
            {
                yi = 1;
                dy = y2 - y1;
            }
            else
            {
                yi = -1;
                dy = y1 - y2;
            }

            // First pixel
            SetPixel(bm, x, y, color);

            // OX
            if (dx > dy)
            {
                ai = (dy - dx) * 2;
                bi = dy * 2;
                d = bi - dx;

                while (x != x2)
                {
                    if (d >= 0)
                    {
                        x += xi;
                        y += yi;
                        d += ai;
                    }
                    else
                    {
                        d += bi;
                        x += xi;
                    }
                    SetPixel(bm, x, y, color);
                }
            }

            // OY
            else
            {
                ai = (dx - dy) * 2;
                bi = dx * 2;
                d = bi - dy;
               
                while (y != y2)
                {
                    if (d >= 0)
                    {
                        x += xi;
                        y += yi;
                        d += ai;
                    }
                    else
                    {
                        d += bi;
                        y += yi;
                    }
                    SetPixel(bm, x, y, color);
                }
            }
        }

        public static void DrawCircle(Bitmap bm, Point center, int r, DrawType drawType, Color? colorParam = null)
        {
            Color color = colorParam ?? Color.Black;

            if (drawType == DrawType.NORMAL)
                DrawCircleNormal(bm, center, r, color);
            else
                DrawCircleAntyaliasing(bm, center, r, color);
        }

        private static double Distance(int x, int y)
        {
            double realPoint = Math.Sqrt(Math.Pow(x, 2) - Math.Pow(y, 2));
            return (Math.Ceiling(realPoint) - realPoint);
        }
        private static int NewColor(double i)
        {
            return (int)Math.Round((i * 127));
        }

        public static void DrawCircleAntyaliasing(Bitmap bm, Point center, int r, Color color)
        {
            int offsetX = center.X;
            int offsetY = center.Y;
            
            int x = r;
            int y = -1;
            double t = 0;
            while (x - 1 > y)
            {
                y++;
                double currentDistance = Distance(r, y);
                if (currentDistance < t)
                    x--;

                //shades
                int transparency = NewColor(currentDistance);
                int alpha = transparency;
                int alpha2 = 127 - transparency;
                Color c2 = Color.FromArgb(255, color.R, color.G, color.B);
                Color c3 = Color.FromArgb(alpha2, color.R, color.G, color.B);
                Color c4 = Color.FromArgb(alpha, color.R, color.G, color.B);
              
                SetPixel(bm, x + offsetX, y + offsetY, c2);
                SetPixel(bm, x + offsetX - 1, y + offsetY, c4);
                SetPixel(bm, x + offsetX + 1, y + offsetY, c3);

                SetPixel(bm, y + offsetX, x + offsetY, c2);
                SetPixel(bm, y + offsetX, x + offsetY - 1, c4);
                SetPixel(bm, y + offsetX, x + offsetY + 1, c3);

                SetPixel(bm, offsetX - x, y + offsetY, c2);
                SetPixel(bm, offsetX - x + 1, y + offsetY, c4);
                SetPixel(bm, offsetX - x - 1, y + offsetY, c3);

                SetPixel(bm, offsetX - y, x + offsetY, c2);
                SetPixel(bm, offsetX - y, x + offsetY - 1, c4);
                SetPixel(bm, offsetX - y, x + offsetY + 1, c3);

                SetPixel(bm, x + offsetX, offsetY - y, c2);
                SetPixel(bm, x + offsetX - 1, offsetY - y, c4);
                SetPixel(bm, x + offsetX + 1, offsetY - y, c3);

                //UP
                SetPixel(bm, y + offsetX, offsetY - x, c2);
                SetPixel(bm, y + offsetX, offsetY - x + 1, c4);
                SetPixel(bm, y + offsetX, offsetY - x - 1, c3);

                SetPixel(bm, offsetX - y, offsetY - x, c2);
                SetPixel(bm, offsetX - y, offsetY - x + 1, c4);
                SetPixel(bm, offsetX - y, offsetY - x - 1, c3);

                SetPixel(bm, offsetX - x, offsetY - y, c2);
                SetPixel(bm, offsetX - x + 1, offsetY - y, c4);
                SetPixel(bm, offsetX - x - 1, offsetY - y, c3);

                t = currentDistance;
            }
        }

        public static void DrawCircleNormal(Bitmap bm, Point center, int r, Color color)
        {
            int x = 0, y = r;
            int d = 3 - 2 * r;
            Draw8CirclePoints(bm, center, x, y, color);
            while (y >= x)
            {
                // for each pixel we will
                // draw all eight pixels

                x++;

                // check for decision parameter
                // and correspondingly
                // update d, x, y
                if (d > 0)
                {
                    y--;
                    d = d + 4 * (x - y) + 10;
                }
                else
                    d = d + 4 * x + 6;

                Draw8CirclePoints(bm, center, x, y, color);
            }
        }

        private static void Draw8CirclePoints(Bitmap bm, Point center, int x, int y, Color color)
        {
            SetPixel(bm, center.X + x, center.Y + y, color);
            SetPixel(bm, center.X - x, center.Y + y, color);
            SetPixel(bm, center.X + x, center.Y - y, color);
            SetPixel(bm, center.X - x, center.Y - y, color);
            SetPixel(bm, center.X + y, center.Y + x, color);
            SetPixel(bm, center.X - y, center.Y + x, color);
            SetPixel(bm, center.X + y, center.Y - x, color);
            SetPixel(bm, center.X - y, center.Y - x, color);
        }

        public static void SetPixel(Bitmap bm, int x, int y, Color color)
        {
            if (
                x < 0
                || y < 0
                || x > bm.Width - 5
                || y > bm.Height - 5
            ) return;

            bm.SetPixel(x, y, color);
        }

        public static double EdgeDistance(Point p, Point edgeA, Point edgeB)
        {
            double pX = edgeB.X - edgeA.X;
            double pY = edgeB.Y - edgeA.Y;
            double tmp = (pX * pX) + (pY * pY);
            double u = ((p.X - edgeA.X) * pX + (p.Y - edgeA.Y) * pY) / tmp;

            if (u > 1) u = 1;
            else if (u < 0) u = 0;

            double x = edgeA.X + u * pX;
            double y = edgeA.Y + u * pY;

            double dX = x - p.X;
            double dY = y - p.Y;

            return Math.Sqrt(dX * dX + dY * dY);
        }

        public static double PointsDistance(Point p1, Point p2)
            => Math.Sqrt(Math.Pow(Math.Abs(p2.X - p1.X), 2) + Math.Pow(Math.Abs(p2.Y - p1.Y), 2));

        public static Point CenterPoint(Point p1, Point p2) 
            => new Point((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2);

        public static Color GetNormalColor(bool selected) => selected ? Color.Red : Color.Black;

        public static Color GetFillColor(bool selected) => selected ? Color.Red : Color.AliceBlue;
    }
}
