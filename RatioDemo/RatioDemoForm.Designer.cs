namespace RatioDemo
{
    partial class RatioDemoForm
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
            this.RatioLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // RatioLabel
            // 
            this.RatioLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RatioLabel.AutoSize = true;
            this.RatioLabel.Location = new System.Drawing.Point(767, 9);
            this.RatioLabel.Name = "RatioLabel";
            this.RatioLabel.Size = new System.Drawing.Size(21, 13);
            this.RatioLabel.TabIndex = 0;
            this.RatioLabel.Text = "wtf";
            this.RatioLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // RatioDemoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.RatioLabel);
            this.Name = "RatioDemoForm";
            this.Text = "Ratio Demo";
            this.Load += new System.EventHandler(this.RatioDemoForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.RatioDemoForm_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RatioDemoForm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.RatioDemoForm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RatioDemoForm_MouseUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label RatioLabel;
    }
}

