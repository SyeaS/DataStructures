using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.LinkedList
{
    public abstract class DoublyLinkedList<T> : LinkedList<T>
    {
        protected class DoublyLinkedListElement : ListElement, IDoublyLinkedListElement
        {
            private IDoublyLinkedListElement _next;
            public IDoublyLinkedListElement Next
            {
                get => _next;
                set
                {
                    _next = value;
                    base.Next = _next as IListElement;
                }
            }
            public IDoublyLinkedListElement Previous { get; set; }

            public DoublyLinkedListElement(ref T content) : base(ref content)
            {
                Content = content;
            }
        }

        protected interface IDoublyLinkedListElement : IListElement
        {
            IDoublyLinkedListElement Next { get; set; }
            IDoublyLinkedListElement Previous { get; set; }
        }

        protected IDoublyLinkedListElement head;
        protected override IListElement Head => head as IListElement;
        protected IDoublyLinkedListElement Tail => head?.Previous;

        protected delegate DoublyLinkedList<T> EmptyLinkedListInitializer();
        private static EmptyLinkedListInitializer initializeEmptyLinkedList;

        protected DoublyLinkedList(EmptyLinkedListInitializer emptyLinkedListInitializer)
        {
            initializeEmptyLinkedList = emptyLinkedListInitializer;
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

            IDoublyLinkedListElement rightElement = right.head;

            for (int i = 0; i < left.Count; i++)
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
            return left.Count < right.Count;
        }

        /// <summary>
        /// Compares the sorted linked lists by their lengths;
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator >(DoublyLinkedList<T> left, DoublyLinkedList<T> right)
        {
            return left.Count > right.Count;
        }

        /// <summary>
        /// Compares the sorted linked lists by their lengths;
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator <=(DoublyLinkedList<T> left, DoublyLinkedList<T> right)
        {
            return left.Count <= right.Count;
        }

        /// <summary>
        /// Compares the sorted linked lists by their lengths;
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator >=(DoublyLinkedList<T> left, DoublyLinkedList<T> right)
        {
            return left.Count >= right.Count;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override void Clear()
        {
            base.Clear();
            head = null;
        }

        public override void Dispose()
        {
            base.Dispose();
            head = null;
        }

        public abstract bool Equals(DoublyLinkedList<T> other);
    }
}