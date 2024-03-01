namespace _14_Infection
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
            textBoxN = new TextBox();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            panelSimulation = new Panel();
            textBoxTimeInterval = new TextBox();
            buttonStart = new Button();
            SuspendLayout();
            // 
            // textBoxN
            // 
            textBoxN.Location = new Point(359, 12);
            textBoxN.Name = "textBoxN";
            textBoxN.Size = new Size(125, 27);
            textBoxN.TabIndex = 0;
            // 
            // panelSimulation
            // 
            panelSimulation.Location = new Point(12, 12);
            panelSimulation.Name = "panelSimulation";
            panelSimulation.Size = new Size(291, 262);
            panelSimulation.TabIndex = 1;
            // 
            // textBoxTimeInterval
            // 
            textBoxTimeInterval.Location = new Point(358, 63);
            textBoxTimeInterval.Name = "textBoxTimeInterval";
            textBoxTimeInterval.Size = new Size(125, 27);
            textBoxTimeInterval.TabIndex = 2;
            // 
            // buttonStart
            // 
            buttonStart.Location = new Point(358, 245);
            buttonStart.Name = "buttonStart";
            buttonStart.Size = new Size(125, 29);
            buttonStart.TabIndex = 3;
            buttonStart.Text = "Start";
            buttonStart.UseVisualStyleBackColor = true;
            buttonStart.Click += buttonStart_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(buttonStart);
            Controls.Add(textBoxTimeInterval);
            Controls.Add(panelSimulation);
            Controls.Add(textBoxN);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBoxN;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Panel panelSimulation;
        private TextBox textBoxTimeInterval;
        private Button buttonStart;
    }
}
