using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Projekt1.Interfaces;
using Projekt1.Shapes;

namespace Projekt1.Relations
{
    abstract class Relation : IDrawable
    {
        public static int RelationsCount { get; set; } = 0;

        public int Id { get; private set; }

        public bool Destroyed { get; private set; } = false;

        public enum BtnStatus
        {
            Disabled,
            Enabled,
            Active
        }

        protected Relation()
        {
            this.Id = ++Relation.RelationsCount;
        }

        public abstract void FixRelation(SimpleShape movingShape, Stack<Tuple<Relation, SimpleShape>> relationsStack);

        public abstract void Draw(Bitmap bm, PaintEventArgs e);

        protected void DrawIcon(PaintEventArgs e, Icon icon, int x, int y)
        {
            e.Graphics.DrawIcon(new Icon(icon, 20, 20), x, y);

            e.Graphics.DrawString(
                this.Id.ToString(),
                new Font("Consolas", 9, FontStyle.Bold),
                Brushes.Gray,
                x + 15,
                y
            );
        }

        protected void InitRelation()
        {
            var relationsStack = RelationManager.GetRelationsStack();
            this.FixRelation(null, relationsStack);
            RelationManager.RunRelations(relationsStack);
        }

        public virtual void Destroy() => this.Destroyed = true;
    }
}
