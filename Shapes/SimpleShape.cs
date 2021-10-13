using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projekt1.Relations;

namespace Projekt1.Shapes
{
    abstract class SimpleShape
    {
        public enum ShapeType
        {
            Circle = 1,
            Polygon = 2,
            Edge = 3,
            Vertex = 4,
            CircleEdge = 5,
            CircleCenter = 6
        }

        public Relation Relation { get; private set; }

        public string Uid { get; private set; }

        protected SimpleShape()
        {
            this.Uid = System.Guid.NewGuid().ToString();
        }

        public abstract ShapeType GetShapeType();

        public virtual Type[] GetPossibleRelationTypes() => null;

        public abstract void Move(int dX, int dY);

        public abstract override string ToString();

        public void AddRelation(Relation relation)
        {

        }

        public void RemoveRelation(Relation relation)
        {

        }
    }
}
