using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3ANT
{
    public class ACO
    {
        public double Alpha { get; set; }
        public double Beta { get; set; }
        public double Ro { get; set; }

        private int _lMin;
        private int[,] _distancesMatrix;
        private double[,] _pheromoneMatrix;
        private int _countOfAnt;
        private int _tMax;

        public ACO(int[,] distanceMatrix, double[,] pheromoneMatrix, int lMin)
        {
            _distancesMatrix = distanceMatrix;
            _pheromoneMatrix = pheromoneMatrix;
            Alpha = 4;
            Beta = 2;
            Ro = 0.3;
            _lMin = lMin;
            _countOfAnt = 45;
            _tMax = 10;
        }

        public List<Ant> GetAnts()
        {
            List<Ant> ants = new List<Ant>();
            Random rnd = new Random();

            for (int i = 0; i < _countOfAnt; i++)
            {
                int randomPosition = 0;
                do
                {
                    randomPosition = rnd.Next(0, _distancesMatrix.GetLength(0));
                } while (ants.Where(x => x.Position == randomPosition).Count() != 0);

                ants.Add(new Ant(randomPosition));
            }

            return ants;
        }

        public int GetMaxIndex(double[] array)
        {
            double max = array.Max();
            for (int i = 0; i < array.Length; i++)
            {
                if (max == array[i]) return i;
            }

            return -1;
        }

        public int GetNewPosition(int currPos, List<int> visitedPos)
        {
            double[] prob = new double[_distancesMatrix.GetLength(0)];

            for (int i = 0; i < prob.Length; i++)
            {
                if (visitedPos.Contains(i))
                {
                    prob[i] = 0;
                }
                else
                {
                    prob[i] = Math.Pow(_pheromoneMatrix[currPos, i], Alpha) * (Math.Pow((1d / _distancesMatrix[currPos, i]), Beta));
                }
            }

            Random rnd = new Random();

            double newPos = rnd.NextDouble() * prob.ToList().Sum();
            double sum = 0;

            int result = GetMaxIndex(prob);

            while (sum < newPos)
            {
                result = GetMaxIndex(prob);
                sum += prob[result];
                prob[result] = 0;
            }

            return result;
        }

        public int GetLength(List<int> way)
        {
            int length = 0;

            for (int i = 0; i < way.Count - 1; i++)
            {
                length += _distancesMatrix[way[i], way[i + 1]];
            }

            return length + _distancesMatrix[_distancesMatrix.GetLength(0) - 1, 0];
        }

        public double[,] GetPheromonic(List<List<int>> ways)
        {
            for (int i = 0; i < _pheromoneMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < _pheromoneMatrix.GetLength(1); j++)
                {
                    _pheromoneMatrix[i, j] *= (1d - Ro);
                }
            }

            foreach (var way in ways)
            {
                int delta = _lMin / GetLength(way);

                for (int i = 0; i < way.Count - 1; i++)
                {
                    _pheromoneMatrix[way[i], way[i + 1]] += delta;
                    _pheromoneMatrix[way[i + 1], way[i]] += delta;
                }

                _pheromoneMatrix[way[ways.Count - 1], way[0]] += delta;
                _pheromoneMatrix[way[0], way[ways.Count - 1]] += delta;
            }

            return _pheromoneMatrix;
        }



        public void Calculate()
        {
            List<Ant> ants = GetAnts();

            int bestLength = int.MaxValue;
            List<int> bestWay = new List<int>();

            for (int i = 0; i < _tMax; i++)
            {
                List<List<int>> ways = new List<List<int>>();

                for (int j = 0; j < ants.Count; j++)
                {
                    int currPos = ants[j].Position;
                    List<int> visPos = new List<int>();

                    while (visPos.Count < _distancesMatrix.GetLength(0))
                    {
                        visPos.Add(currPos);
                        currPos = GetNewPosition(currPos, visPos);
                    }

                    List<int> tWay = new List<int>();

                    for (int k = 0; k < visPos.Count; k++)
                    {
                        tWay.Add(visPos[k]);
                    }

                    ways.Add(tWay);
                }

                foreach(var way in ways)
                {
                    int length = GetLength(way);

                    if(length < bestLength)
                    {
                        bestWay = way;
                        bestLength = length;
                    }
                }


                _pheromoneMatrix = GetPheromonic(ways);
            }


            Console.WriteLine($"Best way: ");

            foreach(var w in bestWay)
            {
                Console.Write(w + " ");
            }
            Console.WriteLine($"\nBest length: {bestLength}");

        }

    }
}
