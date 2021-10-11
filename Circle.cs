﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projekt1
{
    class Circle: Shape
    {
        private Point _startPoint;
        private int _r = 0;

        public Circle(Point startPoint, Form form): base(form)
        {
            this._startPoint = startPoint;
        }

        public override void UpdateLastPoint(Point p)
        {
            this._r = (int) DrawHelper.PointsDistance(p, this._startPoint);
        }

        public override void FinishDrawing(Point p)
        {
            this.Completed = true;
        }

        public override bool IsNearSelectedObjectIndex(Point p)
        {
            if (this.selectedObjectIndex == 1) return this.GetNearestPoint(p, Keys.Control) != null;
            if (this.selectedObjectIndex == 0) return this.GetNearestPoint(p, Keys.Shift) != null;
            return false;
        }

        public override void Draw(Bitmap bm, PaintEventArgs e)
        {
            DrawHelper.DrawCircle(bm, this._startPoint, this._r, this.selectedObjectIndex == 1 ? Color.Red : Color.Black);

            if (Completed)
            {
                Rectangle rect = new Rectangle(this._startPoint.X - 3, this._startPoint.Y - 3, 16, 16);
                var pen = new Pen(this.selectedObjectIndex == 0 ? Color.Red : Color.Black, 1);
                e.Graphics.DrawRectangle(pen, rect);
                pen.Dispose();


            }
        }

        public override int? GetNearestPoint(Point p, Keys key)
        {
            if (key == Keys.Control && DrawHelper.PointsDistance(p, this._startPoint) - this._r < DrawHelper.DISTANCE)
                return 1; 
            if (key == Keys.Shift && DrawHelper.PointsDistance(p, this._startPoint) < DrawHelper.DISTANCE)
                return 0;

            return null;
        }

        public override void UpdateMoving(Point p)
        {
            if (this.selectedObjectIndex == 0)
            {
                int dX = p.X - this._startPoint.X;
                int dY = p.Y - this._startPoint.Y;

                this._startPoint = new Point(this._startPoint.X + dX, this._startPoint.Y + dY);
            }
            else
                this._r = (int)DrawHelper.PointsDistance(this._startPoint, p);
        }

        public override string ToString()
        {
            return $"Circle - startPoint ({this._startPoint.X}, {this._startPoint.Y}) | r = {this._r}";
        }
    }
}
