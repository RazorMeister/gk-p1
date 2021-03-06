using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Projekt1.Properties;
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

        private Dictionary<Type, Button> relationButtons = new Dictionary<Type, Button>();
        private TwoShapesRelation almostCompletedRelation = null;
        private DrawHelper.DrawType currDrawType = DrawHelper.DrawType.NORMAL;

        private Action currAction
        {
            get => this.action;

            set
            {
                if (this.action == Action.Selecting && value != Action.Moving)
                {
                    this.currShape.DeselectShape();
                    this.currShape = null;
                }

                if (value == Action.None)
                    this.currShape = null;

                this.action = value;
                this.label1.Text = value.ToString();

                switch (this.action)
                {
                    case Action.None:
                        this.changeButtonsEnabled(drawPolygon: true, drawCircle: true);
                        this.changeRelationButtonsActive();
                        break;
                    case Action.Drawing:
                        this.changeButtonsEnabled();
                        this.changeRelationButtonsActive();
                        break;
                    case Action.Selecting:
                        this.changeButtonsEnabled(
                            drawPolygon: true, 
                            drawCircle: true,
                            addVertex: this.currShape.GetType() == typeof(Polygon)
                                       && this.currShape.SelectedShape is Edge,
                            removeVertex: this.currShape.GetType() == typeof(Polygon)
                                          && this.currShape.SelectedShape is Vertex,
                            removeShape: true
                        );
                        this.changeRelationButtonsActive();

                        break;
                    case Action.Moving:
                        this.changeButtonsEnabled();
                        break;
                }
            }
        }

        private Point currPoint;

        private DrawingOptions currDrawingOption = DrawingOptions.Polygon;

        private List<AdvancedShape> shapes = new List<AdvancedShape>();
        private AdvancedShape currShape;

        private List<Relation> relations = new List<Relation>();

        public Form1()
        {
            InitializeComponent();
        }

        private void initRelations()
        {
            this.relationButtons.Add(typeof(AnchorCircle), this.anchorCircleBtn);
            this.relationButtons.Add(typeof(FixedRadius), this.fixedRadiusBtn);
            this.relationButtons.Add(typeof(FixedEdge), this.fixedEdgeBtn);
            this.relationButtons.Add(typeof(CircleTangency), this.circleTangencyBtn);
            this.relationButtons.Add(typeof(ParallelEdges), this.parallelEdgesBtn);
            this.relationButtons.Add(typeof(SameSizeEdges), this.sameSizeEdgesBtn);

            foreach (var relationBtn in this.relationButtons)
            {
                Icon icon = (Icon)Resources.ResourceManager.GetObject(relationBtn.Key.Name + "Relation") ?? Resources.AnchorCircleRelation;

                if (icon == null)
                    continue;

                relationBtn.Value.Image = new Icon(icon, 20, 20).ToBitmap();
                relationBtn.Value.ImageAlign = ContentAlignment.MiddleLeft;
                relationBtn.Value.TextAlign = ContentAlignment.MiddleRight;
            }
        }

        private void createDefaultShapes()
        {
            var newShape = new Polygon(new Point(300, 200));
            newShape.AddLine(new Point(500, 270));
            newShape.AddLine(new Point(450, 400));
            newShape.AddLine(new Point(300, 250));
            newShape.AddLine(new Point(100, 300));
            newShape.AddLine(new Point(200, 150));
            newShape.UpdateLastPoint(new Point(300, 200));
            newShape.FinishDrawing();
            this.shapes.Add(newShape);

            var newShape2 = new Polygon(new Point(50, 50));
            newShape2.AddLine(new Point(150, 60));
            newShape2.AddLine(new Point(160, 160));
            newShape2.AddLine(new Point(50, 150));
            newShape2.AddLine(new Point(50, 50));
            newShape2.UpdateLastPoint(new Point(50, 50));
            newShape2.FinishDrawing();
            this.shapes.Add(newShape2);

            var newCircle = new Circle(new Point(400, 150));
            newCircle.SetR(100);
            newCircle.FinishDrawing();
            this.shapes.Add(newCircle);

            var newCircle2 = new Circle(new Point(170, 460));
            newCircle2.SetR(100);
            newCircle2.FinishDrawing();
            this.shapes.Add(newCircle2);

            var parallelEdges = new ParallelEdges();
            parallelEdges.AddShape(newShape.Edges[3]);
            parallelEdges.AddShape(newShape.Edges[5]);
            this.relations.Add(parallelEdges);

            var equalEdges = new SameSizeEdges();
            equalEdges.AddShape(newShape2.Edges[1]);
            equalEdges.AddShape(newShape2.Edges[3]);
            this.relations.Add(equalEdges);

            this.relations.Add(new FixedEdge(newShape.Edges[2], 170));

            this.relations.Add(new FixedRadius(newCircle, 40));
            this.relations.Add(new AnchorCircle(newCircle2));

            var circleTangency = new CircleTangency();
            circleTangency.AddShape(newCircle);
            circleTangency.AddShape(newShape.Edges[1]);
            this.relations.Add(circleTangency);

            var circleTangency2 = new CircleTangency();
            circleTangency2.AddShape(newCircle2);
            circleTangency2.AddShape(newShape.Edges[4]);
            this.relations.Add(circleTangency2);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.initRelations();
            this.createDefaultShapes();

            // Set default behavior to buttons
            this.changeButtonsEnabled(drawPolygon: true, drawCircle: true);
            this.changeRelationButtonsActive();
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
                        Polygon newPolygon = new Polygon(this.currPoint);
                        newPolygon.AddLine(this.currPoint);

                        this.currShape = newPolygon;
                        this.shapes.Add(this.currShape);
                    }
                    else if (this.currDrawingOption == DrawingOptions.Circle)
                    {
                        this.currShape = new Circle(this.currPoint);
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
                            this.currShape.FinishDrawing();
                            this.currAction = Action.None;
                        }
                        else
                            ((Polygon)this.currShape).AddLine(this.currPoint);
                    }
                    else if (this.currDrawingOption == DrawingOptions.Circle)
                    {
                        this.currShape.FinishDrawing();
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
                    this.shapes.ForEach(shape => shape.SavePosition());

                    try
                    {
                        this.currShape.UpdateMoving(this.currPoint);
                    }
                    catch (CannotMoveException exception)
                    {
                        this.shapes.ForEach(shape => shape.BackUpSavedPosition());
                    }
                    
                    break;
                case Action.Drawing:
                    this.currShape.UpdateLastPoint(this.currPoint);
                    break;
                case Action.None:
                case Action.Selecting:
                    bool shapeFound = false;

                    foreach (var shape in this.shapes.AsEnumerable().Reverse())
                    {
                        var nearestShape = shape.GetNearestShape(e.Location);

                        if (nearestShape != null)
                        {
                            shapeFound = true;
                            this.wrapper.Cursor = Cursors.Hand;
                            break;
                        }
                    }

                    if (!shapeFound)
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
                Tuple<Tuple<SimpleShape, AdvancedShape>, double> minNearestShape = null;

                foreach (var shape in this.shapes.AsEnumerable().Reverse())
                {
                    var nearestShape = shape.GetNearestShape(e.Location);

                    if (
                        nearestShape != null
                        && (minNearestShape == null || nearestShape.Item2 < minNearestShape.Item2)
                    )
                    {
                        minNearestShape = new Tuple<Tuple<SimpleShape, AdvancedShape>, double>(
                            new Tuple<SimpleShape, AdvancedShape>(nearestShape.Item1, shape), nearestShape.Item2
                        );
                    }
                }


                if (minNearestShape != null)
                {
                    this.currShape = minNearestShape.Item1.Item2;
                    this.currShape.SelectShape(minNearestShape.Item1.Item1);
                    this.currAction = Action.Moving;
                    this.currShape.StartMoving(e.Location);
                    this.wrapper.Cursor = Cursors.Hand;

                    if (this.almostCompletedRelation != null)
                    {
                        Type leftShapeType = this.almostCompletedRelation.GetLeftShapeType();

                        if (leftShapeType != null)
                        {
                            if (this.currShape.GetType() == leftShapeType)
                                this.almostCompletedRelation.AddShape(this.currShape);
                            else if (this.currShape.SelectedShape.GetType() == leftShapeType)
                                this.almostCompletedRelation.AddShape(this.currShape.SelectedShape);
                        }

                        if (!this.almostCompletedRelation.Completed)
                        {
                            this.almostCompletedRelation.Destroy();
                            this.relations.Remove(this.almostCompletedRelation);

                            this.action = Action.None;
                            MessageBox.Show(
                                "Selected object cannot be added to specified relation!",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error
                            );
                        }

                        this.almostCompletedRelation = null;
                        this.almostCompletedLabel.Text = "";
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

            this.shapes.ForEach(shape => shape.Draw(bm, e, this.currDrawType));

            this.relations.RemoveAll(relation => relation.Destroyed);
            this.relations.ForEach(relation => relation.Draw(bm, e, this.currDrawType));

            this.debugLabel.Text = this.currShape != null ? this.currShape.ToString() : "";

            e.Graphics.DrawImage(bm, 0, 0);
        }

        private void changeButtonsEnabled(
            bool drawPolygon = false,
            bool drawCircle = false,
            bool addVertex = false,
            bool removeVertex = false,
            bool removeShape = false
        )
        {
            this.polygonBtn.Enabled = drawPolygon;
            this.circleBtn.Enabled = drawCircle;
            this.addVertexBtn.Enabled = addVertex;
            this.removeVertexBtn.Enabled = removeVertex;
            this.removeShapeBtn.Enabled = removeShape;
        }

        private void changeRelationButtonsActive()
        {
            List<Type> relationTypes = this.currShape != null ? this.currShape.GetAllRelationTypes() : new List<Type>();

            if (this.currShape?.SelectedShape != null)
                relationTypes.AddRange(this.currShape.SelectedShape.GetAllRelationTypes());

            foreach (var relationBtn in this.relationButtons)
            {
                MethodInfo method = relationBtn.Key.GetMethod("RelationBtnStatus");

                if (method == null)
                    continue;

                Relation.BtnStatus btnStatus = this.currShape == null
                    ? Relation.BtnStatus.Disabled
                    : (Relation.BtnStatus)method.Invoke(null, new object[] { this.currShape });

                relationBtn.Value.FlatAppearance.BorderColor =
                    btnStatus == Relation.BtnStatus.Active ? Color.Green : Color.Black;

                relationBtn.Value.Enabled = btnStatus != Relation.BtnStatus.Disabled;
            }
        }

        private void antyaliasingCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            this.currDrawType = this.antyaliasingCheckbox.Checked
                ? DrawHelper.DrawType.ANTYALIASING
                : DrawHelper.DrawType.NORMAL;

            this.wrapper.Invalidate();
        }
    }
}
