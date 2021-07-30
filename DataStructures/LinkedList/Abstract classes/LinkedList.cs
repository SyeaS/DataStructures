using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.LinkedList
{
    public abstract class LinkedList<T> : ICollection<T>, ICollection, IEquatable<LinkedList<T>>, IDisposable
    {
        protected class ListElement : IListElement
        {
            public T Content { get; set; }
            public IListElement Next { get; set; }

            public ListElement()
            {

            }

            public ListElement(ref T content)
            {
                Content = content;
            }
        }

        protected interface IListElement
        {
            public T Content { get; set; }
            public IListElement Next { get; set; }
        }

        protected class InternalListElementNotFoundException : ListElementNotFoundException
        {
            public IListElement ListElement { get; }

            public InternalListElementNotFoundException(IListElement listElement) : base()
            {
                ListElement = listElement;
            }
        }

        protected abstract IListElement Head { get; }

        public int Count { get; protected set; }
        public bool IsReadOnly => false;
        public bool IsSynchronized => false;
        public object SyncRoot { get; } = new object();
        public bool IsEmpty => Count == 0 && Head is null;

        public LinkedList()
        {

        }

        protected void CreateFromIEnumerable(ref IEnumerable<T> contents)
        {
            foreach (T content in contents)
            {
                Add(content);
            }
        }

        public void Add(T element)
        {
            InternalAdd(ref element);
            Count++;
        }

        protected abstract void InternalAdd(ref T element);

        public void Remove(T element)
        {
            InternalRemove(ref element);
            Count--;
        }

        protected abstract void InternalRemove(ref T element);

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
        /// Removes an element at the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <exception cref="IndexOutOfRangeException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        public abstract void RemoveAt(int index);

        public virtual bool Contains(T item)
        {
            foreach (T content in this)
            {
                if (content.Equals(item))
                {
                    return true;
                }
            }

            return false;
        }

        public void CopyTo(Array array, int index)
        {
            if (index < 0 || array.Length >= index)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }
            else if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }
            else if (index > Count - 1)
            {
                throw new ArgumentException(nameof(index));
            }

            IListElement m = Head;

            for (int i = 0; i < index; i++)
            {
                m = m.Next;
            }

            for (int i = index; i < array.Length; i++)
            {
                try
                {
                    array.SetValue(m.Content, i);
                    m = m.Next;
                }
                finally
                {
                    i = array.Length;
                }
            }
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (arrayIndex < 0 || arrayIndex >= array.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(arrayIndex), arrayIndex.ToString());
            }
            else if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }
            else if (arrayIndex > Count - 1)
            {
                throw new ArgumentException(nameof(arrayIndex));
            }

            IListElement m = Head;

            for (int i = 0; i < arrayIndex; i++)
            {
                m = m.Next;
            }

            for (int i = arrayIndex; i < array.Length; i++)
            {
                try
                {
                    array[i] = m.Content;
                    m = m.Next;
                }
                catch (Exception)
                {
                    i = array.Length;
                }
            }
        }

        /// <summary>
        /// Gets the index of the specified item.
        /// </summary>
        /// <param name="item"></param>
        /// <returns>The index of the item.</returns>
        /// <exception cref="ListElementNotFoundException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        public abstract int IndexOf(T content);

        protected virtual T Search(T content)
        {
            foreach (T item in this)
            {
                if (item.Equals(content))
                {
                    return content;
                }
            }

            throw new ListElementNotFoundException();
        }

        /// <summary>
        /// Gets the element which is on the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns>The element which is on the specified index.</returns>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public T Search(int index)
        {
            return SearchListElement(index).Content;
        }

        protected IListElement SearchListElement(int index)
        {
            IListElement m = Head;

            if (index >= Count)
            {
                throw new IndexOutOfRangeException("Index is out of bounds of the linked list.");
            }

            for (int i = 0; i < index; i++)
            {
                m = m.Next;
            }

            return m;
        }

        public bool this[T content]
        {
            get => Contains(content);
        }

        public T this[int index]
        {
            get { return Search(index); }
        }

        public virtual void Clear()
        {
            Count = 0;
        }

        public virtual void Dispose()
        {
            Count = 0;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator() as IEnumerator;
        }

        public bool Equals(LinkedList<T> other)
        {
            return this == other;
        }

        struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
        {
            private IListElement Head { get; }
            private IListElement current;
            private static IListElement sentinel;
            public T Current => current.Content ?? default(T);

            object IEnumerator.Current => Current as object;

            internal Enumerator(LinkedList<T> linkedList)
            {
                T defaultValue = default(T);
                sentinel = new ListElement(ref defaultValue);
                sentinel.Next = linkedList.Head;
                Head = sentinel;
                current = Head;
                Head = Head.Next;
            }

            public void Dispose()
            {
                current = null;
            }

            public bool MoveNext()
            {
                IListElement prev = current;
                current = current.Next;

                if (current == null || (current == Head && prev != sentinel))
                {
                    return false;
                }

                return true;
            }

            public void Reset()
            {
                current = sentinel;
            }
        }
    }
}