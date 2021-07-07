using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.DynamicArray
{
    public sealed class DynamicArray<T> : ICollection<T>, ICollection
    {
        private T[] array;

        private int Capacity => array.Length;
        private int _count;
        public int Count => _count;

        public bool IsReadOnly => false;

        public bool IsSynchronized => false;
        public object SyncRoot { get; } = new object();

        public DynamicArray() : this(16)
        {

        }

        public DynamicArray(int initialSize)
        {
            array = new T[initialSize];
        }

        public DynamicArray(IEnumerable<T> content) : this()
        {
            foreach (T item in content)
            {
                Add(item);
            }
        }

        public void Add(T item)
        {
            array[_count++] = item;
            Resize();
        }

        public void Clear()
        {
            Array.Clear(array, 0, Capacity);
        }

        public bool Contains(T item)
        {
            foreach (T content in array)
            {
                if (content.Equals(item))
                {
                    return true;
                }
            }

            return false;
        }

        public void CopyTo(Array array, int index)
        {
            this.array.CopyTo(array, index);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            this.array.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            for (int i = 0; i < Capacity; i++)
            {
                if (array[i].Equals(item))
                {
                    array[i] = default(T);
                    return true;
                }
            }

            return false;
        }

        private void Resize()
        {
            if (_count == Capacity)
            {
                Array.Resize(ref array, (Capacity - 1) * 2);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (T item in array)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public T this[int index]
        {
            get { return array[index]; }
            set { array[index] = value; }
        }
    }
}