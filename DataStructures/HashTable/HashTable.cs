using DataStructures.LinkedList;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.HashTable
{
    public sealed class HashTable<T, K> : ICollection, IEnumerable<T>
        where T : IComparable<T>
        where K : IComparable<K>
    {
        private class HashTableElement : IComparable<HashTableElement>
        {
            public K Key { get; set; }
            public T Content { get; set; }

            public HashTableElement(T content, K key)
            {
                Key = key;
                Content = content;
            }

            public int CompareTo(HashTableElement other)
            {
                return Content.CompareTo(other.Content);
            }
        }

        private UniqueSortedLinkedList<HashTableElement>[] table;
        private int _size;
        public int Size => _size;

        public int Count { get; private set; }
        public bool IsSynchronized => false;
        public object SyncRoot { get; } = new object();

        public delegate int HashingAlgorithm(K key, int size);
        private HashingAlgorithm _hash;

        public HashTable(int tableSize, HashingAlgorithm hashFunction, OrderingMode orderingMode = OrderingMode.Descending)
        {
            _size = tableSize;
            table = new UniqueSortedLinkedList<HashTableElement>[tableSize];
            _hash = hashFunction;

            for (int i = 0; i < _size; i++)
            {
                table[i] = new UniqueSortedLinkedList<HashTableElement>(orderingMode);
            }
        }

        public void Add(T content, K key)
        {
            int index = _hash(key, _size);
            table[index].Add(new HashTableElement(content, key));
            Count++;
        }

        public void Remove(T content, K key)
        {
            int index = _hash(key, _size);
            table[index].Remove(new HashTableElement(content, key));
            Count--;
        }

        public T Search(K key)
        {
            int index = _hash(key, _size);

            foreach (HashTableElement element in table[index])
            {
                if (element.Key.CompareTo(key) == 0)
                {
                    return element.Content;
                }
            }

            throw new HashTableElementNotFoundException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator(this);
        }

        public void CopyTo(Array array, int index)
        {
            if (index < 0 || array.Length >= index)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }
            else if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }
            else if (index > Count - 1)
            {
                throw new ArgumentException(nameof(index));
            }

            foreach (T item in this)
            {
                try
                {
                    array.SetValue(item, index++);
                }
                catch (Exception)
                {
                    break;
                }
            }
        }

        public T this[K key]
        {
            get { return Search(key); }
            set { Add(value, key); }
        }

        struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
        {
            public T Current => current.Content;
            object IEnumerator.Current => current as object;
            private HashTableElement current;
            private readonly UniqueSortedLinkedList<HashTableElement>[] table;
            private LinkedList.Queue<HashTableElement> queue;

            public Enumerator(HashTable<T, K> hashTable)
            {
                table = hashTable.table;
                queue = new LinkedList.Queue<HashTableElement>();
                current = null;
                InitializeQueue();
            }

            private void InitializeQueue()
            {
                foreach (UniqueSortedLinkedList<HashTableElement> linkedList in table)
                {
                    foreach (HashTableElement item in linkedList)
                    {
                        queue.Add(item);
                    }
                }
            }

            public bool MoveNext()
            {
                current = queue.Dequeue();
                return current is not null;
            }

            public void Reset()
            {
                InitializeQueue();
            }

            public void Dispose()
            {
                current = null;
            }
        }
    }
}