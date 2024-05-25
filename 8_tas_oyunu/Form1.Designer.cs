namespace _8_tas_oyunu
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            goalStateLabel = new Label();
            moveCountLabel = new Label();
            SuspendLayout();
            // 
            // goalStateLabel
            // 
            goalStateLabel.AutoSize = true;
            goalStateLabel.Location = new Point(97, 119);
            goalStateLabel.Name = "goalStateLabel";
            goalStateLabel.Size = new Size(38, 15);
            goalStateLabel.TabIndex = 0;
            goalStateLabel.Text = "label1";
            // 
            // moveCountLabel
            // 
            moveCountLabel.AutoSize = true;
            moveCountLabel.Location = new Point(636, 119);
            moveCountLabel.Name = "moveCountLabel";
            moveCountLabel.Size = new Size(38, 15);
            moveCountLabel.TabIndex = 1;
            moveCountLabel.Text = "label2";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(moveCountLabel);
            Controls.Add(goalStateLabel);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label goalStateLabel;
        private Label moveCountLabel;
    }
}
