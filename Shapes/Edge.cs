using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt1.Shapes
{
    class Edge : SimpleShape
    {
        public Vertex VertexA { get; set; }
        public Vertex VertexB { get; set; }
        public override ShapeType GetShapeType() => ShapeType.Edge;

        public override void Move(int dX, int dY)
        {
            this.VertexA.Move(dX, dY);
            this.VertexB.Move(dX, dY);
        }

        public override string ToString() => $"({this.VertexA.ToString()}, {this.VertexB.ToString()})";
    }
}
