using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Projekt1.Shapes
{
    class CircleOld: Shape
    {
        private Point _startPoint;
        public int R { get; private set; }

        public CircleOld(Point startPoint, Form form): base(form)
        {
            this._startPoint = startPoint;
        }

        public override void UpdateLastPoint(Point p)
        {
            this.R = (int) DrawHelper.PointsDistance(p, this._startPoint);
        }

        public void SetR(int r)
        {
            this.R = r;
        }

        public override void FinishDrawing(Point p)
        {
            this.Completed = true;
        }

        public override bool IsNearSelectedObjectIndex(Point p)
        {
            if (this.SelectedObjectIndex == null) return false;
            return this.GetNearestPoint(p) == this.SelectedObjectIndex;
        }

        public override void Draw(Bitmap bm, PaintEventArgs e)
        {
            DrawHelper.DrawCircle(bm, this._startPoint, this.R, this.SelectedObjectIndex == 1 ? Color.Red : Color.Black);

            if (Completed)
            {
                Brush fillBrush = this.SelectedObjectIndex == 0
                     ? Brushes.Red
                     : Brushes.AliceBlue;

                e.Graphics.FillEllipse(fillBrush, new Rectangle(
                    this._startPoint.X - this.R,
                    this._startPoint.Y - this.R,
                    this.R + this.R,
                    this.R + this.R
                ));
            }
        }

        public override int? GetNearestPoint(Point p)
        {
            // Edge clicked
            var distance = DrawHelper.PointsDistance(p, this._startPoint) - this.R;
            if (distance >= -DrawHelper.DISTANCE && distance <= DrawHelper.DISTANCE)
                return 1; 

            // Whole circle clicked
            if (DrawHelper.PointsDistance(p, this._startPoint) <= this.R)
                return 0;

            return null;
        }

        public override void UpdateMoving(Point p)
        {
            if (!this.CanMakeMove()) return;

            if (this.SelectedObjectIndex == 0)
            {
                int dX = p.X - this.lastMovingPoint.X;
                int dY = p.Y - this.lastMovingPoint.Y;

                this._startPoint = new Point(this._startPoint.X + dX, this._startPoint.Y + dY);
            }
            else
                this.R = (int)DrawHelper.PointsDistance(this._startPoint, p);

            base.UpdateMoving(p);
        }

        public override string ToString()
        {
            return $"Circle - startPoint ({this._startPoint.X}, {this._startPoint.Y}) | r = {this.R}" + this.GetRelationsString();
        }
    }
}
