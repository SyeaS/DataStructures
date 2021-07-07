using DataStructures.Heaps.Exceptions;
using DataStructures.Heaps.Interfaces;
using DataStructures.Trees.BinaryTrees;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Heaps
{
    /* A binary heap is defined as a binary tree with two additional constraints:
     * 
     * Shape property: a binary heap is a complete binary tree;
     * that is, all levels of the tree, except possibly the last one(deepest) 
     * are fully filled, and, if the last level of the tree is not complete, 
     * the nodes of that level are filled from left to right.
     * 
     * Heap property: the key stored in each node is either greater than
     * or equal to (≥)
     * or less than or equal to(≤)
     * the keys in the node's children, according to some total order.
     * 
     * Source: https://en.wikipedia.org/wiki/Binary_heap
    */

    public abstract class BinaryHeap<T> : IBinaryHeap<T>
        where T : class, IComparable<T>
    {
        protected int Capacity => tree.Length;
        protected int CurrentIndex { get; set; }

        private int _count = 0;
        public int Count => _count;
        public bool IsReadOnly => false;

        public bool IsSynchronized => false;
        public object SyncRoot { get; } = new object();

        protected T[] tree;

        public BinaryHeap()
        {
            tree = new T[1];
        }

        public BinaryHeap(int initialSize)
        {
            if (initialSize < 1)
            {
                throw new ArgumentException("Initial size cannot be less than 1!", nameof(initialSize));
            }
            int exponent = (int)(Math.Log2(initialSize) + 1);
            tree = new T[(int)Math.Pow(2, exponent)];
        }

        public BinaryHeap(IEnumerable<T> content) : this()
        {
            foreach (T item in content)
            {
                Add(item);
            }
        }

        public BinaryHeap(IEnumerable<T> content, int initialSize) : this(initialSize)
        {
            foreach (T item in content)
            {
                Add(item);
            }
        }

        public void Add(T content)
        {
            ResizeTree();
            AddInternal(ref content);
            _count++;
        }

        protected abstract void AddInternal(ref T content);

        public T Remove()
        {
            return Extract();
        }

        public bool Remove(T item)
        {
            throw new NotSupportedException("Binary heap doesn't support the remove operation.");
        }

        public T Extract()
        {
            if (_count == 0)
            {
                throw new HeapEmptyException();
            }

            T content = ExtractInternal();
            _count--;
            ResizeTree();
            return content;
        }

        protected abstract T ExtractInternal();

        private void ResizeTree()
        {
            if (_count + 1 > Capacity)
            {
                Array.Resize(ref tree, ((Capacity + 1) * 2) - 1);
            }
        }

        protected int GetParentIndex(int index)
        {
            return (index - 1) / 2;
        }

        protected T GetParent(int index)
        {
            return tree[GetParentIndex(index)];
        }

        protected int GetLeftChildrenIndex(int index)
        {
            return (index * 2) + 1;
        }

        protected T GetLeftChildren(int index)
        {
            return tree[GetLeftChildrenIndex(index)];
        }

        protected int GetRightChildrenIndex(int index)
        {
            return (index * 2) + 2;
        }

        protected T GetRightChildren(int index)
        {
            return tree[GetRightChildrenIndex(index)];
        }

        protected void Swap(int index, int other)
        {
            T temp = tree[index];
            tree[index] = tree[other];
            tree[other] = temp;
        }

        public void Dispose()
        {
            tree = new T[1];
            _count = 0;
            CurrentIndex = 0;
        }

        public void Clear()
        {
            Array.Clear(tree, 0, tree.Length);
            _count = 0;
            CurrentIndex = 0;
        }

        public bool Contains(T item)
        {
            return tree.Contains(item);
        }

        public void CopyTo(Array array, int index)
        {
            tree.CopyTo(array, index);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            tree.CopyTo(array, arrayIndex);
        }

        public bool Equals(IBinaryTree<T> other)
        {
            foreach (T item in this)
            {
                if (!other.Contains(item))
                {
                    return false;
                }
            }

            return true;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (T item in tree)
            {
                yield return item;
            }
        }
    }
}