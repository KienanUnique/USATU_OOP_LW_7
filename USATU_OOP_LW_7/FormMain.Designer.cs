namespace USATU_OOP_LW_7
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.panelForDrawing = new System.Windows.Forms.Panel();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.panelColorChoose = new System.Windows.Forms.Panel();
            this.buttonChooseColor = new System.Windows.Forms.Button();
            this.controlCurrentColor = new System.Windows.Forms.Control();
            this.labelCurrentColor = new System.Windows.Forms.Label();
            this.groupBoxChooseFigure = new System.Windows.Forms.GroupBox();
            this.radioButtonPentagon = new System.Windows.Forms.RadioButton();
            this.radioButtonTriangle = new System.Windows.Forms.RadioButton();
            this.radioButtonSquare = new System.Windows.Forms.RadioButton();
            this.radioButtonCircle = new System.Windows.Forms.RadioButton();
            this.textBoxControlPromts = new System.Windows.Forms.TextBox();
            this.panelColorChoose.SuspendLayout();
            this.groupBoxChooseFigure.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelForDrawing
            // 
            this.panelForDrawing.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelForDrawing.Location = new System.Drawing.Point(12, 12);
            this.panelForDrawing.Name = "panelForDrawing";
            this.panelForDrawing.Size = new System.Drawing.Size(620, 426);
            this.panelForDrawing.TabIndex = 0;
            this.panelForDrawing.Paint += new System.Windows.Forms.PaintEventHandler(this.panelForDrawing_Paint);
            this.panelForDrawing.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelForDrawing_MouseClick);
            // 
            // panelColorChoose
            // 
            this.panelColorChoose.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelColorChoose.Controls.Add(this.buttonChooseColor);
            this.panelColorChoose.Controls.Add(this.controlCurrentColor);
            this.panelColorChoose.Controls.Add(this.labelCurrentColor);
            this.panelColorChoose.Location = new System.Drawing.Point(638, 12);
            this.panelColorChoose.Name = "panelColorChoose";
            this.panelColorChoose.Size = new System.Drawing.Size(150, 67);
            this.panelColorChoose.TabIndex = 1;
            // 
            // buttonChooseColor
            // 
            this.buttonChooseColor.Location = new System.Drawing.Point(3, 34);
            this.buttonChooseColor.Name = "buttonChooseColor";
            this.buttonChooseColor.Size = new System.Drawing.Size(144, 23);
            this.buttonChooseColor.TabIndex = 2;
            this.buttonChooseColor.Text = "Change color";
            this.buttonChooseColor.UseVisualStyleBackColor = true;
            this.buttonChooseColor.Click += new System.EventHandler(this.buttonChooseColor_Click);
            // 
            // controlCurrentColor
            // 
            this.controlCurrentColor.BackColor = System.Drawing.Color.Black;
            this.controlCurrentColor.Location = new System.Drawing.Point(82, 3);
            this.controlCurrentColor.Name = "controlCurrentColor";
            this.controlCurrentColor.Size = new System.Drawing.Size(25, 25);
            this.controlCurrentColor.TabIndex = 1;
            // 
            // labelCurrentColor
            // 
            this.labelCurrentColor.Location = new System.Drawing.Point(3, 5);
            this.labelCurrentColor.Name = "labelCurrentColor";
            this.labelCurrentColor.Size = new System.Drawing.Size(73, 23);
            this.labelCurrentColor.TabIndex = 0;
            this.labelCurrentColor.Text = "Current color:";
            this.labelCurrentColor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBoxChooseFigure
            // 
            this.groupBoxChooseFigure.Controls.Add(this.radioButtonPentagon);
            this.groupBoxChooseFigure.Controls.Add(this.radioButtonTriangle);
            this.groupBoxChooseFigure.Controls.Add(this.radioButtonSquare);
            this.groupBoxChooseFigure.Controls.Add(this.radioButtonCircle);
            this.groupBoxChooseFigure.Location = new System.Drawing.Point(638, 85);
            this.groupBoxChooseFigure.Name = "groupBoxChooseFigure";
            this.groupBoxChooseFigure.Size = new System.Drawing.Size(150, 151);
            this.groupBoxChooseFigure.TabIndex = 2;
            this.groupBoxChooseFigure.TabStop = false;
            this.groupBoxChooseFigure.Text = "Current figure:";
            // 
            // radioButtonPentagon
            // 
            this.radioButtonPentagon.Location = new System.Drawing.Point(6, 109);
            this.radioButtonPentagon.Name = "radioButtonPentagon";
            this.radioButtonPentagon.Size = new System.Drawing.Size(104, 24);
            this.radioButtonPentagon.TabIndex = 0;
            this.radioButtonPentagon.Text = "Pentagon";
            this.radioButtonPentagon.UseVisualStyleBackColor = true;
            // 
            // radioButtonTriangle
            // 
            this.radioButtonTriangle.Location = new System.Drawing.Point(6, 49);
            this.radioButtonTriangle.Name = "radioButtonTriangle";
            this.radioButtonTriangle.Size = new System.Drawing.Size(104, 24);
            this.radioButtonTriangle.TabIndex = 0;
            this.radioButtonTriangle.Text = "Triangle";
            this.radioButtonTriangle.UseVisualStyleBackColor = true;
            // 
            // radioButtonSquare
            // 
            this.radioButtonSquare.Location = new System.Drawing.Point(6, 79);
            this.radioButtonSquare.Name = "radioButtonSquare";
            this.radioButtonSquare.Size = new System.Drawing.Size(104, 24);
            this.radioButtonSquare.TabIndex = 0;
            this.radioButtonSquare.Text = "Square";
            this.radioButtonSquare.UseVisualStyleBackColor = true;
            // 
            // radioButtonCircle
            // 
            this.radioButtonCircle.Checked = true;
            this.radioButtonCircle.Location = new System.Drawing.Point(6, 19);
            this.radioButtonCircle.Name = "radioButtonCircle";
            this.radioButtonCircle.Size = new System.Drawing.Size(104, 24);
            this.radioButtonCircle.TabIndex = 0;
            this.radioButtonCircle.TabStop = true;
            this.radioButtonCircle.Text = "Circle";
            this.radioButtonCircle.UseVisualStyleBackColor = true;
            // 
            // textBoxControlPromts
            // 
            this.textBoxControlPromts.Location = new System.Drawing.Point(638, 242);
            this.textBoxControlPromts.Multiline = true;
            this.textBoxControlPromts.Name = "textBoxControlPromts";
            this.textBoxControlPromts.ReadOnly = true;
            this.textBoxControlPromts.Size = new System.Drawing.Size(150, 196);
            this.textBoxControlPromts.TabIndex = 3;
            this.textBoxControlPromts.Text = resources.GetString("textBoxControlPromts.Text");
            this.textBoxControlPromts.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.textBoxControlPromts);
            this.Controls.Add(this.groupBoxChooseFigure);
            this.Controls.Add(this.panelColorChoose);
            this.Controls.Add(this.panelForDrawing);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Location = new System.Drawing.Point(15, 15);
            this.Name = "FormMain";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormMain_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FormMain_KeyUp);
            this.panelColorChoose.ResumeLayout(false);
            this.groupBoxChooseFigure.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.TextBox textBoxControlPromts;

        private System.Windows.Forms.RadioButton radioButtonPentagon;

        private System.Windows.Forms.Button buttonChooseColor;

        private System.Windows.Forms.GroupBox groupBoxChooseFigure;
        private System.Windows.Forms.RadioButton radioButtonCircle;
        private System.Windows.Forms.RadioButton radioButtonSquare;
        private System.Windows.Forms.RadioButton radioButtonTriangle;

        private System.Windows.Forms.Panel panelColorChoose;
        private System.Windows.Forms.Label labelCurrentColor;
        private System.Windows.Forms.Control controlCurrentColor;

        private System.Windows.Forms.ColorDialog colorDialog;

        private System.Windows.Forms.Panel panelForDrawing;

        #endregion
    }
}