
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
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.wrapper)).BeginInit();
            this.SuspendLayout();
            // 
            // wrapper
            // 
            this.wrapper.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.wrapper.Location = new System.Drawing.Point(12, 12);
            this.wrapper.Name = "wrapper";
            this.wrapper.Size = new System.Drawing.Size(654, 587);
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
            this.label1.Location = new System.Drawing.Point(693, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(179, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Choose drawing option";
            // 
            // polygonBtn
            // 
            this.polygonBtn.Location = new System.Drawing.Point(697, 77);
            this.polygonBtn.Name = "polygonBtn";
            this.polygonBtn.Size = new System.Drawing.Size(93, 33);
            this.polygonBtn.TabIndex = 1;
            this.polygonBtn.Text = "Polygon";
            this.polygonBtn.UseVisualStyleBackColor = true;
            this.polygonBtn.Click += new System.EventHandler(this.polygonBtn_Click);
            // 
            // circleBtn
            // 
            this.circleBtn.Location = new System.Drawing.Point(837, 77);
            this.circleBtn.Name = "circleBtn";
            this.circleBtn.Size = new System.Drawing.Size(93, 33);
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
            this.resetBtn.Location = new System.Drawing.Point(743, 566);
            this.resetBtn.Margin = new System.Windows.Forms.Padding(10);
            this.resetBtn.Name = "resetBtn";
            this.resetBtn.Padding = new System.Windows.Forms.Padding(5);
            this.resetBtn.Size = new System.Drawing.Size(148, 33);
            this.resetBtn.TabIndex = 1;
            this.resetBtn.Text = "Reset form";
            this.resetBtn.UseVisualStyleBackColor = false;
            this.resetBtn.Click += new System.EventHandler(this.resetBtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(709, 241);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "label2";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 611);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.resetBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.circleBtn);
            this.Controls.Add(this.polygonBtn);
            this.Controls.Add(this.wrapper);
            this.DoubleBuffered = true;
            this.MaximumSize = new System.Drawing.Size(1000, 650);
            this.MinimumSize = new System.Drawing.Size(1000, 650);
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
        private System.Windows.Forms.Label label2;
    }
}

