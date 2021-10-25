using System;
using System.Drawing;

namespace Projekt1
{
    public static class DrawHelperLine
    {
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
            DrawHelper.SetPixel(bm, (int)x, (int)y, color);
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
            DrawHelper.SetPixel(bm, x, y, color);

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
                    DrawHelper.SetPixel(bm, x, y, color);
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
                    DrawHelper.SetPixel(bm, x, y, color);
                }
            }
        }
    }
}