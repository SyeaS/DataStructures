using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Wrappers.Numerics
{
    public class FloatWrapper : AritmethicalWrapper<float>
    {
        public FloatWrapper(float value) : this(ref value)
        {

        }

        public FloatWrapper(ref float value) :
            base(value, new IAritmethicalWrapper<float>.Addition((x, y) => x + y),
                new IAritmethicalWrapper<float>.Subtraction((x, y) => x - y))
        {

        }
    }
}