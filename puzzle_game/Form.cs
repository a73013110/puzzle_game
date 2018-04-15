using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace puzzle_game
{
    public partial class Form : System.Windows.Forms.Form
    {
        // 視窗大小比例
        private float resize_times = 1;
        // 預設tile大小, 這邊的預設值沒用, 要去UI的textarea調
        private int TILE_ROW_SIZE = 3;
        private int TILE_COL_SIZE = 3;
        // 存放btn的Panel
        private TableLayoutPanel puzzle_panel = new TableLayoutPanel();
        // 儲存btn的陣列
        private Button[, ] btn = null;
        // 與btn的值相同, 方便後端處理
        public string[, ] tile = null;
        // 功能function
        private Function func = null;
        // 儲存目前哪個格子是空白的
        public int[] empty_tile_index = new int[2];

        public Form()
        {
            InitializeComponent();

            // 產生功能func
            func = new Function(this);
            // 創建Puzzle
            create_puzzle();
        }

        // 創建Puzzle
        public void create_puzzle()
        {
            TILE_ROW_SIZE = Int32.Parse(row_textarea.Text);
            TILE_COL_SIZE = Int32.Parse(col_textarea.Text);
            // 產生放置按鈕的Panel
            set_puzzle_panel();
            // 創建按鈕, 按鈕無值
            set_puzzle_btn();
            // 初始化Tile
            tile = func.initial_tile(tile);
            // 更新puzle, 賦予按鈕值
            update_puzzle();
            // 找到空白的tile
            empty_tile_index = func.find_empty_tile(tile);
        }

        // 創建Puzzle的Panel, 以放置btn
        public void set_puzzle_panel()
        {
            // 綁定上下左右
            puzzle_panel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            for (int i = 0; i < TILE_ROW_SIZE; i++)
            {
                puzzle_panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, ((float)100.0 / TILE_ROW_SIZE)));
            }
            puzzle_panel.ColumnCount = TILE_COL_SIZE;
            for (int i = 0; i < TILE_COL_SIZE; i++)
            {
                puzzle_panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, ((float)100.0 / TILE_COL_SIZE)));
            }
            puzzle_panel.Location = new System.Drawing.Point(37, 39);
            puzzle_panel.Name = "puzzle_panel";
            puzzle_panel.Size = new System.Drawing.Size(Width - 90, Height - 130);
            puzzle_panel.TabIndex = 10;
            this.Controls.Add(puzzle_panel);
        }

        // 創建Button
        public void set_puzzle_btn()
        {
            btn = new Button[TILE_ROW_SIZE, TILE_COL_SIZE];
            tile = new string[TILE_ROW_SIZE, TILE_COL_SIZE];
            // 產生Tile值, 在這邊創建按鈕沒有給予任何值, 在func.initial_tile的地方再賦予值
            //string[] btn_text = func.generate_btn_text();

            for (int i = 0; i < TILE_ROW_SIZE; i++)
            {
                for(int j = 0; j < TILE_COL_SIZE; j++)
                {
                    // 創建Button
                    btn[i, j] = create_btn("btn_" + i + j);
                    // 將Button加入Puzzle, 這邊注意一下: Column是j, Row是i
                    puzzle_panel.Controls.Add(btn[i, j], j, i);
                }
            }
        }

        // 創建按鈕, 預設為沒有文字
        public Button create_btn(string name, string number = "")
        {
            Button btn = new Button();
            btn.Font = new Font("微軟正黑體", 12 * resize_times, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            btn.Name = name;
            btn.Text = number;
            btn.FlatStyle = FlatStyle.Flat;
            // 置中填滿
            btn.Dock = DockStyle.Fill;
            // 這邊加入按鈕事件
            btn.Click += new EventHandler(btn_click);
            return btn;
        }

        // Puzzle按鈕事件
        private void btn_click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            string name = btn.Name;
            //System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            //sw.Start();
            int[] tile_index = { name[name.Length - 2] - '0', name[name.Length - 1] - '0' };    // 0.0008~0.0036ms
            //int[] tile_index = { int.Parse(name[name.Length - 2].ToString()), int.Parse(name[name.Length - 1].ToString()) }; sw.Stop(); // 0.0032~0.0041ms
            //TimeSpan ts2 = sw.Elapsed;
            //Console.WriteLine("總共耗時: {0}ms.", ts2.TotalMilliseconds);

            empty_tile_index = func.begin_move_tile(tile_index, empty_tile_index);

            // 若成功達成條件, 就不給動了
            if (func.check_puzzle_achieve_goal(tile))
            {
                puzzle_panel.Enabled = false;
            }
        }

        // 更新puzle
        public void update_puzzle()
        {
            for(int i = 0; i < TILE_ROW_SIZE; i++)
            {
                for(int j = 0; j < TILE_COL_SIZE; j++)
                {
                    btn[i, j].Text = tile[i, j];
                }
            }
        }

        // 當視窗大小改變, 也適當的改變Puzzle文字大小, 比較好看= =
        private void Form_resize(object sender, EventArgs e)
        {
            resize_times = Math.Max((float)(this.Width / 270.0), (float)(this.Height / 270.0));
            Font tmp_font = new Font("微軟正黑體", 12 * resize_times, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            //Console.WriteLine("Width: {0}, Height: {1}, Resize Times: {2}", this.Width, this.Height, resize_times);
            for (int i = 0; i < TILE_ROW_SIZE; i++)
            {
                for (int j = 0; j < TILE_COL_SIZE; j++)
                {
                    btn[i, j].Font = tmp_font;
                }
            }
        }

        // 移動tile, 分別調整儲存tile的陣列、畫面上的button
        public int[] move_tile(int empty_tile_row, int empty_tile_col, int tile_row, int tile_col)
        {
            tile[empty_tile_row, empty_tile_col] = tile[tile_row, tile_col];
            tile[tile_row, tile_col] = " ";
            btn[empty_tile_row, empty_tile_col].Text = tile[empty_tile_row, empty_tile_col];
            btn[tile_row, tile_col].Text = tile[tile_row, tile_col];
            //Console.WriteLine("[{0}, {1}]: {2}、{3}, [{4}, {5}]: {6}、{7}", empty_tile_row, empty_tile_col, tile[empty_tile_row, empty_tile_col], btn[empty_tile_row, empty_tile_col].Text, tile_row, tile_col, tile[tile_row, tile_col], btn[tile_row, tile_col].Text);
            // 更新空白的格子
            empty_tile_index[0] = tile_row;
            empty_tile_index[1] = tile_col;
            //Console.WriteLine("空的格子: [{0}, {1}]", empty_tile_index[0], empty_tile_index[1]);
            return empty_tile_index;
        }

        // 重置Puzzle大小
        private void tile_size_reset_btn_Click(object sender, EventArgs e)
        {
            // 初始化狀態
            this.Controls.Remove(puzzle_panel);
            plan_btn.Enabled = true;
            display_btn.Enabled = false;
            list_btn.Enabled = false;
            btn = null;
            tile = null;
            empty_tile_index = null;
            has_process_node = 0;
            plan_result = null;
            try
            {
                bw.Dispose();
                list_form.Dispose();
            }
            catch (Exception) { }

            // 再重新創建Puzzle
            puzzle_panel = new TableLayoutPanel();
            create_puzzle();
        }

        // 紀錄目前已處理多少Node
        private int has_process_node = 0;
        public void update_has_process_node() { has_process_node++; }
        public int get_has_process_node() { return has_process_node;  }

        //儲存規劃結果
        private object plan_result = null;
        public object get_plan_result() { return plan_result; }
        // 建立背景執行
        private BackgroundWorker bw;
        public BackgroundWorker get_bw() { return bw; }
        // 規劃路徑
        private void plan_btn_Click(object sender, EventArgs e)
        {
            // 初始化
            has_process_node = 0;
            // 產生背景執行Worker
            bw = new BackgroundWorker();
            // 產生進度的Form, 並輸入bw使進度視窗能夠控制
            Progress_form progress_form = new Progress_form(bw, this);
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += new DoWorkEventHandler((object sender_, DoWorkEventArgs e_) => {
                // 開始用BFS搜尋路徑
                plan_result = func.BFS(tile, this);
            });
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler((object sender_, RunWorkerCompletedEventArgs e_) => {
                if (plan_result != null)    // 找到解
                {
                    // 將終點的盤面(目標盤面)作為參數
                    progress_form.change_to_solved_status(((List<Node>)plan_result)[0]);
                    // 取得目標盤面後再反轉路徑->起點到終點
                    ((List<Node>)plan_result).Reverse();
                }
                else    // 無解
                {
                    progress_form.change_to_unsolved_status();
                }
            });
            bw.RunWorkerAsync();

            progress_form.ShowDialog();

            //Console.WriteLine("開始找解...");
            //// 找解
            //plan_result = func.BFS(tile);

            //new Progress_form().ShowDialog();
        }

        // 此function由Progress_form使用
        public void change_to_solved_status()
        {
            plan_btn.Enabled = false;
            display_btn.Enabled = true;
            list_btn.Enabled = true;
        }

        // 路徑演示
        private void display_btn_Click(object sender, EventArgs e)
        {
            // 儲存要交換的btn
            List<Tuple< Tuple<int, int>, Tuple<int, int>>> change_path = new List<Tuple<Tuple<int, int>, Tuple<int, int>>>();

            // 取得最原始盤面的空格位置
            int[] empty_index = (int[])empty_tile_index.Clone();
            foreach (var path in (List<Node>)plan_result)
            {
                Tuple<int, int> change_btn_1 = null;
                Tuple<int, int> change_btn_2 = null;

                change_btn_1 = new Tuple<int, int>(empty_index[0], empty_index[1]);
                if (path.direction == "上")
                {
                    change_btn_2 = new Tuple<int, int>(empty_index[0] - 1, empty_index[1]);
                }
                else if (path.direction == "下")
                {
                    change_btn_2 = new Tuple<int, int>(empty_index[0] + 1, empty_index[1]);
                }
                else if (path.direction == "左")
                {
                    change_btn_2 = new Tuple<int, int>(empty_index[0], empty_index[1] - 1);
                }
                else if (path.direction == "右")
                {
                    change_btn_2 = new Tuple<int, int>(empty_index[0], empty_index[1] + 1);
                }

                change_path.Add(new Tuple<Tuple<int, int>, Tuple<int, int>>(change_btn_1, change_btn_2));
                // 取得這個盤面的空格位置
                empty_index = func.find_empty_tile(path.tile);
            }

            // 打開路徑演示視窗
            new Display_path_form(this, tile, change_path).Show();
        }

        // 產生顯示路徑的視窗
        private List_path_form list_form = null;
        private void list_btn_Click(object sender, EventArgs e)
        {
            list_form = new List_path_form(this);
            list_form.Show();
        }
    }
}
