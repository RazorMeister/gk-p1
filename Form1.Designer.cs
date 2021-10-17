
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
            this.polygonBtn = new System.Windows.Forms.Button();
            this.circleBtn = new System.Windows.Forms.Button();
            this.resetBtn = new System.Windows.Forms.Button();
            this.debugLabel = new System.Windows.Forms.Label();
            this.removeVertexBtn = new System.Windows.Forms.Button();
            this.addVertexBtn = new System.Windows.Forms.Button();
            this.removeShapeBtn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.actionWrapper = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.circleTangencyBtn = new System.Windows.Forms.Button();
            this.parallelEdgesBtn = new System.Windows.Forms.Button();
            this.fixedEdgeBtn = new System.Windows.Forms.Button();
            this.fixedRadiusBtn = new System.Windows.Forms.Button();
            this.anchorCircleBtn = new System.Windows.Forms.Button();
            this.almostCompletedLabel = new System.Windows.Forms.Label();
            this.sameSizeEdgesBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.wrapper)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.actionWrapper.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // wrapper
            // 
            this.wrapper.BackColor = System.Drawing.Color.White;
            this.wrapper.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.wrapper.Location = new System.Drawing.Point(12, 12);
            this.wrapper.Name = "wrapper";
            this.wrapper.Size = new System.Drawing.Size(654, 559);
            this.wrapper.TabIndex = 2;
            this.wrapper.TabStop = false;
            this.wrapper.Paint += new System.Windows.Forms.PaintEventHandler(this.wrapper_Paint);
            this.wrapper.MouseClick += new System.Windows.Forms.MouseEventHandler(this.wrapper_MouseClick);
            this.wrapper.MouseDown += new System.Windows.Forms.MouseEventHandler(this.wrapper_MouseDown);
            this.wrapper.MouseMove += new System.Windows.Forms.MouseEventHandler(this.wrapper_MouseMove);
            this.wrapper.MouseUp += new System.Windows.Forms.MouseEventHandler(this.wrapper_MouseUp);
            // 
            // polygonBtn
            // 
            this.polygonBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.polygonBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.polygonBtn.Location = new System.Drawing.Point(25, 30);
            this.polygonBtn.Name = "polygonBtn";
            this.polygonBtn.Size = new System.Drawing.Size(108, 33);
            this.polygonBtn.TabIndex = 1;
            this.polygonBtn.Text = "Polygon";
            this.polygonBtn.UseVisualStyleBackColor = true;
            this.polygonBtn.Click += new System.EventHandler(this.polygonBtn_Click);
            // 
            // circleBtn
            // 
            this.circleBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.circleBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.circleBtn.Location = new System.Drawing.Point(165, 30);
            this.circleBtn.Name = "circleBtn";
            this.circleBtn.Size = new System.Drawing.Size(108, 33);
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
            this.resetBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resetBtn.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.resetBtn.Location = new System.Drawing.Point(758, 558);
            this.resetBtn.Margin = new System.Windows.Forms.Padding(10);
            this.resetBtn.Name = "resetBtn";
            this.resetBtn.Padding = new System.Windows.Forms.Padding(5);
            this.resetBtn.Size = new System.Drawing.Size(148, 40);
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
            this.debugLabel.Location = new System.Drawing.Point(16, 576);
            this.debugLabel.Name = "debugLabel";
            this.debugLabel.Size = new System.Drawing.Size(63, 13);
            this.debugLabel.TabIndex = 3;
            this.debugLabel.Text = "debugLabel";
            // 
            // removeVertexBtn
            // 
            this.removeVertexBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.removeVertexBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.removeVertexBtn.Location = new System.Drawing.Point(21, 30);
            this.removeVertexBtn.Margin = new System.Windows.Forms.Padding(2);
            this.removeVertexBtn.Name = "removeVertexBtn";
            this.removeVertexBtn.Size = new System.Drawing.Size(108, 30);
            this.removeVertexBtn.TabIndex = 4;
            this.removeVertexBtn.Text = "Remove Vertex";
            this.removeVertexBtn.UseVisualStyleBackColor = true;
            this.removeVertexBtn.Click += new System.EventHandler(this.removeVertexBtn_Click);
            // 
            // addVertexBtn
            // 
            this.addVertexBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addVertexBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addVertexBtn.Location = new System.Drawing.Point(161, 30);
            this.addVertexBtn.Margin = new System.Windows.Forms.Padding(2);
            this.addVertexBtn.Name = "addVertexBtn";
            this.addVertexBtn.Size = new System.Drawing.Size(108, 30);
            this.addVertexBtn.TabIndex = 5;
            this.addVertexBtn.Text = "Add vertex";
            this.addVertexBtn.UseVisualStyleBackColor = true;
            this.addVertexBtn.Click += new System.EventHandler(this.addVertexBtn_Click);
            // 
            // removeShapeBtn
            // 
            this.removeShapeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.removeShapeBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.removeShapeBtn.Location = new System.Drawing.Point(21, 83);
            this.removeShapeBtn.Margin = new System.Windows.Forms.Padding(2);
            this.removeShapeBtn.Name = "removeShapeBtn";
            this.removeShapeBtn.Size = new System.Drawing.Size(108, 31);
            this.removeShapeBtn.TabIndex = 6;
            this.removeShapeBtn.Text = "Remove shape";
            this.removeShapeBtn.UseVisualStyleBackColor = true;
            this.removeShapeBtn.Click += new System.EventHandler(this.removeShapeBtn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.polygonBtn);
            this.groupBox1.Controls.Add(this.circleBtn);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(672, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(299, 80);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Choose what you want to draw";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.removeVertexBtn);
            this.groupBox2.Controls.Add(this.addVertexBtn);
            this.groupBox2.Controls.Add(this.removeShapeBtn);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(676, 123);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(294, 131);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Do something";
            // 
            // actionWrapper
            // 
            this.actionWrapper.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(175)))), ((int)(((byte)(175)))));
            this.actionWrapper.Controls.Add(this.label1);
            this.actionWrapper.Location = new System.Drawing.Point(586, 13);
            this.actionWrapper.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.actionWrapper.Name = "actionWrapper";
            this.actionWrapper.Size = new System.Drawing.Size(79, 29);
            this.actionWrapper.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 29);
            this.label1.TabIndex = 10;
            this.label1.Text = "None";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.sameSizeEdgesBtn);
            this.groupBox3.Controls.Add(this.circleTangencyBtn);
            this.groupBox3.Controls.Add(this.parallelEdgesBtn);
            this.groupBox3.Controls.Add(this.fixedEdgeBtn);
            this.groupBox3.Controls.Add(this.fixedRadiusBtn);
            this.groupBox3.Controls.Add(this.anchorCircleBtn);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(676, 289);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(295, 191);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Manage relations";
            // 
            // circleTangencyBtn
            // 
            this.circleTangencyBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.circleTangencyBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.circleTangencyBtn.Location = new System.Drawing.Point(21, 143);
            this.circleTangencyBtn.Margin = new System.Windows.Forms.Padding(2);
            this.circleTangencyBtn.Name = "circleTangencyBtn";
            this.circleTangencyBtn.Size = new System.Drawing.Size(108, 30);
            this.circleTangencyBtn.TabIndex = 9;
            this.circleTangencyBtn.Text = "Tangency";
            this.circleTangencyBtn.UseVisualStyleBackColor = true;
            this.circleTangencyBtn.Click += new System.EventHandler(this.circleTangencyBtn_Click);
            // 
            // parallelEdgesBtn
            // 
            this.parallelEdgesBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.parallelEdgesBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.parallelEdgesBtn.Location = new System.Drawing.Point(161, 92);
            this.parallelEdgesBtn.Margin = new System.Windows.Forms.Padding(2);
            this.parallelEdgesBtn.Name = "parallelEdgesBtn";
            this.parallelEdgesBtn.Size = new System.Drawing.Size(108, 30);
            this.parallelEdgesBtn.TabIndex = 8;
            this.parallelEdgesBtn.Text = "Parallel edges";
            this.parallelEdgesBtn.UseVisualStyleBackColor = true;
            this.parallelEdgesBtn.Click += new System.EventHandler(this.parallelEdgesBtn_Click);
            // 
            // fixedEdgeBtn
            // 
            this.fixedEdgeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.fixedEdgeBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fixedEdgeBtn.Location = new System.Drawing.Point(21, 92);
            this.fixedEdgeBtn.Margin = new System.Windows.Forms.Padding(2);
            this.fixedEdgeBtn.Name = "fixedEdgeBtn";
            this.fixedEdgeBtn.Size = new System.Drawing.Size(108, 30);
            this.fixedEdgeBtn.TabIndex = 7;
            this.fixedEdgeBtn.Text = "Fixed edge";
            this.fixedEdgeBtn.UseVisualStyleBackColor = true;
            this.fixedEdgeBtn.Click += new System.EventHandler(this.fixedEdgeBtn_Click);
            // 
            // fixedRadiusBtn
            // 
            this.fixedRadiusBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.fixedRadiusBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fixedRadiusBtn.Location = new System.Drawing.Point(161, 37);
            this.fixedRadiusBtn.Margin = new System.Windows.Forms.Padding(2);
            this.fixedRadiusBtn.Name = "fixedRadiusBtn";
            this.fixedRadiusBtn.Size = new System.Drawing.Size(108, 30);
            this.fixedRadiusBtn.TabIndex = 6;
            this.fixedRadiusBtn.Text = "Fixed radius";
            this.fixedRadiusBtn.UseVisualStyleBackColor = true;
            this.fixedRadiusBtn.Click += new System.EventHandler(this.fixedRadiusBtn_Click);
            // 
            // anchorCircleBtn
            // 
            this.anchorCircleBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.anchorCircleBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.anchorCircleBtn.Location = new System.Drawing.Point(21, 37);
            this.anchorCircleBtn.Margin = new System.Windows.Forms.Padding(2);
            this.anchorCircleBtn.Name = "anchorCircleBtn";
            this.anchorCircleBtn.Size = new System.Drawing.Size(108, 30);
            this.anchorCircleBtn.TabIndex = 5;
            this.anchorCircleBtn.Text = "Anchor circle";
            this.anchorCircleBtn.UseVisualStyleBackColor = true;
            this.anchorCircleBtn.Click += new System.EventHandler(this.anchorCircleBtn_Click);
            // 
            // almostCompletedLabel
            // 
            this.almostCompletedLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.almostCompletedLabel.BackColor = System.Drawing.Color.Transparent;
            this.almostCompletedLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.almostCompletedLabel.Location = new System.Drawing.Point(738, 493);
            this.almostCompletedLabel.Name = "almostCompletedLabel";
            this.almostCompletedLabel.Size = new System.Drawing.Size(190, 29);
            this.almostCompletedLabel.TabIndex = 11;
            this.almostCompletedLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // sameSizeEdgesBtn
            // 
            this.sameSizeEdgesBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sameSizeEdgesBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sameSizeEdgesBtn.Location = new System.Drawing.Point(161, 143);
            this.sameSizeEdgesBtn.Margin = new System.Windows.Forms.Padding(2);
            this.sameSizeEdgesBtn.Name = "sameSizeEdgesBtn";
            this.sameSizeEdgesBtn.Size = new System.Drawing.Size(108, 30);
            this.sameSizeEdgesBtn.TabIndex = 10;
            this.sameSizeEdgesBtn.Text = "Equal edges";
            this.sameSizeEdgesBtn.UseVisualStyleBackColor = true;
            this.sameSizeEdgesBtn.Click += new System.EventHandler(this.sameSizeEdgesBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(983, 609);
            this.Controls.Add(this.almostCompletedLabel);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.actionWrapper);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.debugLabel);
            this.Controls.Add(this.resetBtn);
            this.Controls.Add(this.wrapper);
            this.DoubleBuffered = true;
            this.MaximumSize = new System.Drawing.Size(999, 648);
            this.MinimumSize = new System.Drawing.Size(999, 648);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.wrapper)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.actionWrapper.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox wrapper;
        private System.Windows.Forms.Button polygonBtn;
        private System.Windows.Forms.Button circleBtn;
        private System.Windows.Forms.Button resetBtn;
        private System.Windows.Forms.Label debugLabel;
        private System.Windows.Forms.Button removeVertexBtn;
        private System.Windows.Forms.Button addVertexBtn;
        private System.Windows.Forms.Button removeShapeBtn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Panel actionWrapper;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button anchorCircleBtn;
        private System.Windows.Forms.Button fixedRadiusBtn;
        private System.Windows.Forms.Button fixedEdgeBtn;
        private System.Windows.Forms.Button parallelEdgesBtn;
        private System.Windows.Forms.Button circleTangencyBtn;
        private System.Windows.Forms.Label almostCompletedLabel;
        private System.Windows.Forms.Button sameSizeEdgesBtn;
    }
}

