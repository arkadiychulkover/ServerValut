namespace ClientCourse
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
            Conector = new Button();
            SendBtn = new Button();
            textBox1 = new TextBox();
            Messages = new ListBox();
            SuspendLayout();
            // 
            // Conector
            // 
            Conector.Location = new Point(12, 12);
            Conector.Name = "Conector";
            Conector.Size = new Size(183, 29);
            Conector.TabIndex = 0;
            Conector.Text = "Start/Stop";
            Conector.UseVisualStyleBackColor = true;
            Conector.Click += Conector_Click;
            // 
            // SendBtn
            // 
            SendBtn.Location = new Point(12, 124);
            SendBtn.Name = "SendBtn";
            SendBtn.Size = new Size(183, 29);
            SendBtn.TabIndex = 1;
            SendBtn.Text = "Send";
            SendBtn.UseVisualStyleBackColor = true;
            SendBtn.Click += SendBtn_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(12, 69);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(183, 27);
            textBox1.TabIndex = 2;
            // 
            // Messages
            // 
            Messages.FormattingEnabled = true;
            Messages.Location = new Point(12, 182);
            Messages.Name = "Messages";
            Messages.Size = new Size(183, 104);
            Messages.TabIndex = 3;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(207, 332);
            Controls.Add(Messages);
            Controls.Add(textBox1);
            Controls.Add(SendBtn);
            Controls.Add(Conector);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button Conector;
        private Button SendBtn;
        private TextBox textBox1;
        private ListBox Messages;
    }
}
