using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicShot_Roulette.Map
{
    public enum NodeType
    {
        None,
        Start,
        Battle,
        Event,
        Rest,
        Boss
    }
    public class Node
    {
        public static Node Zero = new Node(NodeType.None);
        public NodeType NodeType { get; set; }
        private List<(int,int)> Nexts = new List<(int, int)>();
        public Node(NodeType type)
        {
            this.NodeType = type;
        }
        public void AddNextNode((int, int) nodePos)
        {
            Nexts.Add(nodePos);
        }
        public List<(int, int)> GetNextNodes()
        {
            return Nexts;
        }


    }
    public class Floor
    {
        public readonly int ROWCOUNT = 3;
        public int ColumnCount { get; set; }
        Node[,] tables;
        (int, int) PlayerPosition;

        public void SetFirstFloor()
        {
            ColumnCount = 6;
            ClearTable(ColumnCount);
            tables[1, 0] = new Node(NodeType.Start);
            tables[1, 1] = new Node(NodeType.Battle);
            tables[1, 2] = new Node(NodeType.Event);
            tables[1, 3] = new Node(NodeType.Battle);
            tables[1, 4] = new Node(NodeType.Rest);
            tables[1, 5] = new Node(NodeType.Boss);

            for (int i = 0; i < 5; i++)
            {
                tables[1, i].AddNextNode((0,1));
            } 
        }

        public void SetLastFloor()
        {
            ColumnCount = 9;
            ClearTable(ColumnCount);
            tables[1, 0] = new Node(NodeType.Start); tables[1, 0].AddNextNode((0, 1));  tables[1, 0].AddNextNode((1, 1)); 
            tables[1, 1] = new Node(NodeType.Battle); tables[1, 1].AddNextNode((-1, 1)); tables[1, 1].AddNextNode((0, 1));
            tables[2, 1] = new Node(NodeType.Battle); tables[2, 1].AddNextNode((0, 1));

            tables[0, 2] = new Node(NodeType.Battle); tables[0, 2].AddNextNode((0, 1));
            tables[1, 2] = new Node(NodeType.Event);  tables[1, 2].AddNextNode((0, 1));
            tables[2, 2] = new Node(NodeType.Battle); tables[2, 2].AddNextNode((0, 1));

            tables[0, 3] = new Node(NodeType.Rest);   tables[0, 3].AddNextNode((0, 1));
            tables[1, 3] = new Node(NodeType.Battle); tables[1, 3].AddNextNode((0, 1));
            tables[2, 3] = new Node(NodeType.Event);  tables[2, 3].AddNextNode((-1, 1));

            tables[0, 4] = new Node(NodeType.Battle); tables[0, 4].AddNextNode((0, 1)); tables[0, 4].AddNextNode((1, 1));
            tables[1, 4] = new Node(NodeType.Rest); tables[1, 4].AddNextNode((-1, 1));  tables[1, 4].AddNextNode((0, 1)); 

            tables[0, 5] = new Node(NodeType.Event);  tables[0, 5].AddNextNode((0, 1)); 
            tables[1, 5] = new Node(NodeType.Battle); tables[1, 5].AddNextNode((0, 1)); tables[1, 5].AddNextNode((1, 1));

            tables[0, 6] = new Node(NodeType.Battle); tables[0, 6].AddNextNode((0, 1));
            tables[1, 6] = new Node(NodeType.Event); tables[1, 6].AddNextNode((-1, 1));  tables[1, 6].AddNextNode((1, 1)); 
            tables[2, 6] = new Node(NodeType.Battle); tables[2, 6].AddNextNode((0, 1));

            tables[0, 7] = new Node(NodeType.Rest);  tables[0, 7].AddNextNode((1, 1));
            tables[2, 7] = new Node(NodeType.Battle);tables[2, 7].AddNextNode((-1, 1));

            tables[1, 8] = new Node(NodeType.Boss); 
        }
        public void SetPlayerPosition(int row, int col)
        {
            PlayerPosition = (row, col);
        }

        public (int,int) GetPlayerPosition()
        {
            return PlayerPosition;
        }
        public Node GetNode(int row, int col)
        {
            if (row >= ROWCOUNT || row < 0) return null;
            if (col >= ColumnCount || col < 0) return null;
            return tables[row, col];
        }

        private void ClearTable(int col)
        {
            tables = new Node[ROWCOUNT, col];
            for (int i = 0; i < ROWCOUNT; i++)
            {
                for(int j = 0; j < col; j++)
                {
                    tables[i, j] = Node.Zero;
                }
            }
        }
    }
}
