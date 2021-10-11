﻿
namespace Projekt1
{
    partial class Form1
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.wrapper = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.polygonBtn = new System.Windows.Forms.Button();
            this.circleBtn = new System.Windows.Forms.Button();
            this.resetBtn = new System.Windows.Forms.Button();
            this.debugLabel = new System.Windows.Forms.Label();
            this.removeVertexBtn = new System.Windows.Forms.Button();
            this.addVertexBtn = new System.Windows.Forms.Button();
            this.removeShapeBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.wrapper)).BeginInit();
            this.SuspendLayout();
            // 
            // wrapper
            // 
            this.wrapper.BackColor = System.Drawing.Color.White;
            this.wrapper.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.wrapper.Location = new System.Drawing.Point(16, 15);
            this.wrapper.Margin = new System.Windows.Forms.Padding(4);
            this.wrapper.Name = "wrapper";
            this.wrapper.Size = new System.Drawing.Size(871, 687);
            this.wrapper.TabIndex = 2;
            this.wrapper.TabStop = false;
            this.wrapper.Paint += new System.Windows.Forms.PaintEventHandler(this.wrapper_Paint);
            this.wrapper.MouseClick += new System.Windows.Forms.MouseEventHandler(this.wrapper_MouseClick);
            this.wrapper.MouseDown += new System.Windows.Forms.MouseEventHandler(this.wrapper_MouseDown);
            this.wrapper.MouseMove += new System.Windows.Forms.MouseEventHandler(this.wrapper_MouseMove);
            this.wrapper.MouseUp += new System.Windows.Forms.MouseEventHandler(this.wrapper_MouseUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.label1.Location = new System.Drawing.Point(924, 26);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(232, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Choose drawing option";
            // 
            // polygonBtn
            // 
            this.polygonBtn.Location = new System.Drawing.Point(929, 95);
            this.polygonBtn.Margin = new System.Windows.Forms.Padding(4);
            this.polygonBtn.Name = "polygonBtn";
            this.polygonBtn.Size = new System.Drawing.Size(124, 41);
            this.polygonBtn.TabIndex = 1;
            this.polygonBtn.Text = "Polygon";
            this.polygonBtn.UseVisualStyleBackColor = true;
            this.polygonBtn.Click += new System.EventHandler(this.polygonBtn_Click);
            // 
            // circleBtn
            // 
            this.circleBtn.Location = new System.Drawing.Point(1116, 95);
            this.circleBtn.Margin = new System.Windows.Forms.Padding(4);
            this.circleBtn.Name = "circleBtn";
            this.circleBtn.Size = new System.Drawing.Size(124, 41);
            this.circleBtn.TabIndex = 2;
            this.circleBtn.Text = "Circle";
            this.circleBtn.UseVisualStyleBackColor = true;
            this.circleBtn.Click += new System.EventHandler(this.circleBtn_Click);
            // 
            // resetBtn
            // 
            this.resetBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(63)))), ((int)(((byte)(114)))));
            this.resetBtn.FlatAppearance.BorderSize = 0;
            this.resetBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.resetBtn.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.resetBtn.Location = new System.Drawing.Point(991, 697);
            this.resetBtn.Margin = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.resetBtn.Name = "resetBtn";
            this.resetBtn.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.resetBtn.Size = new System.Drawing.Size(197, 41);
            this.resetBtn.TabIndex = 1;
            this.resetBtn.Text = "Reset form";
            this.resetBtn.UseVisualStyleBackColor = false;
            this.resetBtn.Click += new System.EventHandler(this.resetBtn_Click);
            // 
            // debugLabel
            // 
            this.debugLabel.AutoSize = true;
            this.debugLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.debugLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.debugLabel.Location = new System.Drawing.Point(22, 709);
            this.debugLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.debugLabel.Name = "debugLabel";
            this.debugLabel.Size = new System.Drawing.Size(73, 15);
            this.debugLabel.TabIndex = 3;
            this.debugLabel.Text = "debugLabel";
            // 
            // removeVertexBtn
            // 
            this.removeVertexBtn.Location = new System.Drawing.Point(929, 189);
            this.removeVertexBtn.Name = "removeVertexBtn";
            this.removeVertexBtn.Size = new System.Drawing.Size(124, 37);
            this.removeVertexBtn.TabIndex = 4;
            this.removeVertexBtn.Text = "Remove Vertex";
            this.removeVertexBtn.UseVisualStyleBackColor = true;
            this.removeVertexBtn.Click += new System.EventHandler(this.removeVertexBtn_Click);
            // 
            // addVertexBtn
            // 
            this.addVertexBtn.Location = new System.Drawing.Point(1116, 189);
            this.addVertexBtn.Name = "addVertexBtn";
            this.addVertexBtn.Size = new System.Drawing.Size(124, 37);
            this.addVertexBtn.TabIndex = 5;
            this.addVertexBtn.Text = "Add vertex";
            this.addVertexBtn.UseVisualStyleBackColor = true;
            this.addVertexBtn.Click += new System.EventHandler(this.addVertexBtn_Click);
            // 
            // removeShapeBtn
            // 
            this.removeShapeBtn.Location = new System.Drawing.Point(929, 268);
            this.removeShapeBtn.Name = "removeShapeBtn";
            this.removeShapeBtn.Size = new System.Drawing.Size(127, 38);
            this.removeShapeBtn.TabIndex = 6;
            this.removeShapeBtn.Text = "Remove shape";
            this.removeShapeBtn.UseVisualStyleBackColor = true;
            this.removeShapeBtn.Click += new System.EventHandler(this.removeShapeBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1309, 742);
            this.Controls.Add(this.removeShapeBtn);
            this.Controls.Add(this.addVertexBtn);
            this.Controls.Add(this.removeVertexBtn);
            this.Controls.Add(this.debugLabel);
            this.Controls.Add(this.resetBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.circleBtn);
            this.Controls.Add(this.polygonBtn);
            this.Controls.Add(this.wrapper);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximumSize = new System.Drawing.Size(1327, 789);
            this.MinimumSize = new System.Drawing.Size(1327, 789);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.wrapper)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox wrapper;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button polygonBtn;
        private System.Windows.Forms.Button circleBtn;
        private System.Windows.Forms.Button resetBtn;
        private System.Windows.Forms.Label debugLabel;
        private System.Windows.Forms.Button removeVertexBtn;
        private System.Windows.Forms.Button addVertexBtn;
        private System.Windows.Forms.Button removeShapeBtn;
    }
}

