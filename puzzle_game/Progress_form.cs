using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace puzzle_game
{
    public partial class Progress_form : System.Windows.Forms.Form
    {
        public Progress_form(BackgroundWorker bw, Form form)
        {
            InitializeComponent();

            this.bw = bw;
            this.form = form;
        }

        private BackgroundWorker bw;
        private Form form;
        private string message = "正在規劃路徑";

        private void timer_Tick(object sender, EventArgs e)
        {            
            if (this.Text.Length < 9)
            {
                this.Text += ".";
            }
            else
            {
                this.Text = message;
            }

            message_label.Text = "已處理節點數: " + form.get_has_process_node();
        }

        public void set_message(int has_process_node)
        {
            this.message_label.Text = has_process_node.ToString();
        }

        // 取消搜尋路徑
        private void cancel_btn_Click(object sender, EventArgs e)
        {
            bw.CancelAsync();
            this.Dispose();
        }

        private void Progress_form_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 將關閉視窗的事件, 強制產生點擊取消按鈕的事件
            cancel_btn.PerformClick();
        }

        // 找到解的狀態
        public void change_to_solved_status(object goal)
        {
            timer.Enabled = false;
            this.Text = "找到解";
            progress_bar.Style = ProgressBarStyle.Blocks;
            progress_bar.Value = 100;
            cancel_btn.Text = "確定";
            cancel_btn.Click -= cancel_btn_Click;
            cancel_btn.Click += new EventHandler((object sender_, EventArgs e_) => {
                this.Hide();
                form.change_to_solved_status();
            });

            // 加入目標盤面
            set_display_result_panel(goal);
            // 調整視窗位置
            this.Location = new Point(Location.X, Location.Y - display_result_panel.Height / 2);
        }

        // 加入目標盤面
        private void set_display_result_panel(object goal)
        {
            int MARGIN_TOP = 4;
            this.Height = Height + MARGIN_TOP + display_result_panel.Height + MARGIN_TOP;

            // 先抓到tile (這邊只用得到這個)
            string[,] tile = ((Node)goal).tile;
            int ROW = tile.GetLength(0);
            int COL = tile.GetLength(1);
            TableLayoutPanel display_result_tablepanel = new TableLayoutPanel();
            display_result_tablepanel.Dock = DockStyle.Fill;
            // 開始設定盤面
            for (int i = 0; i < ROW; i++)
            {
                display_result_tablepanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, ((float)100.0 / ROW)));
            }
            display_result_tablepanel.ColumnCount = COL;
            for (int i = 0; i < COL; i++)
            {
                display_result_tablepanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, ((float)100.0 / COL)));
            }
            for (int i = 0; i < ROW; i++)
            {
                for (int j = 0; j < COL; j++)
                {
                    // 將Button加入Puzzle, 這邊注意一下: Column是j, Row是i
                    display_result_tablepanel.Controls.Add(create_display_result_btn(tile[i, j]), j, i);
                }
            }

            display_result_panel.Controls.Add(display_result_tablepanel);
            display_result_panel.Visible = true;
        }

        private Button create_display_result_btn(string number = "")
        {
            Button btn = new Button();
            btn.Font = new Font("微軟正黑體", 12, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            btn.Text = number;
            // 置中填滿
            btn.Dock = DockStyle.Fill;
            return btn;
        }

        // 無解的狀態
        public void change_to_unsolved_status()
        {
            timer.Enabled = false;
            message_label.Text = "無解";
            progress_bar.Style = ProgressBarStyle.Blocks;
            progress_bar.Value = 100;
            cancel_btn.Text = "確定";
        }
    }
}
