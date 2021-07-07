using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStructures.LinkedList;

namespace DataStructures.Trees.BinaryTrees
{
    public abstract class AbstractBinarySearchTree<T, TTreeElement> : BinaryTree<T, TTreeElement>
        where T : IComparable<T>
        where TTreeElement : ITreeContent<T>, new()
    {
        protected override IEnumerable<T> DefaultIterator => InOrder();

        public AbstractBinarySearchTree()
        {

        }

        public AbstractBinarySearchTree(IEnumerable<T> content) : base(content)
        {

        }

        protected override TreeElement InternalAdd(ref T content)
        {
            TreeElement element = new TreeElement(content);

            if (root == null)
            {
                root = element;
            }
            else
            {
                TreeElement m = root;
                TreeElement m1 = null;

                while (m != null) // Bal oldal (kisebb), jobb oldal (nagyobb)
                {
                    m1 = m;
                    int value = m.TreeContent.Content.CompareTo(content);

                    if (value > 0) // kisebb
                    {
                        m = m.Left;
                    }
                    else if (value < 0) // nagyobb
                    {
                        m = m.Right;
                    }
                    else
                    {
                        throw new DuplicateTreeElementException(content, m.TreeContent.Content);
                    }
                }
                element.Parent = m1;

                AddElementToTreeElement(ref m1, ref element);
            }

            return element;
        }

        protected override void AddElementToTreeElement(ref TreeElement treeElement, ref TreeElement insertedTreeElement)
        {
            if (treeElement.TreeContent.Content.CompareTo(insertedTreeElement.TreeContent.Content) > 0)
            { // kisebb (bal)
                treeElement.Left = insertedTreeElement;
            }
            else // Nagyobb (jobb)
            {
                treeElement.Right = insertedTreeElement;
            }
        }

        protected override TreeElement InternalRemove(ref T content)
        {
            TreeElement element = Search(ref content);

            RemoveTreeElement(ref element);

            return element;
        }

        protected void RemoveTreeElement(ref TreeElement element)
        {
            if (HasTwoChildren(element))
            {
                TreeElement minElement = Minimum(element.Right);
                DeleteTreeElement(ref minElement);

                element.TreeContent = minElement.TreeContent;
                element = minElement;
            }
            else
            {
                DeleteTreeElement(ref element);
            }
        }

        protected override TreeElement InternalRemove(ref T content, out TreeElement treeElement)
        {
            TreeElement element = Search(ref content);
            treeElement = null;

            if (HasTwoChildren(element))
            {
                treeElement = Minimum(element.Right);
                DeleteTreeElement(ref treeElement, out TreeElement minTreeElement);
                treeElement = minTreeElement;

                element.TreeContent = treeElement.TreeContent;
            }
            else
            {
                DeleteTreeElement(ref element, out TreeElement newTreeElement);
                treeElement = newTreeElement;
            }

            return element;
        }

        protected TreeElement InternalRemoveWithoutDetaching(ref T content, out TreeElement treeElement)
        {
            TreeElement element = Search(ref content);
            treeElement = null;

            if (HasTwoChildren(element))
            {
                treeElement = Minimum(element.Right);
            }
            else
            {
                treeElement = ReplaceTreeElement(element);
            }

            return element;
        }

        protected void InternalRemoveWithoutDetaching(ref TreeElement element, out TreeElement treeElement)
        {
            if (HasTwoChildren(element))
            {
                treeElement = Minimum(element.Right);
            }
            else
            {
                treeElement = ReplaceTreeElement(element);
            }
        }

        protected override void DeleteTreeElement(ref TreeElement treeElement)
        {
            if (IsLeaf(ref treeElement))
            {
                if (!HasParent(treeElement))
                {
                    root = null;
                }
                else if (treeElement.Parent.Right == treeElement)
                {
                    treeElement.Parent.Right = null;
                }
                else
                {
                    treeElement.Parent.Left = null;
                }
            }
            else if (treeElement.Right != null)
            {
                ReplaceTreeElementInParent(ref treeElement, treeElement.Right);
            }
            else
            {
                ReplaceTreeElementInParent(ref treeElement, treeElement.Left);
            }
        }

        protected override void DeleteTreeElement(ref TreeElement treeElement, out TreeElement newTreeElement)
        {
            newTreeElement = null;

            if (IsLeaf(ref treeElement))
            {
                if (!HasParent(treeElement))
                {
                    root = null;
                }
                else if (treeElement.Parent.Right == treeElement)
                {
                    treeElement.Parent.Right = null;
                }
                else
                {
                    treeElement.Parent.Left = null;
                }
            }
            else if (treeElement.Right != null)
            {
                newTreeElement = treeElement.Right;
                ReplaceTreeElementInParent(ref treeElement, treeElement.Right);
            }
            else
            {
                newTreeElement = treeElement.Left;
                ReplaceTreeElementInParent(ref treeElement, treeElement.Left);
            }
        }

        protected override TreeElement ReplaceTreeElement(TreeElement treeElement)
        {
            TreeElement newTreeElement = null;

            if (treeElement.Right != null)
            {
                newTreeElement = treeElement.Right;
            }
            else
            {
                newTreeElement = treeElement.Left;
            }

            return newTreeElement;
        }

        protected override TreeElement Search(ref T content)
        {
            TreeElement m = root;

            while (!(m == null || m.TreeContent.Content.CompareTo(content) == 0))
            {
                if (m.TreeContent.Content.CompareTo(content) > 0)
                {
                    m = m.Left;
                }
                else if (m.TreeContent.Content.CompareTo(content) < 0)
                {
                    m = m.Right;
                }
                else
                {
                    throw new TreeElementNotFoundException(content);
                }
            }

            if (m == null)
            {
                throw new TreeElementNotFoundException(content);
            }

            return m;
        }

        protected override TreeElement Maximum(TreeElement treeElement)
        {
            while (treeElement.Right != null)
            {
                treeElement = treeElement.Right;
            }

            return treeElement;
        }

        protected override TreeElement Minimum(TreeElement treeElement)
        {
            while (treeElement.Left != null)
            {
                treeElement = treeElement.Left;
            }

            return treeElement;
        }

        public override IEnumerable<T> InOrder()
        {
            LinkedList.Stack<TreeElement> stack = new LinkedList.Stack<TreeElement>();
            RedBlackTree<T> processed = new RedBlackTree<T>();
            TreeElement m = root;

            while (m != null)
            {
                stack.Push(m);
                m = m.Left;
            }

            while (stack.Count != 0)
            {
                TreeElement treeElement = stack.Pop();
                yield return treeElement.TreeContent.Content;
                processed.Add(treeElement.TreeContent.Content);

                if (treeElement.Left != null && !processed.Contains(treeElement.Left.TreeContent.Content))
                {
                    stack.Push(treeElement.Left);
                }
                if (treeElement.Right != null && !processed.Contains(treeElement.Right.TreeContent.Content))
                {
                    stack.Push(treeElement.Right);
                }
            }

            processed.Dispose();
        }

        public override IEnumerable<T> PreOrder()
        {
            LinkedList.Stack<TreeElement> stack = new LinkedList.Stack<TreeElement>();
            RedBlackTree<T> processed = new RedBlackTree<T>();
            stack.Push(root);

            while (stack.Count != 0)
            {
                TreeElement treeElement = stack.Pop();
                yield return treeElement.TreeContent.Content;
                processed.Add(treeElement.TreeContent.Content);

                if (treeElement.Left != null && !processed.Contains(treeElement.Left.TreeContent.Content))
                {
                    stack.Push(treeElement.Left);
                }
                if (treeElement.Right != null && !processed.Contains(treeElement.Right.TreeContent.Content))
                {
                    stack.Push(treeElement.Right);
                }
            }

            processed.Dispose();
        }

        public override IEnumerable<T> PostOrder()
        {
            LinkedList.Stack<TreeElement> stack = new LinkedList.Stack<TreeElement>();
            RedBlackTree<T> processed = new RedBlackTree<T>();
            stack.Push(root);

            if (root.Left != null)
            {
                stack.Push(root.Left);
            }
            if (root.Right != null)
            {
                stack.Push(root.Right);
            }

            while (stack.Count != 0)
            {
                TreeElement treeElement = stack.Pop();
                yield return treeElement.TreeContent.Content;
                processed.Add(treeElement.TreeContent.Content);

                if (treeElement.Left != null && !processed.Contains(treeElement.Left.TreeContent.Content))
                {
                    stack.Push(treeElement.Left);
                }
                if (treeElement.Right != null && !processed.Contains(treeElement.Right.TreeContent.Content))
                {
                    stack.Push(treeElement.Right);
                }
            }

            processed.Dispose();
        }

        public override IEnumerable<T> BreadthFirst()
        {
            LinkedList.Queue<TreeElement> queue = new LinkedList.Queue<TreeElement>();
            queue.Enqueue(root);

            while (queue.Count != 0)
            {
                TreeElement actualTreeElement = queue.Dequeue();
                yield return actualTreeElement.TreeContent.Content;

                if (actualTreeElement.Left != null)
                {
                    queue.Enqueue(actualTreeElement.Left);
                }
                if (actualTreeElement.Right != null)
                {
                    queue.Enqueue(actualTreeElement.Right);
                }
            }
        }

        public override IEnumerable<T> DepthFirst()
        {
            LinkedList.Stack<TreeElement> stack = new LinkedList.Stack<TreeElement>();
            stack.Push(root);

            while (stack.Count != 0)
            {
                TreeElement actualTreeElement = stack.Pop();
                yield return actualTreeElement.TreeContent.Content;

                if (actualTreeElement.Right != null)
                {
                    stack.Push(actualTreeElement.Right);
                }
                if (actualTreeElement.Left != null)
                {
                    stack.Push(actualTreeElement.Left);
                }
            }
        }

        public override void InOrder(ITraversableTree<T>.TraversaryDelegate method)
        {
            foreach (T content in this.InOrder())
            {
                method.Invoke(content);
            }
        }

        public override void PreOrder(ITraversableTree<T>.TraversaryDelegate method)
        {
            foreach (T content in this.PreOrder())
            {
                method.Invoke(content);
            }
        }

        public override void PostOrder(ITraversableTree<T>.TraversaryDelegate method)
        {
            foreach (T content in this.PostOrder())
            {
                method.Invoke(content);
            }
        }

        public override void BreadthFirst(ITraversableTree<T>.TraversaryDelegate method)
        {
            foreach (T content in this.BreadthFirst())
            {
                method.Invoke(content);
            }
        }

        public override void DepthFirst(ITraversableTree<T>.TraversaryDelegate method)
        {
            foreach (T content in this.DepthFirst())
            {
                method.Invoke(content);
            }
        }
    }
}