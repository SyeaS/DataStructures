using DataStructures.Heaps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.PriorityQueue
{
    public class MaxPriorityQueue<T> : IMaxPriorityQueue<T>
        where T : class, IComparable<T>
    {
        BinaryMaxHeap<T> maxHeap;
        public int Count => maxHeap.Count;

        public MaxPriorityQueue()
        {
            maxHeap = new BinaryMaxHeap<T>();
        }

        public MaxPriorityQueue(int initialSize)
        {
            maxHeap = new BinaryMaxHeap<T>(initialSize);
        }

        public void Add(T content)
        {
            maxHeap.Add(content);
        }

        public T PeekMax()
        {
            return maxHeap.PeekMax();
        }

        public T PopMax()
        {
            return maxHeap.Extract();
        }
    }
}