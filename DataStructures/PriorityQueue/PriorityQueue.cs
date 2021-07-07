using DataStructures.LinkedList;
using DataStructures.Trees.BinaryTrees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.PriorityQueue
{
    public class PriorityQueue<T> : IPriorityQueue<T>
        where T : IComparable<T>
    {
        RedBlackTree<T> RBTree;
        public int Count => RBTree.Count;

        public PriorityQueue()
        {
            RBTree = new RedBlackTree<T>();
        }

        public PriorityQueue(IEnumerable<T> contents)
        {
            RBTree = new RedBlackTree<T>(contents);
        }

        public void Add(T content)
        {
            RBTree.Add(content);
        }

        public T PeekMax()
        {
            return RBTree.Maximum();
        }

        public T PeekMin()
        {
            return RBTree.Minimum();
        }

        public T PopMax()
        {
            return RBTree.PopMax();
        }

        public T PopMin()
        {
            return RBTree.PopMin();
        }

        public void Remove(T content)
        {
            RBTree.Remove(content);
        }
    }
}