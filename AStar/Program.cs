using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStar
{
    class Program
    {
        static void Main(string[] args)
        {
            int[][] map = CreateMap();

            Action<Node<int>> populateChildren = node =>
            {
                int locX = node.locationX;
                int locY = node.locationY;
                Node<int> child;
                
                    if(locX + 1 < map.Length)
                    {
                        if(map[locX+1][locY] == 0)
                        {
                            child = Node<int>.CreateNode();
                            child.locationX = locX + 1;
                            child.locationY = locY;
                            node.Children.Add(child);
                        }
                    }
                    if (locX - 1 >= 0)
                    {
                        if (map[locX - 1][locY] == 0)
                        {
                            child = Node<int>.CreateNode();
                            child.locationX = locX - 1;
                            child.locationY = locY;
                            node.Children.Add(child);
                        }
                    }
                    if (locY + 1 < map[locX].Length)
                    {
                        if (map[locX][locY + 1] == 0)
                        {
                            child = Node<int>.CreateNode();
                            child.locationX = locX;
                            child.locationY = locY+1;
                            node.Children.Add(child);
                        }
                    }
                    if (locY - 1 >= 0)
                    {
                        if (map[locX][locY-1] == 0)
                        {
                            child = Node<int>.CreateNode();
                            child.locationX = locX;
                            child.locationY = locY - 1;
                            node.Children.Add(child);
                        }
                    }
            };
            Func<Node<int>, Node<int>, int> heuristic = (start, goal) =>
            {
                return Math.Abs(start.locationX - goal.locationX) + Math.Abs(start.locationY - goal.locationY);
            };

            Astar<int> pathFinding = new Astar<int>(heuristic, populateChildren);
            Node<int> s = Node<int>.CreateNode();
            Node<int> g = Node<int>.CreateNode();
            bool found = false;
            int z = 0;
            int q = 0;
            while (!found)
            {
                if (map[z][q] == 0) { 
                    s.locationX = z;
                    s.locationY = q;
                    found = true;
                    break;
                }
                q++;
                if(q == 5)
                {
                    z++;
                    q = 0;
                }
            }
            found = false;
            z = map.Length - 1;
            q = map.Length - 1;
            while (!found)
            {
                if(map[z][q] == 0)
                {
                    g.locationX = z;
                    g.locationY = q;
                    found = true;
                    break;
                }
                q--;
                if (q == -1)
                {
                    z--;
                    q = 4;
                }
            }

            List<Node<int>> path = pathFinding.FindPath(s, g);

            if (path != null && path.Count > 0)
            {
                Console.WriteLine("Path Found");
                foreach(Node<int> x in path)
                {
                    x.printNode();
                }
            }
            else
            {
                Console.WriteLine("Path doesn't exist!");
            }

            PrintMap(map);

            Console.ReadLine();

        }
                
        public static void PrintMap(int[][] map)
        {
            for (int i = 0; i < map.Length; i++)
            {
                for (int j = 0; j < map[i].Length; j++)
                {
                    Console.Write(map[i][j] + " ");
                }
                Console.WriteLine();
            }
        }

        public static int[][] CreateMap()
        {
            int size = 10;
            int[][] map = new int[size][];
            for (int i = 0; i < size; i++)
            {
                map[i] = new int[size];
            }


            Random rand = new Random((int)System.DateTime.Now.Ticks);

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    map[i][j] = rand.Next(2);
                }
            }

            int wallCount = 0;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if(map[i][j] == 0)
                    {
                        if(j+1 < size)
                        {
                            if(map[i][j+1] == 1)
                            {
                                wallCount++;
                            }
                        }
                        if(i+1 < size)
                        {
                            if(map[i+1][j] == 1)
                            {
                                wallCount++;
                            }
                        }
                        if(wallCount > 1)
                        {
                            wallCount = rand.Next(2);
                            if(wallCount == 0)
                            {
                                map[i][j + 1] = 0;
                            }
                            else
                            {
                                map[i + 1][j] = 0;
                            }
                        }
                    }
                    wallCount = 0;
                }
            }

            return map;
        }
    }
}
