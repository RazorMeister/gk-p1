using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projekt1
{
    class Polygon: Shape
    {
        private enum ObjectType
        {
            Vertex,
            Line,
            Whole
        }

        public Dictionary<int, Tuple<int, int>> Lines { get; private set; }
        public Dictionary<int, Point> Vertices { get; private set; }

        private ObjectType selectObjectType;

        private Point StartPoint => this.Vertices.First().Value;

        public bool AlmostCompleted { get; private set; } = false;

        public Polygon(Point startPoint, Form form): base(form)
        {
            this.Lines = new Dictionary<int, Tuple<int, int>>();
            this.Vertices = new Dictionary<int, Point>
            {
                { 1, startPoint }
            };
        }

        public void AddLine(Point p)
        {
            int newPointIndex = this.Vertices.Last().Key + 1;
            this.Vertices.Add(newPointIndex, p);

            int firstPointIndex = this.Lines.Count == 0 ? 1 : this.Lines.Last().Value.Item2;
            int lineIndex = this.Lines.Count == 0 ? 1 : this.Lines.Last().Key + 1;

            this.Lines.Add(lineIndex, new Tuple<int, int>(firstPointIndex, newPointIndex));
        }

        public override void UpdateLastPoint(Point p)
        {
            this.Vertices[this.Vertices.Count] = p;
            this.AlmostCompleted = this.Lines.Count > 2 && DrawHelper.PointsDistance(this.StartPoint, p) < 16;
        }

        public override void FinishDrawing(Point p)
        {
            if (this.AlmostCompleted)
            {
                this.Completed = true;
                this.AlmostCompleted = false;

                // Delete last vertex because is supposed to be StartPoint.
                this.Vertices.Remove(this.Vertices.Count);

                // Set last line second vertex as StartPoint
                var lastLine = this.Lines.Last();
                this.Lines[this.Lines.Count] = new Tuple<int, int>(lastLine.Value.Item1, 1);
            }
        }

        public override void SelectObject(int index)
        {
            if (index == 0)
            {
                this.selectedObjectIndex = 0;
                this.selectObjectType = ObjectType.Whole;
            }
            else if (index > 0)
            {
                this.selectedObjectIndex = index;
                this.selectObjectType = ObjectType.Vertex;
            }
            else // Index < 0
            {
                this.selectedObjectIndex = -index;
                this.selectObjectType = ObjectType.Line;
            }
        }

        public override void Draw(Bitmap bm, PaintEventArgs e)
        {
            if (this.Completed)
                e.Graphics.FillPolygon(Brushes.AliceBlue, this.Vertices.Values.ToArray());

            foreach (var vertex in this.Vertices)
            {
                Brush brush = this.selectObjectType == ObjectType.Vertex && this.selectedObjectIndex == vertex.Key
                    ? Brushes.Red
                    : Brushes.Black;

                int radius = 5;

                e.Graphics.FillEllipse(brush, new Rectangle(
                    vertex.Value.X - radius,
                    vertex.Value.Y - radius,
                    radius + radius,
                    radius + radius
                ));
            }

            foreach (var line in this.Lines)
            {
                Color color = this.selectObjectType == ObjectType.Line && this.selectedObjectIndex == line.Key
                    ? Color.Red
                    : Color.Black;
                DrawHelper.DrawLine(bm, this.Vertices[line.Value.Item1], this.Vertices[line.Value.Item2], color);
            }

            if (this.Completed)
            {
                Rectangle rect = new Rectangle(this.StartPoint.X - 3, this.StartPoint.Y - 3, 16, 16);

                Color color = this.selectObjectType == ObjectType.Whole && this.selectedObjectIndex == 0
                    ? Color.Red
                    : Color.Black;

                var pen = new Pen(color, 1);
                e.Graphics.DrawRectangle(pen, rect);
                pen.Dispose();
            }

            if (this.AlmostCompleted)
            {
                Pen pen = new Pen(Color.Red, 1);

                e.Graphics.DrawEllipse(
                    pen,
                    this.StartPoint.X - DrawHelper.DISTANCE,
                    this.StartPoint.Y - DrawHelper.DISTANCE,
                    DrawHelper.DISTANCE + DrawHelper.DISTANCE,
                    DrawHelper.DISTANCE + DrawHelper.DISTANCE
                );
            }
        }

        public override bool IsNearSelectedObjectIndex(Point p)
        {
            if (this.selectedObjectIndex == null) return false;
            if (this.selectObjectType == ObjectType.Whole) return this.GetNearestPoint(p, Keys.Shift) != null;
            return this.GetNearestPoint(p, Keys.Control) != null;
        }

        public override int? GetNearestPoint(Point p, Keys key) // Function is returning <distance, objectId>
        {
            if (key == Keys.Control)
            {
                foreach (var vertex in this.Vertices)
                    if (DrawHelper.PointsDistance(p, vertex.Value) < DrawHelper.DISTANCE)
                        return vertex.Key;

                foreach (var line in this.Lines)
                {
                    var centerPoint = DrawHelper.CenterPoint(this.Vertices[line.Value.Item1], this.Vertices[line.Value.Item2]);

                    if (DrawHelper.PointsDistance(p, centerPoint) < DrawHelper.DISTANCE)
                        return -line.Key;

                }
            }
            else if (key == Keys.Shift)
            {
                if (DrawHelper.PointsDistance(this.StartPoint, p) < DrawHelper.DISTANCE)
                    return 0;
            }

            return null;
        }

        public override void UpdateMoving(Point p)
        {
            if (!this.isMoving || this.selectedObjectIndex == null) throw new Exception("Moving object index not set");

            switch (this.selectObjectType)
            {
                case ObjectType.Vertex:
                    this.Vertices[(int)this.selectedObjectIndex] = p;
                    break;
                case ObjectType.Line:
                    var line = this.Lines[(int)this.selectedObjectIndex];

                    var lineStart = this.Vertices[line.Item1];
                    var lineEnd = this.Vertices[line.Item2];

                    var centerPoint = DrawHelper.CenterPoint(lineStart, lineEnd);
                    int dX = p.X - centerPoint.X;
                    int dY = p.Y - centerPoint.Y;

                    lineStart.X += dX;
                    lineStart.Y += dY;

                    lineEnd.X += dX;
                    lineEnd.Y += dY;

                    this.Vertices[line.Item1] = lineStart;
                    this.Vertices[line.Item2] = lineEnd;

                    break;
                case ObjectType.Whole:
                    int dXw = p.X - this.StartPoint.X;
                    int dYw = p.Y - this.StartPoint.Y;

                    Dictionary<int, Point> newVertices = new Dictionary<int, Point>();

                    foreach (var vertex in this.Vertices)
                        newVertices.Add(vertex.Key, new Point(vertex.Value.X + dXw, vertex.Value.Y + dYw));

                    this.Vertices = newVertices;

                    break;
            }
        }

        public override string ToString()
        {
            string text =  $"Polygon - {this.Lines.Count} lines | {this.Vertices.Count} vertices";

            if (this.selectedObjectIndex != null)
            {
                if (this.selectObjectType == ObjectType.Vertex)
                    text +=
                        $" | Selected vertex: ({this.Vertices[(int) this.selectedObjectIndex].X}, {this.Vertices[(int) this.selectedObjectIndex].X})";
            }

            return text;
        }
    }
}
