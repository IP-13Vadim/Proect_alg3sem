using System.Linq;
using System.Collections.Generic;

namespace Lab3ANT
{
    public class GreedySearch
    {
        public Graph Graph { get; set; }

        public GreedySearch(Graph graph)
        {
            this.Graph = graph;
        }

        public int GetMin(List<int> revVert, int[] distance)
        {
            int min = Graph.MaxDistance;
            int minI = 0;

            for (int i = 0; i < distance.Length; i++)
            {
                if (distance[i] < min && !revVert.Contains(i) && distance[i] > 0)
                {
                    min = distance[i];
                    minI = i;
                }
            }


            return minI;
        }

        public int GetMinLenght()
        {
            int result = 0;
            int currVert = 0;
            List<int> revVert = new List<int>();

            while (revVert.Count < Graph.Matrix.GetLength(0) - 1)
            {
                List<int> temp = new List<int>();
                for (int i = 0; i < Graph.Matrix.GetLength(1); i++) 
                {
                    temp.Add(Graph.Matrix[currVert, i]);
                }

                int newVert = GetMin(temp, revVert.ToArray());
                result += Graph.Matrix[newVert, currVert];
                revVert.Add(currVert);
                currVert = newVert;
            }

            result += Graph.Matrix[currVert, 0];
            revVert.Add(currVert);
            return result;
        }
    }
}