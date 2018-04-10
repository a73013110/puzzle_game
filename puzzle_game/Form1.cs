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
        private function func = new function();
        // 儲存目前哪個格子是空白的
        public int[] empty_tile_index = new int[2];

        public Form()
        {
            InitializeComponent();

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
            puzzle_panel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            puzzle_panel.RowCount = TILE_ROW_SIZE;
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
            puzzle_panel.Size = new System.Drawing.Size(Width - 90, Height - 90);
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
            //btn.Location = new Point(3, 4);
            btn.Margin = new Padding(3, 4, 3, 4);
            btn.Name = name;
            btn.Size = new Size(60, 60);
            btn.TabIndex = 0;
            btn.Text = number;
            btn.UseVisualStyleBackColor = true;
            // 置中填滿
            btn.Dock = DockStyle.Fill;
            // 這邊加入按鈕事件
            btn.Click += new EventHandler(btn_click);
            return btn;
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
            Console.WriteLine("Width: {0}, Height: {1}, Resize Times: {2}", this.Width, this.Height, resize_times);
            for (int i = 0; i < TILE_ROW_SIZE; i++)
            {
                for (int j = 0; j < TILE_COL_SIZE; j++)
                {
                    btn[i, j].Font = tmp_font;
                }
            }
        }

        // 按鈕事件
        private void btn_click(object sender, EventArgs e)
        {
            System.Windows.Forms.Button btn = sender as System.Windows.Forms.Button;
            string name = btn.Name;
            //System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            //sw.Start();
            int[] tile_index = { name[name.Length - 2] - '0', name[name.Length - 1] - '0' };    // 0.0008~0.0036ms
            //int[] tile_index = { int.Parse(name[name.Length - 2].ToString()), int.Parse(name[name.Length - 1].ToString()) }; sw.Stop(); // 0.0032~0.0041ms
            //TimeSpan ts2 = sw.Elapsed;
            //Console.WriteLine("總共耗時: {0}ms.", ts2.TotalMilliseconds);
            Console.WriteLine("按下: " + tile_index[0] + " " + tile_index[1]);

            if(tile_index[0] - 1 < 0 && tile_index[1] - 1 < 0)  // 左上角的按鈕
            {
                // 根據其位置, 簡單過濾出可能往哪邊移動, EX: 左上角按鈕, 可能往右邊或下方移動
                find_move_tile(tile_index, false, true, false, true);
                Console.WriteLine("觸發左上");
            }
            else if(tile_index[0] - 1 < 0 && tile_index[1] + 1 >= TILE_COL_SIZE)    // 右上角的按鈕
            {
                find_move_tile(tile_index, false, true, true, false);
                Console.WriteLine("觸發右上");
            }
            else if (tile_index[0] + 1 >= TILE_ROW_SIZE && tile_index[1] - 1 < 0)    // 左下角的按鈕
            {
                find_move_tile(tile_index, true, false, false, true);
                Console.WriteLine("觸發左下");
            }
            else if (tile_index[0] + 1 >= TILE_ROW_SIZE && tile_index[1] + 1 >= TILE_COL_SIZE)    // 右下角的按鈕
            {
                find_move_tile(tile_index, true, false, true, false);
                Console.WriteLine("觸發右下");
            }
            else if (tile_index[0] - 1 < 0)    //上方的按鈕
            {
                find_move_tile(tile_index, false, true, true, true);
                Console.WriteLine("觸發上");
            }
            else if (tile_index[0] + 1 >= TILE_ROW_SIZE)    // 下方的按鈕
            {
                find_move_tile(tile_index, true, false, true, true);
                Console.WriteLine("觸發下");
            }
            else if (tile_index[1] - 1 < 0)    // 左邊的按鈕
            {
                find_move_tile(tile_index, true, true, false, true);
                Console.WriteLine("觸發左");
            }
            else if (tile_index[1] + 1 >= TILE_COL_SIZE)    // 右邊的按鈕
            {
                find_move_tile(tile_index, true, true, true, false);
                Console.WriteLine("觸發右");
            }
            else    // 其他種類(該按鈕周圍皆有按鈕)
            {
                find_move_tile(tile_index, true, true, true, true);
                Console.WriteLine("觸發正常");
            }

            // 若成功達成條件, 就不給動了
            if (func.check_puzzle_achieve_goal(tile))
            {
                puzzle_panel.Enabled = false;
            }

            // Debug 顯示目前盤面
            //for (int i = 0; i < TILE_ROW_SIZE; i++)
            //{
            //    string message = "";
            //    for (int j = 0; j < TILE_COL_SIZE; j++)
            //    {
            //        message += tile[i, j] + " ";
            //    }
            //    Console.WriteLine(message);
            //}
        }

        // 判斷上下左右是否是空白, 找出要交換的tile
        public void find_move_tile(int[] tile_index, bool top, bool down, bool left, bool right)
        {
            if (top)
            {
                if (tile_index[0] - 1 == empty_tile_index[0] && tile_index[1] == empty_tile_index[1])    // 上面空白
                {
                    move_tile(empty_tile_index[0], empty_tile_index[1], tile_index[0], tile_index[1]);
                    return;
                }
            }
            if (down)
            {
                if (tile_index[0] + 1 == empty_tile_index[0] && tile_index[1] == empty_tile_index[1])    // 下面空白
                {
                    move_tile(empty_tile_index[0], empty_tile_index[1], tile_index[0], tile_index[1]);
                    return;
                }
            }
            if (left)
            {
                if (tile_index[0] == empty_tile_index[0] && tile_index[1] - 1 == empty_tile_index[1])   // 左邊空白
                {
                    move_tile(empty_tile_index[0], empty_tile_index[1], tile_index[0], tile_index[1]);
                    return;
                }
            }
            if (right)
            {
                if (tile_index[0] == empty_tile_index[0] && tile_index[1] + 1 == empty_tile_index[1])   // 右邊空白
                {
                    move_tile(empty_tile_index[0], empty_tile_index[1], tile_index[0], tile_index[1]);
                    return;
                }
            }
        }

        // 移動tile, 分別調整儲存tile的陣列、畫面上的button
        public void move_tile(int empty_tile_row, int empty_tile_col, int tile_row, int tile_col)
        {
            tile[empty_tile_row, empty_tile_col] = tile[tile_row, tile_col];
            tile[tile_row, tile_col] = "";
            btn[empty_tile_row, empty_tile_col].Text = tile[empty_tile_row, empty_tile_col];
            btn[tile_row, tile_col].Text = tile[tile_row, tile_col];
            Console.WriteLine("[{0}, {1}]: {2}、{3}, [{4}, {5}]: {6}、{7}", empty_tile_row, empty_tile_col, tile[empty_tile_row, empty_tile_col], btn[empty_tile_row, empty_tile_col].Text, tile_row, tile_col, tile[tile_row, tile_col], btn[tile_row, tile_col].Text);
            // 更新空白的格子
            empty_tile_index[0] = tile_row;
            empty_tile_index[1] = tile_col;
            Console.WriteLine("空的格子: [{0}, {1}]", empty_tile_index[0], empty_tile_index[1]);
        }

        // 重置Puzzle大小
        private void tile_size_reset_btn_Click(object sender, EventArgs e)
        {
            // 先清空TablelayoutPanel的欄跟列
            this.Controls.Remove(puzzle_panel);
            puzzle_panel = new TableLayoutPanel();
            // 再重新創建Puzzle
            create_puzzle();
        }

        private void debug()
        {
            Console.WriteLine("test");
        }
    }
}
