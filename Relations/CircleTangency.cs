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
                double a = Math.Abs(AB.Item1); // Ignore direction of edge

                double b = 1;

                int tmp = (int)(distance / Math.Sqrt(Math.Abs(a * a + b * b)));

                // Check if moving in the right direction
                double tmpDistance = this.circle.R  - this.edge.GetDistanceFromPoint(
                    new Point(this.circle.center.X + (int)(tmp * a), this.circle.center.Y + (int)(tmp * b))
                );

                if (Math.Abs(tmpDistance) > Math.Abs(distance)) tmp = -tmp;

                Debug.WriteLine($"tmp - {tmp} | distance - {distance} | tmpDistance - {tmpDistance}");

                // Move circle center
                this.circle.center.Move((int)(tmp * a), (int)(tmp * b));

                // Move edge
                //this.edge.Move(-(int)(tmp * a), -tmp);
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
