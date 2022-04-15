namespace BezierFontEditor
{
    partial class EditorForm
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
            this.ShowControlsCheckbox = new System.Windows.Forms.CheckBox();
            this.LetterComboBox = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.RemoveLetterButton = new System.Windows.Forms.Button();
            this.AddLetterButton = new System.Windows.Forms.Button();
            this.LetterTextBox = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.LoadBackImageButton = new System.Windows.Forms.Button();
            this.LoadFontButton = new System.Windows.Forms.Button();
            this.SaveFontButton = new System.Windows.Forms.Button();
            this.SaveFontDialog = new System.Windows.Forms.SaveFileDialog();
            this.OpenFontDialog = new System.Windows.Forms.OpenFileDialog();
            this.OpenBackImageDialog = new System.Windows.Forms.OpenFileDialog();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ZoomUpDown = new System.Windows.Forms.NumericUpDown();
            this.BaselineUpDown = new System.Windows.Forms.NumericUpDown();
            this.HScrollBar = new System.Windows.Forms.HScrollBar();
            this.VScrollBar = new System.Windows.Forms.VScrollBar();
            this.FormImageBox = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ZoomUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BaselineUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FormImageBox)).BeginInit();
            this.SuspendLayout();
            // 
            // ShowControlsCheckbox
            // 
            this.ShowControlsCheckbox.AutoSize = true;
            this.ShowControlsCheckbox.Location = new System.Drawing.Point(0, 3);
            this.ShowControlsCheckbox.Name = "ShowControlsCheckbox";
            this.ShowControlsCheckbox.Size = new System.Drawing.Size(93, 17);
            this.ShowControlsCheckbox.TabIndex = 7;
            this.ShowControlsCheckbox.Text = "Show controls";
            this.ShowControlsCheckbox.UseVisualStyleBackColor = true;
            this.ShowControlsCheckbox.CheckedChanged += new System.EventHandler(this.ShowControlsCheckbox_CheckedChanged);
            // 
            // LetterComboBox
            // 
            this.LetterComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.LetterComboBox.FormattingEnabled = true;
            this.LetterComboBox.Location = new System.Drawing.Point(0, 84);
            this.LetterComboBox.Name = "LetterComboBox";
            this.LetterComboBox.Size = new System.Drawing.Size(113, 21);
            this.LetterComboBox.TabIndex = 8;
            this.LetterComboBox.SelectedIndexChanged += new System.EventHandler(this.LetterComboBox_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ShowControlsCheckbox);
            this.panel1.Location = new System.Drawing.Point(675, 126);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(113, 25);
            this.panel1.TabIndex = 9;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.RemoveLetterButton);
            this.panel2.Controls.Add(this.AddLetterButton);
            this.panel2.Controls.Add(this.LetterTextBox);
            this.panel2.Controls.Add(this.LetterComboBox);
            this.panel2.Location = new System.Drawing.Point(675, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(113, 108);
            this.panel2.TabIndex = 10;
            // 
            // RemoveLetterButton
            // 
            this.RemoveLetterButton.Location = new System.Drawing.Point(0, 55);
            this.RemoveLetterButton.Name = "RemoveLetterButton";
            this.RemoveLetterButton.Size = new System.Drawing.Size(113, 23);
            this.RemoveLetterButton.TabIndex = 11;
            this.RemoveLetterButton.Text = "Remove letter";
            this.RemoveLetterButton.UseVisualStyleBackColor = true;
            this.RemoveLetterButton.Click += new System.EventHandler(this.RemoveLetterButton_Click);
            // 
            // AddLetterButton
            // 
            this.AddLetterButton.Location = new System.Drawing.Point(0, 26);
            this.AddLetterButton.Name = "AddLetterButton";
            this.AddLetterButton.Size = new System.Drawing.Size(113, 23);
            this.AddLetterButton.TabIndex = 10;
            this.AddLetterButton.Text = "Add letter";
            this.AddLetterButton.UseVisualStyleBackColor = true;
            this.AddLetterButton.Click += new System.EventHandler(this.AddLetterButton_Click);
            // 
            // LetterTextBox
            // 
            this.LetterTextBox.Location = new System.Drawing.Point(0, 0);
            this.LetterTextBox.Name = "LetterTextBox";
            this.LetterTextBox.Size = new System.Drawing.Size(113, 20);
            this.LetterTextBox.TabIndex = 9;
            this.LetterTextBox.TextChanged += new System.EventHandler(this.LetterTextBox_TextChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.LoadBackImageButton);
            this.panel3.Controls.Add(this.LoadFontButton);
            this.panel3.Controls.Add(this.SaveFontButton);
            this.panel3.Location = new System.Drawing.Point(675, 402);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(113, 83);
            this.panel3.TabIndex = 11;
            // 
            // LoadBackImageButton
            // 
            this.LoadBackImageButton.Location = new System.Drawing.Point(0, 57);
            this.LoadBackImageButton.Name = "LoadBackImageButton";
            this.LoadBackImageButton.Size = new System.Drawing.Size(113, 23);
            this.LoadBackImageButton.TabIndex = 8;
            this.LoadBackImageButton.Text = "Load back image";
            this.LoadBackImageButton.UseVisualStyleBackColor = true;
            this.LoadBackImageButton.Click += new System.EventHandler(this.LoadBackImageButton_Click);
            // 
            // LoadFontButton
            // 
            this.LoadFontButton.Location = new System.Drawing.Point(0, 29);
            this.LoadFontButton.Name = "LoadFontButton";
            this.LoadFontButton.Size = new System.Drawing.Size(113, 23);
            this.LoadFontButton.TabIndex = 1;
            this.LoadFontButton.Text = "Load font";
            this.LoadFontButton.UseVisualStyleBackColor = true;
            this.LoadFontButton.Click += new System.EventHandler(this.LoadFontButton_Click);
            // 
            // SaveFontButton
            // 
            this.SaveFontButton.Location = new System.Drawing.Point(0, 0);
            this.SaveFontButton.Name = "SaveFontButton";
            this.SaveFontButton.Size = new System.Drawing.Size(113, 23);
            this.SaveFontButton.TabIndex = 0;
            this.SaveFontButton.Text = "Save font";
            this.SaveFontButton.UseVisualStyleBackColor = true;
            this.SaveFontButton.Click += new System.EventHandler(this.SaveFontButton_Click);
            // 
            // SaveFontDialog
            // 
            this.SaveFontDialog.DefaultExt = "json";
            this.SaveFontDialog.Title = "Save font";
            // 
            // OpenFontDialog
            // 
            this.OpenFontDialog.DefaultExt = "json";
            this.OpenFontDialog.FileName = "openFileDialog1";
            this.OpenFontDialog.Title = "Open font";
            // 
            // OpenBackImageDialog
            // 
            this.OpenBackImageDialog.DefaultExt = "png; jpeg";
            this.OpenBackImageDialog.Title = "Open back image";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.ZoomUpDown);
            this.panel4.Controls.Add(this.BaselineUpDown);
            this.panel4.Location = new System.Drawing.Point(675, 157);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(113, 85);
            this.panel4.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Baseline";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Zoom";
            // 
            // ZoomUpDown
            // 
            this.ZoomUpDown.Location = new System.Drawing.Point(0, 63);
            this.ZoomUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.ZoomUpDown.Minimum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.ZoomUpDown.Name = "ZoomUpDown";
            this.ZoomUpDown.Size = new System.Drawing.Size(112, 20);
            this.ZoomUpDown.TabIndex = 3;
            this.ZoomUpDown.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.ZoomUpDown.ValueChanged += new System.EventHandler(this.ZoomUpDown_ValueChanged);
            // 
            // BaselineUpDown
            // 
            this.BaselineUpDown.Location = new System.Drawing.Point(0, 24);
            this.BaselineUpDown.Name = "BaselineUpDown";
            this.BaselineUpDown.Size = new System.Drawing.Size(113, 20);
            this.BaselineUpDown.TabIndex = 0;
            this.BaselineUpDown.ValueChanged += new System.EventHandler(this.BaselineUpDown_ValueChanged);
            // 
            // HScrollBar
            // 
            this.HScrollBar.Location = new System.Drawing.Point(12, 465);
            this.HScrollBar.Name = "HScrollBar";
            this.HScrollBar.Size = new System.Drawing.Size(640, 17);
            this.HScrollBar.TabIndex = 13;
            this.HScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.HScrollBar_Scroll);
            // 
            // VScrollBar
            // 
            this.VScrollBar.Location = new System.Drawing.Point(655, 12);
            this.VScrollBar.Name = "VScrollBar";
            this.VScrollBar.Size = new System.Drawing.Size(17, 450);
            this.VScrollBar.TabIndex = 14;
            this.VScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.VScrollBar_Scroll);
            // 
            // FormImageBox
            // 
            this.FormImageBox.Location = new System.Drawing.Point(12, 12);
            this.FormImageBox.Name = "FormImageBox";
            this.FormImageBox.Size = new System.Drawing.Size(640, 450);
            this.FormImageBox.TabIndex = 15;
            this.FormImageBox.TabStop = false;
            this.FormImageBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormImageBox_MouseDown);
            this.FormImageBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormImageBox_MouseMove);
            this.FormImageBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormImageBox_MouseUp);
            // 
            // EditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 497);
            this.Controls.Add(this.FormImageBox);
            this.Controls.Add(this.VScrollBar);
            this.Controls.Add(this.HScrollBar);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "EditorForm";
            this.Text = "BezierFormEditor";
            this.Load += new System.EventHandler(this.EditorForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.EditorForm_Paint);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ZoomUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BaselineUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FormImageBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.CheckBox ShowControlsCheckbox;
        private System.Windows.Forms.ComboBox LetterComboBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button RemoveLetterButton;
        private System.Windows.Forms.Button AddLetterButton;
        private System.Windows.Forms.TextBox LetterTextBox;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button SaveFontButton;
        private System.Windows.Forms.Button LoadFontButton;
        private System.Windows.Forms.SaveFileDialog SaveFontDialog;
        private System.Windows.Forms.OpenFileDialog OpenFontDialog;
        private System.Windows.Forms.OpenFileDialog OpenBackImageDialog;
        private System.Windows.Forms.Button LoadBackImageButton;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown ZoomUpDown;
        private System.Windows.Forms.NumericUpDown BaselineUpDown;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.HScrollBar HScrollBar;
        private System.Windows.Forms.VScrollBar VScrollBar;
        private System.Windows.Forms.PictureBox FormImageBox;
    }
}

