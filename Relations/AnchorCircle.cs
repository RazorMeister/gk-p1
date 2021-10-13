using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projekt1.Shapes;

namespace Projekt1.Relations
{
    class AnchorCircle : Relation
    {
        public AnchorCircle(Circle circle) : base(circle, 0)
        {
            
        }

        public override bool CanMakeMove()
        {
            return !this.baseShape.IsSelectedWholeShape();
        }

        public override string ToString()
        {
            return "AnchorCircle";
        }
    }
}
