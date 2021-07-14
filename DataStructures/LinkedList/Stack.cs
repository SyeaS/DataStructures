using DataStructures.LinkedList.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.LinkedList
{
    public class Stack<T> : SinglyLinkedList<T>, ILinear<T>
    {
        public Stack()
        {

        }

        public Stack(IEnumerable<T> content)
        {
            base.CreateFromIEnumerable(ref content);
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
            ListElement listElement = new ListElement(ref content);
            listElement.Next = Head;
            head = listElement;
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
            if (Count == 0)
            {
                throw new EmptyLinkedListException();
            }

            T content = Head.Content;
            head = Head.Next;

            Count--;
            return content;
        }

        public T Peek()
        {
            if (Head == null)
            {
                throw new EmptyLinkedListException();
            }

            return Head.Content;
        }

        public T Remove()
        {
            return Pop();
        }
    }
}