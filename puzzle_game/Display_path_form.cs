using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace puzzle_game
{
    public partial class Display_path_form : System.Windows.Forms.Form
    {
        public Display_path_form(Form form, string[,] tile, List<Tuple<Tuple<int, int>, Tuple<int, int>>> change_path)
        {
            InitializeComponent();

            this.form = form;
            this.tile = tile;
            this.change_path = change_path;
            // 重新設定視窗位置
            set_form_loction();
            // 產生盤面
            set_puzzle_panel();
        }

        private Form form;
        private NoFocusButton[,] btn;
        private string[,] tile;
        private List<Tuple<Tuple<int, int>, Tuple<int, int>>> change_path;

        public class NoFocusButton : Button
        {
            public override void NotifyDefault(bool value)
            {
                base.NotifyDefault(false);
            }
        }

        private void set_form_loction()
        {
            Point form_loc = form.Location;
            Size form_size = form.Size;
            this.Location = new Point(form_loc.X - this.Width + 15, form_loc.Y);
            this.Height = form_size.Height;
        }

        private TableLayoutPanel display_result_tablepanel;
        private void set_puzzle_panel()
        {
            int ROW = tile.GetLength(0);
            int COL = tile.GetLength(1);
            btn = new NoFocusButton[ROW, COL];

            display_result_tablepanel = new TableLayoutPanel();
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
                    btn[i, j] = create_display_result_btn(tile[i, j]);
                    // 將Button加入Puzzle, 這邊注意一下: Column是j, Row是i
                    display_result_tablepanel.Controls.Add(btn[i, j], j, i);
                }
            }

            display_panel.Controls.Add(display_result_tablepanel);
        }

        private NoFocusButton create_display_result_btn(string number = " ")
        {
            NoFocusButton btn = new NoFocusButton();
            btn.Font = new Font("微軟正黑體", 12, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            btn.Text = number;
            // 置中填滿
            btn.Dock = DockStyle.Fill;
            btn.FlatStyle = FlatStyle.Flat;
            return btn;
        }

        private void play_btn_Click(object sender, EventArgs e)
        {
            display_path();
        }

        private BackgroundWorker bw = null;
        private System.Timers.Timer timer = null;
        private void display_path()
        {
            if (bw == null)
            {
                // 產生背景執行Worker
                bw = new BackgroundWorker();
                bw.WorkerSupportsCancellation = true;
                bw.DoWork += (sender_, e_) =>
                {
                    foreach (var path in change_path)
                    {
                        // path.Item? , ? =1(空白按鈕) or 2(另一個按鈕) 是某個按鈕. EX: path.Item1.Item? , ? =1(X座標) or 2(Y座標)
                        Console.WriteLine("{0}, {1} <=> {2}, {3}", path.Item1.Item1, path.Item1.Item2, path.Item2.Item1, path.Item2.Item2);
                        // 產生timer
                        timer = new System.Timers.Timer();
                        timer.Interval = 250;
                        timer.Elapsed += (sender__, e__) => elapsed_mathod(sender__, e__, path);
                        timer.Start();
                        if (bw.CancellationPending) { return; }
                        while (stop) { }
                        Thread.Sleep(1000);
                        if (bw.CancellationPending) { return; }
                        while (stop) { }
                        timer.Dispose();
                        // 按鈕交換
                        this.BeginInvoke(new MethodInvoker(() =>
                        {
                            move_tile(path.Item1.Item1, path.Item1.Item2, path.Item2.Item1, path.Item2.Item2);
                            unhighlight_button(path.Item1.Item1, path.Item1.Item2, path.Item2.Item1, path.Item2.Item2);
                        }));
                    }
                };
                bw.RunWorkerAsync();
            }
            else    // 取消暫停
            {
                stop = false;
            }
            play_btn.Enabled = false;
            stop_btn.Enabled = true;
            reset_btn.Enabled = true;
        }

        private void elapsed_mathod(object sender, System.Timers.ElapsedEventArgs e, object path_)
        {
            Tuple<Tuple<int, int>, Tuple<int, int>> path = (Tuple<Tuple<int, int>, Tuple<int, int>>)path_;
            if (btn[path.Item1.Item1, path.Item1.Item2].FlatAppearance.BorderSize != 3)
            {
                highlight_button(path.Item1.Item1, path.Item1.Item2, path.Item2.Item1, path.Item2.Item2);
            }
            else
            {
                unhighlight_button(path.Item1.Item1, path.Item1.Item2, path.Item2.Item1, path.Item2.Item2);
            }
        }

        // 把要交換的按鈕用紅色框框框起來
        private void highlight_button(int x1, int y1, int x2, int y2)
        {
            btn[x1, y1].FlatAppearance.BorderSize = 3;
            btn[x2, y2].FlatAppearance.BorderSize = 3;
            btn[x1, y1].FlatAppearance.BorderColor = Color.FromArgb(255, 0, 0);
            btn[x2, y2].FlatAppearance.BorderColor = Color.FromArgb(255, 0, 0);
        }

        // 取消紅色框框
        private void unhighlight_button(int x1, int y1, int x2, int y2)
        {
            btn[x1, y1].FlatAppearance.BorderSize = 1;
            btn[x2, y2].FlatAppearance.BorderSize = 1;
            btn[x1, y1].FlatAppearance.BorderColor = Color.FromArgb(0, 0, 0);
            btn[x2, y2].FlatAppearance.BorderColor = Color.FromArgb(0, 0, 0);
        }

        private void move_tile(int x1, int y1, int x2, int y2)
        {
            btn[x1, y1].Text = btn[x2, y2].Text;
            btn[x2, y2].Text = " ";
        }

        private bool stop = false;
        private void stop_btn_Click(object sender, EventArgs e)
        {
            stop = true;
            play_btn.Enabled = true;
            stop_btn.Enabled = false;
        }

        private void reset_btn_Click(object sender, EventArgs e)
        {
            // 重置盤面
            timer.Dispose();
            bw.CancelAsync();
            bw.Dispose();
            display_panel.Controls.Remove(display_result_tablepanel);
            set_puzzle_panel();
            bw = null;
            stop = false;
            play_btn.Enabled = true;
            stop_btn.Enabled = false;
            reset_btn.Enabled = false;
        }

        private void Display_path_form_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer.Dispose();
            bw.CancelAsync();
            bw.Dispose();
        }
    }
}
