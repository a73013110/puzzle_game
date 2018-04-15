namespace puzzle_game
{
    partial class Progress_form
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
            this.progress_bar = new System.Windows.Forms.ProgressBar();
            this.message_label = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.cancel_btn = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.display_result_panel = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // progress_bar
            // 
            this.progress_bar.Location = new System.Drawing.Point(6, 29);
            this.progress_bar.Margin = new System.Windows.Forms.Padding(6, 3, 5, 3);
            this.progress_bar.Name = "progress_bar";
            this.progress_bar.Size = new System.Drawing.Size(220, 24);
            this.progress_bar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progress_bar.TabIndex = 0;
            // 
            // message_label
            // 
            this.message_label.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.message_label.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.message_label.Location = new System.Drawing.Point(2, 2);
            this.message_label.Margin = new System.Windows.Forms.Padding(2, 2, 2, 0);
            this.message_label.Name = "message_label";
            this.message_label.Size = new System.Drawing.Size(227, 24);
            this.message_label.TabIndex = 1;
            this.message_label.Text = "已處理節點數: 0";
            this.message_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 300;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // cancel_btn
            // 
            this.cancel_btn.Location = new System.Drawing.Point(553, 4);
            this.cancel_btn.Margin = new System.Windows.Forms.Padding(89, 4, 0, 4);
            this.cancel_btn.Name = "cancel_btn";
            this.cancel_btn.Size = new System.Drawing.Size(55, 28);
            this.cancel_btn.TabIndex = 2;
            this.cancel_btn.Text = "取消";
            this.cancel_btn.UseVisualStyleBackColor = true;
            this.cancel_btn.Click += new System.EventHandler(this.cancel_btn_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.message_label);
            this.flowLayoutPanel1.Controls.Add(this.progress_bar);
            this.flowLayoutPanel1.Controls.Add(this.display_result_panel);
            this.flowLayoutPanel1.Controls.Add(this.cancel_btn);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(231, 93);
            this.flowLayoutPanel1.TabIndex = 4;
            // 
            // display_result_panel
            // 
            this.display_result_panel.Location = new System.Drawing.Point(234, 3);
            this.display_result_panel.Name = "display_result_panel";
            this.display_result_panel.Size = new System.Drawing.Size(227, 227);
            this.display_result_panel.TabIndex = 4;
            this.display_result_panel.Visible = false;
            // 
            // Progress_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(231, 93);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "Progress_form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "正在規劃路徑";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Progress_form_FormClosing);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar progress_bar;
        private System.Windows.Forms.Label message_label;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Button cancel_btn;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel display_result_panel;
    }
}