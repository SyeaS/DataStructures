using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Wrappers
{
    public interface IComparableWrapper<T> : IWrapper<T>, IComparable<AbstractWrapper<T>>
        where T : struct
    {

    }
}