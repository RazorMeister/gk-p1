using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projekt1
{
    abstract class Shape
    {
        public bool Completed { get; protected set; }

        protected int? selectedObjectIndex = null;
        protected bool isMoving = false;

        protected readonly Form _form;
        protected Shape(Form form)
        {
            this._form = form;
        }

        public abstract bool IsNearSelectedObjectIndex(Point p);

        public virtual void Destroy() { }

        /* Drawing */
        public abstract void Draw(Bitmap bm, PaintEventArgs e);
        public abstract void UpdateLastPoint(Point p);
        public abstract void FinishDrawing(Point p);

        public virtual void SelectObject(int index) => this.selectedObjectIndex = index;
        public virtual void DeselectObject() => this.selectedObjectIndex = null;

        /* Moving */
        public abstract int? GetNearestPoint(Point p, Keys key);
        public virtual void StartMoving() => this.isMoving = true;
        public abstract void UpdateMoving(Point p);
        public virtual void FinishMoving() => this.isMoving = false;
    }
}
