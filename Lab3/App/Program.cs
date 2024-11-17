using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace CPP_Lab_3
{
    public class Program
    {
        static List<int>[] adj;
        static string[] allowedColors;
        static bool[] visited;
        static int[] color;
        static int maxIndigo = 0;
        static HashSet<char> colorsTask = new HashSet<char> { 'I', 'B', 'V' };


        static void Main(string[] args)
        {
            string nameInput = "INPUT.txt";
            string nameOutput = "OUTPUT.txt";

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "--input" && i + 1 < args.Length)
                {
                    nameInput = args[i + 1];
                }
                else if (args[i] == "--output" && i + 1 < args.Length)
                {
                    nameOutput = args[i + 1];
                }
            }

            try
            {
                FindMaxIndigo(nameInput, nameOutput);
            }
            catch (Exception e)
            {
                Console.WriteLine("Помилка:" + e.Message);
            }

        }

        public static void FindMaxIndigo(string input, string output)
        {
            using (StreamReader sr = new StreamReader(input))
            using (StreamWriter sw = new StreamWriter(output))
            {
                int n = int.Parse(sr.ReadLine());

                if (n < 1 || n > 50000)
                {
                    Console.WriteLine("n must be more than 0 and less than or equal 50000");
                    return;
                }

                adj = new List<int>[n + 1];
                for (int i = 1; i <= n; i++)
                {
                    adj[i] = new List<int>();
                }

                for (int i = 0; i < n - 1; i++)
                {
                    var edge = sr.ReadLine().Split();
                    if (!int.TryParse(edge[0], out _) || !int.TryParse(edge[1], out _))
                    {
                        Console.WriteLine("Invalid input, not a number");
                        return;
                    }
                    int u = int.Parse(edge[0]);
                    int v = int.Parse(edge[1]);
                    adj[u].Add(v);
                    adj[v].Add(u);
                }

                allowedColors = new string[n + 1];
                for (int i = 1; i <= n; i++)
                {
                    allowedColors[i] = sr.ReadLine();
                    foreach (char ch in allowedColors[i])
                    {
                        if (!colorsTask.Contains(ch))
                        {
                            Console.WriteLine("Invalid color");
                            return;
                        }
                    }

                }

                visited = new bool[n + 1];
                color = new int[n + 1];

                maxIndigo = 0;

                if (DFS(1, 0))
                {
                    sw.WriteLine(maxIndigo);
                }
                else
                {
                    sw.WriteLine(-1);
                }
            }

        }

        static bool CheckColor(int u, int c)
        {
            char ch;

            if (c == 1)
                ch = 'I';
            else if (c == 2)
                ch = 'B';
            else
                ch = 'V';

            return allowedColors[u].Contains(ch);
        }

        static bool DFS(int u, int parentColor)
        {
            visited[u] = true;

            for (int c = 1; c <= 3; c++)
            {
                if (CheckColor(u, c) && c != parentColor)
                {
                    color[u] = c;
                    if (c == 1)
                        maxIndigo++;

                    foreach (var v in adj[u])
                    {
                        if (!visited[v])
                        {
                            if (!DFS(v, c))
                                return false;
                        }
                        else if (color[v] == c)
                        {
                            return false;
                        }
                    }

                    return true;
                }

                
            }

            return false;
        }
    }

}
