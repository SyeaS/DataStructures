using DataStructures.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Trees.BinaryTrees
{
    public interface ITreapTreeContent<T, TComparableWrapper, TNumber> : ITreeContent<T>
        where T : IComparable<T>
        where TComparableWrapper : IComparableWrapper<TNumber>
        where TNumber : struct
    {
        TComparableWrapper Priority { get; }
    }
}