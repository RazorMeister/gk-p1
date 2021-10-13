using System;
using System.Drawing;
using System.Windows.Forms;

namespace Projekt1.Shapes
{
    abstract class AdvancedShape : SimpleShape
    {
        public bool Completed { get; protected set; }
        protected Point lastPoint;
        protected bool isMoving = false;
        public SimpleShape SelectedShape { get; protected set; } = null;

        public virtual void UpdateLastPoint(Point p) => this.lastPoint = p;

        /* Drawing */
        public abstract void Draw(Bitmap bm, PaintEventArgs e);
        public virtual void FinishDrawing(Point p) => this.Completed = true;

        /* Selecting */
        public abstract SimpleShape GetNearestShape(Point p);
        public bool IsNearSelectedObjectIndex(Point p) => this.GetNearestShape(p) == this.SelectedShape;
        public virtual void SelectShape(SimpleShape shape) => this.SelectedShape = shape;
        public virtual void DeselectShape() => this.SelectedShape = null;

        /* Moving */
        public virtual void StartMoving(Point p)
        {
            this.isMoving = true;
            this.lastPoint = p;
        }
        public virtual void FinishMoving() => this.isMoving = false;
        public void UpdateMoving(Point p)
        {
            if (!this.isMoving || this.SelectedShape == null) throw new Exception("Moving shape not set");

            int dX = p.X - this.lastPoint.X;
            int dY = p.Y - this.lastPoint.Y;
            this.lastPoint = p;
            this.HandleMoving(dX, dY);
        }
        protected virtual void HandleMoving(int dX, int dY) => this.SelectedShape.Move(dX, dY);

        /* Destroying */
        public virtual void Destroy() {}
    }
}
