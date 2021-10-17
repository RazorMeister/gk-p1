using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projekt1.Relations;

namespace Projekt1.Shapes
{
    class Edge : SimpleShape
    {
        private Vertex vertexA;
        private Vertex vertexB;

        public Vertex VertexA
        {
            get => this.vertexA;
            set
            {
                this.vertexA?.RemoveEdge(this);
                this.vertexA = value;
                this.vertexA?.AddEdge(this);
            }
        }

        public Vertex VertexB
        {
            get => this.vertexB;
            set
            {
                this.vertexB?.RemoveEdge(this);
                this.vertexB = value;
                this.vertexB?.AddEdge(this);
            }
        }

        public override ShapeType GetShapeType() => ShapeType.Edge;

        public override void Move(int dX, int dY, Stack<Tuple<Relation, SimpleShape>> relationsStack, bool addRelationsToFix = true)
        {
            this.VertexA.Move(dX, dY, relationsStack, false);
            this.VertexB.Move(dX, dY, relationsStack, false);

            if (addRelationsToFix)
            {
                this.AddRelationsToStack(relationsStack);
                this.vertexA.GetOtherEdge(this)?.AddRelationsToStack(relationsStack);
                this.vertexB.GetOtherEdge(this)?.AddRelationsToStack(relationsStack);
            }
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

        public Point GetMiddlePoint()
            => new Point((this.VertexA.X + this.VertexB.X) / 2, (this.VertexA.Y + this.VertexB.Y) / 2);

        public int GetLength() => (int) DrawHelper.PointsDistance(this.VertexA.GetPoint, this.VertexB.GetPoint);

        public override void SavePosition()
        {
            this.VertexA.SavePosition();
            this.VertexB.SavePosition();
        }

        public override void BackUpSavedPosition()
        {
            this.VertexA.BackUpSavedPosition();
            this.VertexB.BackUpSavedPosition();
        }

        public override string ToString() => $"({this.VertexA.ToString()}, {this.VertexB.ToString()})  - Relations: {this.relations.Count}";
    }
}
