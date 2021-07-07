using DataStructures.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Trees.BinaryTrees
{
    public class TreapTreeContent<T, TComparableWrapper, TNumber> : TreeContent<T>, ITreapTreeContent<T, TComparableWrapper, TNumber>
        where T : IComparable<T>
        where TComparableWrapper : IComparableWrapper<TNumber>
        where TNumber : struct
    {
        public TComparableWrapper Priority { get; set; }

        public TreapTreeContent()
        {

        }
    }
}