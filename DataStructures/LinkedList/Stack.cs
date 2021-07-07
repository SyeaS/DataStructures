using DataStructures.LinkedList.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.LinkedList
{
    public class Stack<TContent> : DoublyLinkedList<TContent>, ILinear<TContent>
    {
        protected override ListElement Tail => head?.Previous;

        public Stack() : base(new EmptyLinkedListInitializer(CreateEmptyStack))
        {

        }

        public Stack(IEnumerable<TContent> content) : this()
        {
            base.CreateLinkedList(content);
        }

        private static Stack<TContent> CreateEmptyStack()
        {
            return new Stack<TContent>();
        }

        /// <summary>
        /// Adds an element to the top of the linked list.
        /// </summary>
        /// <param name="content"></param>
        protected override void InternalAdd(ref TContent content)
        {
            AddToStack(ref content);
        }

        /// <summary>
        /// Adds an element to the top of the linked list.
        /// </summary>
        /// <param name="content"></param>
        public void Push(TContent content)
        {
            base.Add(content);
        }

        private void AddToStack(ref TContent content)
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
        protected override void InternalRemove(ref TContent content)
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
        public TContent Pop()
        {
            if (_count == 0)
            {
                throw new EmptyLinkedListException();
            }

            TContent content;

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

        public TContent Peek()
        {
            if (tail == null)
            {
                throw new EmptyLinkedListException();
            }

            return tail.Content;
        }

        public TContent Remove()
        {
            return Pop();
        }

        public override bool Equals(DoublyLinkedList<TContent> other)
        {
            return other == this;
        }
    }
}