using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace MagicShot_Roulette.Map
{
    public enum GridType
    {
        None,
        Player, 
        Horizontal,
        Vertical,
        HorizontalUp,
        HorizontalDown,
        Cross,
        VerticalLeft,
        VerticalRight,
        Perpendicular45,
        Perpendicular135,
        Perpendicular225,
        Perpendicular315,
    }
    public class FloorVisualizer
    {
        readonly string PADDING = "  ";
        public FloorVisualizer() { }
        private GridType[,] GridTable;
        public void Initiazlie(Floor floor)
        { 
            GridTable = new GridType[floor.ROWCOUNT, floor.ColumnCount];
            if(floor.ColumnCount < 8)
            {
                GridTable[1, 0] = GridType.None;
                GridTable[1, 1] = GridType.Horizontal;
                GridTable[1, 2] = GridType.Horizontal;
                GridTable[1, 3] = GridType.Horizontal;
                GridTable[1, 4] = GridType.Horizontal;
                GridTable[1, 5] = GridType.Horizontal;
            }
            else
            {
                GridTable[0, 0] = GridType.None;
                GridTable[1, 0] = GridType.None;
                GridTable[2, 0] = GridType.None;

                GridTable[0, 1] = GridType.None;
                GridTable[1, 1] = GridType.HorizontalDown;
                GridTable[2, 1] = GridType.Perpendicular45;

                GridTable[0, 2] = GridType.Perpendicular135;
                GridTable[1, 2] = GridType.HorizontalUp;
                GridTable[2, 2] = GridType.Horizontal;

                GridTable[0, 3] = GridType.Horizontal;
                GridTable[1, 3] = GridType.Horizontal;
                GridTable[2, 3] = GridType.Horizontal;

                GridTable[0, 4] = GridType.Horizontal;
                GridTable[1, 4] = GridType.HorizontalDown;
                GridTable[2, 4] = GridType.Perpendicular315;

                GridTable[0, 5] = GridType.HorizontalDown;
                GridTable[1, 5] = GridType.HorizontalUp;
                GridTable[2, 5] = GridType.None;

                GridTable[0, 6] = GridType.Horizontal;
                GridTable[1, 6] = GridType.HorizontalDown;
                GridTable[2, 6] = GridType.Perpendicular45;

                GridTable[0, 7] = GridType.HorizontalDown;
                GridTable[1, 7] = GridType.VerticalLeft;
                GridTable[2, 7] = GridType.HorizontalUp;

                GridTable[0, 8] = GridType.Perpendicular225;
                GridTable[1, 8] = GridType.VerticalRight;
                GridTable[2, 8] = GridType.Perpendicular315; 
            }
        }
        public void Visualizer(Floor floor)
        {
            (int, int) PlayerPosition = floor.GetPlayerPosition();
            for (int i = 0; i < floor.ROWCOUNT; i++)
            {
                if(i == PlayerPosition.Item1)
                { 
                    for (int j = 0; j < PlayerPosition.Item2; j++)
                    {
                        Console.Write(GetGridString(GridType.None) + "          ");
                    }
                    Console.Write(GetGridString(GridType.None));
                    Console.Write(GetGridString(GridType.Player));

                }
                Console.WriteLine();
                for (int j = 0; j < floor.ColumnCount; j++)
                {
                    Node now = floor.GetNode(i, j);
                    Console.Write(GetGridString(GridTable[i, j]));
                    Console.Write(GetNodeString(now.NodeType));
                }
                Console.WriteLine();
            }
        }
        public static string GetNodeString(NodeType type)
        {
            switch (type)
            {
                case NodeType.Start:
                    return "[  출발  ]";
                case NodeType.Battle:
                    return "[  전투  ]";
                case NodeType.Event:
                    return "[  미지  ]";
                case NodeType.Rest:
                    return "[  휴식  ]";
                case NodeType.Boss:
                    return "[  보스  ]";
                default:
                    return "          ";
            }
        }
        public static string GetGridString(GridType type)
        {
            switch (type)
            {
                case GridType.Player:
                    return "    ▼   ";
                case GridType.None:
                    return "         "; 
                case GridType.Horizontal:
                    return "━━━━━━━━━";
                case GridType.Vertical:
                    return "   ┃   ";
                case GridType.HorizontalUp:
                    return "━━━━┻━━━━";
                    break;
                case GridType.HorizontalDown:
                    return "━━━━┳━━━━";
                    break;
                case GridType.Cross:
                    return "━━━━╋━━━━"; 
                case GridType.VerticalLeft:
                    return "━━━━┫    ";
                case GridType.VerticalRight:
                    return "    ┣━━━━";
                case GridType.Perpendicular45:
                    return "    ┗━━━━"; 
                case GridType.Perpendicular135:
                    return "    ┏━━━━"; 
                case GridType.Perpendicular225:
                    return "━━━━┓    "; 
                case GridType.Perpendicular315:
                    return "━━━━┛    "; 
                default: 
                    return "         ";
            }

        }
    }
}