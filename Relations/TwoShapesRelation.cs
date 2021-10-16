using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projekt1.Shapes;

namespace Projekt1.Relations
{
    abstract class TwoShapesRelation : Relation
    {
        public bool Completed { get; protected set; }

        public abstract SimpleShape.ShapeType? GetLeftShapeType();

        public abstract void AddShape(SimpleShape shape);
    }
}
