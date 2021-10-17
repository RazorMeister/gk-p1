using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Projekt1.Relations;

namespace Projekt1.Shapes
{
    abstract class AdvancedShape : SimpleShape
    {
        public bool Completed { get; protected set; }
        protected Point lastPoint;
        protected bool isMoving = false;

        public SimpleShape SelectedShape { get; protected set; } = null;

        public int DX { get; private set; }
        public int DY { get; private set; }

        public virtual void UpdateLastPoint(Point p) => this.lastPoint = p;

        /* Drawing */
        public abstract void Draw(Bitmap bm, PaintEventArgs e);

        public virtual void FinishDrawing() => this.Completed = true;

        /* Selecting */
        public abstract Tuple<SimpleShape, double> GetNearestShape(Point p);

        public bool IsNearSelectedObjectIndex(Point p) => this.GetNearestShape(p)?.Item1 == this.SelectedShape;

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

            DX = p.X - this.lastPoint.X;
            DY = p.Y - this.lastPoint.Y;
            this.lastPoint = p;
            this.HandleMoving(DX, DY);
        }

        protected virtual void HandleMoving(int dX, int dY)
        {
            Stack<Tuple<Relation, SimpleShape>> relationsStack = new Stack<Tuple<Relation, SimpleShape>>();
            this.SelectedShape.Move(dX, dY, relationsStack);
            this.RunRelationsStack(relationsStack);
        }

        protected void RunRelationsStack(Stack<Tuple<Relation, SimpleShape>> relationsStack)
        {
            int fixesNumber = 0;
            string lastRelationUid = null;

            while (true)
            {
                if (relationsStack.Count == 0)
                    break;

                var relationTuple = relationsStack.Pop();

                if (relationTuple.Item1.Uid == lastRelationUid)
                    continue;

                lastRelationUid = relationTuple.Item1.Uid;

                //Debug.WriteLine($"{relationTuple.Item1.GetType()} - {relationTuple.Item1.Uid}");
                relationTuple.Item1.FixRelation(relationTuple.Item2, relationsStack);
                fixesNumber++;

                if (fixesNumber >= 30)
                {
                    Debug.WriteLine($"{fixesNumber}");
                    Debug.WriteLine("");
                    throw new CannotMoveException();
                }
            }

            //Debug.WriteLine($"{fixesNumber}");
        }

        public override void AddRelationsToStack(Stack<Tuple<Relation, SimpleShape>> relationsStack, Type exceptType = null)
            => this.relations
                .FindAll(relation => relation.GetType() != exceptType)
                .ForEach(relation => relationsStack.Push(new Tuple<Relation, SimpleShape>(relation, this.SelectedShape)));
    }

    internal class Uid
    {
    }
}
