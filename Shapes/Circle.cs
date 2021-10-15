using System;
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

        public override SimpleShape GetNearestShape(Point p)
        {
            // Center point clicked
            if (DrawHelper.PointsDistance(p, this.center.GetPoint) < DrawHelper.DISTANCE)
                return this.center;

            // Edge clicked
            var distance = DrawHelper.PointsDistance(p, this.center.GetPoint) - this.R;
            if (distance is >= -DrawHelper.DISTANCE and <= DrawHelper.DISTANCE)
                return this.edge;

            // Whole circle clicked
            if (DrawHelper.PointsDistance(p, this.center.GetPoint) <= this.R)
                return this;

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
    }
}
