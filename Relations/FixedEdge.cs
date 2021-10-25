using System;
using System.Collections.Generic;
using System.Drawing;
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

            this.InitRelation();
        }

        public override void FixRelation(SimpleShape movingShape, Stack<Tuple<Relation, SimpleShape>> relationsStack)
        {
            if (this.edge.GetLength() == this.lineLength)
                return;

            Vertex vertexToMove = (movingShape is Edge && ((Edge)movingShape).FromVertex == this.edge.VertexA) ? this.edge.VertexB : this.edge.VertexA;
            Vertex otherVertex = this.edge.VertexA == vertexToMove ? this.edge.VertexB : this.edge.VertexA;

            var AB = this.edge.GetLineEquation();

            // X = sth
            if (AB.Item2 == null || (AB.Item2 != null && Math.Abs(AB.Item1) > 20))
            {
                int newY1 = otherVertex.Y + this.lineLength;
                int newY2 = otherVertex.Y - this.lineLength;

                int newY = newY1;

                if (Math.Abs(vertexToMove.Y - newY1) > Math.Abs(vertexToMove.Y - newY2))
                    newY = newY2;

                vertexToMove.SetPoint(new Point(otherVertex.X, newY));
                vertexToMove.GetOtherEdge(this.edge).AddRelationsToStack(relationsStack);
                return;
            }

            var a = AB.Item1;

            int newX1 = (int)(otherVertex.X + (this.lineLength / Math.Sqrt(1 + a * a)));
            int newX2 = (int)(otherVertex.X - (this.lineLength / Math.Sqrt(1 + a * a)));

            // Determine in which direction we want to 'move'
            int newX = Math.Abs(vertexToMove.X - newX1) > Math.Abs(vertexToMove.X - newX2) ? newX2 : newX1;
            vertexToMove.SetPoint(new Point(newX, (int) (a * newX + AB.Item2)));
            vertexToMove.GetOtherEdge(this.edge).AddRelationsToStack(relationsStack);
        }

        public override void Draw(Bitmap bm, PaintEventArgs e, DrawHelper.DrawType drawType)
        {
            var middlePoint = this.edge.GetMiddlePoint();
            this.DrawIcon(
                e,
                Resources.FixedEdgeRelation, 
                middlePoint.X - 18, 
                middlePoint.Y - 18
            );
        }

        public static BtnStatus RelationBtnStatus(AdvancedShape shape)
        {
            return shape.SelectedShape is Edge && shape.SelectedShape.GetRelationsNumberExcept(typeof(FixedEdge)) == 0
                ? (shape.HasRelationByType(typeof(FixedEdge)) ? BtnStatus.Active : BtnStatus.Enabled)
                : BtnStatus.Disabled;
        }

        public override void Destroy()
        {
            this.edge.RemoveRelation(this);
            base.Destroy();
        }
    }
}
