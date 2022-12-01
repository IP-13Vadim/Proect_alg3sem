using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    internal class RBFS
    {
        private static int limit = 10;
        public static Board Execute(Board root)
        {
            DateTime dt = DateTime.Now;
            Board result = RBFS_algo(root, 30, 0);
            Console.WriteLine("Time: " + (DateTime.Now - dt).TotalSeconds + " s");
            return result;
        }

        private static Board RBFS_algo(Board node, int f_limit, int depth)
        {
            if (isSolved(node))
            {
                return node;
            }

            if (depth >= limit)
            {
                return null;
            }

            List<Board> childStates = node.ExpandState().ToList();
            List<int> f = new List<int>();
            foreach (var child in childStates)
            {
                f.Add(child.f2());
            }

            while (true)
            {
                int bestValue = f.Min();
                int bestIndex = f.IndexOf(bestValue);
                Board bestState = childStates[bestIndex];

                if (bestValue > f_limit)
                {
                    return null;
                }

                childStates.Remove(bestState);
                f.Remove(bestValue);

                int altValue = f.Min();
                Board result = RBFS_algo(bestState, Math.Min(f_limit, altValue), depth + 1);

                if (result != null)
                {
                    return result;
                }
            }
        }

        private static bool isSolved(Board state)
        {
            for (int i = 0; i < state.State.Length; ++i)
            {
                for (int j = 0; j < state.State.Length; ++j)
                {
                    if (i == j) continue;
                    if (state.State[i] == state.State[j] || i + state.State[i] == j + state.State[j] || i - state.State[i] == j - state.State[j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
