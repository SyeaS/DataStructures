using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Trees.BinaryTrees
{
    public abstract partial class BinaryTree<T, TTreeContent>
    {
        protected interface IDataFactory<TData>
        {
            TData CreateEmpty();
            void Initialize();
        }

        protected class InOrderData : IDataFactory<InOrderData>
        {
            public LinkedList.Stack<TreeElement> stack;
            public TreeElement current;

            public void Initialize()
            {
                stack = new LinkedList.Stack<TreeElement>();
            }

            public InOrderData CreateEmpty()
            {
                return new InOrderData();
            }
        }

        protected class PreOrderData : IDataFactory<PreOrderData>
        {
            public LinkedList.Stack<TreeElement> stack;
            public RedBlackTree<T> processed;

            public void Initialize()
            {
                stack = new LinkedList.Stack<TreeElement>();
                processed = new RedBlackTree<T>();
            }

            public PreOrderData CreateEmpty()
            {
                return new PreOrderData();
            }
        }

        protected class PostOrderData : IDataFactory<PostOrderData>
        {
            public LinkedList.Stack<TreeElement> stack;
            public RedBlackTree<T> processed;
            public TreeElement current;
            public TreeElement previous;

            public void Initialize()
            {
                stack = new LinkedList.Stack<TreeElement>();
                processed = new RedBlackTree<T>();
            }

            public PostOrderData CreateEmpty()
            {
                return new PostOrderData();
            }
        }

        protected class BreadthFirstData : IDataFactory<BreadthFirstData>
        {
            public LinkedList.Queue<TreeElement> queue;

            public void Initialize()
            {
                queue = new LinkedList.Queue<TreeElement>();
            }

            public BreadthFirstData CreateEmpty()
            {
                return new BreadthFirstData();
            }
        }

        public IEnumerable<T> InOrder()
        {
            return InternalInOrder().DefaultEnumerator;
        }

        public void InOrder(ITraversableTree<T>.TraversaryDelegate method)
        {
            foreach (T item in InOrder())
            {
                method(item);
            }
        }

        public IEnumerable<T> PreOrder()
        {
            return InternalPreOrder().DefaultEnumerator;
        }

        public void PreOrder(ITraversableTree<T>.TraversaryDelegate method)
        {
            foreach (T item in PreOrder())
            {
                method(item);
            }
        }

        public IEnumerable<T> PostOrder()
        {
            return InternalPostOrder().DefaultEnumerator;
        }

        public void PostOrder(ITraversableTree<T>.TraversaryDelegate method)
        {
            foreach (T item in PostOrder())
            {
                method(item);
            }
        }

        public IEnumerable<T> BreadthFirst()
        {
            return InternalBreadthFirst().DefaultEnumerator;
        }

        public void BreadthFirst(ITraversableTree<T>.TraversaryDelegate method)
        {
            foreach (T item in BreadthFirst())
            {
                method(item);
            }
        }

        protected abstract TreeElement MoveInOrderEnumerator(ref InOrderData data);
        protected abstract void InitializeInOrderEnumerator(ref InOrderData data);
        protected Enumerator<InOrderData> InternalInOrder()
        {
            InOrderData data = new InOrderData();
            return new Enumerator<InOrderData>(ref data, MoveInOrderEnumerator,
                InitializeInOrderEnumerator, this);
        }

        protected abstract TreeElement MovePreOrderEnumerator(ref PreOrderData data);
        protected abstract void InitializePreOrderEnumerator(ref PreOrderData data);
        protected Enumerator<PreOrderData> InternalPreOrder()
        {
            PreOrderData data = new PreOrderData();
            return new Enumerator<PreOrderData>(ref data, MovePreOrderEnumerator,
                InitializePreOrderEnumerator, this);
        }

        protected abstract TreeElement MovePostOrderEnumerator(ref PostOrderData data);
        protected abstract void InitializePostOrderEnumerator(ref PostOrderData data);
        protected Enumerator<PostOrderData> InternalPostOrder()
        {
            PostOrderData data = new PostOrderData();
            return new Enumerator<PostOrderData>(ref data, MovePostOrderEnumerator,
                InitializePostOrderEnumerator, this);
        }

        protected abstract TreeElement MoveBreadthFirstEnumerator(ref BreadthFirstData data);
        protected abstract void InitializeBreadthFirstEnumerator(ref BreadthFirstData data);
        protected Enumerator<BreadthFirstData> InternalBreadthFirst()
        {
            BreadthFirstData data = new BreadthFirstData();
            return new Enumerator<BreadthFirstData>(ref data, MoveBreadthFirstEnumerator,
                InitializeBreadthFirstEnumerator, this);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return DefaultEnumerator.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return DefaultEnumerator.GetEnumerator();
        }

        protected Enumerator<TData> CreateEnumerator<TData>(TData data, Enumerator<TData>.MoveEnumerator move, Enumerator<TData>.InitializeEnumerator initialize, BinaryTree<T, TTreeContent> binaryTree)
            where TData : IDataFactory<TData>
        {
            return new Enumerator<TData>(ref data, move, initialize, binaryTree);
        }

        protected Enumerator<TData> CreateEnumerator<TData>(ref TData data, ref Enumerator<TData>.MoveEnumerator move, ref Enumerator<TData>.InitializeEnumerator initialize, ref BinaryTree<T, TTreeContent> binaryTree)
            where TData : IDataFactory<TData>
        {
            return new Enumerator<TData>(ref data, move, initialize, binaryTree);
        }

        protected struct Enumerator<TData> : IEnumerator<TreeElement>, IEnumerator<T>, IEnumerator, IEnumerable<TreeElement>, IEnumerable<T>, IDisposable
            where TData : IDataFactory<TData>
        {
            public TreeElement Current => current;
            T IEnumerator<T>.Current => current.TreeContent.Content;
            object IEnumerator.Current => current as object;
            private TreeElement current;

            public IEnumerable<T> DefaultEnumerator => this as IEnumerable<T>;

            private TData data;

            public delegate TreeElement MoveEnumerator(ref TData data);
            private MoveEnumerator Move { get; }

            public delegate void InitializeEnumerator(ref TData data);
            private InitializeEnumerator Initialize { get; }

            public Enumerator(ref TData data, MoveEnumerator move, InitializeEnumerator initialize, BinaryTree<T, TTreeContent> binaryTree)
            {
                this.data = data;
                Move = move;
                current = null;
                data.Initialize();
                Initialize = initialize;
                Initialize(ref data);
            }

            public IEnumerator<TreeElement> GetEnumerator()
            {
                return this;
            }

            IEnumerator<T> IEnumerable<T>.GetEnumerator()
            {
                return this;
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this;
            }

            public bool MoveNext()
            {
                current = Move(ref data);
                return current is not null;
            }

            public void Reset()
            {
                current = null;
                data.Initialize();
                Initialize(ref data);
            }

            public void Dispose()
            {
                current = null;
                data = default(TData);
            }
        }
    }
}