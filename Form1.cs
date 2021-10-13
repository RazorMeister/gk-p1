using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Projekt1.Relations;
using Projekt1.Shapes;

namespace Projekt1
{
    public partial class Form1 : Form
    {
        private enum DrawingOptions
        {
            Polygon,
            Circle
        }

        private enum Action
        {
            None,
            Drawing,
            Moving,
            Selecting
        }

        private Action action = Action.None;

        private Action currAction
        {
            get => this.action;

            set
            {
                if (this.action == Action.Selecting && value != Action.Moving)
                {
                    this.currShape.DeselectObject();
                    this.currShape = null;
                }

                if (value == Action.None)
                {
                    this.currShape = null;
                }

                this.action = value;
                this.label1.Text = value.ToString();

                switch (this.action)
                {
                    case Action.None:
                        this.changeButtonsEnabled(drawPolygon: true, drawCircle: true);
                        break;
                    case Action.Drawing:
                        this.changeButtonsEnabled();
                        break;
                    case Action.Selecting:
                        this.changeButtonsEnabled(
                            drawPolygon: true, 
                            drawCircle: true,
                            addVertex: this.currShape.GetType() == typeof(Polygon)
                                       && ((Polygon)this.currShape).SelectObjectType == Polygon.ObjectType.Line,
                            removeVertex: this.currShape.GetType() == typeof(Polygon)
                                          && ((Polygon)this.currShape).SelectObjectType == Polygon.ObjectType.Vertex,
                            removeShape: true,
                            anchorCircle: this.currShape.GetType() == typeof(Circle),
                            fixedRadius: this.currShape.GetType() == typeof(Circle),
                            fixedEdge: this.currShape.GetType() == typeof(Polygon)
                                       && ((Polygon)this.currShape).SelectObjectType == Polygon.ObjectType.Line
                        );

                        break;
                    case Action.Moving:
                        this.changeButtonsEnabled();
                        break;
                }
            }
        }

        private Point currPoint;

        private DrawingOptions currDrawingOption = DrawingOptions.Polygon;

        private List<Shape> shapes = new List<Shape>();
        private Shape currShape = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.currAction = Action.None;
        }

        private void wrapper_MouseClick(object sender, MouseEventArgs e)
        {
            switch (this.currAction)
            {
                case Action.None:
                    if (e.Button != MouseButtons.Left) break;

                    this.currAction = Action.Drawing;
                    this.currPoint = e.Location;

                    if (this.currDrawingOption == DrawingOptions.Polygon)
                    {
                        Polygon newPolygon = new Polygon(this.currPoint, this);
                        newPolygon.AddLine(this.currPoint);

                        this.currShape = newPolygon;
                        this.shapes.Add(this.currShape);
                    }
                    else if (this.currDrawingOption == DrawingOptions.Circle)
                    {
                        this.currShape = new Circle(this.currPoint, this);
                        this.shapes.Add(this.currShape);
                    }

                    break;

                case Action.Drawing:
                    if (e.Button != MouseButtons.Left)
                    {
                        this.currShape.Destroy();
                        this.shapes.Remove(this.currShape);
                        this.currAction = Action.None;
                        break;
                    }

                    if (this.currDrawingOption == DrawingOptions.Polygon)
                    {
                        if (((Polygon)this.currShape).AlmostCompleted)
                        {
                            this.currShape.FinishDrawing(this.currPoint);
                            this.currAction = Action.None;
                        }
                        else
                            ((Polygon)this.currShape).AddLine(this.currPoint);
                    }
                    else if (this.currDrawingOption == DrawingOptions.Circle)
                    {
                        this.currShape.FinishDrawing(this.currPoint);
                        this.action = Action.None;
                    }

                    break;

                case Action.Selecting:
                    if (e.Button != MouseButtons.Left)
                    {
                        this.currAction = Action.None;
                    }

                    break;
            }

            this.wrapper.Invalidate();
        }

        private void wrapper_MouseUp(object sender, MouseEventArgs e)
        {
            switch (this.action)
            {
                case Action.Moving:
                    this.currShape.FinishMoving();
                    this.currAction = Action.Selecting;
                    this.wrapper.Cursor = Cursors.Default;
                    break;

            }
        }
        private void wrapper_MouseMove(object sender, MouseEventArgs e)
        {
            this.currPoint = e.Location;

            switch (this.currAction)
            {
                case Action.Moving:
                    this.currShape.UpdateMoving(this.currPoint);
                    break;
                case Action.Drawing:
                    this.currShape.UpdateLastPoint(this.currPoint);
                    break;
                case Action.None:
                case Action.Selecting:
                    bool indexFound = false;

                    foreach (var shape in this.shapes.AsEnumerable().Reverse())
                    {
                        int? index = shape.GetNearestPoint(e.Location);

                        if (index != null)
                        {
                            indexFound = true;
                            this.wrapper.Cursor = Cursors.Hand;
                            break;
                        }
                    }

                    if (!indexFound)
                        this.wrapper.Cursor = Cursors.Default;

                    break;
            }

            this.wrapper.Invalidate();
        }

        private void wrapper_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.currAction == Action.Selecting)
            {
                if (e.Button == MouseButtons.Left && this.currShape.IsNearSelectedObjectIndex(e.Location))
                {
                    this.currAction = Action.Moving;
                    this.currShape.StartMoving(e.Location);
                    this.wrapper.Cursor = Cursors.Hand;
                }
                else
                {
                    this.currAction = Action.None;
                }
            }

            if (this.currAction == Action.None)
            {
                foreach (var shape in this.shapes.AsEnumerable().Reverse())
                {
                    int? index = shape.GetNearestPoint(e.Location);

                    if (index != null)
                    {
                        this.currShape = shape;
                        this.currShape.SelectObject((int)index);
                        this.currAction = Action.Moving;
                        this.currShape.StartMoving(e.Location);
                        this.wrapper.Cursor = Cursors.Hand;
                        break;
                    }
                }
            }
        }

        private void resetBtn_Click(object sender, EventArgs e)
        {
            this.ClearForm();
        }

        private void wrapper_Paint(object sender, PaintEventArgs e)
        {
            var bm = new Bitmap(this.wrapper.Width, this.wrapper.Height);

            this.shapes.ForEach(shape => shape.Draw(bm, e));

            this.debugLabel.Text = this.currShape != null ? this.currShape.ToString() : "";

            e.Graphics.DrawImage(bm, 0, 0);
        }

        private void polygonBtn_Click(object sender, EventArgs e)
        {
            this.currDrawingOption = DrawingOptions.Polygon;
            this.currAction = Action.None;
            this.wrapper.Invalidate();
        }

        private void circleBtn_Click(object sender, EventArgs e)
        {
            this.currDrawingOption = DrawingOptions.Circle;
            this.currAction = Action.None;
            this.wrapper.Invalidate();
        }

        private void ClearForm()
        {
            foreach (var shape in this.shapes)
                shape.Destroy();

            this.shapes.Clear();
            this.currAction = Action.None;
            this.wrapper.Invalidate();
        }

        private void addVertexBtn_Click(object sender, EventArgs e)
        {
            if (this.currShape != null && this.currShape is Polygon)
            {
                ((Polygon)this.currShape).AddVertexOnLine();
                this.wrapper.Invalidate();
            }
        }

        private void removeVertexBtn_Click(object sender, EventArgs e)
        {
            if (this.currShape != null && this.currShape is Polygon)
            {
                ((Polygon)this.currShape).RemoveCurrentVertex();
                this.wrapper.Invalidate();
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

        private void changeButtonsEnabled(
            bool drawPolygon = false,
            bool drawCircle = false,
            bool addVertex = false,
            bool removeVertex = false,
            bool removeShape = false,
            bool anchorCircle = false,
            bool fixedRadius = false,
            bool fixedEdge = false
        )
        {
            this.polygonBtn.Enabled = drawPolygon;
            this.circleBtn.Enabled = drawCircle;
            this.addVertexBtn.Enabled = addVertex;
            this.removeVertexBtn.Enabled = removeVertex;
            this.removeShapeBtn.Enabled = removeShape;
            this.anchorCircleBtn.Enabled = anchorCircle;
            this.fixedRadiusBtn.Enabled = fixedRadius;
            this.fixedEdgeBtn.Enabled = fixedEdge;
        }

        private void anchorCircleBtn_Click(object sender, EventArgs e)
        {
            if (!this.currShape.HasRelation(typeof(AnchorCircle)))
                this.currShape.AddRelation(new AnchorCircle((Circle) this.currShape));
            else
                this.currShape.RemoveRelation(typeof(AnchorCircle));
        }

        private void fixedRadiusBtn_Click(object sender, EventArgs e)
        {
            if (!this.currShape.HasRelation(typeof(FixedRadius)))
            {
                using (Prompt prompt = new Prompt("text", "caption", ((Circle)this.currShape).R.ToString()))
                {
                    string result = prompt.Result;

                    if (result != "")
                        this.currShape.AddRelation(new FixedRadius((Circle)this.currShape, Int32.Parse(result)));
                }
            }
            else
                this.currShape.RemoveRelation(typeof(FixedRadius));

            this.wrapper.Invalidate();
        }

        private void fixedEdgeBtn_Click(object sender, EventArgs e)
        {
            if (!this.currShape.HasRelation(typeof(FixedEdge)))
            {
                using (Prompt prompt = new Prompt("text", "caption", ((Polygon)this.currShape).GetLineLength((int)this.currShape.SelectedObjectIndex).ToString()))
                {
                    string result = prompt.Result;

                    if (result != "")
                        this.currShape.AddRelation(
                            new FixedEdge((Polygon)this.currShape, (int)this.currShape.SelectedObjectIndex, Int32.Parse(result))
                        );
                }
            }
            else
                this.currShape.RemoveRelation(typeof(FixedEdge));

            this.wrapper.Invalidate();
        }
    }
}
