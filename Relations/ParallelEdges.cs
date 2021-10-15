using System;
using System.Drawing;
using Projekt1.Shapes;

namespace Projekt1.Relations
{
    class ParallelEdges : Relation
    {
        private Edge firstEdge;
        private Edge secondEdge;

        public ParallelEdges(Edge edge, Edge secondEdge) : base(edge, 0)
        {
            this.firstEdge = edge;
            this.secondEdge = secondEdge;

            this.FixRelation(null);
        }

        public override bool CanMakeMove()
        {
            throw new NotImplementedException();
        }

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

        public override string ToString()
        {
            return "AnchorCircle";
        }
    }
}
