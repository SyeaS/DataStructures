using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresAndAlgorithms.Trees.BinaryTrees
{
    public interface ITreapTreeContent<T, TNumber> : ITreeContent<T>
        where T : IComparable<T>
        where TNumber : struct, INumber<TNumber>, IMinMaxValue<TNumber>
	{
        TNumber Priority { get; }
    }
}