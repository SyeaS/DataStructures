using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresAndAlgorithms.Trees.BinaryTrees
{
	public class TreapTreeContent<T, TNumber> : TreeContent<T>, ITreapTreeContent<T, TNumber>
		where T : IComparable<T>
		where TNumber : struct, INumber<TNumber>, IMinMaxValue<TNumber>
	{
		public TNumber Priority { get; set; }

		public TreapTreeContent()
		{

		}
	}
}