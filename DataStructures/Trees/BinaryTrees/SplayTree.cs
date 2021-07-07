using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Trees.BinaryTrees
{
    public sealed class SplayTree<T> : RotatableBinarySearchTree<T, TreeContent<T>>
        where T : IComparable<T>
    {
        protected override RotationCallBack CallBack { get; }
        private TreeElement NewRoot { get; set; }

        public SplayTree()
        {
            CallBack = new RotationCallBack(Rotation);
        }

        public SplayTree(IEnumerable<T> content) : this()
        {
            base.CreateFromIEnumerable(ref content);
        }

        protected override TreeElement InternalAdd(ref T content)
        {
            TreeElement treeElement = base.InternalAdd(ref content);
            Splay(ref treeElement);
            return root;
        }

        protected override TreeElement InternalRemove(ref T content)
        {
            TreeElement treeElement = base.InternalRemove(ref content);
            return treeElement;
        }

        public override T Pop(T content)
        {
            return InternalRemove(ref content).TreeContent.Content;
        }

        public override T PopMin()
        {
            TreeElement minimum = Minimum(root);

            RemoveTreeElement(ref minimum);

            return minimum.TreeContent.Content;
        }

        public override T PopMax()
        {
            TreeElement maximum = Maximum(root);

            RemoveTreeElement(ref maximum);

            return maximum.TreeContent.Content;
        }

        protected override TreeElement Search(ref T content)
        {
            TreeElement treeElement = base.Search(ref content);
            Splay(ref treeElement);
            return root;
        }

        private void Splay(ref TreeElement treeElement)
        {
            TreeElement parent = null;
            TreeElement grandParent = null;

            while (treeElement.Parent != null)
            {
                parent = treeElement.Parent;
                grandParent = parent.Parent;

                if (parent == root)
                {
                    if (parent.Left == treeElement)
                    {
                        RotateToRight(parent);
                    }
                    else
                    {
                        RotateToLeft(parent);
                    }

                    treeElement = NewRoot;
                }
                else
                {
                    if (parent.Left == treeElement && grandParent.Left == parent)
                    {
                        RotateToRight(grandParent);
                        RotateToRight(treeElement.Parent);
                    }
                    else if (parent.Right == treeElement && grandParent.Right == parent)
                    {
                        RotateToLeft(grandParent);
                        RotateToLeft(treeElement.Parent);
                    }
                    else if (parent.Left == treeElement && grandParent.Right == parent)
                    {
                        RotateToRight(parent);
                        RotateToLeft(treeElement.Parent);
                    }
                    else
                    {
                        RotateToLeft(parent);
                        RotateToRight(treeElement.Parent);
                    }

                    treeElement = NewRoot;
                }
            }

            root = treeElement;
        }

        private void Rotation(ref TreeElement newRoot, ref TreeElement oldRoot)
        {
            NewRoot = newRoot;
        }
    }
}