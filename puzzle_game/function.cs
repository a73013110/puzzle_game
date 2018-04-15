using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace puzzle_game
{
    class Function
    {
        public Function(Form form)
        {
            this.form = form;
        }

        private Form form = null;

        //public string [] generate_btn_text(int tile_size = 0)
        //{
        //    string[] init_text = { "1", "2", "3", "4", "5", "6", "7", "8", "" };
        //    return init_text;
        //}

        private int TILE_ROW_SIZE = 3;
        private int TILE_COL_SIZE = 3;
        // 初始化盤面
        public string[, ] initial_tile(string[, ] tile)
        {
            int TILE_ROW_SIZE = tile.GetLength(0);
            int TILE_COL_SIZE = tile.GetLength(1);
            // 產生準備用來隨機排列值得array
            string[] rand_array = new string[tile.Length];
            // 先按照順序賦予值, 並把值為0的陣列設為空白
            for (int i = 0; i < tile.Length; i++)
            {
                rand_array[i] = i.ToString();
            }
            rand_array[0] = " ";
            Random rand = new Random();
            // 隨機排列
            rand_array = rand_array.OrderBy(x => rand.Next()).ToArray();
            // 1-D to 2-D
            tile = new string[TILE_ROW_SIZE, TILE_COL_SIZE];
            for (int i = 0, index = 0; i < TILE_ROW_SIZE; i++)    // 因為當row!=col時, 如果用rand_array[i*row+j]取值, 會發現有問題, 因此額外使用index變數來記錄rand_array目前取到哪
            {
                for (int j = 0; j < TILE_COL_SIZE; j++, index++)
                {
                    tile[i, j] = rand_array[index];
                }
            }

            return tile;
        }

        // 找到盤面空白的
        public int[] find_empty_tile(string[, ] tile)
        {
            int[] empty = { -1, -1 };
            int ROW = tile.GetLength(0);
            int COL = tile.GetLength(1);
            for (int i = 0; i < ROW; i++)
            {
                for (int j = 0; j < COL; j++)
                {
                    if(tile[i, j] == " ")
                    {
                        empty[0] = i;
                        empty[1] = j;
                        return empty;
                    }
                }
            }
            return empty;
        }

        // 檢查是否達到目標盤面
        public bool check_puzzle_achieve_goal(string[,] tile)
        {
            int ROW = tile.GetLength(0);
            int COL = tile.GetLength(1);
            for (int i = 0, index = 1; i < ROW; i++)
            {
                for (int j = 0; j < COL; j++, index++)
                {
                    if (i == ROW - 1 && j == COL - 1)
                    {
                        if (tile[i, j] != " ")
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (tile[i, j] != index.ToString())
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        // 儲存目標盤面
        private string[,] goal_1 = null;
        private string[,] goal_2 = null;
        // 產生目標盤面
        private void generate_goal()
        {
            goal_1 = new string[TILE_ROW_SIZE, TILE_COL_SIZE];
            for (int i = 0, index = 1; i < TILE_ROW_SIZE; i++)
            {
                for (int j = 0; j < TILE_COL_SIZE; j++, index++)
                {
                    goal_1[i, j] = index.ToString();
                }
            }
            goal_1[TILE_ROW_SIZE - 1, TILE_COL_SIZE - 1] = " ";
            // 產生第二個目標盤面
            goal_2 = (string[,])goal_1.Clone();
            move_tile(ref goal_2[TILE_ROW_SIZE - 1, TILE_COL_SIZE - 3], ref goal_2[TILE_ROW_SIZE - 1, TILE_COL_SIZE - 2]);
        }

        // 移動tile的function-----------------------------------------------------------------------------------------------------------------------------------------------

        // 取得要移動的格子後, 準備移動
        public int[] begin_move_tile(int[] tile_index, int[] empty_tile_index)
        {
            //Console.WriteLine("按下: " + tile_index[0] + " " + tile_index[1]);

            if (tile_index[0] - 1 < 0 && tile_index[1] - 1 < 0)  // 左上角的按鈕
            {
                //Console.WriteLine("觸發左上");
                // 根據其位置, 簡單過濾出可能往哪邊移動, EX: 左上角按鈕, 可能往右邊或下方移動
                return find_move_tile(tile_index, empty_tile_index, false, true, false, true);
            }
            else if (tile_index[0] - 1 < 0 && tile_index[1] + 1 >= TILE_COL_SIZE)    // 右上角的按鈕
            {
                //Console.WriteLine("觸發右上");
                return find_move_tile(tile_index, empty_tile_index, false, true, true, false);
            }
            else if (tile_index[0] + 1 >= TILE_ROW_SIZE && tile_index[1] - 1 < 0)    // 左下角的按鈕
            {
                //Console.WriteLine("觸發左下");
                return find_move_tile(tile_index, empty_tile_index, true, false, false, true);
            }
            else if (tile_index[0] + 1 >= TILE_ROW_SIZE && tile_index[1] + 1 >= TILE_COL_SIZE)    // 右下角的按鈕
            {
                //Console.WriteLine("觸發右下");
                return find_move_tile(tile_index, empty_tile_index, true, false, true, false);
            }
            else if (tile_index[0] - 1 < 0)    //上方的按鈕
            {
                //Console.WriteLine("觸發上");
                return find_move_tile(tile_index, empty_tile_index, false, true, true, true);
            }
            else if (tile_index[0] + 1 >= TILE_ROW_SIZE)    // 下方的按鈕
            {
                //Console.WriteLine("觸發下");
                return find_move_tile(tile_index, empty_tile_index, true, false, true, true);
            }
            else if (tile_index[1] - 1 < 0)    // 左邊的按鈕
            {
                //Console.WriteLine("觸發左");
                return find_move_tile(tile_index, empty_tile_index, true, true, false, true);
            }
            else if (tile_index[1] + 1 >= TILE_COL_SIZE)    // 右邊的按鈕
            {
                //Console.WriteLine("觸發右");
                return find_move_tile(tile_index, empty_tile_index, true, true, true, false);
            }
            else    // 其他種類(該按鈕周圍皆有按鈕)
            {
                //Console.WriteLine("觸發正常");
                return find_move_tile(tile_index, empty_tile_index, true, true, true, true);
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
        public int[] find_move_tile(int[] tile_index, int[] empty_tile_index, bool top, bool down, bool left, bool right)
        {
            // 將function指定成另一個變數
            Func<int, int, int, int, int[]> move_tile = form.move_tile; ;

            if (top)
            {
                if (tile_index[0] - 1 == empty_tile_index[0] && tile_index[1] == empty_tile_index[1])    // 上面空白
                {
                    return move_tile(empty_tile_index[0], empty_tile_index[1], tile_index[0], tile_index[1]);
                }
            }
            if (down)
            {
                if (tile_index[0] + 1 == empty_tile_index[0] && tile_index[1] == empty_tile_index[1])    // 下面空白
                {
                    return move_tile(empty_tile_index[0], empty_tile_index[1], tile_index[0], tile_index[1]);
                }
            }
            if (left)
            {
                if (tile_index[0] == empty_tile_index[0] && tile_index[1] - 1 == empty_tile_index[1])   // 左邊空白
                {
                    return move_tile(empty_tile_index[0], empty_tile_index[1], tile_index[0], tile_index[1]);
                }
            }
            if (right)
            {
                if (tile_index[0] == empty_tile_index[0] && tile_index[1] + 1 == empty_tile_index[1])   // 右邊空白
                {
                    return move_tile(empty_tile_index[0], empty_tile_index[1], tile_index[0], tile_index[1]);
                }
            }
            // 沒有動的情況
            return empty_tile_index;
        }

        // Auto-plan------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        
        // BFS窮舉, 傳入起始盤面
        public List<Node> BFS(string[,] root_tile, Form form)
        {
            Queue<Node> queue = new Queue<Node>();
            // 儲存走過的盤面(狀態), 避免重複行走
            SortedList<string, bool> book = new SortedList<string, bool>();
            // 產生終點盤面
            generate_goal();
            // 設定終點Node
            Node end_1 = new Node(null, goal_1);
            Node end_2 = new Node(null, goal_2);
            // 設定起點Node
            Node start = new Node(null, root_tile);
            // 把起點加入Queue
            queue.Enqueue(start);
            // 把起點加入走過的路徑, 避免重複走
            book.Add(start.toValue(), true);

            // 先產生終點字串value(把盤面轉成1維)
            string end_value_1 = end_1.toValue();
            string end_value_2 = end_2.toValue();

            Node now = null;
            // 使用bw參考, 取得是否要停止規劃
            while (queue.Count > 0)
            {
                // 若終止搜尋
                if (form.get_bw().CancellationPending)
                {
                    return null;
                }
                // 把目前要找的Node抓出來
                now = queue.Dequeue();
                // 檢查是否到終點
                string now_value = now.toValue();
                if (now_value == end_value_1)
                {
                    //Console.WriteLine("找到解1!");
                    // 回溯法找路徑
                    return backtracking_path(now);
                }
                else if( now_value == end_value_2)
                {
                    //Console.WriteLine("找到解2!");
                    // 回溯法找路徑
                    return backtracking_path(now);
                }
                // 取得能走的小孩
                List<Node> children = generate_children(now);
                // 開始找小孩
                foreach (var child in children)
                {
                    string value = child.toValue();
                    // 若目前狀態沒有走過
                    if (!book.Keys.Contains(value))
                    {
                        // 把目前Node加入Queue
                        queue.Enqueue(child);
                        book.Add(value, true);
                    }
                }
                // 更新已搜尋節點數目
                form.update_has_process_node();
            }

            Console.WriteLine("無解!");
            return null;
        }

        // 產生孩子
        private List<Node> generate_children(Node now)
        {
            // 先找到目前空格
            int[] index = find_empty_tile(now.tile);
            int row = index[0];
            int col = index[1];
            // 記錄孩子的List
            List<Node> children = new List<Node>();
            // 小孩
            string[,] child;
            // 開始判斷空格能往哪走-------------------
            if (row > 0)    // 往上
            {
                // 先從parent複製相同的tile給child
                child = (string[, ])now.tile.Clone();
                // 與上面交換
                move_tile(ref child[row, col], ref child[row - 1, col]);
                // 加到孩子節點List
                children.Add(new Node(now, child, "上"));
            }
            if (row < TILE_ROW_SIZE - 1)    // 往下
            {
                child = (string[,])now.tile.Clone();
                move_tile(ref child[row, col], ref child[row + 1, col]);
                children.Add(new Node(now, child, "下"));
            }
            if (col > 0)    // 往左
            {
                child = (string[,])now.tile.Clone();
                move_tile(ref child[row, col], ref child[row, col - 1]);
                children.Add(new Node(now, child, "左"));
            }
            if (col < TILE_COL_SIZE - 1)    // 往右
            {
                child = (string[,])now.tile.Clone();
                move_tile(ref child[row, col], ref child[row, col + 1]);
                children.Add(new Node(now, child, "右"));
            }

            return children;
        }

        // 移動(交換), 使用指針方式交換
        private void move_tile<T>(ref T empty_tile, ref T tile)
        {
            T temp = empty_tile;
            empty_tile = tile;
            tile = temp;
        }

        // 回溯法找路徑
        private List<Node> backtracking_path(Node now)
        {
            List<Node> path_node = new List<Node>();
            while(now.parent != null)
            {
                path_node.Add(now);
                now = now.parent;
            }
            // 回傳的路徑是從終點到起點
            return path_node;
        }
    }
}
