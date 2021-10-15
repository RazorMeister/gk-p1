using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace Projekt1.Shapes
{
    class Polygon : AdvancedShape
    {
        public override ShapeType GetShapeType() => ShapeType.Polygon;

        public SortedDictionary<int, Edge> Edges { get; private set; } = new SortedDictionary<int, Edge>();
        public SortedDictionary<int, Vertex> Vertices { get; private set; } = new SortedDictionary<int, Vertex>();

        public bool AlmostCompleted { get; private set; } = false;

        private Vertex StartVertex => this.Vertices[1];

        public Polygon(Point startPoint)
        {
            this.Vertices.Add(1, new Vertex(startPoint));
        }

        public override void UpdateLastPoint(Point p)
        {
            this.Vertices.Last().Value.SetPoint(p);
            this.AlmostCompleted = this.Edges.Count > 2 && DrawHelper.PointsDistance(this.StartVertex.GetPoint, p) < 16;
        }

        public void AddLine(Point p)
        {
            int newPointIndex = this.Vertices.Last().Key + 1;
            var newVertex = new Vertex(p);
            this.Vertices.Add(newPointIndex, newVertex);

            var firstVertex = this.Edges.Count == 0 ? this.StartVertex : this.Edges.Last().Value.VertexB;
            int edgeIndex = this.Edges.Count == 0 ? 1 : this.Edges.Last().Key + 1;

            this.Edges.Add(edgeIndex, new Edge(){ VertexA = firstVertex, VertexB = newVertex});
        }

        public override void FinishDrawing()
        {
            if (this.AlmostCompleted)
            {
                this.Completed = true;
                this.AlmostCompleted = false;

                // Delete last vertex because is supposed to be StartPoint.
                this.Vertices.Remove(this.Vertices.Count);

                // Set last edge second vertex as StartPoint
                var lastLine = this.Edges.Last();
                this.Edges[this.Edges.Count] = new Edge(){ VertexA = lastLine.Value.VertexA, VertexB = this.StartVertex};
            }
        }

        public override void Draw(Bitmap bm, PaintEventArgs e)
        {
            // If polygon if closed (completed), fill it with some color 
            if (this.Completed)
            {
                List<Point> points = new List<Point>();

                foreach (var vertex in this.Vertices)
                    points.Add(vertex.Value.GetPoint);

                try
                {
                    e.Graphics.FillPolygon(
                        new SolidBrush(
                            DrawHelper.GetFillColor(this.SelectedShape?.GetShapeType() == ShapeType.Polygon)),
                        points.ToArray()
                    );
                }
                catch (Exception error)
                {
                }
            }

            // Draw vertices
            foreach (var vertex in this.Vertices.Values)
            {
                int radius = 5;

                try
                {
                    e.Graphics.FillEllipse(
                        new SolidBrush(DrawHelper.GetNormalColor(this.SelectedShape?.Uid == vertex.Uid)),
                        new Rectangle(
                            vertex.X - radius,
                            vertex.Y - radius,
                            radius + radius,
                            radius + radius
                        )
                    );
                }
                catch (Exception exception)
                {
                }
            }

            // Draw lines
            foreach (var edge in this.Edges.Values)
            {
                DrawHelper.DrawLine(
                    bm, 
                    edge.VertexA.GetPoint, 
                    edge.VertexB.GetPoint, 
                    DrawHelper.GetNormalColor(this.SelectedShape?.Uid == edge.Uid)
                );
            }

            // Draw circle around start point to easily finish polygon
            if (this.AlmostCompleted)
            {
                Pen pen = new Pen(DrawHelper.GetNormalColor(true), 1);

                e.Graphics.DrawEllipse(
                    pen,
                    this.StartVertex.X - DrawHelper.DISTANCE,
                    this.StartVertex.Y - DrawHelper.DISTANCE,
                    DrawHelper.DISTANCE + DrawHelper.DISTANCE,
                    DrawHelper.DISTANCE + DrawHelper.DISTANCE
                );

                pen.Dispose();
            }
        }

        public override SimpleShape GetNearestShape(Point p)
        {
            // Vertex clicked
            foreach (var vertex in this.Vertices.Values)
                if (DrawHelper.PointsDistance(p, vertex.GetPoint) < DrawHelper.DISTANCE)
                    return vertex;

            // Edge clicked
            foreach (var edge in this.Edges.Values)
                if (DrawHelper.EdgeDistance(p, edge.VertexA.GetPoint, edge.VertexB.GetPoint) < DrawHelper.DISTANCE)
                    return edge;

            // Detect if whole polygon is clicked
            var polygon = new GraphicsPath();
            List<Point> points = new List<Point>();

            foreach (var vertex in this.Vertices)
                points.Add(vertex.Value.GetPoint);

            polygon.AddPolygon(points.ToArray());

            if (polygon.IsVisible(p))
                return this;

            return null;
        }

        public override void Move(int dX, int dY)
        {
            foreach (var vertex in this.Vertices.Values)
                vertex.Move(dX, dY);
        }


        public void AddVertexOnEdge()
        {
            if (this.SelectedShape.GetShapeType() != ShapeType.Edge) return;

            var currentEdgeIndex = this.Edges.First((edge) => edge.Value.Uid == this.SelectedShape.Uid).Key;

            /*var currentLine = this.Lines[(int)this.SelectedObjectIndex];*/

            var currentEdge = (Edge) this.SelectedShape;
            var vertexA = currentEdge.VertexA;
            var vertexB = currentEdge.VertexB;

            // Sanitize vertices dictionary
            var newVertices = new SortedDictionary<int, Vertex>();
            int newVertexIndex = this.Vertices.First((vertex) => vertex.Value.Uid == currentEdge.VertexA.Uid).Key + 1;

            foreach (var vertex in this.Vertices)
            {
                if (vertex.Key >= newVertexIndex) newVertices.Add(vertex.Key + 1, vertex.Value);
                else if (vertex.Key < newVertexIndex) newVertices.Add(vertex.Key, vertex.Value);
            }

            // Save new vertex
            var newVertex = new Vertex(new Point((vertexA.X + vertexB.X) / 2, (vertexA.Y + vertexB.Y) / 2));
            newVertices.Add(newVertexIndex, newVertex);

            // Sanitize edges dictionary
            var newEdges = new SortedDictionary<int, Edge>();

            foreach (var edge in this.Edges)
            {
                if (edge.Key < currentEdgeIndex) newEdges.Add(edge.Key, edge.Value);
                else if (edge.Key > currentEdgeIndex) newEdges.Add(edge.Key + 1, edge.Value);
            }

            // Add new lines (split existing one)
            newEdges.Add(currentEdgeIndex, new Edge(){ VertexA = vertexA, VertexB = newVertex});
            newEdges.Add(currentEdgeIndex + 1, new Edge() { VertexA = newVertex, VertexB = currentEdge.VertexB });

            this.Vertices = newVertices;
            this.Edges = newEdges;

            // Select newly created vertex
            this.SelectedShape = newVertex;
        }

        public void RemoveCurrentVertex()
        {
            if (this.SelectedShape.GetShapeType() != ShapeType.Vertex || this.Vertices.Count <= 3) return;

            int selectedShapeIndex = this.Vertices.First((vertex) => vertex.Value.Uid == this.SelectedShape.Uid).Key;
            this.Vertices.Remove(selectedShapeIndex);

            var edgeWithSelectedVertexAsFirst =
                this.Edges.First((edge) => edge.Value.VertexA.Uid == this.SelectedShape.Uid);

            int edgeBeforeKey = edgeWithSelectedVertexAsFirst.Key > 1 ? edgeWithSelectedVertexAsFirst.Key - 1 : this.Edges.Count;

            // Remove edge which first vertex is the one we want to delete
            this.Edges.Remove(edgeWithSelectedVertexAsFirst.Key);

            // Change edge before
            this.Edges[edgeBeforeKey].VertexB = edgeWithSelectedVertexAsFirst.Value.VertexB;

            // Sanitize keys
            this.SanitizeVertexKeys();
            this.SanitizeEdgeKeys();

            // Deselect vertex
            this.DeselectShape();
        }

        private void SanitizeVertexKeys()
        {
            var newVertices = new SortedDictionary<int, Vertex>();

            int i = 1;

            foreach (var vertex in this.Vertices.Values)
                newVertices.Add(i++, vertex);

            this.Vertices = newVertices;
        }

        private void SanitizeEdgeKeys()
        {
            var newEdges = new SortedDictionary<int, Edge>();

            int i = 1;

            foreach (var edge in this.Edges.Values)
                newEdges.Add(i++, edge);

            this.Edges = newEdges;
        }

        public override string ToString()
        {
            string text = $"Polygon - {this.Edges.Count} edges | {this.Vertices.Count} vertices";

            if (this.SelectedShape != null)
            {
                switch (this.SelectedShape.GetShapeType())
                {
                    case ShapeType.Vertex:
                        text += $" | Selected vertex {this.SelectedShape.ToString()}";
                        break;
                    case ShapeType.Edge:
                        text += $" | Selected edge {this.SelectedShape.ToString()}";
                        break;
                    case ShapeType.Polygon:
                        text += " | Selected whole polygon";
                        break;
                }
            }

            //return text + this.GetRelationsString();
            return text;
        }
    }
}
