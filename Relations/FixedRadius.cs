﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Projekt1.Properties;
using Projekt1.Shapes;

namespace Projekt1.Relations
{
    class FixedRadius : Relation
    {
        private int r;
        private Circle circle;

        public FixedRadius(Circle circle, int r)
        {
            this.circle = circle;
            this.r = r;

            this.circle.AddRelation(this);

            this.InitRelation();
        }

        public override void FixRelation(SimpleShape movingShape, Stack<Tuple<Relation, SimpleShape>> relationsStack)
        {
            if (movingShape == this.circle.SelectedShape && movingShape is CircleEdge)
                throw new CannotMoveException();

            this.circle.SetR(this.r);
        }

        public override void Draw(Bitmap bm, PaintEventArgs e, DrawHelper.DrawType drawType)
        {
            this.DrawIcon(
                e,
                Resources.FixedRadiusRelation,
                this.circle.center.X - 25,
                this.circle.center.Y - 25
            );
        }

        public override void Destroy()
        {
            this.circle.RemoveRelation(this);
            base.Destroy();
        }

        public static BtnStatus RelationBtnStatus(AdvancedShape shape)
        {
            if (shape.GetType() == typeof(Circle))
                return shape.HasRelationByType(typeof(FixedRadius)) ? BtnStatus.Active : BtnStatus.Enabled;

            return BtnStatus.Disabled;
        }
    }
}
