namespace ServerValut
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
            Starter = new Button();
            Conections = new ListBox();
            numericUpDown1 = new NumericUpDown();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            SuspendLayout();
            // 
            // Starter
            // 
            Starter.Location = new Point(12, 12);
            Starter.Name = "Starter";
            Starter.Size = new Size(358, 29);
            Starter.TabIndex = 0;
            Starter.Text = "StartServer";
            Starter.UseVisualStyleBackColor = true;
            Starter.Click += Starter_Click;
            // 
            // Conections
            // 
            Conections.FormattingEnabled = true;
            Conections.Location = new Point(12, 47);
            Conections.Name = "Conections";
            Conections.Size = new Size(358, 184);
            Conections.TabIndex = 1;
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(402, 47);
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(171, 27);
            numericUpDown1.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(402, 21);
            label1.Name = "label1";
            label1.Size = new Size(171, 20);
            label1.TabIndex = 3;
            label1.Text = "Select count of requests ";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(637, 321);
            Controls.Add(label1);
            Controls.Add(numericUpDown1);
            Controls.Add(Conections);
            Controls.Add(Starter);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button Starter;
        private ListBox Conections;
        private NumericUpDown numericUpDown1;
        private Label label1;
    }
}
