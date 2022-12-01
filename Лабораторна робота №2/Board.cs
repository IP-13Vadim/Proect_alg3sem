using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    public class Board
    {
        public int[] State { get; private set; }

        public Board()
        {
            State = new int[8];
        }
        public int f_coef = 0;

        public bool QueensHits()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = i; j < 8; j++)
                {
                    if (i != j)
                    {
                        if (State[i] == State[j] || j - i == State[j] - State[i] || j - i == -(State[j] - State[i]))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public Board(int[] board)
        {
            State = new int[8];
            for (int i = 0; i < 8; i++)
            {
                State[i] = board[i];
            }
        }

        public Board[] ExpandState()
        {
            List<Board> expandedStates = new List<Board>();
            for (int i = 0; i < 8; i++)
            {
                int y = State[i];
                for (int j = 0; j < 7; j++)
                {
                    Board tempState = new Board(State);
                    if (y <= j)
                    {
                        tempState.State[i] = j + 1;
                    }
                    else
                    {
                        tempState.State[i] = j;
                    }
                    expandedStates.Add(tempState);
                }
            }
            return expandedStates.ToArray();
        }

        public int f2()
        {
            int result = 0;
            for (int i = 1; i < 8; ++i)
            {
                for (int j = 0; j < i; ++j)
                {
                    if (State[i] + i == State[j] + j)
                    {
                        result++;
                    }
                    if (State[i] - i == State[j] - j)
                    {
                        result++;
                    }
                    if (State[i] == State[j])
                    {
                        result++;
                    }
                }
            }
            return result;
        }

        public void DisplayState()
        {
            Console.Write("  ");
            for (int c = 0; c < 8; c++)
            {
                Console.Write($" {c + 1} ");
            }
            Console.WriteLine();

            for (int i = 0; i < 8; i++)
            {
                Console.Write(i + 1 + " ");
                for (int j = 0; j < 8; j++)
                {
                    if (State[i] == j)
                    {
                        Console.Write("[Q]");
                    }
                    else
                    {
                        Console.Write("[_]");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
