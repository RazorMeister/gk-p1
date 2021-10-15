using System;
using System.Diagnostics;
using System.Drawing;
using Projekt1.Shapes;

namespace Projekt1.Relations
{
    class CircleTangency : Relation
    {
        private Edge edge;
        private Circle circle;

        public CircleTangency(Circle circle, Edge edge) : base(edge, 0)
        {
            this.edge = edge;
            this.circle = circle;

            this.FixRelation(null);
        }

        public override bool CanMakeMove()
        {
            throw new NotImplementedException();
        }

        public override void FixRelation(AdvancedShape movingShape)
        {
            if (
                movingShape?.GetShapeType() == SimpleShape.ShapeType.Circle 
                && movingShape.SelectedShape.GetShapeType() == SimpleShape.ShapeType.CircleEdge
                )
            {
                var AB = this.edge.GetLineEquation();

                var newA = -1 / AB.Item1;

                double distance = this.circle.R - this.edge.GetDistanceFromPoint(this.circle.center.GetPoint);
                double a = AB.Item1;

                double tmp = distance / Math.Sqrt(Math.Abs(a * a + 1));

                this.circle.center.Move((int)(tmp * a), (int)(tmp));

                //this.edge.Move(movingShape.DX, movingShape.DY);

                /*var AB = this.edge.GetLineEquation();

                double a = AB.Item1;
                double distance = this.edge.GetDistanceFromPoint(this.circle.center.GetPoint) - this.circle.R;

                double tmp = distance / Math.Sqrt(Math.Abs(a * a + 1));

                Debug.WriteLine($" distance - {distance} | tmp - {tmp} | DX = {tmp * a} | DY = {tmp}");

                this.edge.Move((int)(tmp * a), (int)(tmp));*/
            }
            else
            {
                double distance = this.edge.GetDistanceFromPoint(this.circle.center.GetPoint);
                this.circle.SetR((int)distance);
            }
        }

        public override string ToString()
        {
            return "AnchorCircle";
        }
    }
}
