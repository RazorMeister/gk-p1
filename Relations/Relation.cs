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

        public string Uid { get; private set; }


        protected Relation()
        {
            this.Uid = System.Guid.NewGuid().ToString();
        }

        public abstract void FixRelation(SimpleShape movingShape, Stack<Tuple<Relation, SimpleShape>> relationsStack);

        public abstract void Draw(Bitmap bm, PaintEventArgs e);

        protected void InitRelation()
        {
            var relationsStack = RelationManager.GetRelationsStack();
            this.FixRelation(null, relationsStack);
            RelationManager.RunRelations(relationsStack);
        }

        public virtual void Destroy() => this.Destroyed = true;
    }
}
