using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortedPlayerQueue
{
    public class BinarySearchTree<TContent> : IDisposable, IEquatable<BinarySearchTree<TContent>>, ITraversableTree<TContent>
        where TContent : IComparable<TContent>
    {
        protected class TreeElement
        {
            public TContent Content { get; set; }
            public TreeElement Parent { get; set; }
            public TreeElement Right { get; set; }
            public TreeElement Left { get; set; }

            public TreeElement(TContent content)
            {
                Content = content;
            }
        }

        protected TreeElement root;
        protected int _count = 0;
        public int Count => _count;

        public bool IsReadOnly => false;

        protected delegate void TreeElementAdded(TreeElement treeElement);
        protected TreeElementAdded Added;

        public BinarySearchTree()
        {

        }

        protected BinarySearchTree(TreeElementAdded added)
        {
            Added = added;
        }

        /// <summary>
        /// Fills an IEnumerable of TContent into the binary search tree.
        /// </summary>
        /// <exception cref="DuplicateTreeElementException"></exception>
        /// <param name="content"></param>
        public BinarySearchTree(IEnumerable<TContent> content)
        {
            foreach (TContent item in content)
            {
                this.Add(item);
            }
        }

        /// <summary>
        /// Adds an element to the binary search tree.
        /// </summary>
        /// <exception cref="DuplicateTreeElementException"></exception>
        /// <param name="content"></param>
        public virtual void Add(TContent content)
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
                    if (m.Content.CompareTo(content) > 0) // kisebb
                    {
                        m = m.Left;
                    }
                    else if (m.Content.CompareTo(content) < 0) // nagyobb
                    {
                        m = m.Right;
                    }
                    else
                    {
                        throw new DuplicateTreeElementException(content, m.Content);
                    }
                }
                element.Parent = m1;

                AddElementToTreeElement(ref m1, ref element);
            }

            Added?.Invoke(element);
            _count++;
        }

        /// <summary>
        /// Removes an element from the binary search tree.
        /// </summary>
        /// <exception cref="TreeElementNotFoundException"></exception>
        /// <param name="content"></param>
        public virtual void Remove(TContent content)
        {
            TreeElement element = Search(ref content);

            if (element.Left != null && element.Right != null)
            {
                TreeElement minElement = Minimum(element.Right);
                DeleteTreeElement(ref minElement);
                element.Content = minElement.Content;
            }
            else
            {
                DeleteTreeElement(ref element);
            }

            _count--;
        }

        protected void DeleteTreeElement(ref TreeElement treeElement)
        {
            if (IsLeaf(ref treeElement))
            {
                if (treeElement.Parent.Right == treeElement)
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

        protected void ReplaceTreeElementInParent(ref TreeElement treeElement, TreeElement element)
        {
            if (treeElement.Parent != null)
            {
                if (treeElement.Parent.Right == treeElement)
                {
                    treeElement.Parent.Right = element;
                }
                else
                {
                    treeElement.Parent.Left = element;
                }

                element.Parent = treeElement.Parent;
            }
        }

        protected bool IsLeaf(ref TreeElement treeElement)
        {
            try
            {
                if (treeElement.Left == null && treeElement.Right == null)
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }

            return false;
        }

        protected bool HasTwoChildren(TreeElement treeElement)
        {
            return treeElement.Right != null && treeElement.Left != null;
        }

        protected TreeElement GrandParent(TreeElement treeElement)
        {
            return treeElement?.Parent.Parent;
        }

        protected void AddElementToTreeElement(ref TreeElement treeElement, ref TreeElement insertedTreeElement)
        {
            if (treeElement.Content.CompareTo(insertedTreeElement.Content) > 0)
            { // kisebb (bal)
                treeElement.Left = insertedTreeElement;
            }
            else // Nagyobb (jobb)
            {
                treeElement.Right = insertedTreeElement;
            }
        }

        /// <summary>
        /// Searches the binary search tree to found the value.
        /// </summary>
        /// <param name="content"></param>
        /// <returns>True if found; False if not found.</returns>
        public bool Contains(TContent content)
        {
            try
            {
                Search(ref content);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Searches the specified content.
        /// </summary>
        /// <param name="content"></param>
        /// <returns>The tree element which contains the element.</returns>
        /// <exception cref="TreeElementNotFoundException"></exception>
        protected TreeElement Search(ref TContent content)
        {
            TreeElement m = root;

            while (!(m == null || m.Content.CompareTo(content) == 0))
            {
                if (m.Content.CompareTo(content) > 0)
                {
                    m = m.Left;
                }
                else if (m.Content.CompareTo(content) < 0)
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

        protected void ReplaceTreeElement(TreeElement replacableTreeElement, TreeElement treeElement)
        {
            TContent content = replacableTreeElement.Content;
            TreeElement element = Search(ref content);

            if (replacableTreeElement == root)
            {
                root = treeElement;
            }
            else if (element.Parent.Parent.Right == replacableTreeElement)
            {
                replacableTreeElement.Parent.Parent.Right = treeElement;
            }
            else
            {
                replacableTreeElement.Parent.Parent.Left = treeElement;
            }
        }

        protected TreeElement Maximum(TreeElement treeElement)
        {
            while (treeElement.Right != null)
            {
                treeElement = treeElement.Right;
            }

            return treeElement;
        }

        protected TreeElement Minimum(TreeElement treeElement)
        {
            while (treeElement.Left != null)
            {
                treeElement = treeElement.Left;
            }

            return treeElement;
        }

        /// <summary>
        /// Counts the height between two tree elements.
        /// </summary>
        /// <param name="treeElement"></param>
        /// <param name="otherTreeElement"></param>
        /// <exception cref="TreeElementNotFoundException"></exception>
        /// <returns>The height difference between the two tree elements.</returns>
        protected int HeightBetween(TreeElement treeElement, TreeElement otherTreeElement)
        {
            int height = 0;

            while (treeElement != otherTreeElement)
            {
                if (treeElement.Content.CompareTo(otherTreeElement.Content) > 0)
                {
                    treeElement = treeElement.Left;
                }
                else if (treeElement.Content.CompareTo(otherTreeElement.Content) < 0)
                {
                    treeElement = treeElement.Right;
                }
                else
                {
                    throw new TreeElementNotFoundException(otherTreeElement);
                }

                height++;
            }

            return height;
        }

        protected int BalanceFactor(TreeElement treeElement)
        {
            int rightDistance = 0;
            int leftDistance = 0;

            if (treeElement is null)
            {
                return 0;
            }
            if (treeElement.Right != null)
            {
                rightDistance = Height(treeElement.Right);
            }
            if (treeElement.Left != null)
            {
                leftDistance = Height(treeElement.Left);
            }

            return rightDistance - leftDistance;
        }

        protected int Height(TreeElement treeElement)
        {
            return BreadthFirst(treeElement);
        }

        public bool Equals(BinarySearchTree<TContent> other)
        {
            foreach (TContent item in other.InOrder())
            {
                if (!this.Contains(item))
                {
                    return false;
                }
            }

            return true;
        }

        public void Dispose()
        {
            root = null;
            _count = 0;
        }

        public IEnumerable<TContent> InOrder()
        {
            Stack<TreeElement> stack = new Stack<TreeElement>();
            BinarySearchTree<TContent> processed = new BinarySearchTree<TContent>();
            TreeElement m = root;

            while (m != null)
            {
                stack.Push(m);
                m = m.Left;
            }

            while (stack.Count != 0)
            {
                TreeElement treeElement = stack.Pop();
                yield return treeElement.Content;
                processed.Add(treeElement.Content);

                if (treeElement.Left != null && !processed.Contains(treeElement.Left.Content))
                {
                    stack.Push(treeElement.Left);
                }
                if (treeElement.Right != null && !processed.Contains(treeElement.Right.Content))
                {
                    stack.Push(treeElement.Right);
                }
            }

            processed.Dispose();
        }

        public IEnumerable<TContent> PreOrder()
        {
            Stack<TreeElement> stack = new Stack<TreeElement>();
            BinarySearchTree<TContent> processed = new BinarySearchTree<TContent>();
            stack.Push(root);

            while (stack.Count != 0)
            {
                TreeElement treeElement = stack.Pop();
                yield return treeElement.Content;
                processed.Add(treeElement.Content);

                if (treeElement.Left != null && !processed.Contains(treeElement.Left.Content))
                {
                    stack.Push(treeElement.Left);
                }
                if (treeElement.Right != null && !processed.Contains(treeElement.Right.Content))
                {
                    stack.Push(treeElement.Right);
                }
            }

            processed.Dispose();
        }

        public IEnumerable<TContent> PostOrder()
        {
            Stack<TreeElement> stack = new Stack<TreeElement>();
            BinarySearchTree<TContent> processed = new BinarySearchTree<TContent>();
            stack.Push(root);

            if (root.Left != null)
            {
                stack.Push(root.Right);
                stack.Push(root.Left);
            }
            else
            {
                stack.Push(root.Left);
                stack.Push(root.Right);
            }

            while (stack.Count != 0)
            {
                TreeElement treeElement = stack.Pop();
                yield return treeElement.Content;
                processed.Add(treeElement.Content);

                if (treeElement.Left != null && !processed.Contains(treeElement.Left.Content))
                {
                    stack.Push(treeElement.Left);
                }
                if (treeElement.Right != null && !processed.Contains(treeElement.Right.Content))
                {
                    stack.Push(treeElement.Right);
                }
            }

            processed.Dispose();
        }

        public IEnumerable<TContent> BreadthFirst()
        {
            Queue<TreeElement> queue = new Queue<TreeElement>();
            queue.Enqueue(root);

            while (queue.Count != 0)
            {
                TreeElement actualTreeElement = queue.Dequeue();
                yield return actualTreeElement.Content;

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

        public IEnumerable<TContent> DepthFirst()
        {
            Stack<TreeElement> stack = new Stack<TreeElement>();
            stack.Push(root);

            while (stack.Count != 0)
            {
                TreeElement actualTreeElement = stack.Pop();
                yield return actualTreeElement.Content;

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

        public void InOrder(ITraversableTree<TContent>.TraversaryDelegate method)
        {
            Stack<TreeElement> stack = new Stack<TreeElement>();
            BinarySearchTree<TContent> processed = new BinarySearchTree<TContent>();
            TreeElement m = root;

            while (m != null)
            {
                stack.Push(m);
                m = m.Left;
            }

            while (stack.Count != 0)
            {
                TreeElement treeElement = stack.Pop();
                method?.Invoke(treeElement.Content);
                processed.Add(treeElement.Content);

                if (treeElement.Left != null && !processed.Contains(treeElement.Left.Content))
                {
                    stack.Push(treeElement.Left);
                }
                if (treeElement.Right != null && !processed.Contains(treeElement.Right.Content))
                {
                    stack.Push(treeElement.Right);
                }
            }

            processed.Dispose();
        }

        public void PreOrder(ITraversableTree<TContent>.TraversaryDelegate method)
        {
            Stack<TreeElement> stack = new Stack<TreeElement>();
            BinarySearchTree<TContent> processed = new BinarySearchTree<TContent>();
            stack.Push(root);

            while (stack.Count != 0)
            {
                TreeElement treeElement = stack.Pop();
                method?.Invoke(treeElement.Content);
                processed.Add(treeElement.Content);

                if (treeElement.Left != null && !processed.Contains(treeElement.Left.Content))
                {
                    stack.Push(treeElement.Left);
                }
                if (treeElement.Right != null && !processed.Contains(treeElement.Right.Content))
                {
                    stack.Push(treeElement.Right);
                }
            }

            processed.Dispose();
        }

        public void PostOrder(ITraversableTree<TContent>.TraversaryDelegate method)
        {
            Stack<TreeElement> stack = new Stack<TreeElement>();
            BinarySearchTree<TContent> processed = new BinarySearchTree<TContent>();
            stack.Push(root);

            if (root.Left != null)
            {
                stack.Push(root.Right);
                stack.Push(root.Left);
            }
            else
            {
                stack.Push(root.Left);
                stack.Push(root.Right);
            }

            while (stack.Count != 0)
            {
                TreeElement treeElement = stack.Pop();
                method?.Invoke(treeElement.Content);
                processed.Add(treeElement.Content);

                if (treeElement.Left != null && !processed.Contains(treeElement.Left.Content))
                {
                    stack.Push(treeElement.Left);
                }
                if (treeElement.Right != null && !processed.Contains(treeElement.Right.Content))
                {
                    stack.Push(treeElement.Right);
                }
            }

            processed.Dispose();
        }

        public void BreadthFirst(ITraversableTree<TContent>.TraversaryDelegate method)
        {
            Queue<TreeElement> queue = new Queue<TreeElement>();
            queue.Enqueue(root);

            while (queue.Count != 0)
            {
                TreeElement actualTreeElement = queue.Dequeue();
                method?.Invoke(actualTreeElement.Content);

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

        private int BreadthFirst(TreeElement treeElement)
        {
            Queue<TreeElement> queue = new Queue<TreeElement>();
            TreeElement lowestTreeElement = null;
            queue.Enqueue(treeElement);

            while (queue.Count != 0)
            {
                TreeElement actualTreeElement = queue.Dequeue();

                if (queue.Count == 0 && IsLeaf(ref actualTreeElement))
                {
                    lowestTreeElement = actualTreeElement;
                    break;
                }
                if (actualTreeElement.Left != null)
                {
                    queue.Enqueue(actualTreeElement.Left);
                }
                if (actualTreeElement.Right != null)
                {
                    queue.Enqueue(actualTreeElement.Right);
                }
            }

            return HeightBetween(treeElement, lowestTreeElement) + 1;
        }

        public void DepthFirst(ITraversableTree<TContent>.TraversaryDelegate method)
        {
            Stack<TreeElement> stack = new Stack<TreeElement>();
            stack.Push(root);

            while (stack.Count != 0)
            {
                TreeElement actualTreeElement = stack.Pop();
                method?.Invoke(actualTreeElement.Content);

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
    }
}