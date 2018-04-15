using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace puzzle_game
{
    // 用來儲存BFS的Node
    class Node
    {
        public Node(Node parent, string[, ] tile, string direction = "")
        {
            this.parent = parent;
            this.tile = tile;
            this.direction = direction;
        }

        public Node parent = null;
        public string[,] tile = null;
        // 用來儲存, 這個Node是從parent的哪個方向產生的
        public string direction = "";

        public string toValue()
        {
            string temp = "";
            for(int i = 0; i < tile.GetLength(0); i++)
            {
                for(int j = 0; j < tile.GetLength(1); j++)
                {
                    temp += tile[i, j];
                }
            }

            return temp;
        }
    }
}
