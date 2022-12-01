using System;
using System.Collections.Generic;
using System.Text;

namespace lab2
{
    public static class BFS
    {
        public static Board Execute(Board root)
        {
            DateTime dt = DateTime.Now;
            Board currentState = new Board();
            Queue<Board> queue = new Queue<Board>();
            queue.Enqueue(root);
            while (queue.TryPeek(out _))
            {
                currentState = queue.Dequeue();
                foreach (Board child in currentState.ExpandState())
                {
                    if (!child.QueensHits())
                    {
                        Console.WriteLine("Time: " + (DateTime.Now - dt).TotalSeconds + " s");
                        return child;
                    }
                    queue.Enqueue(child);
                }
            }
            return null;
        }
    }
}
