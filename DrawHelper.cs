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

        public static void DrawLine(Bitmap bm, Point p1, Point p2, DrawHelper.DrawType drawType, Color? colorParam = null)
        {
            Color color = colorParam ?? Color.Black;

            if (drawType == DrawHelper.DrawType.NORMAL)
                DrawHelperLine.DrawLineNormal(bm, p1, p2, color);
            else
                DrawHelperLine.DrawLineAntyaliasing(bm, p1, p2, color);
        }

        public static void DrawCircle(Bitmap bm, Point center, int r, DrawHelper.DrawType drawType, Color? colorParam = null)
        {
            Color color = colorParam ?? Color.Black;

            if (drawType == DrawHelper.DrawType.NORMAL)
                DrawHelperCircle.DrawCircleNormal(bm, center, r, color);
            else
                DrawHelperCircle.DrawCircleAntyaliasing(bm, center, r, color);
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
