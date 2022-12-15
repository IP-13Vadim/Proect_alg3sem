using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3ANT
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Graph graph = new Graph(250);
            GreedySearch greedySearch = new GreedySearch(graph);

            int lMin = greedySearch.GetMinLenght();

            Console.WriteLine($"lMin: {lMin}");

            double[,] pheromone = new double[graph.Matrix.GetLength(0), graph.Matrix.GetLength(1)];

            for(int i = 0; i < pheromone.GetLength(0); i++)
            {
                for(int j = 0; j < pheromone.GetLength(1); j++)
                {
                    pheromone[i, j] = 0.0001;
                }
            }

            ACO aco = new ACO(graph.Matrix, pheromone, lMin);
            aco.Calculate();
            
        }
    }
}
