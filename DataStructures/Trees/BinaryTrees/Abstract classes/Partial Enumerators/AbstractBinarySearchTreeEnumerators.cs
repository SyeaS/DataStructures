using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Trees.BinaryTrees
{
    public abstract partial class AbstractBinarySearchTree<T, TTreeElement>
    {
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
                    TreeElement peek = data.stack.PeekLast();

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