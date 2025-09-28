using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresAndAlgorithms.Trees.BinaryTrees
{
    public abstract class RotatableBinaryTree<T, TTreeContent> : BinaryTree<T, TTreeContent>
        where T : IComparable<T>
        where TTreeContent : ITreeContent<T>, new()
    {
        protected delegate void RotationCallback(ref TreeElement newRoot, ref TreeElement oldRoot);
        protected abstract RotationCallback Callback { get; }

        protected void RotateToRight(TreeElement treeElement)
        {
            TreeElement copy = treeElement;
            TreeElement parent = treeElement.Parent;
            TreeElement newRoot = treeElement.Left;
            if (newRoot.Right is not null)
            {
                newRoot.Right.Parent = treeElement;
            }
            treeElement.Left = newRoot.Right;
            newRoot.Parent = treeElement.Parent;
            treeElement.Parent = newRoot;
            newRoot.Right = treeElement;

            Callback?.Invoke(ref newRoot, ref treeElement);

            ConnectToTree(ref newRoot, ref copy, ref parent);
        }

        protected void RotateToLeft(TreeElement treeElement)
        {
            TreeElement copy = treeElement;
            TreeElement parent = treeElement.Parent;
            TreeElement newRoot = treeElement.Right;

            if (newRoot.Left is not null)
            {
                newRoot.Left.Parent = treeElement;
            }
            treeElement.Right = newRoot.Left;
            newRoot.Parent = treeElement.Parent;
            treeElement.Parent = newRoot;
            newRoot.Left = treeElement;

            Callback?.Invoke(ref newRoot, ref treeElement);

            ConnectToTree(ref newRoot, ref copy, ref parent);
        }

        protected void ConnectToTree(ref TreeElement newRoot, ref TreeElement oldRoot, ref TreeElement oldParent)
        {
            if (root == oldRoot)
            {
                root = newRoot;
            }
            else if (oldParent.Right == oldRoot)
            {
                oldParent.Right = newRoot;
            }
            else
            {
                oldParent.Left = newRoot;
            }
        }
    }
}