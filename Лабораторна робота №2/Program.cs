using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace lab2
{
    internal class Program
    {
         static void Main(string[] args)
         {
            int[] initialQueens = new int[8];
            Console.WriteLine("Generate State:");
            Random rnd = new Random();
            for (int i = 0; i < 8; i++)
            {
                initialQueens[i] = rnd.Next(8);
            }
            Console.WriteLine();
            Board root = new Board(initialQueens);
            root.DisplayState();
            Console.WriteLine();
            Console.WriteLine("BFS algorithm find solution:");
            Board result = BFS.Execute(root);
            if (result == null)
            {
                Console.WriteLine("Error. Can't find solution");
            }
            else
            {
                result.DisplayState();
            }
            Console.WriteLine();
            Console.WriteLine("RBFS algorithm find solution:");
            result = RBFS.Execute(root);
            if (result == null)
            {
                Console.WriteLine("Error. Can't find solution");
            }
            else
            {
                result.DisplayState();
            }
            Console.WriteLine();
         }
    }
}
