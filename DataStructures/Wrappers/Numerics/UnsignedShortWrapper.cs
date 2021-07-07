using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Wrappers.Numerics
{
    public class UnsignedShortWrapper : ComparableWrapper<ushort>
    {
        public UnsignedShortWrapper(ushort value) : base(ref value)
        {

        }

        public UnsignedShortWrapper(ref ushort value) : base(ref value)
        {

        }
    }
}
