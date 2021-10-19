using System;
using System.Collections.Generic;
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

        public override void Move(int dX, int dY, Stack<Tuple<Relation, SimpleShape>> relationsStack, bool addRelationsToFix = true)
        {
            this.center.Move(dX, dY, relationsStack, false);
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
                DrawHelper.GetNormalColor(this.SelectedShape is CircleEdge)
            );

            if (Completed)
            {
                e.Graphics.FillEllipse(
                    new SolidBrush(DrawHelper.GetFillColor(this.SelectedShape is Circle)),
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
                new SolidBrush(DrawHelper.GetNormalColor(this.SelectedShape is CircleCenter)),
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
            var relationsStack = RelationManager.GetRelationsStack();

            if (this.SelectedShape is CircleEdge)
                this.SetR((int)DrawHelper.PointsDistance(this.center.GetPoint, this.lastPoint));
            else
                this.SelectedShape.Move(dX, dY, relationsStack);

            this.AddRelationsToStack(relationsStack);

            RelationManager.RunRelations(relationsStack);
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

            if (this.SelectedShape != null)
            {
                Type selectedShapeType = this.SelectedShape.GetType();

                if (selectedShapeType == typeof(Circle))
                    text += " | Selected whole shape";
                else if (selectedShapeType == typeof(CircleEdge))
                    text += " | Selected circle edge";
                else
                    text += " | Selected center";
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
