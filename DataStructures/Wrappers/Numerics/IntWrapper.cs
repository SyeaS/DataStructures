using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Wrappers.Numerics
{
    public class IntWrapper : AritmethicalWrapper<int>
    {
        public IntWrapper(int value) : this(ref value)
        {

        }

        public IntWrapper(ref int value) :
            base(value, new IAritmethicalWrapper<int>.Addition((x, y) => x + y),
                new IAritmethicalWrapper<int>.Subtraction((x, y) => x - y))
        {

        }
    }
}