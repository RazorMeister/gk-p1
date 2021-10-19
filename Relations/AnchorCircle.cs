using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Projekt1.Properties;
using Projekt1.Shapes;

namespace Projekt1.Relations
{
    class AnchorCircle : Relation
    {
        private Point startPoint;
        private Circle circle;

        public AnchorCircle(Circle circle)
        {
            var point = circle.center.GetPoint;
            this.startPoint = new Point(point.X, point.Y);
            this.circle = circle;

            this.circle.AddRelation(this);
        }

        public override void FixRelation(SimpleShape movingShape, Stack<Tuple<Relation, SimpleShape>> relationsStack)
        {
            if (movingShape == this.circle || movingShape == this.circle.center)
                throw new CannotMoveException();

            this.circle.center.SetPoint(this.startPoint);
        }

        public override void Draw(Bitmap bm, PaintEventArgs e)
        {
            this.DrawIcon(
                e,
                Resources.AnchorCircleRelation, 
                this.circle.center.X - 35, 
                this.circle.center.Y - 8
            );
        }

        public static BtnStatus RelationBtnStatus(AdvancedShape shape)
        {
            return shape is Circle 
                ? (shape.HasRelationByType(typeof(AnchorCircle)) ? BtnStatus.Active : BtnStatus.Enabled)
                : BtnStatus.Disabled;
        }

        public override void Destroy()
        {
            this.circle.RemoveRelation(this);
            base.Destroy();
        }
    }
}
