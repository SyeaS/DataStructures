using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Wrappers.Numerics
{
    public class DoubleWrapper : AritmethicalWrapper<double>
    {
        public DoubleWrapper(double value) : this(ref value)
        {

        }

        public DoubleWrapper(ref double value) :
            base(value, new IAritmethicalWrapper<double>.Addition((x, y) => x + y),
                new IAritmethicalWrapper<double>.Subtraction((x, y) => x - y))
        {

        }
    }
}