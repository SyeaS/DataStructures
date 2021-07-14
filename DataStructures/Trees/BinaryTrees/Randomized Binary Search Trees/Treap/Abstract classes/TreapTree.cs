using DataStructures.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Trees.BinaryTrees
{
    public abstract class TreapTree<T, TComparableWrapper, TNumber> : RotatableBinarySearchTree<T, TreapTreeContent<T, TComparableWrapper, TNumber>>
        where T : IComparable<T>
        where TComparableWrapper : AbstractWrapper<TNumber>, IComparableWrapper<TNumber>
        where TNumber : struct
    {
        protected TComparableWrapper Value { get; }

        public delegate TComparableWrapper RandomGenerator();
        private RandomGenerator GenerateRandom { get; }
        protected override RotationCallBack CallBack { get; }

        protected TreapTree(TComparableWrapper value, RandomGenerator randomGenerator)
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
        public abstract IEnumerable<TComparableWrapper> InOrderByPriority();

        protected TComparableWrapper GetPriority(TreeElement treeElement)
        {
            return GetPriority(ref treeElement);
        }

        protected TComparableWrapper GetPriority(ref TreeElement treeElement)
        {
            return treeElement is not null ? treeElement.TreeContent.Priority : Value;
        }
    }
}