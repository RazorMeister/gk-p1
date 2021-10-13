using System.Drawing;
using System.Windows.Forms;

namespace Projekt1.Shapes
{
    class Circle : AdvancedShape
    {
        private Vertex startVertex; // Center of the circle
        public int R { get; set; }

        public Circle(Point startPoint)
        {
            this.startVertex = new Vertex(startPoint);
        }

        public override ShapeType GetShapeType() => ShapeType.Circle;

        public override void Move(int dX, int dY)
        {
            this.startVertex.Move(dX, dY);
        }

        public override void UpdateLastPoint(Point p)
        {
            this.R = (int)DrawHelper.PointsDistance(p, this.startVertex.GetPoint);
        }

        public override void Draw(Bitmap bm, PaintEventArgs e)
        {
            DrawHelper.DrawCircle(
                bm, 
                this.startVertex.GetPoint, 
                this.R,
                DrawHelper.GetNormalColor(this.SelectedShape != null && this.SelectedShape.GetShapeType() != ShapeType.Circle)
            );

            if (Completed)
            {
                e.Graphics.FillEllipse(
                    new SolidBrush(DrawHelper.GetFillColor(this.SelectedShape?.GetShapeType() == ShapeType.Circle)),
                    new Rectangle(
                        this.startVertex.X - this.R,
                        this.startVertex.Y - this.R,
                        this.R + this.R,
                        this.R + this.R
                    )
                );
            }
        }

        protected override void HandleMoving(int dX, int dY)
        {
            if (this.SelectedShape.GetShapeType() == ShapeType.Circle) this.SelectedShape.Move(dX, dY);
            else this.R = (int)DrawHelper.PointsDistance(this.startVertex.GetPoint, this.lastPoint);
        }

        public override SimpleShape GetNearestShape(Point p)
        {
            // Edge clicked
            var distance = DrawHelper.PointsDistance(p, this.startVertex.GetPoint) - this.R;
            if (distance is >= -DrawHelper.DISTANCE and <= DrawHelper.DISTANCE)
                return this.startVertex;

            // Whole circle clicked
            if (DrawHelper.PointsDistance(p, this.startVertex.GetPoint) <= this.R)
                return this;

            return null;
        }

        public override string ToString()
        {
            var text = $"Circle | startPoint {this.startVertex.ToString()} | R = {this.R}";

            switch (this.SelectedShape?.GetShapeType())
            {
                case ShapeType.Circle:
                    text += " | Selected whole shape";
                    break;
                case ShapeType.Vertex:
                    text += " | Selected circle edge";
                    break;
            }

            return text;
        }
    }
}
