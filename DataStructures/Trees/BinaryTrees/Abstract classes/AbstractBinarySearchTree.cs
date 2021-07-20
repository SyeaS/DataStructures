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
        protected override IEnumerable<T> DefaultEnumerator => InternalInOrder() as IEnumerable<T>;

        public AbstractBinarySearchTree()
        {
        }

        public AbstractBinarySearchTree(IEnumerable<T> content)
        {
            base.CreateFromIEnumerable(ref content);
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

        protected override TreeElement MoveInOrderEnumerator(ref InOrderData data)
        {
            while (!data.stack.IsEmpty || data.current is not null)
            {
                if (data.current is not null)
                {
                    data.stack.Push(data.current);
                    data.current = data.current.Left;
                }
                else
                {
                    TreeElement copy = data.stack.Pop();
                    data.current = copy.Right;
                    return copy;
                }
            }

            data.stack.Dispose();
            return null;
        }

        protected override void InitializeInOrderEnumerator(ref InOrderData data)
        {
            data.current = root;
        }

        protected override TreeElement MovePreOrderEnumerator(ref PreOrderData data)
        {
            if (data.stack.Count != 0)
            {
                TreeElement treeElement = data.stack.Pop();
                data.processed.Add(treeElement.TreeContent.Content);

                if (treeElement.Right != null && !data.processed.Contains(treeElement.Right.TreeContent.Content))
                {
                    data.stack.Push(treeElement.Right);
                }
                if (treeElement.Left != null && !data.processed.Contains(treeElement.Left.TreeContent.Content))
                {
                    data.stack.Push(treeElement.Left);
                }

                return treeElement;
            }

            data.processed.Dispose();
            data.stack.Dispose();
            return null;
        }

        protected override void InitializePreOrderEnumerator(ref PreOrderData data)
        {
            data.stack.Push(root);
        }

        protected override TreeElement MovePostOrderEnumerator(ref PostOrderData data)
        {
            while (data.current is not null || !data.stack.IsEmpty)
            {
                if (data.current is not null)
                {
                    data.stack.Push(data.current);
                    data.current = data.current.Left;
                }
                else
                {
                    TreeElement peek = data.stack.Peek();

                    if (peek.Right is not null && data.previous != peek.Right)
                    {
                        data.current = peek.Right;
                    }
                    else
                    {
                        data.previous = data.stack.Pop();
                        return peek;
                    }
                }
            }

            data.processed.Dispose();
            data.stack.Dispose();
            return null;
        }

        protected override void InitializePostOrderEnumerator(ref PostOrderData data)
        {
            data.current = root;
        }

        protected override TreeElement MoveBreadthFirstEnumerator(ref BreadthFirstData data)
        {
            if (data.queue.Count != 0)
            {
                TreeElement treeElement = data.queue.Dequeue();

                if (treeElement.Left != null)
                {
                    data.queue.Enqueue(treeElement.Left);
                }
                if (treeElement.Right != null)
                {
                    data.queue.Enqueue(treeElement.Right);
                }

                return treeElement;
            }

            data.queue.Dispose();
            return null;
        }

        protected override void InitializeBreadthFirstEnumerator(ref BreadthFirstData data)
        {
            data.queue.Enqueue(root);
        }
    }
}