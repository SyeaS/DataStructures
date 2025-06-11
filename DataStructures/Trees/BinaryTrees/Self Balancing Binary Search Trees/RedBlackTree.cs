using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Trees.BinaryTrees
{
    // https://www.andrew.cmu.edu/user/mm6/95-771/examples/RedBlackTreeProject/dist/javadoc/redblacktreeproject/RedBlackTree.html#RBInsertFixup(redblacktreeproject.RedBlackNode)
    // http://staff.ustc.edu.cn/~csli/graduate/algorithms/book6/chap14.htm
    // http://software.ucv.ro/~mburicea/lab8ASD.pdf
    // https://simpledevcode.wordpress.com/2014/12/25/red-black-tree-in-c/

    public sealed class RedBlackTree<T> : RotatableBinarySearchTree<T, RedBlackTreeContent<T>>
        where T : IComparable<T>
    {
        protected override RotationCallBack CallBack { get; }

        public RedBlackTree()
        {
            CallBack = new RotationCallBack(Rotation);
        }

        public RedBlackTree(IEnumerable<T> content) : this()
        {
            base.CreateFromIEnumerable(ref content);
        }

        protected override TreeElement InternalAdd(ref T content)
        {
            TreeElement treeElement = base.InternalAdd(ref content);

            if (treeElement.Parent is null)
            {
                ChangeColor(ref treeElement, true);
                return treeElement;
            }

            if (GrandParent(treeElement) is null)
            {
                return treeElement;
            }

            InsertFixUp(treeElement);
            return treeElement;
        }

        protected override TreeElement InternalRemove(ref T content)
        {
            TreeElement treeElement = base.InternalRemoveWithoutDetaching(ref content, out TreeElement newElement);
            InternalRedBlackTreeRemove(ref treeElement, ref newElement);
            return treeElement;
        }

        private void InternalRedBlackTreeRemove(ref TreeElement treeElement, ref TreeElement newElement)
        {
            bool uvBlack = (newElement == null || IsBlack(ref newElement) && (IsBlack(ref treeElement)));

            // True when treeElement and newElement are both black
            TreeElement parent = treeElement.Parent;

            if (newElement == null)
            {
                // treeElement is NULL therefore newElement is leaf
                if (treeElement == root)
                {
                    // newElement is root, making root null
                    root = null;
                }
                else
                {
                    if (uvBlack)
                    {
                        // treeElement and newElement both black
                        // newElement is leaf, fix double black at newElement
                        FixDoubleBlack(treeElement);
                    }
                    else
                    {
                        // treeElement or newElement is red
                        TreeElement sibling = Sibling(treeElement);
                        if (sibling != null)
                        {
                            // sibling is not null, make it red"
                            ChangeColor(ref sibling, false);
                        }
                    }

                    // delete newElement from the tree
                    if (treeElement == treeElement.Parent.Left)
                    {
                        parent.Left = null;
                    }
                    else
                    {
                        parent.Right = null;
                    }
                }
                return;
            }

            if (treeElement.Left == null || treeElement.Right == null)
            {
                // newElement has 1 child
                if (treeElement == root)
                {
                    // newElement is root, assign the value of treeElement to newElement, and delete treeElement
                    treeElement.TreeContent.Content = newElement.TreeContent.Content;

                    treeElement.Left = null;
                    treeElement.Right = null;
                }
                else
                {
                    // Detach newElement from tree and move treeElement up
                    if (treeElement == treeElement.Parent.Left)
                    {
                        parent.Left = newElement;
                    }
                    else
                    {
                        parent.Right = newElement;
                    }

                    treeElement = null;
                    newElement.Parent = parent;
                    if (uvBlack)
                    {
                        // treeElement and newElement both black, fix double black at treeElement
                        FixDoubleBlack(newElement);
                    }
                    else
                    {
                        // treeElement or newElement red, color treeElement black
                        ChangeColor(ref newElement, true);
                    }
                }
                return;
            }

            // newElement has 2 children, swap values with successor and recurse
            SwapValues(newElement, treeElement);
            InternalRedBlackTreeRemove(ref newElement);
        }

        private void InternalRedBlackTreeRemove(ref TreeElement treeElement)
        {
            TreeElement newElement = ReplaceTreeElement(treeElement);
            InternalRedBlackTreeRemove(ref treeElement, ref newElement);
        }

        protected override T InternalPopMin()
        {
            TreeElement minimum = Minimum(root);
            base.InternalRemoveWithoutDetaching(ref minimum, out TreeElement newElement);
            InternalRedBlackTreeRemove(ref minimum, ref newElement);
            return minimum.TreeContent.Content;
        }

        protected override T InternalPopMax()
        {
            TreeElement maximum = Maximum(root);
            base.InternalRemoveWithoutDetaching(ref maximum, out TreeElement newElement);
            InternalRedBlackTreeRemove(ref maximum, ref newElement);
            return maximum.TreeContent.Content;
        }

        private void InsertFixUp(TreeElement treeElement)
        {
            TreeElement uncle;
            while (GetColor(treeElement.Parent) == false)
            {
                TreeElement grandParent = GrandParent(treeElement);

                if (treeElement.Parent == grandParent.Right)
                {
                    uncle = grandParent.Left;
                    if (GetColor(ref uncle) == false)
                    {
                        ChangeColor(ref uncle, true);
                        ChangeColor(treeElement.Parent, true);
                        ChangeColor(ref grandParent, false);
                        treeElement = grandParent;
                    }
                    else
                    {
                        if (treeElement == treeElement.Parent.Left)
                        {
                            treeElement = treeElement.Parent;
                            RotateToRight(treeElement);
                        }

                        ChangeColor(treeElement.Parent, true);
                        ChangeColor(ref grandParent, false);
                        RotateToLeft(grandParent);
                    }
                }
                else
                {
                    uncle = grandParent.Right;

                    if (GetColor(uncle) == false)
                    {
                        ChangeColor(ref uncle, true);
                        ChangeColor(treeElement.Parent, true);
                        ChangeColor(grandParent, false);
                        treeElement = grandParent;
                    }
                    else
                    {
                        if (treeElement == treeElement.Parent.Right)
                        {
                            treeElement = treeElement.Parent;
                            RotateToLeft(treeElement);
                        }

                        ChangeColor(treeElement.Parent, true);
                        ChangeColor(ref grandParent, false);
                        RotateToRight(grandParent);
                    }
                }
                if (treeElement == root)
                {
                    break;
                }
            }

            ChangeColor(ref root, true);
        }

        private void Rotation(ref TreeElement newRoot, ref TreeElement oldRoot)
        {

        }

        private void FixDoubleBlack(TreeElement treeElement)
        {
            if (treeElement == root)
            {// Reached root
                return;
            }

            TreeElement sibling = Sibling(treeElement);
            TreeElement parent = treeElement.Parent;
            if (sibling == null)
            {
                // No sibiling, double black pushed up
                FixDoubleBlack(parent);
            }
            else
            {
                if (IsRed(ref sibling))
                {
                    // Sibling red
                    ChangeColor(ref parent, false);
                    ChangeColor(ref sibling, true);
                    if (sibling == sibling.Parent.Left)
                    {
                        // left case
                        RotateToRight(parent);
                    }
                    else
                    {
                        // right case
                        RotateToLeft(parent);
                    }
                    FixDoubleBlack(treeElement);
                }
                else
                {
                    // Sibling black
                    if (IsRed(sibling.Right) || IsRed(sibling.Left))
                    {
                        // at least 1 red children
                        if (sibling.Left != null && IsRed(sibling.Left))
                        {
                            if (sibling == sibling.Parent.Left)
                            {
                                // left left
                                ChangeColor(sibling.Left, GetColor(ref sibling));
                                ChangeColor(ref sibling, GetColor(ref parent));
                                RotateToRight(parent);
                            }
                            else
                            {
                                // right left
                                ChangeColor(sibling.Left, GetColor(ref parent));
                                RotateToRight(sibling);
                                RotateToLeft(parent);
                            }
                        }
                        else
                        {
                            if (sibling == sibling.Parent.Left)
                            {
                                // left right
                                ChangeColor(sibling.Right, GetColor(ref parent));
                                RotateToLeft(sibling);
                                RotateToRight(parent);
                            }
                            else
                            {
                                // right right
                                ChangeColor(sibling.Right, GetColor(ref sibling));
                                ChangeColor(ref sibling, GetColor(ref parent));
                                RotateToLeft(parent);
                            }
                        }
                        ChangeColor(ref parent, true);
                    }
                    else
                    {
                        // 2 black children
                        ChangeColor(ref sibling, false);
                        if (IsBlack(ref parent))
                        {
                            FixDoubleBlack(parent);
                        }
                        else
                        {
                            ChangeColor(ref parent, true);
                        }
                    }
                }
            }
        }

        private void SwapValues(TreeElement treeElement, TreeElement treeElement1)
        {
            T temp;
            temp = treeElement.TreeContent.Content;
            treeElement.TreeContent.Content = treeElement1.TreeContent.Content;
            treeElement1.TreeContent.Content = temp;
        }

        private static bool GetColor(TreeElement treeElement)
        {
            return GetColor(ref treeElement);
        }

        private static bool GetColor(ref TreeElement treeElement)
        {
            if (treeElement is null)
            {
                return true;
            }

            return treeElement.TreeContent.IsBlack;
        }

        private static bool IsBlack(TreeElement treeElement)
        {
            return IsBlack(ref treeElement);
        }

        private static bool IsBlack(ref TreeElement treeElement)
        {
            if (treeElement is null)
            {
                return true;
            }
            return treeElement.TreeContent.IsBlack;
        }

        private static bool IsRed(TreeElement treeElement)
        {
            return IsRed(ref treeElement);
        }

        private static bool IsRed(ref TreeElement treeElement)
        {
            if (treeElement is null)
            {
                return false;
            }
            return !treeElement.TreeContent.IsBlack;
        }

        private static void ChangeColor(TreeElement treeElement, bool isBlack)
        {
            if (treeElement is null)
            {
                return;
            }
            treeElement.TreeContent.IsBlack = isBlack;
        }

        private static void ChangeColor(ref TreeElement treeElement, bool isBlack)
        {
            if (treeElement is null)
            {
                return;
            }
            ChangeColor(treeElement, isBlack);
        }
    }
}