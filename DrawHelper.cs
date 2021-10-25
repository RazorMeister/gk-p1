﻿using System;
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

        private static int ipart(double x) { return (int)x; }

        private static int round(double x) { return ipart(x + 0.5); }

        private static double fpart(double x)
        {
            if (x < 0) return (1 - (x - Math.Floor(x)));
            return (x - Math.Floor(x));
        }

        private static double rfpart(double x)
        {
            return 1 - fpart(x);
        }

        private static void plot(Bitmap bm, double x, double y, double c, Color colorParam)
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

            double xEnd = Math.Round(x0);
            double yEnd = y0 + gradient * (xEnd - x0);
            double xGap = rfpart(x0 + 0.5);
            double xPixel1 = xEnd;
            double yPixel1 = ipart(yEnd);

            if (steep)
            {
                plot(bm, yPixel1, xPixel1, rfpart(yEnd) * xGap, color);
                plot(bm, yPixel1 + 1, xPixel1, fpart(yEnd) * xGap, color);
            }
            else
            {
                plot(bm, xPixel1, yPixel1, rfpart(yEnd) * xGap, color);
                plot(bm, xPixel1, yPixel1 + 1, fpart(yEnd) * xGap, color);
            }
            double intery = yEnd + gradient;

            xEnd = round(x1);
            yEnd = y1 + gradient * (xEnd - x1);
            xGap = fpart(x1 + 0.5);
            double xPixel2 = xEnd;
            double yPixel2 = ipart(yEnd);
            if (steep)
            {
                plot(bm, yPixel2, xPixel2, rfpart(yEnd) * xGap, color);
                plot(bm, yPixel2 + 1, xPixel2, fpart(yEnd) * xGap, color);
            }
            else
            {
                plot(bm, xPixel2, yPixel2, rfpart(yEnd) * xGap, color);
                plot(bm, xPixel2, yPixel2 + 1, fpart(yEnd) * xGap, color);
            }

            if (steep)
            {
                for (int x = (int)(xPixel1 + 1); x <= xPixel2 - 1; x++)
                {
                    plot(bm, ipart(intery), x, rfpart(intery), color);
                    plot(bm, ipart(intery) + 1, x, fpart(intery), color);
                    intery += gradient;
                }
            }
            else
            {
                for (int x = (int)(xPixel1 + 1); x <= xPixel2 - 1; x++)
                {
                    plot(bm, x, ipart(intery), rfpart(intery), color);
                    plot(bm, x, ipart(intery) + 1, fpart(intery), color);
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

        private static double distance(int x, int y)
        {
            double real_point = Math.Sqrt(Math.Pow(x, 2) - Math.Pow(y, 2));
            return (Math.Ceiling(real_point) - real_point);
        }
        private static int new_color(double i)
        {
            return (int)Math.Round((i * 127));
        }

        public static void DrawCircleAntyaliasing(Bitmap bm, Point center, int r, Color color)
        {
            int offset_x = center.X;
            int offset_y = center.Y;
            
            int x = r;
            int y = -1;
            double t = 0;
            while (x - 1 > y)
            {
                y++;
                double current_distance = distance(r, y);
                if (current_distance < t)
                    x--;

                //shades
                int transparency = new_color(current_distance);
                int alpha = transparency;
                int alpha2 = 127 - transparency;
                Color c2 = Color.FromArgb(255, color.R, color.G, color.B);
                Color c3 = Color.FromArgb(alpha2, color.R, color.G, color.B);
                Color c4 = Color.FromArgb(alpha, color.R, color.G, color.B);
              
                SetPixel(bm, x + offset_x, y + offset_y, c2);
                SetPixel(bm, x + offset_x - 1, y + offset_y, c4);
                SetPixel(bm, x + offset_x + 1, y + offset_y, c3);

                SetPixel(bm, y + offset_x, x + offset_y, c2);
                SetPixel(bm, y + offset_x, x + offset_y - 1, c4);
                SetPixel(bm, y + offset_x, x + offset_y + 1, c3);

                SetPixel(bm, offset_x - x, y + offset_y, c2);
                SetPixel(bm, offset_x - x + 1, y + offset_y, c4);
                SetPixel(bm, offset_x - x - 1, y + offset_y, c3);

                SetPixel(bm, offset_x - y, x + offset_y, c2);
                SetPixel(bm, offset_x - y, x + offset_y - 1, c4);
                SetPixel(bm, offset_x - y, x + offset_y + 1, c3);

                SetPixel(bm, x + offset_x, offset_y - y, c2);
                SetPixel(bm, x + offset_x - 1, offset_y - y, c4);
                SetPixel(bm, x + offset_x + 1, offset_y - y, c3);

                //UP
                SetPixel(bm, y + offset_x, offset_y - x, c2);
                SetPixel(bm, y + offset_x, offset_y - x + 1, c4);
                SetPixel(bm, y + offset_x, offset_y - x - 1, c3);

                SetPixel(bm, offset_x - y, offset_y - x, c2);
                SetPixel(bm, offset_x - y, offset_y - x + 1, c4);
                SetPixel(bm, offset_x - y, offset_y - x - 1, c3);

                SetPixel(bm, offset_x - x, offset_y - y, c2);
                SetPixel(bm, offset_x - x + 1, offset_y - y, c4);
                SetPixel(bm, offset_x - x - 1, offset_y - y, c3);

                t = current_distance;
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
