using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Projekt1.Properties;
using Projekt1.Shapes;

namespace Projekt1.Relations
{
    class ParallelEdges : TwoShapesRelation
    {
        private Edge firstEdge;
        private Edge secondEdge;

        public override void FixRelation(SimpleShape movingShape, Stack<Tuple<Relation, SimpleShape>> relationsStack)
        {
            Tuple<double, double?> AB;
            Edge otherEdge;

            //Debug.WriteLine($"{movingShape?.GetShapeType()}");

            if (firstEdge == movingShape)
            {
                AB = this.firstEdge.GetLineEquation();
                otherEdge = this.secondEdge;
            }
            else
            {
                AB = this.secondEdge.GetLineEquation();
                otherEdge = this.firstEdge;
            }

            // Edges are parallel
            if (Math.Abs(AB.Item1 - otherEdge.GetLineEquation().Item1) <= 0.01)
                return;

            int newX;
            int newY;

            Vertex vertexToMove = otherEdge.VertexB.GetOtherEdge(otherEdge).GetRelationsNumberExcept(null) == 0
                ? otherEdge.VertexB : otherEdge.VertexA;
            Vertex otherVertex = otherEdge.VertexA == vertexToMove ? otherEdge.VertexB : otherEdge.VertexA;

            // Line has equation like: X = N
            if (AB.Item2 == null)
            {
                newY = Int32.MaxValue; // We just want to change X 
                newX =otherVertex.X;
            }
            else
            {
                var newB = otherVertex.Y - AB.Item1 * otherVertex.X;
                newY = (int)(AB.Item1 * vertexToMove.X + newB);
                newX = (int)((vertexToMove.Y - newB) / AB.Item1);
            }

            if (Math.Abs(newX - vertexToMove.X) < Math.Abs(newY - vertexToMove.Y))
                vertexToMove.SetPoint(new Point(newX, vertexToMove.Y));
            else
                vertexToMove.SetPoint(new Point(vertexToMove.X, newY));

            vertexToMove.GetOtherEdge(otherEdge)?.AddRelationsToStack(relationsStack);
        }

        public override void Draw(Bitmap bm, PaintEventArgs e)
        {
            if (!this.Completed) return;

            var icon = new Icon(Resources.ParallelEdgesRelation, 20, 20);

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
                this.firstEdge = (Edge)shape;
            else
            {
                this.secondEdge = (Edge)shape;

                if (
                    this.firstEdge.VertexA == this.secondEdge.VertexA
                    || this.firstEdge.VertexA == this.secondEdge.VertexB
                    || this.firstEdge.VertexB == this.secondEdge.VertexA
                    || this.firstEdge.VertexB == this.secondEdge.VertexB
                    || this.secondEdge.GetRelationsNumberExcept(null) != 0
                )
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
            if (shape.SelectedShape.GetShapeType() == SimpleShape.ShapeType.Edge)
            {
                return shape.SelectedShape.HasRelationByType(typeof(ParallelEdges))
                    ? BtnStatus.Active
                    : (
                        shape.SelectedShape.GetRelationsNumberExcept(typeof(ParallelEdges)) == 0
                            ? BtnStatus.Enabled
                            : BtnStatus.Disabled
                    );
            }

            return BtnStatus.Disabled;
        }
    }
}
