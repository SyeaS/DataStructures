using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.LinkedList
{
    public abstract class SinglyLinkedList<T> : ICollection<T>, ICollection
    {
        protected class ListElement
        {
            public ListElement Next { get; set; }
            public T Content { get; set; }

            public ListElement(T content)
            {
                Content = content;
            }
        }

        protected ListElement head;

        public int Count { get; protected set; }
        public bool IsReadOnly => false;
        public bool IsSynchronized => false;
        public object SyncRoot { get; } = new object();

        public SinglyLinkedList()
        {

        }

        public SinglyLinkedList(IEnumerable<T> content)
        {
            foreach (T item in content)
            {
                Add(item);
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

        protected void InsertAfter(ListElement listElement, ListElement newElement)
        {
            newElement.Next = listElement.Next;
            listElement.Next = newElement;
        }

        protected void InsertBefore(ListElement listElement, ListElement newElement)
        {
            T copy = listElement.Content;
            listElement.Content = newElement.Content;
            newElement.Content = copy;
            newElement.Next = listElement.Next;
            listElement.Next = newElement;
        }

        protected void RemoveElement(ListElement previous, ListElement listElement)
        {
            if (previous == null)
            {
                head = head.Next;
            }
            else if (listElement.Next == null)
            {
                previous.Next = null;
            }
            else
            {
                previous.Next = listElement.Next;
            }
        }

        public void Clear()
        {
            head = null;
        }

        public bool Contains(T item)
        {
            ListElement m = head;

            while (!(m == null || m.Content.Equals(item)))
            {
                m = m.Next;
            }

            if (m == null)
            {
                return false;
            }

            return true;
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

            ListElement m = head;

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

            ListElement m = head;

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

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator(this);
        }

        public struct Enumerator : IEnumerator<T>, IDisposable, IEnumerator
        {
            private ListElement Head { get; }
            private ListElement current;
            private static ListElement sentinel = new ListElement(default(T));
            public T Current => current.Content ?? default(T);
            
            object IEnumerator.Current => Current as object;

            internal Enumerator(SinglyLinkedList<T> linkedList)
            {
                sentinel.Next = linkedList.head;
                Head = sentinel;
                current = Head;
            }

            public void Dispose()
            {
                current = null;
            }

            public bool MoveNext()
            {
                ListElement prev = current;
                current = current.Next;

                if (current == null || (current == Head && prev != sentinel))
                {
                    return false;
                }

                return true;
            }

            public void Reset()
            {
                current = Head;
            }
        }
    }
}