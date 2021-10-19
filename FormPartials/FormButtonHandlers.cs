using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projekt1.Relations;
using Projekt1.Shapes;

namespace Projekt1.FormPartials
{
    partial class Form1
    {
        private void anchorCircleBtn_Click(object sender, EventArgs e)
        {
            Relation r = this.currShape.GetRelationByType(typeof(AnchorCircle));

            if (r == null)
            {
                r = new AnchorCircle((Circle)this.currShape);
                this.relations.Add(r);
            }
            else
            {
                r.Destroy();
                this.relations.Remove(r);
            }

            this.changeRelationButtonsActive();
            this.wrapper.Invalidate();
        }
    }
}
