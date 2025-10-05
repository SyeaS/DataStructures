using DataStructuresAndAlgorithms.Heaps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresAndAlgorithms.Trees.BinaryTrees
{
    public sealed class MinTreapTree<T, TNumber> : TreapTree<T, TNumber>
        where T : IComparable<T>
        where TNumber : struct, INumber<TNumber>, IMinMaxValue<TNumber>
	{
        public MinTreapTree(RandomGenerator randomGenerator) : base(TNumber.MaxValue, randomGenerator)
        {

        }

        public MinTreapTree(IEnumerable<T> content, RandomGenerator randomGenerator) : base(TNumber.MaxValue, randomGenerator)
        {
            base.CreateFromIEnumerable(ref content);
        }

        protected override void FixTreeAfterInsertion(TreeElement treeElement)
        {
            while (!(treeElement == root || treeElement == null))
            {
                if (treeElement.Parent.Right == treeElement)
                {
                    if (GetPriority(ref treeElement).CompareTo(GetPriority(treeElement.Parent)) <= 0)
                    {
                        RotateToLeft(treeElement.Parent);
                    }
                }
                else
                {
                    if (GetPriority(ref treeElement).CompareTo(GetPriority(treeElement.Parent)) <= 0)
                    {
                        RotateToRight(treeElement.Parent);
                    }
                }

                treeElement = treeElement.Parent;
            }
        }

        protected override void Remove(ref TreeElement treeElement)
        {
            if (!HasTwoChildren(treeElement))
            {
                DeleteTreeElement(ref treeElement);
            }
            else
            {
                treeElement.TreeContent.Priority = Value;
                RemoveTwoChildrenNode(treeElement);
            }
        }

        private void RemoveTwoChildrenNode(TreeElement treeElement)
        {
            while (!(treeElement.Right == null || treeElement.Left == null))
            {
                if (GetPriority(ref treeElement).CompareTo(GetPriority(treeElement.Parent)) >= 0)
                {
                    RotateToLeft(treeElement);
                }
                else
                {
                    RotateToRight(treeElement);
                }
            }

            DeleteTreeElement(ref treeElement);
        }

        protected override T InternalPopMin()
        {
            TreeElement treeElement = Minimum(root);

            Remove(ref treeElement);

            return treeElement.TreeContent.Content;
        }

        protected override T InternalPopMax()
        {
            TreeElement treeElement = Maximum(root);

            Remove(ref treeElement);

            return treeElement.TreeContent.Content;
        }

        public T PopMinPriority()
        {
            T copy = root.TreeContent.Content;
            Remove(copy);
            return copy;
        }

        public override IEnumerable<TNumber> InOrderByPriority()
        {
            BinaryMinHeap<TNumber> minHeap = new BinaryMinHeap<TNumber>(this.Count);

            foreach (TreeElement treeElement in this.InternalPreOrder())
            {
                minHeap.Add(treeElement.TreeContent.Priority);
            }

            int count = minHeap.Count;

            for (int i = 0; i < count; i++)
            {
                yield return minHeap.Extract();
            }
        }
    }
}