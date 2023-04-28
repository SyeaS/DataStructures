using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.LinkedList
{
    public interface IStack<T>
    {
        void Push(T element);
        T Pop();
        T PeekLast();
    }
}