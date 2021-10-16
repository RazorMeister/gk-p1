using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projekt1.Relations;

namespace Projekt1.Shapes
{
    class Vertex : SimpleShape
    {
        private Point p;
        private Point savedP;

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

        public Vertex(Point p)
        {
            this.p = p;
        }

        public void SetPoint(Point p) => this.p = p;

        public override ShapeType GetShapeType() => ShapeType.Vertex;

        public override void Move(int dX, int dY)
        {
            this.X += dX;
            this.Y += dY;
        }

        public override void SavePosition() => this.savedP = new Point(this.p.X, this.p.Y);

        public override void BackUpSavedPosition() => this.p = this.savedP;

        public override string ToString() => $"({this.X}, {this.Y})";
    }
}
