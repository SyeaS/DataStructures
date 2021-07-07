using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortedPlayerQueue
{
    public sealed class AVLTree<TContent> : BinarySearchTree<TContent>
        where TContent : IComparable<TContent>
    {
        /*private class AVLTreeElement : TreeElement
        {
            public int Height { get; set; }

            public AVLTreeElement(TContent content) : base(content)
            {

            }
        }*/

        public AVLTree()
        {
            Added = new TreeElementAdded(Rotate);
        }

        public AVLTree(IEnumerable<TContent> content) : this()
        {
            foreach (TContent item in content)
            {
                this.Add(item);
            }
        }

        private void Rotate(TreeElement treeElement)
        {
            TreeElement grandParent = null;

            try
            {
                grandParent = GrandParent(treeElement);
            }
            catch (Exception)
            {
                return;
            }
            if (grandParent == null)
            {
                return;
            }

            TreeElement element = null;

            int distance = BalanceFactor(grandParent);
            int rightDistance = BalanceFactor(grandParent?.Right);
            int leftDistance = BalanceFactor(grandParent?.Left);

            /*
             * int leftDistance1 = BalanceFactor(grandParent?.Left?.Left);
             * int rightLeftDistance = BalanceFactor(grandParent?.Right?.Left);
             * int leftRightDistance = BalanceFactor(grandParent?.Left?.Right);
             * int rightDistance1 = BalanceFactor(grandParent?.Right?.Right);
             */

            if (distance == -2)
            {
                if (leftDistance == 1 && BalanceFactor(grandParent.Left.Right) == 0 &&
                grandParent.Left != null && grandParent.Left.Right != null)
                {
                    element = LeftToRight(treeElement, grandParent);
                }
                else if (leftDistance == -1 && BalanceFactor(grandParent.Left.Left) == 0 &&
                grandParent.Left != null && grandParent.Left.Left != null)
                {
                    element = LeftToLeft(treeElement, grandParent);
                }
            }
            else if (distance == 2)
            {
                if (rightDistance == -1 && BalanceFactor(grandParent.Right.Left) == 0 &&
                grandParent.Right != null && grandParent.Right.Left != null)
                {
                    element = RightToLeft(treeElement, grandParent);
                }
                else if (rightDistance == 1 && BalanceFactor(grandParent.Right.Right) == 0 &&
                grandParent.Right != null && grandParent.Right.Right != null)
                {
                    element = RightToRight(treeElement, grandParent);
                }
            }
            
            
            
            

            if (element != null)
            {
                ReplaceTreeElement(grandParent, element);
            }
        }

        private TreeElement RightToRight(TreeElement treeElement, TreeElement grandParent)
        {
            grandParent.Right = null;

            treeElement.Parent.Left = grandParent;
            treeElement.Parent.Parent = grandParent.Parent;
            treeElement.Parent.Left.Parent = treeElement;

            return treeElement.Parent;
        }

        private TreeElement RightToLeft(TreeElement treeElement, TreeElement grandParent)
        {
            treeElement.Right = treeElement.Parent;
            treeElement.Left = treeElement.Parent.Parent;

            treeElement.Parent = grandParent.Parent;

            treeElement.Right.Parent = treeElement;
            treeElement.Left.Parent = treeElement;

            treeElement.Right.Left = null;
            treeElement.Left.Right = null;

            //treeElement.Parent = treeElement; // új!

            return treeElement;
        }

        private TreeElement LeftToLeft(TreeElement treeElement, TreeElement grandParent)
        {
            //treeElement.Right.Parent = treeElement.Parent;
            //treeElement.Parent.Parent = treeElement;
            treeElement = treeElement.Parent;
            treeElement.Parent = grandParent.Parent;
            treeElement.Right = grandParent;
            grandParent.Left = null;
            treeElement.Right.Parent = treeElement;

            //CopyToTreeElement(ref grandParent, treeElement);

            /*grandParent.Left = null;

            treeElement.Parent.Right = grandParent;
            treeElement.Parent.Parent = grandParent.Parent;
            treeElement.Parent.Right.Parent = treeElement.Parent;*/

            return treeElement;
        }

        private TreeElement LeftToRight(TreeElement treeElement, TreeElement grandParent)
        {
            treeElement.Left = treeElement.Parent;
            treeElement.Right = treeElement.Parent.Parent;

            treeElement.Parent = grandParent.Parent;

            treeElement.Right.Parent = treeElement;
            treeElement.Left.Parent = treeElement;

            treeElement.Right.Left = null;
            treeElement.Left.Right = null;

            return treeElement;
        }
    }
}