using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Projekt1.Properties;
using Projekt1.Shapes;

namespace Projekt1.Relations
{
    class CircleTangency : TwoShapesRelation
    {
        private Edge edge;
        private Circle circle;

        public override void AddShape(SimpleShape shape)
        {
            if (shape is Circle)
                this.circle = (Circle)shape;
            else
                this.edge = (Edge)shape;

            shape.AddRelation(this);

            if (this.edge != null && this.circle != null)
            {
                this.Completed = true;
                this.InitRelation();
            }
        }

        public override void FixRelation(SimpleShape movingShape, Stack<Tuple<Relation, SimpleShape>> relationsStack)
        {
            if (!this.Completed) return;

            int startDistance = (int) this.edge.GetDistanceFromPoint(this.circle.center.GetPoint);
            if (startDistance == this.circle.R)
                return;

            // 1 - move circle center
            // 2 - move edge
            // 3 - change r
            int moveType = 0;


            // 1 - moving edge
            // 2 - moving circle
            int currentlyMovingType = (movingShape == this.circle.SelectedShape) 
                ? (movingShape is CircleEdge ? 1 : 2)
                : 0;

            //Debug.WriteLine($"currentlyMoving: {currentlyMovingType} | hasAnchor: {this.circle.HasRelationByType(typeof(AnchorCircle))} | hasFixed: {this.circle.HasRelationByType(typeof(FixedRadius))}");

            if (this.circle.HasRelationByType(typeof(AnchorCircle)))
            {
                if (
                    this.circle.HasRelationByType(typeof(FixedRadius))
                    || currentlyMovingType == 1
                ) 
                    moveType = 2;
                else
                    moveType = 3;
            } else if (
                this.circle.HasRelationByType(typeof(FixedRadius))
                || currentlyMovingType == 1
            )
            {
                if (currentlyMovingType == 2)
                    moveType = 2;
                else
                    moveType = 1;
            }
            else
            {
                moveType = 3;
            }


            //Debug.WriteLine($"m9oveType: {moveType}");

            if (moveType == 3)
            {
                double distance = this.edge.GetDistanceFromPoint(this.circle.center.GetPoint);
                this.circle.SetR((int)distance);
            }
            else
            {
                // Problem with it

                var AB = this.edge.GetLineEquation();

                var newA = -1 / AB.Item1;

                double distance = this.circle.R - this.edge.GetDistanceFromPoint(this.circle.center.GetPoint);
                double a = Math.Abs(AB.Item1); // Ignore direction of edge

                double b = -1;

                int tmp = (int)(distance / Math.Sqrt(Math.Abs(a * a + b * b)));

                // Check if moving in the right direction
                double tmpDistance = this.circle.R - this.edge.GetDistanceFromPoint(
                    new Point(this.circle.center.X + (int)(tmp * a), this.circle.center.Y + (int)(tmp * b))
                );

                if (Math.Abs(tmpDistance) > Math.Abs(distance)) tmp = -tmp;

                /*Debug.WriteLine($"tmp - {tmp} | distance - {distance} | tmpDistance - {tmpDistance}");
                Debug.WriteLine($"v - [{(int)(tmp * a)}, {(int)(tmp * b)}]");*/

                if (moveType == 2)
                {
                    // Move edge
                    this.edge.Move(-(int)(tmp * a), -(int)(tmp * b), relationsStack);
                }
                else
                {
                    // Move circle center
                    this.circle.center.Move((int)(tmp * a), (int)(tmp * b), relationsStack);
                }
            }
        }

        public override void Draw(Bitmap bm, PaintEventArgs e)
        {
            if (!this.Completed) return;

            var middlePoint = this.edge.GetMiddlePoint();

            var d = (int)((this.circle.R + 18) / 1.41);

            this.DrawIcon(e, Resources.CircleTangencyRelation, this.circle.center.X - d, this.circle.center.Y - d);
            this.DrawIcon(e, Resources.CircleTangencyRelation, middlePoint.X - 18, middlePoint.Y - 18);
        }

        public override void Destroy()
        {
            this.circle?.RemoveRelation(this);
            this.edge?.RemoveRelation(this);
            base.Destroy();
        }

        public override Type GetLeftShapeType()
        {
            return this.Completed
                ? null
                : (this.circle != null ? typeof(Edge) : typeof(Circle));
        }

        public static BtnStatus RelationBtnStatus(AdvancedShape shape)
        {
            if (shape.GetType() == typeof(Circle))
                return shape.HasRelationByType(typeof(CircleTangency)) ? BtnStatus.Active : BtnStatus.Enabled;

            if (shape.SelectedShape is Edge)
            {
                return shape.SelectedShape.HasRelationByType(typeof(CircleTangency))
                    ? BtnStatus.Active
                    : (
                        shape.SelectedShape.GetRelationsNumberExcept(typeof(CircleTangency)) == 0
                        ? BtnStatus.Enabled
                        : BtnStatus.Disabled
                    );
            }

            return BtnStatus.Disabled;
        }
    }
}
