using DataStructures.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Trees.BinaryTrees
{
    public sealed class MaxTreapTree<T, TComparableWrapper, TNumber> : TreapTree<T, TComparableWrapper, TNumber>
        where T : IComparable<T>
        where TComparableWrapper : IComparableWrapper<TNumber>
        where TNumber : struct
    {
        public MaxTreapTree(RandomGenerator randomGenerator, TComparableWrapper minValue) : base(minValue, randomGenerator)
        {

        }

        public MaxTreapTree(IEnumerable<T> content, RandomGenerator randomGenerator, TComparableWrapper minValue) : base(minValue, randomGenerator)
        {
            base.CreateFromIEnumerable(ref content);
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

        protected override void FixTreeAfterInsertion(TreeElement treeElement)
        {
            while (!(treeElement == root || treeElement == null))
            {
                if (treeElement.Parent.Right == treeElement)
                {

                    if (GetPriority(ref treeElement).CompareTo(GetPriority(treeElement.Parent).Value) >= 0)
                    {
                        RotateToLeft(treeElement.Parent);
                    }
                }
                else
                {
                    if (GetPriority(ref treeElement).CompareTo(GetPriority(treeElement.Parent).Value) >= 0)
                    {
                        RotateToRight(treeElement.Parent);
                    }
                }

                treeElement = treeElement.Parent;
            }
        }

        private void RemoveTwoChildrenNode(TreeElement treeElement)
        {
            while (!(treeElement.Right == null || treeElement.Left == null))
            {
                if (GetPriority(ref treeElement).CompareTo(GetPriority(treeElement.Parent).Value) <= 0)
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

        public override T Pop(T content)
        {
            return InternalRemove(ref content).TreeContent.Content;
        }

        public override T PopMin()
        {
            TreeElement treeElement = Minimum(root);

            Remove(ref treeElement);

            return treeElement.TreeContent.Content;
        }

        public override T PopMax()
        {
            TreeElement treeElement = Maximum(root);

            Remove(ref treeElement);

            return treeElement.TreeContent.Content;
        }

        public T PopMaxPriority()
        {
            T copy = root.TreeContent.Content;
            Remove(ref root);
            return copy;
        }
    }
}