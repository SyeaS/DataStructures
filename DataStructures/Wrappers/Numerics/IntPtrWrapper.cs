using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Wrappers.Numerics
{
    public class IntPtrWrapper : AritmethicalWrapper<IntPtr>
    {
        public IntPtrWrapper(nint value) : this(ref value)
        {

        }

        public IntPtrWrapper(ref nint value) :
            base(value, new IAritmethicalWrapper<nint>.Addition((x, y) => x + y),
                new IAritmethicalWrapper<nint>.Subtraction((x, y) => x - y))
        {

        }
    }
}
