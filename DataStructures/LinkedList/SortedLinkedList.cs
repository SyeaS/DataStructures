using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.LinkedList
{
    public sealed class SortedLinkedList<T> : DoublyLinkedList<T>
        where T : IComparable<T>
    {
        private static OrderingMode orderingMode;
        public OrderingMode OrderingMode => orderingMode;
        private Comparison<T> Comparator { get; }
        protected override ListElement Tail => head?.Previous;

        private SortedLinkedList(OrderingMode ordering) : base(new EmptyLinkedListInitializer(CreateEmptySortedLinkedList))
        {
            orderingMode = ordering;
        }

        public SortedLinkedList(OrderingMode ordering, Comparison<T> comparator) : base(new EmptyLinkedListInitializer(CreateEmptySortedLinkedList))
        {
            orderingMode = ordering;
            this.Comparator = comparator;
        }

        /// <summary>
        /// Creates the sorted linked list with an IEnumerable of T.
        /// </summary>
        /// <param name="content"></param>
        /// <param name="ordering"></param>
        public SortedLinkedList(IEnumerable<T> content, Comparison<T> comparator, OrderingMode ordering = OrderingMode.Descending) : this(ordering, comparator)
        {
            base.CreateLinkedList(content);
        }

        private static DoublyLinkedList<T> CreateEmptySortedLinkedList()
        {
            return new SortedLinkedList<T>(orderingMode);
        }

        /// <summary>
        /// Adds an element to the sorted linked list by the specified ordering mode.
        /// </summary>
        /// <param name="content"></param>
        /// <param name="ordering"></param>
        protected override void InternalAdd(ref T content)
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

        private void AddElementByDescendingOrder(ref T content)
        {
            ListElement listElement = new ListElement(content);

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
            else if (Tail.Content.CompareTo(content) >= 0)
            {
                listElement.Next = head;
                listElement.Previous = head.Previous;
                head.Previous.Next = listElement;
                head.Previous = listElement;
            }
            else
            {
                ListElement m = head;

                while (!(m == Tail || m.Content.CompareTo(content) < 0))
                {
                    m = m.Next;
                }

                listElement.Next = m;
                listElement.Previous = m.Previous;

                m.Previous.Next = listElement;
                m.Previous = listElement;
            }
        }

        private void AddElementByAscendingOrder(ref T content)
        {
            ListElement listElement = new ListElement(content);

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
            else if (Tail.Content.CompareTo(content) <= 0)
            {
                listElement.Next = head;
                listElement.Previous = head.Previous;
                head.Previous.Next = listElement;
                head.Previous = listElement;
            }
            else
            {
                ListElement m = head;

                while (!(m == Tail || m.Content.CompareTo(content) > 0))
                {
                    m = m.Next;
                }

                listElement.Next = m;
                listElement.Previous = m.Previous;

                m.Previous.Next = listElement;
                m.Previous = listElement;
            }
        }

        protected override T Search(T content)
        {
            ListElement m = head;

            while (!(m == Tail || Comparator(m.Content, content) == 0))
            {
                m = m.Next;
            }

            if (m == Tail && Comparator(m.Content, content) != 0)
            {
                throw new ListElementNotFoundException();
            }

            return m.Content;
        }

        /// <summary>
        /// Removes the specified content from the sorted linked list.
        /// </summary>
        /// <param name="content"></param>
        /// <exception cref="ListElementNotFoundException"></exception>
        protected override void InternalRemove(ref T content)
        {
            if (_count == 0)
            {
                throw new ListElementNotFoundException();
            }

            if (Comparator(head.Content, content) == 0)
            {
                if (_count > 2)
                {
                    head.Next.Previous = Tail;
                    head = head.Next;
                    Tail.Next = head;
                }
                else
                {
                    head = head.Next;
                    if (head != null)
                    {
                        head.Next = null;
                        head.Previous = null;
                    }
                }
            }
            else if (Tail != null && Comparator(Tail.Content, content) == 0)
            {
                if (_count > 2)
                {
                    Tail.Previous.Next = head;
                    head.Previous = Tail.Previous;
                }
                else
                {
                    head.Next = null;
                    head.Previous = null;
                }
            }
            else
            {
                ListElement m = head;

                while (!(m == Tail || Comparator(m.Content, content) == 0))
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
        }

        public override void RemoveAt(int index)
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

        public override int IndexOf(T item)
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
        /// Compares the two sorted linked lists by their values.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(SortedLinkedList<T> left, SortedLinkedList<T> right)
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
        public static bool operator !=(SortedLinkedList<T> left, SortedLinkedList<T> right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Union of the two sorted linked lists.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>A new sorted linked list which contains the values of the sorted linked lists.</returns>
        public static SortedLinkedList<T> operator +(SortedLinkedList<T> left, SortedLinkedList<T> right)
        {
            SortedLinkedList<T> container = new SortedLinkedList<T>(left.OrderingMode);

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
        public static SortedLinkedList<T> operator -(SortedLinkedList<T> left, SortedLinkedList<T> right)
        {
            SortedLinkedList<T> container = new SortedLinkedList<T>(left.OrderingMode);

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

        public override bool Equals(DoublyLinkedList<T> other)
        {
            return other as SortedLinkedList<T> == this;
        }

        public override bool Equals(object obj)
        {
            return obj as SortedLinkedList<T> == this;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}