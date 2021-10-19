using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Projekt1.Relations;

namespace Projekt1.Shapes
{
    class Vertex : SimpleShape
    {
        private Point p;
        private Point savedP;
        public List<Edge> Edges { get; private set; } = new List<Edge>();

        public int X
        {
            get => this.p.X;
            set => this.p.X = value;
        }

        public int Y
        {
            get => this.p.Y;
            set => this.p.Y = value;
        }

        public Point GetPoint => this.p;

        public Vertex(Point p) => this.p = p;

        public void SetPoint(Point p) => this.p = p;

        public override void Move(int dX, int dY, Stack<Tuple<Relation, SimpleShape>> relationsStack, bool addRelationsToFix = true)
        {
            this.X += dX;
            this.Y += dY;

            if (addRelationsToFix)
                this.Edges.ForEach(edge => edge.AddRelationsToStack(relationsStack));
        }

        public override void SavePosition() => this.savedP = new Point(this.p.X, this.p.Y);

        public override void BackUpSavedPosition() => this.p = this.savedP;

        public void AddEdge(Edge edge) => this.Edges.Add(edge);

        public void RemoveEdge(Edge edge) => this.Edges.Remove(edge);

        public Edge GetOtherEdge(Edge edge) => this.Edges.First(_edge => _edge != edge);

        public override string ToString() => $"({this.X}, {this.Y})";
    }
}
