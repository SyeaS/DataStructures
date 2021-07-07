using DataStructures.Heaps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.PriorityQueue
{
    public class MinPriorityQueue<T> : IMinPriorityQueue<T>
        where T : class, IComparable<T>
    {
        BinaryMinHeap<T> minHeap;

        public MinPriorityQueue()
        {
            minHeap = new BinaryMinHeap<T>();
        }

        public MinPriorityQueue(int initialSize)
        {
            minHeap = new BinaryMinHeap<T>(initialSize);
        }

        public void Add(T content)
        {
            minHeap.Add(content);
        }

        public T PeekMin()
        {
            return minHeap.PeekMin();
        }

        public T PopMin()
        {
            return minHeap.Extract();
        }
    }
}