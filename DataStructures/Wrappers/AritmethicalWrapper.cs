using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Wrappers
{
    public class AritmethicalWrapper<T> : ComparableWrapper<T>, IAritmethicalWrapper<T>
        where T : struct, IComparable<T>
    {
        private static IAritmethicalWrapper<T>.Addition Addition { get; set; }
        private static IAritmethicalWrapper<T>.Subtraction Subtraction { get; set; }

        public AritmethicalWrapper(T value, IAritmethicalWrapper<T>.Addition add, IAritmethicalWrapper<T>.Subtraction subtract) : 
            this(ref value, ref add, ref subtract)
        {

        }

        protected AritmethicalWrapper(ref T value, ref IAritmethicalWrapper<T>.Addition add, ref IAritmethicalWrapper<T>.Subtraction subtract) : base(ref value)
        {
            Addition = add;
            Subtraction = subtract;
        }

        public T Add(T value, T otherValue)
        {
            return Addition(value, otherValue);
        }

        public T Subtract(T value, T otherValue)
        {
            return Subtraction(value, otherValue);
        }

        public override object Clone()
        {
            return new AritmethicalWrapper<T>(this.Value,
                Addition.Clone() as IAritmethicalWrapper<T>.Addition,
                Subtraction.Clone() as IAritmethicalWrapper<T>.Subtraction);
        }

        protected override AbstractWrapper<T> CreateWrapper(T value)
        {
            return new AritmethicalWrapper<T>(value, Addition, Subtraction);
        }

        public static implicit operator T(AritmethicalWrapper<T> wrapper)
        {
            return wrapper.Value;
        }
    }
}