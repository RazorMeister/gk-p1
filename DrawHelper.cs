using System;
using System.Drawing;

namespace Projekt1
{
    public static class DrawHelper
    {
        public const int DISTANCE = 20;

        public static void DrawLine(Bitmap bm, Point p1, Point p2, Color? colorParam = null)
        {
            Color color = colorParam ?? Color.Black;

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

        public static void DrawCircle(Bitmap bm, Point center, int r, Color? colorParam = null)
        {
            Color color = colorParam ?? Color.Black;

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
