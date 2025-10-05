using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresAndAlgorithms.Trees.BinaryTrees
{
	public abstract class TreapTree<T, TNumber> : RotatableBinarySearchTree<T, TreapTreeContent<T, TNumber>>
		where T : IComparable<T>
		where TNumber : struct, INumber<TNumber>, IMinMaxValue<TNumber>
	{
		protected abstract class InOrderByPriorityData : IDataFactory<InOrderByPriorityData>
		{
			public abstract InOrderByPriorityData CreateEmpty();
			public abstract void Initialize();
		}

		protected TNumber Value { get; }

		public delegate TNumber RandomGenerator();
		private RandomGenerator GenerateRandom { get; }
		protected override RotationCallback Callback { get; }

		protected TreapTree(TNumber value, RandomGenerator randomGenerator)
		{
			Value = value;
			GenerateRandom = randomGenerator;
		}

		protected override TreeElement InternalAdd(ref T content)
		{
			TreeElement treeElement = base.InternalAdd(ref content);
			treeElement.TreeContent.Priority = GenerateRandom();
			FixTreeAfterInsertion(treeElement);
			return treeElement;
		}

		protected override TreeElement InternalRemove(ref T content)
		{
			TreeElement treeElement = base.Search(ref content);

			Remove(ref treeElement);

			return treeElement;
		}

		protected abstract void FixTreeAfterInsertion(TreeElement treeElement);
		protected abstract void Remove(ref TreeElement treeElement);
		public abstract IEnumerable<TNumber> InOrderByPriority();

		protected TNumber GetPriority(TreeElement treeElement)
		{
			return GetPriority(ref treeElement);
		}

		protected TNumber GetPriority(ref TreeElement treeElement)
		{
			return treeElement is not null ?
				treeElement.TreeContent.Priority : Value;
		}
	}
}