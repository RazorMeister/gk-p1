using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projekt1.Shapes;

namespace Projekt1.Relations
{
    class AnchorCircle : Relation
    {
        private Point startPoint;
        private Circle circle;

        public AnchorCircle(Circle circle) : base(circle, 0)
        {
            var point = circle.center.GetPoint;
            this.startPoint = new Point(point.X, point.Y);
            this.circle = circle;
        }

        public override string ToString()
        {
            return "AnchorCircle";
        }

        public override bool CanMakeMove()
        {
            throw new NotImplementedException();
        }

        public override void FixRelation(AdvancedShape movingShape)
        {
            this.circle.center.SetPoint(this.startPoint);
        }
    }
}
