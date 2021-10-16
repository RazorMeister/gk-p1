﻿using System;
using System.Drawing;
using System.Windows.Forms;
using Projekt1.Relations;

namespace Projekt1.Shapes
{
    class Circle : AdvancedShape
    {
        private const int MIN_R = 8;

        public CircleCenter center { get; private set; } // Center of the circle
        private CircleEdge edge;

        public int R { get; private set; }

        private int savedR;

        public Circle(Point startPoint)
        {
            this.center = new CircleCenter(startPoint);
            this.edge = new CircleEdge();
        }

        public override ShapeType GetShapeType() => ShapeType.Circle;

        public override void Move(int dX, int dY)
        {
            this.center.Move(dX, dY);
        }

        public override void UpdateLastPoint(Point p)
        {
            this.SetR((int)DrawHelper.PointsDistance(p, this.center.GetPoint));
        }

        public void SetR(int r)
        {
            this.R = Math.Max(MIN_R, r);
        }

        public override void Draw(Bitmap bm, PaintEventArgs e)
        {
            DrawHelper.DrawCircle(
                bm, 
                this.center.GetPoint, 
                this.R,
                DrawHelper.GetNormalColor(this.SelectedShape?.GetShapeType() == ShapeType.CircleEdge)
            );

            if (Completed)
            {
                e.Graphics.FillEllipse(
                    new SolidBrush(DrawHelper.GetFillColor(this.SelectedShape?.GetShapeType() == ShapeType.Circle)),
                    new Rectangle(
                        this.center.X - this.R,
                        this.center.Y - this.R,
                        this.R + this.R,
                        this.R + this.R
                    )
                );
            }

            int radius = 5;
            e.Graphics.FillEllipse(
                new SolidBrush(DrawHelper.GetNormalColor(this.SelectedShape?.GetShapeType() == ShapeType.CircleCenter)),
                new Rectangle(
                    center.X - radius,
                    center.Y - radius,
                    radius + radius,
                    radius + radius
                )
            );
        }

        protected override void HandleMoving(int dX, int dY)
        {
            if (this.SelectedShape.GetShapeType() == ShapeType.CircleEdge)
                this.SetR((int)DrawHelper.PointsDistance(this.center.GetPoint, this.lastPoint));
            else
                this.SelectedShape.Move(dX, dY);
        }

        public override Tuple<SimpleShape, double> GetNearestShape(Point p)
        {
            var distance = DrawHelper.PointsDistance(p, this.center.GetPoint);

            // Center point clicked
            if (distance < DrawHelper.DISTANCE)
                return new Tuple<SimpleShape, double>(this.center, distance);

            // Edge clicked
            distance = Math.Abs(distance - this.R);
            if (distance <= DrawHelper.DISTANCE)
                return new Tuple<SimpleShape, double>(this.edge, distance);

            // Whole circle clicked
            if (DrawHelper.PointsDistance(p, this.center.GetPoint) <= this.R)
                return new Tuple<SimpleShape, double>(this, DrawHelper.DISTANCE + 1);

            return null;
        }

        public override string ToString()
        {
            var text = $"Circle | startPoint {this.center.ToString()} | R = {this.R}";

            switch (this.SelectedShape?.GetShapeType())
            {
                case ShapeType.Circle:
                    text += " | Selected whole shape";
                    break;
                case ShapeType.CircleEdge:
                    text += " | Selected circle edge";
                    break;
                case ShapeType.CircleCenter:
                    text += " | Selected center";
                    break;
            }

            return text;
        }

        public override void SavePosition()
        {
            this.savedR = this.R;
            this.center.SavePosition();
        }

        public override void BackUpSavedPosition()
        {
            this.R = this.savedR;
            this.center.BackUpSavedPosition();
        }
    }
}
