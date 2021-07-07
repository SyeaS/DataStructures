using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Wrappers
{
    public interface IAritmethicalWrapper<T> : IComparableWrapper<T>
        where T : struct
    {
        delegate T Addition(T value, T otherValue);
        T Add(T value, T otherValue);

        delegate T Subtraction(T value, T otherValue);
        T Subtract(T value, T otherValue);
    }
}