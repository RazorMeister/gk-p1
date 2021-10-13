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

        public FixedRadius(Circle circle, int r) : base(circle, 0)
        {
            //circle.SetR(r);
            this.r = r;
        }

        public override bool CanMakeMove()
        {
            return true; //this.baseShape.IsSelectedWholeShape();
        }

        public override string ToString()
        {
            return $"FixedRadius (R = {this.r})";
        }
    }
}
