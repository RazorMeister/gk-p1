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
            Keys? pressedKey = null;

            if ((ModifierKeys & Keys.Control) == Keys.Control) pressedKey = Keys.Control;
            else if ((ModifierKeys & Keys.Shift) == Keys.Shift) pressedKey = Keys.Shift;

            if (pressedKey != null && this.action != Action.Drawing)
            {
                if (this.action == Action.Selecting)
                    this.currShape.DeselectObject();

                this.action = Action.Selecting;

                foreach (var shape in this.shapes)
                {
                    int? index = shape.GetNearestPoint(e.Location, (Keys)pressedKey);

                    if (index != null)
                    {
                        this.currShape = shape;
                        this.currShape.SelectObject((int)index);
                        break;
                    }
                }

                if (this.currShape == null)
                    this.action = Action.None;
            }
            else
            {
                switch (this.action)
                {
                    case Action.None:
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
                        }
                        else
                        {
                            if (this.currDrawingOption == DrawingOptions.Polygon)
                            {
                                if (((Polygon)this.currShape).Completed)
                                {
                                    this.currShape.FinishDrawing(this.currPoint);
                                    this.currShape = null;
                                    this.action = Action.None;
                                }
                                else
                                {
                                    ((Polygon)this.currShape).AddLine(this.currPoint);
                                }
                            }
                            else if (this.currDrawingOption == DrawingOptions.Circle)
                            {
                                this.currShape.FinishDrawing(this.currPoint);
                                this.currShape = null;
                                this.action = Action.None;
                            }
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
            if (this.action == Action.None) return;

            this.currPoint = e.Location;

            switch (this.action)
            {
                case Action.Moving:
                    this.currShape.UpdateMoving(this.currPoint);
                    break;
                case Action.Drawing:
                    this.currShape.UpdateLastPoint(this.currPoint);
                    break;
            }

            this.wrapper.Invalidate();
        }

        private void wrapper_MouseDown(object sender, MouseEventArgs e)
        {
            Keys? pressedKey = null;

            if ((ModifierKeys & Keys.Control) == Keys.Control) pressedKey = Keys.Control;
            else if ((ModifierKeys & Keys.Shift) == Keys.Shift) pressedKey = Keys.Shift;

            if (
                pressedKey == null 
                && e.Button == MouseButtons.Left
                && this.action == Action.Selecting 
                && this.currShape.IsNearSelectedObjectIndex(e.Location)
            )
            {
                this.action = Action.Moving;
                this.currShape.StartMoving();
                this.wrapper.Cursor = Cursors.Hand;
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

            this.label2.Text = this.currShape != null ? this.currShape.ToString() : "";

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
    }
}
