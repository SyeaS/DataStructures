using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Wrappers.Numerics
{
    public class DecimalWrapper : AritmethicalWrapper<decimal>
    {
        public DecimalWrapper(decimal value) : this(ref value)
        {

        }

        public DecimalWrapper(ref decimal value) : base(value,
            new IAritmethicalWrapper<decimal>.Addition((x, y) => x + y),
            new IAritmethicalWrapper<decimal>.Subtraction((x, y) => x - y))
        {

        }
    }
}