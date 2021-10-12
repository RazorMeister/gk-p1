using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Projekt1.Properties;

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
        }

        private void wrapper_MouseClick(object sender, MouseEventArgs e)
        {
            switch (this.action)
            {
                case Action.None:
                    if (e.Button != MouseButtons.Left) break;

                    this.action = Action.Drawing;
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
                        this.currShape = null;
                        this.action = Action.None;
                        break;
                    }

                    if (this.currDrawingOption == DrawingOptions.Polygon)
                    {
                        if (((Polygon)this.currShape).AlmostCompleted)
                        {
                            this.currShape.FinishDrawing(this.currPoint);
                            this.currShape = null;
                            this.action = Action.None;
                        }
                        else
                            ((Polygon)this.currShape).AddLine(this.currPoint);
                    }
                    else if (this.currDrawingOption == DrawingOptions.Circle)
                    {
                        this.currShape.FinishDrawing(this.currPoint);
                        this.currShape = null;
                        this.action = Action.None;
                    }

                    break;

                case Action.Selecting:
                    if (e.Button != MouseButtons.Left)
                    {
                        this.currShape.DeselectObject();
                        this.currShape = null;
                        this.action = Action.None;
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
                    this.action = Action.Selecting;
                    this.wrapper.Cursor = Cursors.Default;
                    break;

            }
        }
        private void wrapper_MouseMove(object sender, MouseEventArgs e)
        {
            this.currPoint = e.Location;

            switch (this.action)
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

                    foreach (var shape in this.shapes)
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
            if (this.action == Action.Selecting)
            {
                if (e.Button == MouseButtons.Left && this.currShape.IsNearSelectedObjectIndex(e.Location))
                {
                    this.action = Action.Moving;
                    this.currShape.StartMoving(e.Location);
                    this.wrapper.Cursor = Cursors.Hand;
                }
                else
                {
                    this.action = Action.None;
                    this.currShape.DeselectObject();
                    this.currShape = null;
                }
            }

            if (this.action == Action.None)
            {
                foreach (var shape in this.shapes)
                {
                    int? index = shape.GetNearestPoint(e.Location);

                    if (index != null)
                    {
                        this.currShape = shape;
                        this.currShape.SelectObject((int)index);
                        this.action = Action.Moving;
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

            if (this.action == Action.Selecting && this.currShape != null)
                this.currShape.DeselectObject();

            this.currShape = null;
            this.action = Action.None;
        }

        private void circleBtn_Click(object sender, EventArgs e)
        {
            this.currDrawingOption = DrawingOptions.Circle;

            if (this.action == Action.Selecting && this.currShape != null)
                this.currShape.DeselectObject();

            this.currShape = null;
            this.action = Action.None;
        }

        private void ClearForm()
        {
            foreach (var shape in this.shapes)
                shape.Destroy();

            this.shapes.Clear();
            this.currShape = null;
            this.action = Action.None;
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
                this.currShape = null;
                this.action = Action.None;
                this.wrapper.Invalidate();
            }
        }
    }
}
