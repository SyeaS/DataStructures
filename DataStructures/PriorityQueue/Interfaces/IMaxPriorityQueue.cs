using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.PriorityQueue
{
    public interface IMaxPriorityQueue<T>
        where T : IComparable<T>
    {
        int Count { get; }

        void Add(T content);
        T PopMax();
        T PeekMax();
    }
}