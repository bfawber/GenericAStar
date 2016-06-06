using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStar
{
    class Node<T> : IEquatable<Node<T>>
    {
        public int g { get; set; }
        public int h { get; set; }
        public int f { get; set; }
        public int locationX { get; set; }
        public int locationY { get; set; }

        public List<Node<T>> Children;

        public Node<T> Parent;

        public Node() {
            this.Children = new List<Node<T>>();
            this.g = 0;
            this.h = 0;
        }

        private Node(int g, int h, int f, List<Node<T>> Children)
        {
            this.g = g;
            this.h = h;
            this.f = f;

            this.Children = new List<Node<T>>();
            
            for(int i = 0; i < Children.Count; i++)
            {
                this.Children.Add(Children[i].Clone());
            }
        }

        public Node<T> Clone()
        {
            return new Node<T>(this.g, this.h, this.f, this.Children);
        }

        public static Node<T> CreateNode()
        {
            return new Node<T>();
        }

        public bool Equals(Node<T> other)
        {
            if(other.locationX == this.locationX && other.locationY == this.locationY)
            {
                return true;
            }
            return false;
        }

        public void printNode()
        {
            Console.WriteLine("( " + this.locationX + ", " + this.locationY + ")");
        }
    }
}
