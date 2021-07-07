using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.LinkedList
{
    public abstract class DoublyLinkedList<T> : ICollection<T>, ICollection, IEquatable<DoublyLinkedList<T>>
    {
        protected class ListElement
        {
            public T Content { get; set; }

            public ListElement Next { get; set; }
            public ListElement Previous { get; set; }

            public ListElement(T content)
            {
                Content = content;
            }
        }

        protected ListElement head;
        protected ListElement tail;
        protected abstract ListElement Tail { get; }

        protected int _count = 0;
        public int Count => _count;
        public bool IsEmpty => _count == 0;

        public bool IsReadOnly => false;

        public bool IsSynchronized => false;
        public object SyncRoot { get; } = new object();

        protected delegate DoublyLinkedList<T> EmptyLinkedListInitializer();
        private static EmptyLinkedListInitializer initializeEmptyLinkedList;

        protected DoublyLinkedList(EmptyLinkedListInitializer emptyLinkedListInitializer)
        {
            initializeEmptyLinkedList = emptyLinkedListInitializer;
        }

        protected void CreateLinkedList(IEnumerable<T> content)
        {
            foreach (T item in content)
            {
                this.Add(item);
            }
        }

        /// <summary>
        /// Adds an element to the linked list.
        /// </summary>
        /// <param name="content"></param>
        public void Add(T content)
        {
            InternalAdd(ref content);
            _count++;
        }

        protected abstract void InternalAdd(ref T content);

        /// <summary>
        /// Removes an element from the linked list.
        /// </summary>
        /// <param name="content"></param>
        /// <exception cref="ListElementNotFoundException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        public void Remove(T content)
        {
            InternalRemove(ref content);
            _count--;
        }

        protected abstract void InternalRemove(ref T content);

        /// <summary>
        /// Gets the index of the specified item.
        /// </summary>
        /// <param name="item"></param>
        /// <returns>The index of the item.</returns>
        /// <exception cref="ListElementNotFoundException"></exception>
        public virtual int IndexOf(T content)
        {
            int index = 0;
            ListElement m = head;

            while (!(m == null || m.Content.Equals(content) || m == Tail))
            {
                index++;
                m = m.Next;
            }

            if (m == Tail && !m.Content.Equals(content))
            {
                throw new ListElementNotFoundException();
            }

            return index;
        }

        /// <summary>
        /// Removes an element at the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public abstract void RemoveAt(int index);

        public void CopyTo(Array array, int index)
        {
            if (array.Length == index - 1 || index - 1 > _count || index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }
            else if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            ListElement m = head;

            for (int i = 0; i < index; i++)
            {
                m = m.Next;
            }

            for (int i = index; i < array.Length; i++)
            {
                array.SetValue(m.Content, i);
                m = m.Next;
            }
        }

        public virtual void CopyTo(T[] array, int arrayIndex)
        {
            if (array.Length == arrayIndex - 1 || arrayIndex - 1 > _count || arrayIndex < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(arrayIndex));
            }
            else if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            ListElement m = head;

            for (int i = 0; i < arrayIndex; i++)
            {
                m = m.Next;
            }

            for (int i = arrayIndex; i < array.Length; i++)
            {
                array[i] = m.Content;
                m = m.Next;
            }
        }

        bool ICollection<T>.Remove(T content)
        {
            try
            {
                Remove(content);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected virtual T Search(T content)
        {
            foreach (T item in this)
            {
                if (item.Equals(content))
                {
                    return content;
                }
            }

            return default(T);
        }

        /// <summary>
        /// Gets the element which is on the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns>The element which is on the specified index.</returns>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public T Search(int index)
        {
            ListElement m = head;

            if (index >= _count)
            {
                throw new IndexOutOfRangeException("Index is out of bounds of the linked list.");
            }

            for (int i = 0; i < index; i++)
            {
                m = m.Next;
            }

            return m.Content;
        }

        protected ListElement SearchListElement(int index)
        {
            ListElement m = head;

            if (index >= _count)
            {
                throw new IndexOutOfRangeException("Index is out of bounds of the linked list.");
            }

            for (int i = 0; i < index; i++)
            {
                m = m.Next;
            }

            return m;
        }

        /// <summary>
        /// Iterates through the linked list to find the specified element.
        /// </summary>
        /// <param name="content"></param>
        /// <returns>True if found; False if not found.</returns>
        public bool Contains(T content)
        {
            try
            {
                return Search(content) != null;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool this[T content]
        {
            get => Contains(content);
        }

        public T this[int index]
        {
            get { return Search(index); }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return new Enumerator(this);
        }

        public void Clear()
        {
            head = null;
            _count = 0;
            GC.Collect();
        }

        public override bool Equals(object obj)
        {
            return obj == this;
        }

        /// <summary>
        /// Union of the two sorted linked lists.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>A new sorted linked list which contains the values of the sorted linked lists.</returns>
        public static DoublyLinkedList<T> operator +(DoublyLinkedList<T> left, DoublyLinkedList<T> right)
        {
            DoublyLinkedList<T> container = initializeEmptyLinkedList();

            foreach (T content in left)
            {
                try
                {
                    container.Add(content);
                }
                catch (Exception)
                {

                }
            }

            foreach (T content in right)
            {
                try
                {
                    container.Add(content);
                }
                catch (Exception)
                {

                }
            }

            return container;
        }

        /// <summary>
        /// Difference of the two sorted linked lists.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>The difference between the two sorted linked list.</returns>
        public static DoublyLinkedList<T> operator -(DoublyLinkedList<T> left, DoublyLinkedList<T> right)
        {
            DoublyLinkedList<T> container = initializeEmptyLinkedList();

            ListElement rightElement = right.head;

            for (int i = 0; i < left._count; i++)
            {
                try
                {
                    right.Remove(rightElement.Content);
                    rightElement = rightElement.Next;
                }
                catch (Exception)
                {

                }
            }

            foreach (T content in left)
            {
                container.Add(content);
            }

            foreach (T content in right)
            {
                container.Add(content);
            }

            return container;
        }

        /// <summary>
        /// Compares the sorted linked lists by their lengths;
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator <(DoublyLinkedList<T> left, DoublyLinkedList<T> right)
        {
            return left._count < right._count;
        }

        /// <summary>
        /// Compares the sorted linked lists by their lengths;
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator >(DoublyLinkedList<T> left, DoublyLinkedList<T> right)
        {
            return left._count > right._count;
        }

        /// <summary>
        /// Compares the sorted linked lists by their lengths;
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator <=(DoublyLinkedList<T> left, DoublyLinkedList<T> right)
        {
            return left._count <= right._count;
        }

        /// <summary>
        /// Compares the sorted linked lists by their lengths;
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator >=(DoublyLinkedList<T> left, DoublyLinkedList<T> right)
        {
            return left._count >= right._count;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public abstract bool Equals(DoublyLinkedList<T> other);

        public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
        {
            private DoublyLinkedList<T> linkedList;
            private ListElement CurrentListElement { get; set; }
            private ListElement Previous { get; set; }
            public T Current => CurrentListElement is null ? default(T) : CurrentListElement.Content;

            object IEnumerator.Current => Current as object;

            internal Enumerator(DoublyLinkedList<T> linkedList)
            {
                this.linkedList = linkedList;
                ListElement sentinel = new ListElement(default(T));
                sentinel.Next = linkedList.head;
                CurrentListElement = sentinel;
                Previous = null;
            }

            public bool MoveNext()
            {
                Previous = CurrentListElement;
                CurrentListElement = CurrentListElement.Next;

                if (CurrentListElement == null || (CurrentListElement == linkedList.head
                     && (Previous == linkedList.Tail || Previous == linkedList.tail)))
                {
                    return false;
                }

                return true;
            }

            public void Reset()
            {
                CurrentListElement = linkedList.head;
            }

            public void Dispose()
            {
                linkedList = null;
                CurrentListElement = null;
            }
        }
    }
}