using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Projekt1.Shapes
{
    class Polygon: Shape
    {
        public enum ObjectType
        {
            Vertex,
            Line,
            Whole
        }

        public SortedDictionary<int, Tuple<int, int>> Lines { get; private set; }
        public SortedDictionary<int, Point> Vertices { get; private set; }

        public ObjectType SelectObjectType { get; protected set; }

        private Point StartPoint => this.Vertices.First().Value;

        public bool AlmostCompleted { get; private set; } = false;

        public Polygon(Point startPoint, Form form): base(form)
        {
            this.Lines = new SortedDictionary<int, Tuple<int, int>>();
            this.Vertices = new SortedDictionary<int, Point>
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

        public int GetLineLength(int lineIndex)
        {
            var line = this.Lines[lineIndex];
            return (int) DrawHelper.PointsDistance(this.Vertices[line.Item1], this.Vertices[line.Item2]);
        }

        public void SetLineLength(int lineIndex, int length)
        {
            var line = this.Lines[lineIndex];
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
                this.SelectedObjectIndex = 0;
                this.SelectObjectType = ObjectType.Whole;
            }
            else if (index > 0)
            {
                this.SelectedObjectIndex = index;
                this.SelectObjectType = ObjectType.Vertex;
            }
            else // Index < 0
            {
                this.SelectedObjectIndex = -index;
                this.SelectObjectType = ObjectType.Line;
            }
        }

        public override void Draw(Bitmap bm, PaintEventArgs e)
        {
            // If polygon if closed (completed), fill it with some color 
            if (this.Completed)
            {
                Brush fillBrush = this.SelectObjectType == ObjectType.Whole && this.SelectedObjectIndex == 0
                    ? Brushes.Red
                    : Brushes.AliceBlue;

                e.Graphics.FillPolygon(fillBrush, this.Vertices.Values.ToArray());
            }

            // Draw vertices
            foreach (var vertex in this.Vertices)
            {
                Brush brush = this.SelectObjectType == ObjectType.Vertex && this.SelectedObjectIndex == vertex.Key
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

            // Draw lines
            foreach (var line in this.Lines)
            {
                Color color = this.SelectObjectType == ObjectType.Line && this.SelectedObjectIndex == line.Key
                    ? Color.Red
                    : Color.Black;
                DrawHelper.DrawLine(bm, this.Vertices[line.Value.Item1], this.Vertices[line.Value.Item2], color);
            }

            // Draw circle around start point to easily finish polygon
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
            if (this.SelectedObjectIndex == null) return false;

            var nearestIndex = this.GetNearestPoint(p);

            if (this.SelectObjectType == ObjectType.Whole && nearestIndex == 0) return true;
            if (this.SelectObjectType == ObjectType.Line && nearestIndex == -this.SelectedObjectIndex) return true;
            if (this.SelectObjectType == ObjectType.Vertex && nearestIndex == this.SelectedObjectIndex) return true;

            return false;
        }

        public override int? GetNearestPoint(Point p)
        {
            // Vertex clicked
            foreach (var vertex in this.Vertices)
                if (DrawHelper.PointsDistance(p, vertex.Value) < DrawHelper.DISTANCE)
                    return vertex.Key;

            // Edge clicked
            foreach (var line in this.Lines)
                if (DrawHelper.EdgeDistance(p, this.Vertices[line.Value.Item1], this.Vertices[line.Value.Item2]) < DrawHelper.DISTANCE)
                    return -line.Key;

            // Detect if whole polygon is clicked
            var polygon = new GraphicsPath();
            polygon.AddPolygon(this.Vertices.Values.ToArray());

            if (polygon.IsVisible(p))
                return 0;
           
            return null;
        }

        public override void UpdateMoving(Point p)
        {
            if (!this.isMoving || this.SelectedObjectIndex == null) throw new Exception("Moving object index not set");

            switch (this.SelectObjectType)
            {
                case ObjectType.Vertex:
                    this.Vertices[(int)this.SelectedObjectIndex] = p;
                    break;
                case ObjectType.Line:
                    var line = this.Lines[(int)this.SelectedObjectIndex];

                    var lineStart = this.Vertices[line.Item1];
                    var lineEnd = this.Vertices[line.Item2];

                    int dX = p.X - this.lastMovingPoint.X;
                    int dY = p.Y - this.lastMovingPoint.Y;

                    lineStart.X += dX;
                    lineStart.Y += dY;

                    lineEnd.X += dX;
                    lineEnd.Y += dY;

                    this.Vertices[line.Item1] = lineStart;
                    this.Vertices[line.Item2] = lineEnd;

                    break;
                case ObjectType.Whole:
                    int dXw = p.X - this.lastMovingPoint.X;
                    int dYw = p.Y - this.lastMovingPoint.Y;

                    var newVertices = new SortedDictionary<int, Point>();

                    foreach (var vertex in this.Vertices)
                        newVertices.Add(vertex.Key, new Point(vertex.Value.X + dXw, vertex.Value.Y + dYw));

                    this.Vertices = newVertices;

                    break;
            }

            base.UpdateMoving(p);
        }

        public void AddVertexOnLine()
        {
            if (this.SelectObjectType != ObjectType.Line) return;

            var currentLine = this.Lines[(int)this.SelectedObjectIndex];

            var vertexA = this.Vertices[currentLine.Item1];
            var vertexB = this.Vertices[currentLine.Item2];

            // Sanitize vertices dictionary
            var newVertices = new SortedDictionary<int, Point>();
            int newVertexIndex = currentLine.Item1 + 1;

            foreach (var vertex in this.Vertices)
            {
                if (vertex.Key >= newVertexIndex) newVertices.Add(vertex.Key + 1, vertex.Value);
                else if (vertex.Key < newVertexIndex) newVertices.Add(vertex.Key, vertex.Value);
            }

            // Save new vertex
            newVertices.Add(newVertexIndex, new Point((vertexA.X + vertexB.X) / 2, (vertexA.Y + vertexB.Y) / 2));

            // Sanitize lines dictionary
            var newLines = new SortedDictionary<int, Tuple<int, int>>();

            foreach (var line in this.Lines)
            {
                if (line.Key < this.SelectedObjectIndex) newLines.Add(line.Key, line.Value);
                else if (line.Key > this.SelectedObjectIndex) newLines.Add(line.Key + 1, new Tuple<int, int>(line.Value.Item1 + 1, line.Value.Item2 == 1 ? 1 : line.Value.Item2 + 1));
            }

            // Add new lines (split existing one)
            newLines.Add((int)this.SelectedObjectIndex, new Tuple<int, int>(currentLine.Item1, newVertexIndex));
            newLines.Add((int)this.SelectedObjectIndex + 1, new Tuple<int, int>(newVertexIndex, currentLine.Item2 == 1 ? 1 : currentLine.Item2 + 1));

            this.Vertices = newVertices;
            this.Lines = newLines;

            // Select newly created vertex
            this.SelectObjectType = ObjectType.Vertex;
            this.SelectedObjectIndex = newVertexIndex;
        }

        public void RemoveCurrentVertex()
        {
            if (this.SelectObjectType != ObjectType.Vertex || this.Lines.Count <= 3) return;

            // Sanitize vertices dictionary
            var newVertices = new SortedDictionary<int, Point>();

            foreach (var vertex in this.Vertices)
            {
                if (vertex.Key > this.SelectedObjectIndex) newVertices.Add(vertex.Key - 1, vertex.Value);
                else if (vertex.Key < this.SelectedObjectIndex) newVertices.Add(vertex.Key, vertex.Value);
            }

            // Save new vertices
            this.Vertices = newVertices;

            // Delete last line (it doesnt matter which one)
            this.Lines.Remove(this.Lines.Count);
            this.Lines[this.Lines.Count] = new Tuple<int, int>(this.Lines.Last().Value.Item1, 1);

            // Deselect vertex
            this.DeselectObject();
        }

        public override string ToString()
        {
            string text =  $"Polygon - {this.Lines.Count} lines | {this.Vertices.Count} vertices";

            if (this.SelectedObjectIndex != null)
            {
                switch (this.SelectObjectType)
                {
                    case ObjectType.Vertex:
                        text +=
                            $" | Selected vertex ({this.SelectedObjectIndex}): ({this.Vertices[(int)this.SelectedObjectIndex].X}, {this.Vertices[(int)this.SelectedObjectIndex].Y})";
                        break;
                    case ObjectType.Line:
                        var line = this.Lines[(int)this.SelectedObjectIndex];
                        text +=
                            $" | Selected edge ({this.SelectedObjectIndex}): ({this.Vertices[line.Item1].X}, {this.Vertices[line.Item1].Y}), ({this.Vertices[line.Item2].X}, {this.Vertices[line.Item2].Y})";
                        break;
                    case ObjectType.Whole:
                        text +=
                            $" | Selected polygon";
                        break;
                }
            }
            
            return text + this.GetRelationsString();
        }
    }
}
