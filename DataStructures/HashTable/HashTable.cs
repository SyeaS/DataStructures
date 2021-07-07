using DataStructures.LinkedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.HashTable
{
    public sealed class HashTable<T, K> where T : IComparable<T>
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
        }

        public void Remove(T content, K key)
        {
            int index = _hash(key, _size);
            table[index].Remove(new HashTableElement(content, key));
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

        public T this[K key]
        {
            get { return Search(key); }
            set { Add(value, key); }
        }
    }
}