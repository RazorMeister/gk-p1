using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Projekt1.Properties;
using Projekt1.Shapes;

namespace Projekt1.Relations
{
    class SameSizeEdges : TwoShapesRelation
    {
        private Edge firstEdge;
        private Edge secondEdge;
        private int lineLength;

        public override void FixRelation(SimpleShape movingShape, Stack<Tuple<Relation, SimpleShape>> relationsStack)
        {
            Tuple<double, double?> AB;
            Edge otherEdge;

            if (this.firstEdge.GetLength() == this.secondEdge.GetLength())
                return;

            if (firstEdge == movingShape)
            {
                AB = this.secondEdge.GetLineEquation();
                otherEdge = this.secondEdge;
                this.lineLength = this.firstEdge.GetLength();
            }
            else
            {
                AB = this.firstEdge.GetLineEquation();
                otherEdge = this.firstEdge;
                this.lineLength = this.secondEdge.GetLength();
            }

            var vertexToMove = otherEdge.VertexA;
            var otherVertex = otherEdge.VertexB;

            // Edges has one common vertex
            if (
                movingShape?.GetShapeType() == SimpleShape.ShapeType.Edge
                && (vertexToMove == ((Edge)movingShape).VertexA || vertexToMove == ((Edge)movingShape).VertexB)
            )
            {
                var tmp = vertexToMove;
                vertexToMove = otherVertex;
                otherVertex = tmp;
            }

            // X = sth
            if (AB.Item2 == null)
            {
                int newY = otherVertex.Y + this.lineLength;

                if (Math.Abs(vertexToMove.Y - newY) < Math.Abs(vertexToMove.Y + newY))
                    newY = otherVertex.Y - this.lineLength;

                vertexToMove.SetPoint(new Point(otherVertex.X, newY));
                vertexToMove.Edges.ForEach(edge => edge.AddRelationsToStack(relationsStack));
                return;
            }

            var a = AB.Item1;

            int newX1 = (int)(otherVertex.X + (this.lineLength / Math.Sqrt(1 + a * a)));
            int newX2 = (int)(otherVertex.X - (this.lineLength / Math.Sqrt(1 + a * a)));

            // Determine in which direction we want to 'move'
            int newX = Math.Abs(vertexToMove.X - newX1) > Math.Abs(vertexToMove.X - newX2) ? newX2 : newX1;
            vertexToMove.SetPoint(new Point(newX, (int)(a * newX + AB.Item2)));

            vertexToMove.Edges
                .FindAll(edge => edge != this.firstEdge && edge != this.secondEdge)
                .ForEach(edge => edge.AddRelationsToStack(relationsStack));
        }

        public override void Draw(Bitmap bm, PaintEventArgs e)
        {
            if (!this.Completed) return;

            var icon = new Icon(Resources.SameSizeEdgesRelation, 20, 20);

            var firstMiddlePoint = this.firstEdge.GetMiddlePoint();
            e.Graphics.DrawIcon(icon, firstMiddlePoint.X - 18, firstMiddlePoint.Y - 18);

            var secondMiddlePoint = this.secondEdge.GetMiddlePoint();
            e.Graphics.DrawIcon(icon, secondMiddlePoint.X - 18, secondMiddlePoint.Y - 18);
        }

        public override void Destroy()
        {
            this.firstEdge?.RemoveRelation(this);
            this.secondEdge?.RemoveRelation(this);
            base.Destroy();
        }

        public override SimpleShape.ShapeType? GetLeftShapeType()
        {
            if (this.Completed) return null;
            return SimpleShape.ShapeType.Edge;
        }

        public override void AddShape(SimpleShape shape)
        {
            if (this.firstEdge == null)
            {
                this.firstEdge = (Edge)shape;
                this.lineLength = this.firstEdge.GetLength();
            }
            else
            {
                this.secondEdge = (Edge)shape;

                if (this.secondEdge.GetRelationsNumberExcept(null) != 0)
                {
                    this.secondEdge = null;
                    return;
                }
            }
                

            shape.AddRelation(this);

            if (this.firstEdge != null && this.secondEdge != null)
            {
                this.Completed = true;
                Stack<Tuple<Relation, SimpleShape>> relationsStack = new Stack<Tuple<Relation, SimpleShape>>();
                this.FixRelation(null, relationsStack);
            }
        }

        public static BtnStatus RelationBtnStatus(AdvancedShape shape)
        {
            if (shape.SelectedShape.GetShapeType() == SimpleShape.ShapeType.Edge)
            {
                return shape.SelectedShape.HasRelationByType(typeof(SameSizeEdges))
                    ? BtnStatus.Active
                    : (
                        shape.SelectedShape.GetRelationsNumberExcept(typeof(SameSizeEdges)) == 0
                            ? BtnStatus.Enabled
                            : BtnStatus.Disabled
                    );
            }

            return BtnStatus.Disabled;
        }
    }
}
