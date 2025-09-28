using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresAndAlgorithms.PriorityQueue
{
    public interface IPriorityQueue<T> : IMaxPriorityQueue<T>, IMinPriorityQueue<T>
        where T : IComparable<T>
    {
        void Remove(T content);
    }
}