using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Wrappers
{
    public class ComparableWrapper<T> : Wrapper<T>, IComparableWrapper<T>
        where T : struct, IComparable<T>
    {
        public ComparableWrapper(T value) : base(ref value)
        {

        }

        protected ComparableWrapper(ref T value) : base(ref value)
        {

        }

        public int CompareTo(AbstractWrapper<T> other)
        {
            return Value.CompareTo(other.Value);
        }
    }
}