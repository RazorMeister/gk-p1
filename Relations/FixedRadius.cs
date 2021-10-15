using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projekt1.Shapes;

namespace Projekt1.Relations
{
    class FixedRadius : Relation
    {
        private int r;
        private Circle circle;

        public FixedRadius(Circle circle, int r) : base(circle, 0)
        {
            this.circle = circle;
            this.r = r;

            this.FixRelation(null);
        }

        public override bool CanMakeMove()
        {
            return true; //this.baseShape.IsSelectedWholeShape();
        }

        public override void FixRelation(AdvancedShape movingShape)
        {
            this.circle.SetR(this.r);
        }

        public override string ToString()
        {
            return $"FixedRadius (R = {this.r})";
        }
    }
}
