using System;
using System.Runtime.Serialization.Formatters;

namespace Lab3ANT
{
    public class Graph
    {
        public int MinDistance { get; } = 1;
        public int MaxDistance { get; } = 40;

        public int[,] Matrix { get; set; }

        public Graph(int numberOfVertexes)
        {
            Matrix = new int[numberOfVertexes, numberOfVertexes];
            Random rnd = new Random();

            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < Matrix.GetLength(1); j++)
                {
                    if (i == j)
                    {
                        Matrix[i, j] = 0;
                    }
                    else
                    {
                        Matrix[i, j] = Matrix[j, i] = rnd.Next(MinDistance, MaxDistance + 1);
                    }
                }
            }

        }

        
    }
}