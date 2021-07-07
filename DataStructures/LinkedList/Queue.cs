using DataStructures.LinkedList.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.LinkedList
{
    public sealed class Queue<T> : SinglyLinkedList<T>, ILinear<T>
    {
        private ListElement tail;

        public Queue()
        {

        }

        public Queue(IEnumerable<T> content) : base(content)
        {

        }

        protected override void InternalAdd(ref T content)
        {
            AddToQueue(ref content);
        }

        public void Enqueue(T content)
        {
            base.Add(content);
        }

        private void AddToQueue(ref T content)
        {
            ListElement listElement = new ListElement(content);

            if (head == null)
            {
                head = listElement;
            }
            else
            {
                tail.Next = listElement;
            }

            tail = listElement;
        }

        /// <summary>
        /// The remove operation is not supported for Queue.
        /// </summary>
        /// <param name="content"></param>
        protected override void InternalRemove(ref T content)
        {
            throw new NotSupportedException("The remove operation is not supported for Queue.");
        }

        public T Dequeue()
        {
            if (Count == 0)
            {
                throw new EmptyLinkedListException();
            }

            T content = head.Content;

            if (Count == 1)
            {
                head = null;
                tail = null;
            }
            else
            {
                head = head.Next;
            }

            Count--;
            return content;
        }

        public T Remove()
        {
            return Dequeue();
        }

        public T Peek()
        {
            if (head == null)
            {
                throw new EmptyLinkedListException();
            }

            return head.Content;
        }
    }
}