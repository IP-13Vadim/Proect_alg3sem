using System;
using System.Collections.Generic;
using System.IO;

namespace Laba1
{
    class Program
    {

        //Зовнішнє Природне (адаптивне) сортування

        static int count = 1;
        static string bufferL = null;
       // 184_000_000 = 1GB
       // 17_900_000 = 100MB
       // 1_790_000 = 10MB
        static void Main(string[] args)
        {
            Console.WriteLine("-------------------------------------------------------------------------------------------");
            Console.Write("What file you want to sort? (test1GB.txt/test100MB.txt/test10MB.txt) ");
            string path;
            path = Console.ReadLine();
            if (path == "test10MB.txt")
            {
                DateTime time = DateTime.Now;
                natural_adaptive_merge_sort(path);
                DateTime time2 = DateTime.Now;
                Console.WriteLine("Time for sorting " + time2.Subtract(time).TotalSeconds + " seconds");
            }
            else
            {
                DateTime time = DateTime.Now;
                optimized_adaptive_merge_sort(path);
                DateTime time2 = DateTime.Now;
                Console.WriteLine("Time for sorting " + time2.Subtract(time).TotalSeconds + " seconds");
            }
            Console.Write("Do you want to print first 10000 elements in file? press y/n ");
            string mode;
            mode = Console.ReadLine();
            if (mode == "y")
            {
                printFile(path);
            }
            //createFile(path, 17_900_000);
            //DateTime time = DateTime.Now;
            //natural_adaptive_merge_sort(path);
            //optimized_adaptive_merge_sort(path);
            //printFile(path);
            //DateTime time2 = DateTime.Now;
            //Console.WriteLine("Time for sorting " + time2.Subtract(time).TotalSeconds + " seconds");
        }

        static void SplitTwoFilesOptimised(StreamReader streamReaderA)
        {
            while (!streamReaderA.EndOfStream)
            {
                List<string> series = new List<string>();

                List<int> numbers = new List<int>();

                const int numbersToSort = 100000000;

                for (int i = 0; i < numbersToSort && !streamReaderA.EndOfStream; i++)
                {
                    numbers.Add(int.Parse(streamReaderA.ReadLine()));
                }

                numbers.Sort();

                foreach (var number in numbers)
                {
                    series.Add(number.ToString());
                }

                write_info_to_file(series);
            }

        }

        static void createFile(string path, int size)
        {
            Random rand = new Random();
            using (StreamWriter sw = new StreamWriter(path, false))
            {
                for (int i = 0; i < size; i++)
                {
                    sw.WriteLine(rand.Next(0, 10_000).ToString());
                }
            }
        }

        static void printFile(string path)
        {
            using (StreamReader sw = new StreamReader(path, false))
            {
                for (int i = 0; i < 100000; i++)
                {
                    Console.WriteLine(sw.ReadLine());
                }
            }

        }

        static void natural_adaptive_merge_sort(string path)
        {

            while (!is_Sorted(path))
            {
                StreamReader read_A = new StreamReader(path);
                File.WriteAllText("B.txt", "");
                File.WriteAllText("C.txt", "");

                split_two_files(read_A);
                read_A.Close();
                merge_files(path, "B.txt", "C.txt");
            }
        }

        static void optimized_adaptive_merge_sort(string path)
        {
            int i = 0;
            // Optimised part
            StreamReader streamReaderA = new StreamReader(path);
            File.WriteAllText("B.txt", "");
            File.WriteAllText("C.txt", "");
            SplitTwoFilesOptimised(streamReaderA);

            streamReaderA.Close();
            merge_files(path, "B.txt", "C.txt");

            while (!is_Sorted(path))
            {
                Console.WriteLine($"Iteration №{++i}");
                streamReaderA = new StreamReader(path);
                File.WriteAllText("B.txt", "");
                File.WriteAllText("C.txt", "");

                split_two_files(streamReaderA);
                streamReaderA.Close();
                merge_files(path, "B.txt", "C.txt");
            }
        }

        static void split_two_files(StreamReader read_A)
        {

            while (!read_A.EndOfStream)
            {
                List<string> series = new List<string>();

                string line = bufferL;
                if (line == null) { line = read_A.ReadLine(); }
                series.Add(line);


                while (!read_A.EndOfStream)
                {
                    string prevL = line;
                    line = read_A.ReadLine();
                    int intline = Convert.ToInt32(line);
                    int intprevLine = Convert.ToInt32(prevL);
                    if (intprevLine <= intline)
                    {
                        series.Add(line);
                    }
                    else
                    {
                        bufferL = line;
                        break;
                    }

                }
                write_info_to_file(series);
            }
        }
        static bool is_Sorted(string path)
        {
            StreamReader read_from = new StreamReader(path);

            int num1 = Convert.ToInt32(read_from.ReadLine());
            int num2 = Convert.ToInt32(read_from.ReadLine());
            while (!read_from.EndOfStream)
            {

                if (num1 > num2)
                {
                    read_from.Close();
                    return false;
                }
                num1 = num2;
                num2 = Convert.ToInt32(read_from.ReadLine());

            }
            read_from.Close();
            return true;
        }
        static void write_info_to_file(List<string> series)
        {
            if (count % 2 == 1)
            {
                StreamWriter writer = new StreamWriter("B.txt", true);
                foreach (string line in series)
                {
                    writer.WriteLine(line);
                }
                writer.Flush();
                writer.Close();
            }
            else
            {
                StreamWriter writer = new StreamWriter("C.txt", true);
                foreach (string line in series)
                {
                    writer.WriteLine(line);
                }
                writer.Flush();
                writer.Close();
            }
            count++;
        }

        static void merge_files(string pathA, string pathB, string pathC)
        {
            StreamReader read_from_B = new StreamReader(pathB);
            StreamReader read_from_C = new StreamReader(pathC);
            StreamWriter write_to_A = new StreamWriter(pathA);
            int prevNumC, prevNumB;
            int numB = Convert.ToInt32(read_from_B.ReadLine());
            int numC = Convert.ToInt32(read_from_C.ReadLine());
            while (!read_from_B.EndOfStream && !read_from_C.EndOfStream)
            {

                if (numB > numC)
                {
                    write_to_A.WriteLine(numC);
                    prevNumC = numC;
                    numC = Convert.ToInt32(read_from_C.ReadLine());
                    if (numC < prevNumC)
                    {
                        do
                        {
                            write_to_A.WriteLine(numB);
                            prevNumB = numB;
                            numB = Convert.ToInt32(read_from_B.ReadLine());
                        } while (numB >= prevNumB);
                    }
                }
                else
                {
                    write_to_A.WriteLine(numB);
                    prevNumB = numB;
                    numB = Convert.ToInt32(read_from_B.ReadLine());
                    if (numB < prevNumB)
                    {
                        do
                        {
                            write_to_A.WriteLine(numC);
                            prevNumC = numC;
                            numC = Convert.ToInt32(read_from_C.ReadLine());
                        } while (numC >= prevNumC);
                    }
                }
            }
            write_to_A.WriteLine(Math.Min(numB, numC));
            write_to_A.WriteLine(Math.Max(numB, numC));
            if (read_from_B.EndOfStream)
            {
                while (!read_from_C.EndOfStream)
                {
                    write_to_A.WriteLine(Convert.ToInt32(read_from_C.ReadLine()));
                }
            }
            else
            {
                while (!read_from_B.EndOfStream)
                {
                    write_to_A.WriteLine(Convert.ToInt32(read_from_B.ReadLine()));
                }
            }
            read_from_B.Close();
            read_from_C.Close();
            write_to_A.Flush();
            write_to_A.Close();
        }
    }
}
