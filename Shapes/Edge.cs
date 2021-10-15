using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
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

        // Return a and b from line equation
        public Tuple<double, double?> GetLineEquation()
        {
            // Line has equation like: X = N
            if (this.VertexB.X == this.VertexA.X)
                return new Tuple<double, double?>(this.VertexB.X, null);

            double a = (this.VertexA.Y - this.VertexB.Y) / (double)(this.VertexA.X - this.VertexB.X);
            double b = this.VertexA.Y - a * this.VertexA.X;

            return new Tuple<double, double?>(a, b);
        }

        public double GetDistanceFromPoint(Point p)
        {
            return Math.Abs(
                (this.VertexB.X - this.VertexA.X) * (this.VertexA.Y - p.Y) - (this.VertexA.X - p.X) * (this.VertexB.Y - this.VertexA.Y)
                ) / DrawHelper.PointsDistance(this.VertexA.GetPoint, this.VertexB.GetPoint);
        }

        public override string ToString() => $"({this.VertexA.ToString()}, {this.VertexB.ToString()})";
    }
}
