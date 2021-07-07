using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Wrappers.Numerics
{
    public class UnsignedIntWrapper : AritmethicalWrapper<uint>
    {
        public UnsignedIntWrapper(uint value) : this(ref value)
        {

        }

        public UnsignedIntWrapper(ref uint value) :
            base(value, new IAritmethicalWrapper<uint>.Addition((x, y) => x + y),
                new IAritmethicalWrapper<uint>.Subtraction((x, y) => x - y))
        {

        }
    }
}