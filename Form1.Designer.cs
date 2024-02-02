namespace Comp_test_Ameritech
{
    partial class CSVFileCollectionForm
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
            button1 = new Button();
            DataBox = new Label();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(328, 153);
            button1.Name = "button1";
            button1.Size = new Size(175, 62);
            button1.TabIndex = 0;
            button1.Text = "Select File";
            button1.UseVisualStyleBackColor = true;
            button1.Click += OpenFileSelector;
            // 
            // DataBox
            // 
            DataBox.BackColor = SystemColors.ControlDark;
            DataBox.Location = new Point(235, 301);
            DataBox.Name = "DataBox";
            DataBox.Size = new Size(374, 85);
            DataBox.TabIndex = 1;
            DataBox.Text = "DATA";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(DataBox);
            Controls.Add(button1);
            Name = "Form1";
            Text = "Process Large Integers";
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private Label DataBox;
    }
}
