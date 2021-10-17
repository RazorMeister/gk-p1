using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Projekt1.Properties;
using Projekt1.Shapes;

namespace Projekt1.Relations
{
    class FixedEdge : Relation
    {
        private int lineLength;
        private Edge edge;

        public FixedEdge(Edge edge, int lineLength)
        {
            this.edge = edge;
            this.edge.AddRelation(this);
            this.lineLength = lineLength;

            Stack<Tuple<Relation, SimpleShape>> relationsStack = new Stack<Tuple<Relation, SimpleShape>>();
            this.FixRelation(null, relationsStack);
        }

        public override void FixRelation(SimpleShape movingShape, Stack<Tuple<Relation, SimpleShape>> relationsStack)
        {
            if (this.edge.GetLength() == this.lineLength)
                return;

            Vertex vertexToMove = movingShape == this.edge.VertexA ? this.edge.VertexB : this.edge.VertexA;
            Vertex otherVertex = this.edge.VertexA == vertexToMove ? this.edge.VertexB : this.edge.VertexA;

            var AB = this.edge.GetLineEquation();

            // X = sth
            if (AB.Item2 == null)
            {
                int newY = otherVertex.Y + this.lineLength;

                if (Math.Abs(vertexToMove.Y - newY) < Math.Abs(vertexToMove.Y + newY))
                    newY = otherVertex.Y - this.lineLength;

                vertexToMove.SetPoint(new Point(otherVertex.X, newY));
                vertexToMove.Edges.ForEach(edge => edge.AddRelationsToStack(relationsStack));
                return;
            }

            var a = AB.Item1;

            int newX1 = (int)(otherVertex.X + (this.lineLength / Math.Sqrt(1 + a * a)));
            int newX2 = (int)(otherVertex.X - (this.lineLength / Math.Sqrt(1 + a * a)));

            // Determine in which direction we want to 'move'
            int newX = Math.Abs(vertexToMove.X - newX1) > Math.Abs(vertexToMove.X - newX2) ? newX2 : newX1;
            vertexToMove.SetPoint(new Point(newX, (int) (a * newX + AB.Item2)));
            vertexToMove.Edges.ForEach(edge => edge.AddRelationsToStack(relationsStack));
        }

        public override void Draw(Bitmap bm, PaintEventArgs e)
        {
            var middlePoint = this.edge.GetMiddlePoint();
            e.Graphics.DrawIcon(
                new Icon(Resources.FixedEdgeRelation, 20, 20), 
                middlePoint.X - 18, 
                middlePoint.Y - 18
            );
        }

        public static BtnStatus RelationBtnStatus(AdvancedShape shape)
        {
            return shape.SelectedShape.GetShapeType() == SimpleShape.ShapeType.Edge && shape.SelectedShape.GetRelationsNumberExcept(typeof(FixedEdge)) == 0
                ? (shape.HasRelationByType(typeof(FixedEdge)) ? BtnStatus.Active : BtnStatus.Enabled)
                : BtnStatus.Disabled;
        }
    }
}
