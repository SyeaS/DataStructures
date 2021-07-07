using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Wrappers
{
    public class Wrapper<T> : AbstractWrapper<T>
        where T : struct
    {
        public Wrapper(T value) : base(ref value)
        {

        }

        protected Wrapper(ref T value) : base(ref value)
        {

        }

        public override object Clone()
        {
            return new Wrapper<T>(this.Value);
        }

        protected override AbstractWrapper<T> CreateWrapper(T value)
        {
            return new Wrapper<T>(value);
        }
    }
}