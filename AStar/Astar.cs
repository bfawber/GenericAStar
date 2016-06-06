﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStar
{
    class Astar<T>
    {
        private Func<Node<T>, int> hueristic;
        private Action<Node<T>> populateChildren;

        private List<Node<T>> open;
        private List<Node<T>> closed;

        public Astar(Func<Node<T>, int> hueristic, Action<Node<T>> populateChildren)
        {
            this.hueristic = hueristic;
            this.populateChildren = populateChildren;
            this.open = new List<Node<T>>();
            this.closed = new List<Node<T>>();
        }

        public List<Node<T>> FindPath(Node<T> start, Node<T> goal)
        {
            List<Node<T>> path = new List<Node<T>>();

            start.g = 0;
            start.h = hueristic(start);
            open.Add(start);

            Node<T> n;

            while(open.Count > 0)
            {
                n = removeLowestF(open);
                if (n.Equals(goal))
                {
                    return path;
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
                        child.h = hueristic(n);
                        open.Add(child);
                    }
                }
            }

            return path;
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
    }
}