namespace puzzle_game
{
    partial class Display_path_form
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
            this.display_panel = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.play_btn = new System.Windows.Forms.Button();
            this.stop_btn = new System.Windows.Forms.Button();
            this.reset_btn = new System.Windows.Forms.Button();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // display_panel
            // 
            this.display_panel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.display_panel.Location = new System.Drawing.Point(12, 12);
            this.display_panel.Name = "display_panel";
            this.display_panel.Size = new System.Drawing.Size(260, 261);
            this.display_panel.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.play_btn);
            this.flowLayoutPanel1.Controls.Add(this.stop_btn);
            this.flowLayoutPanel1.Controls.Add(this.reset_btn);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 279);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(260, 27);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // play_btn
            // 
            this.play_btn.Location = new System.Drawing.Point(3, 3);
            this.play_btn.Name = "play_btn";
            this.play_btn.Size = new System.Drawing.Size(81, 23);
            this.play_btn.TabIndex = 0;
            this.play_btn.Text = "播放";
            this.play_btn.UseVisualStyleBackColor = true;
            this.play_btn.Click += new System.EventHandler(this.play_btn_Click);
            // 
            // stop_btn
            // 
            this.stop_btn.Enabled = false;
            this.stop_btn.Location = new System.Drawing.Point(90, 3);
            this.stop_btn.Name = "stop_btn";
            this.stop_btn.Size = new System.Drawing.Size(81, 23);
            this.stop_btn.TabIndex = 1;
            this.stop_btn.Text = "暫停";
            this.stop_btn.UseVisualStyleBackColor = true;
            this.stop_btn.Click += new System.EventHandler(this.stop_btn_Click);
            // 
            // reset_btn
            // 
            this.reset_btn.Enabled = false;
            this.reset_btn.Location = new System.Drawing.Point(177, 3);
            this.reset_btn.Name = "reset_btn";
            this.reset_btn.Size = new System.Drawing.Size(79, 23);
            this.reset_btn.TabIndex = 2;
            this.reset_btn.Text = "重置";
            this.reset_btn.UseVisualStyleBackColor = true;
            this.reset_btn.Click += new System.EventHandler(this.reset_btn_Click);
            // 
            // Display_path_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 310);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.display_panel);
            this.Name = "Display_path_form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "路徑演示";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Display_path_form_FormClosing);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel display_panel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button play_btn;
        private System.Windows.Forms.Button stop_btn;
        private System.Windows.Forms.Button reset_btn;
    }
}