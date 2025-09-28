using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresAndAlgorithms.LinkedList
{
    public interface IQueue<T>
    {
        void Enqueue(T element);
        T Dequeue();
        T PeekFirst();
    }
}