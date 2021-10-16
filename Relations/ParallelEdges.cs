using System;
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

        public override void FixRelation(AdvancedShape movingShape)
        {
            Tuple<double, double?> AB;
            Edge otherEdge;

            if (firstEdge.VertexA == movingShape?.SelectedShape || firstEdge.VertexB == movingShape?.SelectedShape)
            {
                AB = this.firstEdge.GetLineEquation();
                otherEdge = this.secondEdge;
            }
            else
            {
                AB = this.secondEdge.GetLineEquation();
                otherEdge = this.firstEdge;
            }

            int newX;
            int newY;

            // Line has equation like: X = N
            if (AB.Item2 == null)
            {
                newY = Int32.MaxValue; // We just want to change X 
                newX = otherEdge.VertexA.X;
            }
            else
            {
                var newB = otherEdge.VertexA.Y - AB.Item1 * otherEdge.VertexA.X;
                newY = (int)(AB.Item1 * otherEdge.VertexB.X + newB);
                newX = (int)((otherEdge.VertexB.Y - newB) / AB.Item1);
            }

            if (Math.Abs(newX - otherEdge.VertexB.X) < Math.Abs(newY - otherEdge.VertexB.Y))
                otherEdge.VertexB.SetPoint(new Point(newX, otherEdge.VertexB.Y));
            else
                otherEdge.VertexB.SetPoint(new Point(otherEdge.VertexB.X, newY));
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
                this.FixRelation(null);
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
