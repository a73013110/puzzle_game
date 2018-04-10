using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace puzzle_game
{
    class function
    {
        public function()
        {

        }

        public string [] generate_btn_text(int tile_size = 0)
        {
            string[] init_text = { "1", "2", "3", "4", "5", "6", "7", "8", "" };
            return init_text;
        }

        // 初始化盤面
        public string[, ] initial_tile(string[, ] tile)
        {
            int ROW = tile.GetLength(0);
            int COL = tile.GetLength(1);
            // 產生準備用來隨機排列值得array
            string[] rand_array = new string[tile.Length];
            // 先按照順序賦予值, 並把值為0的陣列設為空白
            for (int i = 0; i < tile.Length; i++)
            {
                rand_array[i] = i.ToString();
            }
            rand_array[0] = "";
            Random rand = new Random();
            // 隨機排列
            rand_array = rand_array.OrderBy(x => rand.Next()).ToArray();
            // 1-D to 2-D
            string[,] result = new string[ROW, COL];
            for (int i = 0, index = 0; i < ROW; i++)    // 因為當row!=col時, 如果用rand_array[i*row+j]取值, 會發現有問題, 因此額外使用index變數來記錄rand_array目前取到哪
            {
                for (int j = 0; j < COL; j++, index++)
                {
                    result[i, j] = rand_array[index];
                }
            }

            return result;
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
                    if(tile[i, j] == "")
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
            for (int i = 0, index = 0; i < ROW; i++)
            {
                for (int j = 0; j < COL; j++, index++)
                {
                    if (i == 0 && j == 0)
                    {
                        if (tile[i, j] != "")
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
    }
}
