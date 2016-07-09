using System;
using System.Collections.Generic;
using HTable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStar
{
    class Astar<T>
    {
        private Func<Node<T>, Node<T>, int> hueristic;
        private Action<Node<T>> populateChildren;

        // TODO: Change this to a Map of f value -> node
        //private List<Node<T>> open;
        private HTbl<int, Node<T>> open;
        private List<Node<T>> closed;

        public Astar(Func<Node<T>, Node<T>, int> hueristic, Action<Node<T>> populateChildren)
        {
            this.hueristic = hueristic;
            this.populateChildren = populateChildren;
            this.open = new HTbl<int, Node<T>>();
            this.closed = new List<Node<T>>();
        }

        public List<Node<T>> FindPath(Node<T> start, Node<T> goal)
        {
            List<Node<T>> path = new List<Node<T>>();

            start.g = 0;
            start.h = hueristic(start, goal);
            start.f = start.g + start.h;
            open.Push(start.f, start);

            Node<T> n;

            while(open.Count > 0)
            {
                n = removeLowestF(open);
                if (n.Equals(goal))
                {
                    return getPath(n);
                }
                closed.Add(n);

                // Populate Children of n
                populateChildren(n);

                foreach(Node<T> child in n.Children)
                {
                    if(!open.Contains(child) && !closed.Contains(child))
                    {
                        child.Parent = n;
                        child.g = n.g + 1;
                        child.h = hueristic(n, goal);
                        child.f = child.g + child.h;
                        open.Push(child.f, child);
                    }
                }
            }

            return null;
        }

        private Node<T> removeLowestF(List<Node<T>> nodes)
        {
            Node<T> lowestF = nodes[0];

            for(int i = 1; i < nodes.Count; i++)
            {
                if(nodes[i].f < lowestF.f)
                {
                    lowestF = nodes[i];
                }
            }

            nodes.Remove(lowestF);
            return lowestF;
        }

        private Node<T> removeLowestF(HTbl<int, Node<T>> nodes)
        {
            Node<T> node = nodes.Pop(0);
            for(int i = 0; node == null; i++)
            {
                node = nodes.Pop(i);
            }

            return node;
        }

        private List<Node<T>> getPath(Node<T> goal)
        {
            List<Node<T>> path = new List<Node<T>>();
            Node<T> curr = goal;

            while(curr.Parent != null)
            {
                path.Add(curr.Parent);
                curr = curr.Parent;
            }

            path.Reverse();

            return path;
        }
    }
}
