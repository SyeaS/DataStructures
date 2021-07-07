using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Wrappers.Numerics
{
    public class UnsignedIntPtrWrapper : AritmethicalWrapper<UIntPtr>
    {
        public UnsignedIntPtrWrapper(nuint value) : this(ref value)
        {

        }

        public UnsignedIntPtrWrapper(ref nuint value) :
            base(value, new IAritmethicalWrapper<nuint>.Addition((x, y) => x + y),
                new IAritmethicalWrapper<nuint>.Subtraction((x, y) => x - y))
        {

        }
    }
}
