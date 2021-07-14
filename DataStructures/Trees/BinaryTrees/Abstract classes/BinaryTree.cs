using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Trees.BinaryTrees
{
    public abstract class BinaryTree<T, TTreeContent> : IBinaryTree<T>
        where T : IComparable<T>
        where TTreeContent : ITreeContent<T>, new()
    {
        protected class TreeElement
        {
            public TTreeContent TreeContent { get; set; }
            public TreeElement Right { get; set; }
            public TreeElement Parent { get; set; }
            public TreeElement Left { get; set; }

            public TreeElement(T content)
            {
                TreeContent = new TTreeContent();
                TreeContent.Content = content;
            }

            public TreeElement(TTreeContent content)
            {
                TreeContent = content;
            }
        }

        protected TreeElement root;
        private int _count = 0;
        public int Count => _count;

        public bool IsReadOnly => false;
        public bool IsSynchronized => false;
        public object SyncRoot { get; } = new object();

        protected abstract IEnumerable<T> DefaultIterator { get; }

        public BinaryTree()
        {

        }

        /// <summary>
        /// Fills an IEnumerable of T into the binary search tree.
        /// </summary>
        /// <exception cref="DuplicateTreeElementException"></exception>
        /// <param name="content"></param>
        public BinaryTree(IEnumerable<T> content)
        {
            CreateFromIEnumerable(ref content);
        }

        protected void CreateFromIEnumerable(ref IEnumerable<T> content)
        {
            foreach (T item in content)
            {
                this.Add(item);
            }
        }

        /// <summary>
        /// Adds an element to the binary search tree.
        /// </summary>
        /// <exception cref="DuplicateTreeElementException"></exception>
        /// <param name="content"></param>
        public void Add(T content)
        {
            InternalAdd(ref content);
            _count++;
        }

        protected abstract TreeElement InternalAdd(ref T content);

        /// <summary>
        /// Removes an element from the binary search tree.
        /// </summary>
        /// <exception cref="TreeElementNotFoundException"></exception>
        /// <param name="content"></param>
        public void Remove(T content)
        {
            InternalRemove(ref content);
            _count--;
        }

        bool ICollection<T>.Remove(T item)
        {
            try
            {
                Remove(item);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Returns the removed tree element.
        /// </summary>
        /// <param name="content"></param>
        /// <returns>The removed element.</returns>
        protected abstract TreeElement InternalRemove(ref T content);

        /// <summary>
        /// Returns the removed tree element and the out (second parameter) returns the new tree element.
        /// </summary>
        /// <param name="content"></param>
        /// <returns>The removed element.</returns>
        protected abstract TreeElement InternalRemove(ref T content, out TreeElement treeElement);

        /// <summary>
        /// Pops an element from the binary tree.
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public abstract T Pop(T content);

        /// <summary>
        /// Pops the maximum element from the binary tree.
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public abstract T PopMax();

        /// <summary>
        /// Pops the minimum element from the binary tree.
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public abstract T PopMin();

        protected abstract void DeleteTreeElement(ref TreeElement treeElement);

        protected abstract void DeleteTreeElement(ref TreeElement treeElement, out TreeElement newTreeElement);

        /// <summary>
        /// Returns what will be replaced when the <paramref name="treeElement"/> is deleted.
        /// </summary>
        /// <param name="treeElement"></param>
        /// <returns></returns>
        protected abstract TreeElement ReplaceTreeElement(TreeElement treeElement);

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
            else
            {
                if (treeElement.Right != null)
                {
                    root = treeElement.Right;
                }
                else
                {
                    root = treeElement.Left;
                }

                root.Parent = null;
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
            if (treeElement?.Right != null && treeElement?.Left != null)
            {
                return true;
            }

            return false;
        }

        protected bool HasParent(TreeElement treeElement)
        {
            return treeElement.Parent != null;
        }

        protected TreeElement GrandParent(TreeElement treeElement)
        {
            return treeElement?.Parent?.Parent;
        }

        protected TreeElement Uncle(TreeElement treeElement)
        {
            TreeElement grandParent = GrandParent(treeElement);

            if (grandParent is null)
            {
                return null;
            }

            if (grandParent.Right == treeElement.Parent)
            {
                return grandParent.Left;
            }

            return grandParent.Right;
        }

        protected TreeElement Sibling(TreeElement treeElement)
        {
            if (treeElement is null)
            {
                return null;
            }

            if (treeElement.Parent.Right == treeElement)
            {
                return treeElement.Parent.Left;
            }

            return treeElement.Parent.Right;
        }

        protected abstract void AddElementToTreeElement(ref TreeElement treeElement, ref TreeElement insertedTreeElement);

        /// <summary>
        /// Searches the binary search tree to found the value.
        /// </summary>
        /// <param name="content"></param>
        /// <returns>True if found; False if not found.</returns>
        public bool Contains(T content)
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
        protected abstract TreeElement Search(ref T content);

        protected void ReplaceTreeElement(TreeElement replacableTreeElement, TreeElement treeElement)
        {
            T content = replacableTreeElement.TreeContent.Content;
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

        protected abstract TreeElement Maximum(TreeElement treeElement);

        public T Maximum()
        {
            return Maximum(root).TreeContent.Content;
        }

        protected abstract TreeElement Minimum(TreeElement treeElement);

        public T Minimum()
        {
            return Minimum(root).TreeContent.Content;
        }

        /// <summary>
        /// Counts the height between two tree elements.
        /// </summary>
        /// <param name="treeElement"></param>
        /// <param name="otherTreeElement"></param>
        /// <exception cref="TreeElementNotFoundException"></exception>
        /// <returns>The height difference between the two tree elements.</returns>
        protected uint HeightBetween(TreeElement treeElement, TreeElement otherTreeElement)
        {
            uint height = 0;

            while (treeElement != otherTreeElement)
            {
                if (treeElement.TreeContent.Content.CompareTo(otherTreeElement.TreeContent.Content) > 0)
                {
                    treeElement = treeElement.Left;
                }
                else if (treeElement.TreeContent.Content.CompareTo(otherTreeElement.TreeContent.Content) < 0)
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

        protected uint GetHeight(TreeElement treeElement)
        {
            if (treeElement == null || IsLeaf(ref treeElement))
            {
                return 0;
            }

            Queue<TreeElement> queue = new Queue<TreeElement>();
            queue.Enqueue(treeElement);
            uint height = 0;

            while (true)
            {
                int queueCount = queue.Count;
                if (queueCount == 0)
                {
                    return height;
                }

                while (queueCount > 0)
                {
                    TreeElement act = queue.Dequeue();

                    if (act.Left != null)
                    {
                        queue.Enqueue(act.Left);
                    }
                    if (act.Right != null)
                    {
                        queue.Enqueue(act.Right);
                    }

                    queueCount--;
                }

                height++;
            }
        }

        public bool Equals(IBinaryTree<T> other)
        {
            foreach (T item in other.InOrder())
            {
                if (!this.Contains(item))
                {
                    return false;
                }
            }

            return true;
        }

        public void CopyTo(Array array, int index)
        {
            foreach (T item in DefaultIterator)
            {
                try
                {
                    array.SetValue(item, index++);
                }
                catch (IndexOutOfRangeException)
                {
                    break;
                }
            }
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            foreach (T item in DefaultIterator)
            {
                try
                {
                    array[arrayIndex++] = item;
                }
                catch (Exception)
                {
                    break;
                }
            }
        }

        public IEnumerable<T> InOrder()
        {
            foreach (TreeElement treeElement in InternalInOrder())
            {
                yield return treeElement.TreeContent.Content;
            }
        }

        public IEnumerable<T> PreOrder()
        {
            foreach (TreeElement treeElement in InternalPreOrder())
            {
                yield return treeElement.TreeContent.Content;
            }
        }

        public IEnumerable<T> PostOrder()
        {
            foreach (TreeElement treeElement in InternalPostOrder())
            {
                yield return treeElement.TreeContent.Content;
            }
        }

        public IEnumerable<T> BreadthFirst()
        {
            foreach (TreeElement treeElement in InternalBreadthFirst())
            {
                yield return treeElement.TreeContent.Content;
            }
        }

        public IEnumerable<T> DepthFirst()
        {
            foreach (TreeElement treeElement in InternalDepthFirst())
            {
                yield return treeElement.TreeContent.Content;
            }
        }

        public void InOrder(ITraversableTree<T>.TraversaryDelegate method)
        {
            foreach (T item in InOrder())
            {
                method(item);
            }
        }

        public void PreOrder(ITraversableTree<T>.TraversaryDelegate method)
        {
            foreach (T item in PreOrder())
            {
                method(item);
            }
        }

        public void PostOrder(ITraversableTree<T>.TraversaryDelegate method)
        {
            foreach (T item in PostOrder())
            {
                method(item);
            }
        }

        public void BreadthFirst(ITraversableTree<T>.TraversaryDelegate method)
        {
            foreach (T item in BreadthFirst())
            {
                method(item);
            }
        }

        public void DepthFirst(ITraversableTree<T>.TraversaryDelegate method)
        {
            foreach (T item in DepthFirst())
            {
                method(item);
            }
        }

        protected abstract IEnumerable<TreeElement> InternalInOrder();
        protected abstract IEnumerable<TreeElement> InternalPreOrder();
        protected abstract IEnumerable<TreeElement> InternalPostOrder();
        protected abstract IEnumerable<TreeElement> InternalBreadthFirst();
        protected abstract IEnumerable<TreeElement> InternalDepthFirst();

        public IEnumerator<T> GetEnumerator()
        {
            return DefaultIterator.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return DefaultIterator.GetEnumerator();
        }

        public void Dispose()
        {
            root = null;
            _count = 0;
        }

        public void Clear()
        {
            root = null;
            _count = 0;
        }
    }
}