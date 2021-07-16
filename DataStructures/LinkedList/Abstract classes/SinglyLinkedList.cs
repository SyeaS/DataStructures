using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.LinkedList
{
    public abstract class SinglyLinkedList<T> : LinkedList<T>
    {
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

        protected IListElement head;
        protected override IListElement Head => head;

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

        public override int IndexOf(T content)
        {
            throw new NotSupportedException();
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
    }
}