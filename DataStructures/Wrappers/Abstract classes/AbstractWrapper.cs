using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Wrappers
{
    public abstract class AbstractWrapper<T> : IWrapper<T>
        where T : struct
    {
        public T Value { get; protected set; }
        private delegate AbstractWrapper<T> WrapperFactory(T value);
        private static WrapperFactory Factory { get; set; }

        public AbstractWrapper(T value) : this(ref value)
        {

        }

        protected AbstractWrapper(ref T value)
        {
            Value = value;
            Factory = CreateWrapper;
        }

        public abstract object Clone();

        protected abstract AbstractWrapper<T> CreateWrapper(T value);

        public static implicit operator AbstractWrapper<T>(T value)
        {
            return Factory(value);
        }

        public static implicit operator T(AbstractWrapper<T> wrapper)
        {
            return wrapper.Value;
        }
    }
}