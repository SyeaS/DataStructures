using DataStructures.Heaps.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Heaps
{
    public sealed class BinaryMinHeap<T> : BinaryHeap<T>
        where T : class, IComparable<T>
    {
        public BinaryMinHeap()
        {

        }

        public BinaryMinHeap(int initialSize) : base(initialSize)
        {

        }

        public BinaryMinHeap(IEnumerable<T> content) : base(content)
        {

        }

        public BinaryMinHeap(IEnumerable<T> content, int initialSize) : base(content, initialSize)
        {

        }

        public T PeekMin()
        {
            if (Count != 0)
            {
                return tree[0];
            }

            throw new HeapEmptyException();
        }

        protected override void AddInternal(ref T content)
        {
            tree[CurrentIndex] = content;
            if (Count != 0)
            {
                HeapifyUp();
            }
            CurrentIndex++;
        }

        private void HeapifyUp()
        {
            int index = CurrentIndex;
            int parentIndex = GetParentIndex(index);

            while (!(tree[index].CompareTo(tree[parentIndex]) >= 0 || index == 0))
            {
                Swap(index, parentIndex);
                index = parentIndex;
                parentIndex = GetParentIndex(index);
            }
        }

        protected override T ExtractInternal()
        {
            T content = tree[0];

            if (Count == 1)
            {
                tree[0] = null;
                return content;
            }

            tree[0] = tree[CurrentIndex - 1];
            tree[CurrentIndex - 1] = default(T);

            HeapifyDown();
            CurrentIndex--;
            return content;
        }

        private void HeapifyDown()
        {
            int index = 0;

            for (int i = 0; GetRightChildrenIndex(i) < Capacity;)
            {
                try
                {
                    index = GetSmaller(index);
                }
                catch (Exception)
                {
                    return;
                }
                Swap(i, index);
                i = index;
            }
        }

        private int GetSmaller(int index)
        {
            int rightIndex = GetRightChildrenIndex(index);
            int leftIndex = GetLeftChildrenIndex(index);

            if (tree[index].CompareTo(tree[leftIndex]) <= 0 &&
                tree[index].CompareTo(tree[rightIndex]) <= 0)
            {
                throw new NoSmallerElementException();
            }
            else if (tree[leftIndex] != null && tree[leftIndex].CompareTo(tree[rightIndex]) <= 0)
            {
                return leftIndex;
            }
            else if (tree[rightIndex] != null)
            {
                return rightIndex;
            }

            throw new NoSmallerElementException();
        }
    }
}