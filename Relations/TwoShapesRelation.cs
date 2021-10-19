using System;
using Projekt1.Shapes;

namespace Projekt1.Relations
{
    abstract class TwoShapesRelation : Relation
    {
        public bool Completed { get; protected set; }

        public abstract Type GetLeftShapeType();

        public abstract void AddShape(SimpleShape shape);
    }
}
