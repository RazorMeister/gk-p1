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
                movingShape is Edge
                && (vertexToMove == ((Edge)movingShape).VertexA || vertexToMove == ((Edge)movingShape).VertexB)
            )
            {
                var tmp = vertexToMove;
                vertexToMove = otherVertex;
                otherVertex = tmp;
            }

            // X = sth
            if (AB.Item2 == null || (AB.Item2 != null && Math.Abs(AB.Item1) > 20))
            {
                int newY1 = otherVertex.Y + this.lineLength;
                int newY2 = otherVertex.Y - this.lineLength;

                int newY = newY1;

                if (Math.Abs(vertexToMove.Y - newY1) > Math.Abs(vertexToMove.Y - newY2))
                    newY = newY2;

                vertexToMove.SetPoint(new Point(otherVertex.X, newY));
                vertexToMove.Edges
                    .Find(edge => edge != this.firstEdge && edge != this.secondEdge)
                    .AddRelationsToStack(relationsStack);
                return;
            }

            var a = AB.Item1;

            int newX1 = (int)(otherVertex.X + (this.lineLength / Math.Sqrt(1 + a * a)));
            int newX2 = (int)(otherVertex.X - (this.lineLength / Math.Sqrt(1 + a * a)));

            // Determine in which direction we want to 'move'
            int newX = Math.Abs(vertexToMove.X - newX1) > Math.Abs(vertexToMove.X - newX2) ? newX2 : newX1;
            vertexToMove.SetPoint(new Point(newX, (int)(a * newX + AB.Item2)));

            vertexToMove.Edges
                .Find(edge => edge != this.firstEdge && edge != this.secondEdge)
                .AddRelationsToStack(relationsStack);
        }

        public override void Draw(Bitmap bm, PaintEventArgs e)
        {
            if (!this.Completed) return;

            var firstMiddlePoint = this.firstEdge.GetMiddlePoint();
            this.DrawIcon(e, Resources.SameSizeEdgesRelation, firstMiddlePoint.X - 18, firstMiddlePoint.Y - 18);

            var secondMiddlePoint = this.secondEdge.GetMiddlePoint();
            this.DrawIcon(e, Resources.SameSizeEdgesRelation, secondMiddlePoint.X - 18, secondMiddlePoint.Y - 18);
        }

        public override void Destroy()
        {
            this.firstEdge?.RemoveRelation(this);
            this.secondEdge?.RemoveRelation(this);
            base.Destroy();
        }

        public override Type GetLeftShapeType()
        {
            if (this.Completed) return null;
            return typeof(Edge);
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
                this.InitRelation();
            }
        }

        public static BtnStatus RelationBtnStatus(AdvancedShape shape)
        {
            if (shape.SelectedShape is Edge)
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
