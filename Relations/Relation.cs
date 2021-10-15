using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projekt1.Shapes;

namespace Projekt1.Relations
{
    abstract class Relation
    {
        protected int objectIndex;
        protected SimpleShape baseShape;

        public Relation(SimpleShape baseShape, int objectIndex)
        {
            this.objectIndex = objectIndex;
            this.baseShape = baseShape;
        }

        public abstract bool CanMakeMove();

        public abstract void FixRelation(AdvancedShape movingShape);

        /*public abstract void HandleAction();*/
    }
}
