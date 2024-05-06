namespace physicsGameConcept
{
    partial class platformerGame
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
            this.components = new System.ComponentModel.Container();
            this.gameTimer = new System.Windows.Forms.Timer(this.components);
            this.npcTextOutput = new System.Windows.Forms.Label();
            this.timerLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // gameTimer
            // 
            this.gameTimer.Interval = 15;
            this.gameTimer.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // npcTextOutput
            // 
            this.npcTextOutput.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.npcTextOutput.AutoSize = true;
            this.npcTextOutput.BackColor = System.Drawing.Color.White;
            this.npcTextOutput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.npcTextOutput.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.npcTextOutput.Location = new System.Drawing.Point(285, 9);
            this.npcTextOutput.Name = "npcTextOutput";
            this.npcTextOutput.Size = new System.Drawing.Size(48, 15);
            this.npcTextOutput.TabIndex = 0;
            this.npcTextOutput.Text = "npcText";
            this.npcTextOutput.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // timerLabel
            // 
            this.timerLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.timerLabel.BackColor = System.Drawing.Color.Gray;
            this.timerLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.timerLabel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.timerLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.timerLabel.Location = new System.Drawing.Point(496, 9);
            this.timerLabel.Name = "timerLabel";
            this.timerLabel.Size = new System.Drawing.Size(144, 22);
            this.timerLabel.TabIndex = 1;
            this.timerLabel.Text = "npcText";
            this.timerLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // platformerGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(652, 614);
            this.Controls.Add(this.timerLabel);
            this.Controls.Add(this.npcTextOutput);
            this.Cursor = System.Windows.Forms.Cursors.No;
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "platformerGame";
            this.Text = "Game :D";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer gameTimer;
        private System.Windows.Forms.Label npcTextOutput;
        private System.Windows.Forms.Label timerLabel;
    }
}

