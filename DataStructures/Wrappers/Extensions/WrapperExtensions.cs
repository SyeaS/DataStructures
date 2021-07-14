using DataStructures.Wrappers.Numerics;
using System;

namespace DataStructures.Wrappers
{
    public static class WrapperExtensions
    {
        public static IntWrapper ToWrapper(this int value)
        {
            return new IntWrapper(value);
        }

        public static UnsignedIntWrapper ToWrapper(this uint value)
        {
            return new UnsignedIntWrapper(value);
        }

        public static LongWrapper ToWrapper(this long value)
        {
            return new LongWrapper(value);
        }

        public static UnsignedLongWrapper ToWrapper(this ulong value)
        {
            return new UnsignedLongWrapper(value);
        }

        public static FloatWrapper ToWrapper(this float value)
        {
            return new FloatWrapper(value);
        }

        public static DoubleWrapper ToWrapper(this double value)
        {
            return new DoubleWrapper(value);
        }

        public static DecimalWrapper ToWrapper(this decimal value)
        {
            return new DecimalWrapper(value);
        }

        public static ByteWrapper ToWrapper(this byte value)
        {
            return new ByteWrapper(value);
        }

        public static SignedByteWrapper ToWrapper(this sbyte value)
        {
            return new SignedByteWrapper(value);
        }

        public static ShortWrapper ToWrapper(this short value)
        {
            return new ShortWrapper(value);
        }

        public static UnsignedShortWrapper ToWrapper(this ushort value)
        {
            return new UnsignedShortWrapper(value);
        }

        public static IntPtrWrapper ToWrapper(this IntPtr value)
        {
            return new IntPtrWrapper(value);
        }

        public static UnsignedIntPtrWrapper ToWrapper(this UIntPtr value)
        {
            return new UnsignedIntPtrWrapper(value);
        }
    }
}