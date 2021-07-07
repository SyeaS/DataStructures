using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.PriorityQueue
{
    public interface IMinPriorityQueue<T>
        where T : IComparable<T>
    {
        void Add(T content);
        T PopMin();
        T PeekMin();
    }
}