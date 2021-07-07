using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Wrappers
{
    public interface IWrapper : ICloneable
    {
        object Value { get; }
    }

    public interface IWrapper<T> : ICloneable
        where T : struct
    {
        T Value { get; }
    }
}