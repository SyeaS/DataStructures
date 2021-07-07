using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Wrappers.Numerics
{
    public class ShortWrapper : ComparableWrapper<short>
    {
        public ShortWrapper(short value) : base(ref value)
        {

        }

        public ShortWrapper(ref short value) : base(ref value)
        {

        }
    }
}
