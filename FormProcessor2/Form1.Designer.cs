namespace FormProcessor
{
    partial class Form1
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
            this.DpiUpDown = new System.Windows.Forms.NumericUpDown();
            this.DpiButton = new System.Windows.Forms.Button();
            this.FormImagePanel = new System.Windows.Forms.Panel();
            this.FormImageBox = new System.Windows.Forms.PictureBox();
            this.TransformButton = new System.Windows.Forms.Button();
            this.FindAnchorsButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DpiUpDown)).BeginInit();
            this.FormImagePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FormImageBox)).BeginInit();
            this.SuspendLayout();
            // 
            // DpiUpDown
            // 
            this.DpiUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DpiUpDown.Location = new System.Drawing.Point(713, 12);
            this.DpiUpDown.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.DpiUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.DpiUpDown.Name = "DpiUpDown";
            this.DpiUpDown.Size = new System.Drawing.Size(75, 20);
            this.DpiUpDown.TabIndex = 0;
            this.DpiUpDown.Value = new decimal(new int[] {
            72,
            0,
            0,
            0});
            // 
            // DpiButton
            // 
            this.DpiButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DpiButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DpiButton.Location = new System.Drawing.Point(713, 38);
            this.DpiButton.Name = "DpiButton";
            this.DpiButton.Size = new System.Drawing.Size(75, 23);
            this.DpiButton.TabIndex = 1;
            this.DpiButton.Text = "Set";
            this.DpiButton.UseVisualStyleBackColor = true;
            this.DpiButton.Click += new System.EventHandler(this.DpiButton_Click);
            // 
            // FormImagePanel
            // 
            this.FormImagePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FormImagePanel.AutoScroll = true;
            this.FormImagePanel.Controls.Add(this.FormImageBox);
            this.FormImagePanel.Location = new System.Drawing.Point(12, 12);
            this.FormImagePanel.Name = "FormImagePanel";
            this.FormImagePanel.Size = new System.Drawing.Size(695, 426);
            this.FormImagePanel.TabIndex = 3;
            // 
            // FormImageBox
            // 
            this.FormImageBox.Location = new System.Drawing.Point(0, 0);
            this.FormImageBox.Name = "FormImageBox";
            this.FormImageBox.Size = new System.Drawing.Size(499, 306);
            this.FormImageBox.TabIndex = 3;
            this.FormImageBox.TabStop = false;
            // 
            // TransformButton
            // 
            this.TransformButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TransformButton.Location = new System.Drawing.Point(713, 67);
            this.TransformButton.Name = "TransformButton";
            this.TransformButton.Size = new System.Drawing.Size(75, 23);
            this.TransformButton.TabIndex = 4;
            this.TransformButton.Text = "Transform";
            this.TransformButton.UseVisualStyleBackColor = true;
            this.TransformButton.Click += new System.EventHandler(this.TransformButton_Click);
            // 
            // FindAnchorsButton
            // 
            this.FindAnchorsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FindAnchorsButton.Location = new System.Drawing.Point(713, 96);
            this.FindAnchorsButton.Name = "FindAnchorsButton";
            this.FindAnchorsButton.Size = new System.Drawing.Size(75, 23);
            this.FindAnchorsButton.TabIndex = 5;
            this.FindAnchorsButton.Text = "Find";
            this.FindAnchorsButton.UseVisualStyleBackColor = true;
            this.FindAnchorsButton.Click += new System.EventHandler(this.FindAnchorsButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.FindAnchorsButton);
            this.Controls.Add(this.TransformButton);
            this.Controls.Add(this.FormImagePanel);
            this.Controls.Add(this.DpiButton);
            this.Controls.Add(this.DpiUpDown);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DpiUpDown)).EndInit();
            this.FormImagePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FormImageBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NumericUpDown DpiUpDown;
        private System.Windows.Forms.Button DpiButton;
        private System.Windows.Forms.Panel FormImagePanel;
        private System.Windows.Forms.PictureBox FormImageBox;
        private System.Windows.Forms.Button TransformButton;
        private System.Windows.Forms.Button FindAnchorsButton;
    }
}

