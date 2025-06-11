using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Trees.BinaryTrees
{
    public sealed class AVLTree<T> : RotatableBinarySearchTree<T, AVLTreeContent<T>>
        where T : IComparable<T>
    {
        protected override RotationCallBack CallBack { get; }

        public AVLTree()
        {
            CallBack = new RotationCallBack(Rotation);
        }

        public AVLTree(IEnumerable<T> content) : this()
        {
            base.CreateFromIEnumerable(ref content);
        }

        protected override TreeElement InternalAdd(ref T content)
        {
            TreeElement treeElement = base.InternalAdd(ref content);
            UpdateHeight(ref treeElement);
            return treeElement;
        }

        protected override TreeElement InternalRemove(ref T content)
        {
            TreeElement treeElement = base.InternalRemove(ref content);
            UpdateHeight(ref treeElement);
            return treeElement;
        }

        protected override T InternalPopMin()
        {
            TreeElement minimum = Minimum(root);
            base.RemoveTreeElement(ref minimum);
            UpdateHeight(ref minimum);
            return minimum.TreeContent.Content;
        }

        protected override T InternalPopMax()
        {
            TreeElement maximum = Maximum(root);
            base.RemoveTreeElement(ref maximum);
            UpdateHeight(ref maximum);
            return maximum.TreeContent.Content;
        }

        private void UpdateHeight(ref TreeElement treeElement)
        {
            TreeElement m = treeElement?.Parent;

            while (m != null)
            {
                m.TreeContent.Height = ComputeHeightFromChildren(ref m);

                Balance(ref m);
                m = m.Parent;
            }
        }

        private void Balance(ref TreeElement treeElement)
        {
            if (treeElement == null)
            {
                return;
            }

            uint leftH = GetAVLHeight(treeElement.Left);
            uint rightH = GetAVLHeight(treeElement.Right);

            if (leftH > rightH + 1)
            {
                uint leftLeftH = GetAVLHeight(treeElement.Left.Left);
                uint leftRightH = GetAVLHeight(treeElement.Left.Right);

                if (leftLeftH >= leftRightH)
                {
                    RotateToRight(treeElement);
                }
                else
                {
                    LeftToRight(treeElement);
                }
            }

            if (rightH > leftH + 1)
            {
                uint rightLeftH = GetAVLHeight(treeElement.Right.Left);
                uint rightRighttH = GetAVLHeight(treeElement.Right.Right);

                if (rightRighttH >= rightLeftH)
                {
                    RotateToLeft(treeElement);
                }
                else
                {
                    RightToLeft(treeElement);
                }
            }
        }

        private uint GetAVLHeight(TreeElement treeElement)
        {
            if (treeElement is null)
            {
                return 0;
            }
            return treeElement.TreeContent.Height;
        }

        private uint ComputeHeightFromChildren(ref TreeElement treeElement)
        {
            uint leftHeight = GetAVLHeight(treeElement.Left);
            uint rightHeight = GetAVLHeight(treeElement.Right);
            return 1 + Math.Max(leftHeight, rightHeight);
        }

        private void RightToLeft(TreeElement treeElement)
        {
            RotateToRight(treeElement.Right);
            RotateToLeft(treeElement);
        }

        private void LeftToRight(TreeElement treeElement)
        {
            RotateToLeft(treeElement.Left);
            RotateToRight(treeElement);
        }

        private void Rotation(ref TreeElement newRoot, ref TreeElement oldRoot)
        {
            oldRoot.TreeContent.Height = ComputeHeightFromChildren(ref oldRoot);
            newRoot.TreeContent.Height = ComputeHeightFromChildren(ref newRoot);
        }
    }
}