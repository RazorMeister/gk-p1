using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Projekt1.Properties;
using Projekt1.Shapes;

namespace Projekt1.Relations
{
    abstract class Relation
    {
        public bool Destroyed { get; private set; } = false;

        public enum BtnStatus
        {
            Disabled,
            Enabled,
            Active
        }

        public abstract void FixRelation(AdvancedShape movingShape);

        public abstract void Draw(Bitmap bm, PaintEventArgs e);

        public virtual void Destroy() => this.Destroyed = true;
    }
}
