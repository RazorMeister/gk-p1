using System;
using System.ComponentModel;
using System.Diagnostics;
using Projekt1.Relations;
using Projekt1.Shapes;

namespace Projekt1
{
    partial class Form1
    {
        /* Draw button */
        private void polygonBtn_Click(object sender, EventArgs e)
        {
            this.currDrawingOption = DrawingOptions.Polygon;
            this.currAction = Action.None;
        }

        private void circleBtn_Click(object sender, EventArgs e)
        {
            this.currDrawingOption = DrawingOptions.Circle;
            this.currAction = Action.None;
        }


        /* Edit buttons */
        private void addVertexBtn_Click(object sender, EventArgs e)
        {
            if (this.currShape != null && this.currShape is Polygon)
            {
                ((Polygon)this.currShape).AddVertexOnEdge();
                this.wrapper.Invalidate();
            }
        }

        private void removeVertexBtn_Click(object sender, EventArgs e)
        {
            if (this.currShape != null && this.currShape is Polygon)
            {
                ((Polygon)this.currShape).RemoveCurrentVertex();
                this.wrapper.Invalidate();
                this.action = Action.None;
            }
        }

        private void removeShapeBtn_Click(object sender, EventArgs e)
        {
            if (this.currShape != null)
            {
                this.currShape.Destroy();
                this.shapes.Remove(this.currShape);
                this.currAction = Action.None;
                this.wrapper.Invalidate();
            }
        }


        /* Clear form buttom */
        private void ClearForm()
        {
            foreach (var shape in this.shapes)
                shape.Destroy();

            this.shapes.Clear();
            this.currAction = Action.None;
            this.wrapper.Invalidate();
        }


        /* Relation buttons */
        private void anchorCircleBtn_Click(object sender, EventArgs e)
        {
            Relation r = this.currShape.GetRelationByType(typeof(AnchorCircle));

            if (r == null)
            {
                r = new AnchorCircle((Circle)this.currShape);
                this.relations.Add(r);
            }
            else
            {
                r.Destroy();
                this.relations.Remove(r);
            }

            this.changeRelationButtonsActive();
            this.wrapper.Invalidate();
        }

        private void fixedRadiusBtn_Click(object sender, EventArgs e)
        {
            Relation r = this.currShape.GetRelationByType(typeof(FixedRadius));

            if (r == null)
            {
                using (Prompt prompt = new Prompt(
                    "Please provide radius length",
                    "Provide radius length",
                    ((Circle)this.currShape).R.ToString()
                ))
                {
                    string result = prompt.Result;

                    if (result != "")
                    {
                        r = new FixedRadius((Circle)this.currShape, Int32.Parse(result));
                        this.relations.Add(r);
                    }
                }
            }
            else
            {
                r.Destroy();
                this.relations.Remove(r);
            }

            this.changeRelationButtonsActive();
            this.wrapper.Invalidate();
        }

        private void fixedEdgeBtn_Click(object sender, EventArgs e)
        {
            Relation r = this.currShape.SelectedShape.GetRelationByType(typeof(FixedEdge));

            if (r == null)
            {
                Edge edge = (Edge)this.currShape.SelectedShape;

                int edgeLength = (int)DrawHelper.PointsDistance(
                    edge.VertexA.GetPoint,
                    edge.VertexB.GetPoint
                );

                using (Prompt prompt = new Prompt(
                    "Please provide edge length",
                    "Provide edge length",
                    edgeLength.ToString()
                ))
                {
                    string result = prompt.Result;

                    if (result != "")
                    {
                        r = new FixedEdge(edge, Int32.Parse(result));
                        this.relations.Add(r);
                    }
                }
            }
            else
            {
                r.Destroy();
                this.relations.Remove(r);
            }

            this.changeRelationButtonsActive();
            this.wrapper.Invalidate();
        }

        private void circleTangencyBtn_Click(object sender, EventArgs e)
        {
            Relation r = this.currShape.GetRelationByType(typeof(CircleTangency))
                         ?? this.currShape.SelectedShape.GetRelationByType(typeof(CircleTangency));

            if (r == null)
            {
                r = new CircleTangency();
                this.relations.Add(r);

                if (this.currShape is Circle)
                    ((CircleTangency)r).AddShape(this.currShape);
                else
                    ((CircleTangency)r).AddShape(this.currShape.SelectedShape);

                this.almostCompletedRelation = (TwoShapesRelation)r;
                
                this.almostCompletedLabel.Text = $"Select {((TwoShapesRelation)r).GetLeftShapeType().Name}";
            }
            else
            {
                r.Destroy();
                this.relations.Remove(r);
            }

            this.changeRelationButtonsActive();
            this.wrapper.Invalidate();
        }

        private void parallelEdgesBtn_Click(object sender, EventArgs e)
        {
            Relation r = this.currShape.SelectedShape.GetRelationByType(typeof(ParallelEdges));

            if (r == null)
            {
                r = new ParallelEdges();
                ((TwoShapesRelation)r).AddShape(this.currShape.SelectedShape);
                this.relations.Add(r);

                this.almostCompletedRelation = (TwoShapesRelation)r;
                this.almostCompletedLabel.Text = $"Select {((TwoShapesRelation)r).GetLeftShapeType().Name}";
            }
            else
            {
                r.Destroy();
                this.relations.Remove(r);
            }

            this.changeRelationButtonsActive();
            this.wrapper.Invalidate();
        }

        private void sameSizeEdgesBtn_Click(object sender, EventArgs e)
        {
            Relation r = this.currShape.SelectedShape.GetRelationByType(typeof(SameSizeEdges));

            if (r == null)
            {
                r = new SameSizeEdges();
                ((TwoShapesRelation)r).AddShape(this.currShape.SelectedShape);
                this.relations.Add(r);

                this.almostCompletedRelation = (TwoShapesRelation)r;
                this.almostCompletedLabel.Text = $"Select {((TwoShapesRelation)r).GetLeftShapeType().Name}";
            }
            else
            {
                r.Destroy();
                this.relations.Remove(r);
            }

            this.changeRelationButtonsActive();
            this.wrapper.Invalidate();
        }
    }
}
