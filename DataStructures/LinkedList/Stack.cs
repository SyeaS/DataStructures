using DataStructures.LinkedList.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.LinkedList
{
    public class Stack<T> : DoublyLinkedList<T>, ILinear<T>
    {
        protected override ListElement Tail => head?.Previous;

        public Stack() : base(new EmptyLinkedListInitializer(CreateEmptyStack))
        {

        }

        public Stack(IEnumerable<T> content) : this()
        {
            base.CreateLinkedList(content);
        }

        private static Stack<T> CreateEmptyStack()
        {
            return new Stack<T>();
        }

        /// <summary>
        /// Adds an element to the top of the linked list.
        /// </summary>
        /// <param name="content"></param>
        protected override void InternalAdd(ref T content)
        {
            AddToStack(ref content);
        }

        /// <summary>
        /// Adds an element to the top of the linked list.
        /// </summary>
        /// <param name="content"></param>
        public void Push(T content)
        {
            base.Add(content);
        }

        private void AddToStack(ref T content)
        {
            ListElement listElement = new ListElement(content);

            if (head == null)
            {
                head = listElement;
                tail = head;
            }
            else
            {
                tail.Next = listElement;
                listElement.Previous = tail;
                tail = tail.Next;
            }
        }

        /// <summary>
        /// The remove operation for Stack is not supported.
        /// </summary>
        /// <param name="content"></param>
        /// <exception cref="NotSupportedException"></exception>
        protected override void InternalRemove(ref T content)
        {
            throw new NotSupportedException("The remove operation for Stack is not supported.");
        }

        /// <summary>
        /// The RemoveAt operation is not supported for Stack.
        /// </summary>
        /// <param name="index"></param>
        /// <exception cref="NotSupportedException"></exception>
        public override void RemoveAt(int index)
        {
            throw new NotSupportedException("The RemoveAt operation is not supported for Stack.");
        }

        /// <summary>
        /// Removes an element from the top of the linked list.
        /// </summary>
        /// <returns></returns>
        public T Pop()
        {
            if (_count == 0)
            {
                throw new EmptyLinkedListException();
            }

            T content;

            if (_count == 1)
            {
                content = head.Content;
                head = null;
                tail = null;
            }
            else
            {
                content = tail.Content;
                tail.Previous.Next = null;
                tail = tail.Previous;
            }

            _count--;
            return content;
        }

        public T Peek()
        {
            if (tail == null)
            {
                throw new EmptyLinkedListException();
            }

            return tail.Content;
        }

        public T Remove()
        {
            return Pop();
        }

        public override bool Equals(DoublyLinkedList<T> other)
        {
            return other == this;
        }
    }
}