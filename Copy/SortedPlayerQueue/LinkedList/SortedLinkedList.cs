using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortedPlayerQueue
{
    public enum OrderingMode
    {
        Ascending,
        Descending
    }

    public sealed class SortedLinkedList<TContent> : ICollection<TContent>, IEnumerator<TContent>, IEquatable<SortedLinkedList<TContent>>
        where TContent : IComparable<TContent>
    {
        class ListElement
        {
            public TContent Content { get; set; }

            public ListElement Next { get; set; }
            public ListElement Previous { get; set; }

            public ListElement(TContent content)
            {
                Content = content;
            }
        }

        private ListElement head;
        private ListElement Tail => head?.Previous;

        private ListElement CurrentListElement { get; set; }
        public TContent Current
        {
            get
            {
                if (CurrentListElement is null)
                {
                    return default(TContent);
                }

                return CurrentListElement.Content;
            }
        }

        object IEnumerator.Current => Current as object;
        public OrderingMode OrderingMode { get; init; }

        private int _count = 0;
        public int Count => _count;

        public bool IsReadOnly => false;

        public SortedLinkedList(OrderingMode ordering = OrderingMode.Descending)
        {
            OrderingMode = ordering;
        }

        /// <summary>
        /// Creates the sorted linked list with an IEnumerable of TContent.
        /// </summary>
        /// <param name="content"></param>
        /// <param name="ordering"></param>
        /// <exception cref="DuplicateListElementException"></exception>
        public SortedLinkedList(IEnumerable<TContent> content, OrderingMode ordering = OrderingMode.Descending) : this(ordering)
        {
            foreach (TContent item in content)
            {
                this.Add(item);
            }
        }

        /// <summary>
        /// Adds an element to the sorted linked list by the specified ordering mode.
        /// </summary>
        /// <param name="content"></param>
        /// <param name="ordering"></param>
        /// <exception cref="DuplicateListElementException"></exception>
        public void Add(TContent content)
        {
            switch (OrderingMode)
            {
                case OrderingMode.Ascending:
                    AddElementByAscendingOrder(ref content);
                    break;
                case OrderingMode.Descending:
                default:
                    AddElementByDescendingOrder(ref content);
                    break;
            }
        }

        private void AddElementByDescendingOrder(ref TContent content)
        {
            ListElement listElement = new ListElement(content);

            if (head?.Content.CompareTo(content) == 0 || Tail?.Content.CompareTo(content) == 0)
            {
                throw new DuplicateListElementException();
            }

            if (head == null)
            {
                head = listElement;
            }
            else if (head.Content.CompareTo(content) < 0)
            {
                listElement.Next = head;
                listElement.Previous = head.Previous;
                head = listElement;
                head.Next.Previous = head;

                if (_count == 1)
                {
                    head.Previous = head.Next;
                    head.Next.Next = head;
                }
                else
                {
                    head.Previous.Next = head;
                }
            }
            else if (Tail == null)
            {
                listElement.Previous = head;
                listElement.Next = head;
                head.Next = listElement;
                head.Previous = listElement;
            }
            else if (Tail.Content.CompareTo(content) > 0)
            {
                listElement.Next = head;
                listElement.Previous = head.Previous;
                head.Previous.Next = listElement;
                head.Previous = listElement;
            }
            else
            {
                ListElement m = head.Next;

                while (!(m == Tail || m.Content.CompareTo(content) < 0))
                {
                    m = m.Next;
                }

                if (m.Previous.Content.CompareTo(content) == 0)
                {
                    throw new DuplicateListElementException();
                }

                listElement.Next = m;
                listElement.Previous = m.Previous;

                m.Previous.Next = listElement;
                m.Previous = listElement;
            }
            _count++;
        }

        private void AddElementByAscendingOrder(ref TContent content)
        {
            ListElement listElement = new ListElement(content);

            if (head?.Content.CompareTo(content) == 0 || Tail?.Content.CompareTo(content) == 0)
            {
                throw new DuplicateListElementException();
            }

            if (head == null)
            {
                head = listElement;
            }
            else if (head.Content.CompareTo(content) > 0)
            {
                listElement.Next = head;
                listElement.Previous = head.Previous;
                head = listElement;
                head.Next.Previous = head;

                if (_count == 1)
                {
                    head.Previous = head.Next;
                    head.Next.Next = head;
                }
                else
                {
                    head.Previous.Next = head;
                }
            }
            else if (Tail == null)
            {
                listElement.Previous = head;
                listElement.Next = head;
                head.Next = listElement;
                head.Previous = listElement;
            }
            else if (Tail.Content.CompareTo(content) < 0)
            {
                listElement.Next = head;
                listElement.Previous = head.Previous;
                head.Previous.Next = listElement;
                head.Previous = listElement;
            }
            else
            {
                ListElement m = head.Next;

                while (!(m == Tail || m.Content.CompareTo(content) > 0))
                {
                    m = m.Next;
                }

                if (m.Previous.Content.CompareTo(content) == 0)
                {
                    throw new DuplicateListElementException();
                }

                listElement.Next = m;
                listElement.Previous = m.Previous;

                m.Previous.Next = listElement;
                m.Previous = listElement;
            }
            _count++;
        }

        /// <summary>
        /// Gets the index of the specified item.
        /// </summary>
        /// <param name="item"></param>
        /// <returns>The index of the item.</returns>
        /// <exception cref="ListElementNotFoundException"></exception>
        public int IndexOf(TContent item)
        {
            if (Tail.Content.CompareTo(item) == 0)
            {
                return Count - 1;
            }

            ListElement m = head;
            int index = 0;

            for (int i = index; !(i > Count && m.Content.CompareTo(item) == 0); i++)
            {
                m = m.Next;
            }

            if (index == Count - 1)
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
        public void RemoveAt(int index)
        {
            if (index >= _count || index < 0)
            {
                throw new IndexOutOfRangeException();
            }

            if (index == 0)
            {
                head.Next.Previous = Tail;
                head = head.Next;
            }
            else if (index == _count - 1)
            {
                head.Previous.Previous.Next = head;
                head.Previous = head.Previous.Previous;
            }
            else
            {
                ListElement m = head.Next;

                for (int i = 1; i != index; i++)
                {
                    m = m.Next;
                }

                m.Previous.Next = m.Next;
                m.Next.Previous = m.Previous;
            }

            _count--;
        }

        public void CopyTo(TContent[] array, int arrayIndex)
        {
            ListElement listElement = SearchListElement(arrayIndex);

            while (!(listElement.Next == head))
            {
                array[arrayIndex++] = listElement.Content;
                listElement = listElement.Next;
            }
        }

        bool ICollection<TContent>.Remove(TContent item)
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
        /// Removes the specified content from the sorted linked list.
        /// </summary>
        /// <param name="content"></param>
        /// <exception cref="ListElementNotFoundException"></exception>
        public void Remove(TContent content)
        {
            if (_count == 0)
            {
                throw new ListElementNotFoundException();
            }

            if (head.Content.CompareTo(content) == 0)
            {
                head.Next.Previous = Tail;
                head = head.Next;
            }
            else if (Tail.Content.CompareTo(content) == 0)
            {
                head.Previous.Previous.Next = head;
                head.Previous = head.Previous.Previous;
            }
            else
            {
                ListElement m = head.Next;

                while (!(m == Tail || m.Content.CompareTo(content) == 0))
                {
                    m = m.Next;
                }

                if (m == Tail)
                {
                    throw new ListElementNotFoundException();
                }

                m.Previous.Next = m.Next;
                m.Next.Previous = m.Previous;
            }

            _count--;
        }

        private TContent Search(TContent content)
        {
            ListElement m = head;

            while (!(m == Tail || m.Content.CompareTo(content) == 0))
            {
                m = m.Next;
            }

            if (m == Tail && m.Content.CompareTo(content) != 0)
            {
                throw new ListElementNotFoundException();
            }

            return m.Content;
        }

        /// <summary>
        /// Gets the element which is on the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns>The element which is on the specified index.</returns>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public TContent Search(int index)
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

        private ListElement SearchListElement(int index)
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
        /// Iterates through the sorted linked list to find the specified element.
        /// </summary>
        /// <param name="content"></param>
        /// <returns>True if found; False if not found.</returns>
        public bool Contains(TContent content)
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

        public bool this[TContent content]
        {
            get => Contains(content);
        }

        public TContent this[int index]
        {
            get { return Search(index); }
        }

        public IEnumerator<TContent> GetEnumerator()
        {
            return this as IEnumerator<TContent>;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this as IEnumerator;
        }

        IEnumerator<TContent> IEnumerable<TContent>.GetEnumerator()
        {
            return this as IEnumerator<TContent>;
        }

        public bool MoveNext()
        {
            if (CurrentListElement == null)
            {
                CurrentListElement = head;
            }

            try
            {
                CurrentListElement = CurrentListElement.Next;
                if (CurrentListElement == Tail)
                {
                    return false;
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Reset()
        {
            CurrentListElement = head;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public void Clear()
        {
            head = null;
            _count = 0;
            GC.Collect();
        }

        /// <summary>
        /// Compares the two sorted linked lists by their values.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(SortedLinkedList<TContent> left, SortedLinkedList<TContent> right)
        {
            if (left._count != right._count || left.OrderingMode != right.OrderingMode)
            {
                return false;
            }

            ListElement leftElement = right.head;
            ListElement rightElement = left.head;

            for (int i = 0; i < left._count; i++)
            {
                if (leftElement.Content.CompareTo(rightElement.Content) != 0)
                {
                    return false;
                }

                leftElement = leftElement.Next;
                rightElement = rightElement.Next;
            }

            return true;
        }

        /// <summary>
        /// Compares the two sorted linked lists by their values.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(SortedLinkedList<TContent> left, SortedLinkedList<TContent> right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Union of the two sorted linked lists.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>A new sorted linked list which contains the values of the sorted linked lists.</returns>
        public static SortedLinkedList<TContent> operator +(SortedLinkedList<TContent> left, SortedLinkedList<TContent> right)
        {
            SortedLinkedList<TContent> container = new SortedLinkedList<TContent>(left.OrderingMode);

            foreach (TContent content in left)
            {
                try
                {
                    container.Add(content);
                }
                catch (Exception)
                {

                }
            }

            foreach (TContent content in right)
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
        public static SortedLinkedList<TContent> operator -(SortedLinkedList<TContent> left, SortedLinkedList<TContent> right)
        {
            SortedLinkedList<TContent> container = new SortedLinkedList<TContent>(left.OrderingMode);

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

            foreach (TContent content in left)
            {
                container.Add(content);
            }

            foreach (TContent content in right)
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
        public static bool operator <(SortedLinkedList<TContent> left, SortedLinkedList<TContent> right)
        {
            return left._count < right._count;
        }

        /// <summary>
        /// Compares the sorted linked lists by their lengths;
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator >(SortedLinkedList<TContent> left, SortedLinkedList<TContent> right)
        {
            return left._count > right._count;
        }

        /// <summary>
        /// Compares the sorted linked lists by their lengths;
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator <=(SortedLinkedList<TContent> left, SortedLinkedList<TContent> right)
        {
            return left._count <= right._count;
        }

        /// <summary>
        /// Compares the sorted linked lists by their lengths;
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator >=(SortedLinkedList<TContent> left, SortedLinkedList<TContent> right)
        {
            return left._count >= right._count;
        }

        public override bool Equals(object obj)
        {
            return obj as SortedLinkedList<TContent> == this;
        }

        public bool Equals(SortedLinkedList<TContent> other)
        {
            return other == this;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}