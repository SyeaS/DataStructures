using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Wrappers.Numerics
{
    public class ByteWrapper : ComparableWrapper<byte>
    {
        public ByteWrapper(byte value) : base(ref value)
        {

        }

        public ByteWrapper(ref byte value) : base(ref value)
        {

        }
    }
}
