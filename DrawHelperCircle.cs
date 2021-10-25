using System;
using System.Drawing;

namespace Projekt1
{
    public static class DrawHelperCircle
    {
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

                DrawHelper.SetPixel(bm, x + offsetX, y + offsetY, c2);
                DrawHelper.SetPixel(bm, x + offsetX - 1, y + offsetY, c4);
                DrawHelper.SetPixel(bm, x + offsetX + 1, y + offsetY, c3);

                DrawHelper.SetPixel(bm, y + offsetX, x + offsetY, c2);
                DrawHelper.SetPixel(bm, y + offsetX, x + offsetY - 1, c4);
                DrawHelper.SetPixel(bm, y + offsetX, x + offsetY + 1, c3);

                DrawHelper.SetPixel(bm, offsetX - x, y + offsetY, c2);
                DrawHelper.SetPixel(bm, offsetX - x + 1, y + offsetY, c4);
                DrawHelper.SetPixel(bm, offsetX - x - 1, y + offsetY, c3);

                DrawHelper.SetPixel(bm, offsetX - y, x + offsetY, c2);
                DrawHelper.SetPixel(bm, offsetX - y, x + offsetY - 1, c4);
                DrawHelper.SetPixel(bm, offsetX - y, x + offsetY + 1, c3);

                DrawHelper.SetPixel(bm, x + offsetX, offsetY - y, c2);
                DrawHelper.SetPixel(bm, x + offsetX - 1, offsetY - y, c4);
                DrawHelper.SetPixel(bm, x + offsetX + 1, offsetY - y, c3);

                //UP
                DrawHelper.SetPixel(bm, y + offsetX, offsetY - x, c2);
                DrawHelper.SetPixel(bm, y + offsetX, offsetY - x + 1, c4);
                DrawHelper.SetPixel(bm, y + offsetX, offsetY - x - 1, c3);

                DrawHelper.SetPixel(bm, offsetX - y, offsetY - x, c2);
                DrawHelper.SetPixel(bm, offsetX - y, offsetY - x + 1, c4);
                DrawHelper.SetPixel(bm, offsetX - y, offsetY - x - 1, c3);

                DrawHelper.SetPixel(bm, offsetX - x, offsetY - y, c2);
                DrawHelper.SetPixel(bm, offsetX - x + 1, offsetY - y, c4);
                DrawHelper.SetPixel(bm, offsetX - x - 1, offsetY - y, c3);

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
            DrawHelper.SetPixel(bm, center.X + x, center.Y + y, color);
            DrawHelper.SetPixel(bm, center.X - x, center.Y + y, color);
            DrawHelper.SetPixel(bm, center.X + x, center.Y - y, color);
            DrawHelper.SetPixel(bm, center.X - x, center.Y - y, color);
            DrawHelper.SetPixel(bm, center.X + y, center.Y + x, color);
            DrawHelper.SetPixel(bm, center.X - y, center.Y + x, color);
            DrawHelper.SetPixel(bm, center.X + y, center.Y - x, color);
            DrawHelper.SetPixel(bm, center.X - y, center.Y - x, color);
        }
    }
}