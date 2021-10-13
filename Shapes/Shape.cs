using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Projekt1.Relations;

namespace Projekt1.Shapes
{
    abstract class Shape
    {
        public bool Completed { get; protected set; }

        public int? SelectedObjectIndex { get; protected set;  }= null;
        protected bool isMoving = false;
        protected Point lastMovingPoint;

        protected readonly Form _form;
        protected Shape(Form form) => this._form = form;

        protected List<Relation> relations = new List<Relation>();

        public void AddRelation(Relation relation)
        {
            this.relations.Add(relation);
        }

        public void RemoveRelation(Type relationType)
        {
            foreach (var relation in this.relations)
                if (relation.GetType() == relationType)
                {
                    this.relations.Remove(relation);
                    break;
                }
        }

        public bool HasRelation(Type relationType)
        {
            foreach (var relation in this.relations)
                if (relation.GetType() == relationType)
                    return true;

            return false;
        }

        protected bool CanMakeMove()
        {
            foreach (var relation in this.relations)
                if (!relation.CanMakeMove())
                    return false;

            return true;
        }

        public abstract bool IsNearSelectedObjectIndex(Point p);

        public virtual void Destroy() { }

        /* Drawing */
        public abstract void Draw(Bitmap bm, PaintEventArgs e);
        public abstract void UpdateLastPoint(Point p);
        public abstract void FinishDrawing(Point p);

        public virtual void SelectObject(int index) => this.SelectedObjectIndex = index;
        public virtual void DeselectObject() => this.SelectedObjectIndex = null;

        /* Moving */
        public abstract int? GetNearestPoint(Point p);
        public virtual void StartMoving(Point p)
        {
            this.isMoving = true;
            this.lastMovingPoint = p;
        }
        public virtual void UpdateMoving(Point p) => this.lastMovingPoint = p;
        public virtual void FinishMoving() => this.isMoving = false;

        public bool IsSelectedWholeShape() => this.SelectedObjectIndex == 0;

        protected string GetRelationsString()
        {
            string txt = "        | Relations: ";

            foreach (var relation in this.relations)
                txt += relation.ToString() + ", ";

            return txt;
        }
    }
}
