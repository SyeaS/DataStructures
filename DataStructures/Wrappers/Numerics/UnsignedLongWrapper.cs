using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Wrappers.Numerics
{
    public class UnsignedLongWrapper : AritmethicalWrapper<ulong>
    {
        public UnsignedLongWrapper(ulong value) : this(ref value)
        {

        }

        public UnsignedLongWrapper(ref ulong value) :
            base(value, new IAritmethicalWrapper<ulong>.Addition((x, y) => x + y),
                new IAritmethicalWrapper<ulong>.Subtraction((x, y) => x - y))
        {

        }
    }
}
