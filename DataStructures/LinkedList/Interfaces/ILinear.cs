using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresAndAlgorithms.LinkedList
{
    public interface ILinear<T>
    {
        void Add(T content);
        T Remove();
    }
}