using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Wrappers.Numerics
{
    public class LongWrapper : AritmethicalWrapper<long>
    {
        public LongWrapper(long value) : this(ref value)
        {

        }

        public LongWrapper(ref long value) :
            base(value, new IAritmethicalWrapper<long>.Addition((x, y) => x + y),
                new IAritmethicalWrapper<long>.Subtraction((x, y) => x - y))
        {

        }
    }
}
