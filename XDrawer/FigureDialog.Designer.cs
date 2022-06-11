namespace _24_XDrawer
{
    partial class FigureDialog
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
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.textY2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textX2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textY1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textX1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.selectBox = new System.Windows.Forms.ComboBox();
            this.blackButton = new System.Windows.Forms.RadioButton();
            this.redButton = new System.Windows.Forms.RadioButton();
            this.greenButton = new System.Windows.Forms.RadioButton();
            this.blueButton = new System.Windows.Forms.RadioButton();
            this.selectColorButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(150, 214);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 49;
            this.cancelButton.Text = "Cancle";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(60, 214);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 48;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // textY2
            // 
            this.textY2.Location = new System.Drawing.Point(180, 49);
            this.textY2.Name = "textY2";
            this.textY2.Size = new System.Drawing.Size(46, 21);
            this.textY2.TabIndex = 47;
            this.textY2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(148, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 12);
            this.label3.TabIndex = 46;
            this.label3.Text = "y2 :";
            // 
            // textX2
            // 
            this.textX2.Location = new System.Drawing.Point(77, 49);
            this.textX2.Name = "textX2";
            this.textX2.Size = new System.Drawing.Size(42, 21);
            this.textX2.TabIndex = 45;
            this.textX2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(45, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 12);
            this.label4.TabIndex = 44;
            this.label4.Text = "x2 :";
            // 
            // textY1
            // 
            this.textY1.Location = new System.Drawing.Point(180, 12);
            this.textY1.Name = "textY1";
            this.textY1.Size = new System.Drawing.Size(46, 21);
            this.textY1.TabIndex = 43;
            this.textY1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(148, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 12);
            this.label2.TabIndex = 42;
            this.label2.Text = "y1 :";
            // 
            // textX1
            // 
            this.textX1.Location = new System.Drawing.Point(77, 12);
            this.textX1.Name = "textX1";
            this.textX1.Size = new System.Drawing.Size(42, 21);
            this.textX1.TabIndex = 41;
            this.textX1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 12);
            this.label1.TabIndex = 40;
            this.label1.Text = "x1 :";
            // 
            // selectBox
            // 
            this.selectBox.FormattingEnabled = true;
            this.selectBox.Location = new System.Drawing.Point(86, 85);
            this.selectBox.Name = "selectBox";
            this.selectBox.Size = new System.Drawing.Size(121, 20);
            this.selectBox.TabIndex = 50;
            this.selectBox.SelectedIndexChanged += new System.EventHandler(this.SelectBox_SelectedIndexChanged);
            // 
            // blackButton
            // 
            this.blackButton.AutoSize = true;
            this.blackButton.Location = new System.Drawing.Point(26, 123);
            this.blackButton.Name = "blackButton";
            this.blackButton.Size = new System.Drawing.Size(54, 16);
            this.blackButton.TabIndex = 51;
            this.blackButton.TabStop = true;
            this.blackButton.Text = "Black";
            this.blackButton.UseVisualStyleBackColor = true;
            this.blackButton.CheckedChanged += new System.EventHandler(this.BlackButton_CheckedChanged);
            // 
            // redButton
            // 
            this.redButton.AutoSize = true;
            this.redButton.Location = new System.Drawing.Point(86, 123);
            this.redButton.Name = "redButton";
            this.redButton.Size = new System.Drawing.Size(45, 16);
            this.redButton.TabIndex = 52;
            this.redButton.TabStop = true;
            this.redButton.Text = "Red";
            this.redButton.UseVisualStyleBackColor = true;
            this.redButton.CheckedChanged += new System.EventHandler(this.RedButton_CheckedChanged);
            // 
            // greenButton
            // 
            this.greenButton.AutoSize = true;
            this.greenButton.Location = new System.Drawing.Point(137, 123);
            this.greenButton.Name = "greenButton";
            this.greenButton.Size = new System.Drawing.Size(57, 16);
            this.greenButton.TabIndex = 53;
            this.greenButton.TabStop = true;
            this.greenButton.Text = "Green";
            this.greenButton.UseVisualStyleBackColor = true;
            this.greenButton.CheckedChanged += new System.EventHandler(this.GreenButton_CheckedChanged);
            // 
            // blueButton
            // 
            this.blueButton.AutoSize = true;
            this.blueButton.Location = new System.Drawing.Point(200, 123);
            this.blueButton.Name = "blueButton";
            this.blueButton.Size = new System.Drawing.Size(48, 16);
            this.blueButton.TabIndex = 54;
            this.blueButton.TabStop = true;
            this.blueButton.Text = "Blue";
            this.blueButton.UseVisualStyleBackColor = true;
            this.blueButton.CheckedChanged += new System.EventHandler(this.BlueButton_CheckedChanged);
            // 
            // selectColorButton
            // 
            this.selectColorButton.Location = new System.Drawing.Point(73, 166);
            this.selectColorButton.Name = "selectColorButton";
            this.selectColorButton.Size = new System.Drawing.Size(134, 23);
            this.selectColorButton.TabIndex = 55;
            this.selectColorButton.Text = "Select Color";
            this.selectColorButton.UseVisualStyleBackColor = true;
            this.selectColorButton.Click += new System.EventHandler(this.SelectColorButton_Click);
            // 
            // FigureDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(287, 272);
            this.Controls.Add(this.selectColorButton);
            this.Controls.Add(this.blueButton);
            this.Controls.Add(this.greenButton);
            this.Controls.Add(this.redButton);
            this.Controls.Add(this.blackButton);
            this.Controls.Add(this.selectBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.textY2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textX2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textY1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textX1);
            this.Controls.Add(this.label1);
            this.Name = "FigureDialog";
            this.Text = "FigureDialog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.TextBox textY2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textX2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textY1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textX1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox selectBox;
        private System.Windows.Forms.RadioButton blackButton;
        private System.Windows.Forms.RadioButton redButton;
        private System.Windows.Forms.RadioButton greenButton;
        private System.Windows.Forms.RadioButton blueButton;
        private System.Windows.Forms.Button selectColorButton;
    }
}