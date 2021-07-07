using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Wrappers.Numerics
{
    public class SignedByteWrapper : ComparableWrapper<sbyte>
    {
        public SignedByteWrapper(sbyte value) : base(ref value)
        {

        }

        public SignedByteWrapper(ref sbyte value) : base(ref value)
        {

        }
    }
}
