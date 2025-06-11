using System;
using System.Collections.Generic;

namespace DataStructures.LinkedList
{
    public class Deque<T> : DoublyLinkedList<T>, IQueue<T>, IStack<T>
    {
        private IDoublyLinkedListElement tail;

        public Deque() : base(CreateEmptyDeque)
        {

        }

        public Deque(IEnumerable<T> content) : this()
        {
            foreach (T item in content)
            {
                Add(item);
            }
        }

        private static DoublyLinkedList<T> CreateEmptyDeque()
        {
            return new Deque<T>();
        }

        protected override void InternalAdd(ref T element)
        {
            IDoublyLinkedListElement newHead = new DoublyLinkedListElement(ref element);
            head.Previous = newHead;
            newHead.Next = head;
            head = newHead;
        }

        protected override void InternalRemove(ref T element)
        {
            throw new NotSupportedException("Cannot remove a specific element from a dequeue.");
        }

        public void Enqueue(T element)
        {
            InternalAdd(ref element);
            Count++;
        }

        public T Dequeue()
        {
            T content = InternalDequeue();
            Count--;
            return content;
        }

        private T InternalDequeue()
        {
            T content = head.Content;
            head = head.Next;
            return content;
        }

        public T PeekFirst()
        {
            return head.Content;
        }

        public void Push(T element)
        {
            InternalAdd(ref element);
            Count++;
        }

        public T Pop()
        {
            T content = tail.Content;
            Count--;
            return content;
        }

        public T PeekLast()
        {
            return tail.Content;
        }

        public void Append(T element)
        {
            IDoublyLinkedListElement newTail = new DoublyLinkedListElement(ref element);
            tail.Next = newTail;
            newTail.Previous = tail;
            tail = newTail;
        }

        public override void RemoveAt(int index)
        {
            throw new NotSupportedException("The RemoveAt method is not supported for dequeue.");
        }

        public override int IndexOf(T content)
        {
            int index = 0;

            foreach (T item in this)
            {
                if (item.Equals(content))
                {
                    return index;
                }

                index++;
            }

            throw new ListElementNotFoundException();
        }

        public override bool Equals(DoublyLinkedList<T> other)
        {
            if (Count == other.Count)
            {
                IDoublyLinkedListElement m = head;

                while (m != null)
                {
                    if (!other.Contains(m.Content))
                    {
                        return false;
                    }
                }

                return true;
            }

            return false;
        }
    }
}