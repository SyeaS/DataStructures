using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortedPlayerQueue
{
    public sealed class HashTable<TContent, K> where TContent : IComparable<TContent>
        where K : IComparable<K>
    {
        class HashTableElement : IComparable<HashTableElement>
        {
            public K Key { get; set; }
            public TContent Content { get; set; }

            public HashTableElement(TContent content, K key)
            {
                Key = key;
                Content = content;
            }

            public int CompareTo(HashTableElement other)
            {
                return Content.CompareTo(other.Content);
            }
        }

        private SortedLinkedList<HashTableElement>[] table;
        private int _size;
        public int Size => _size;

        public delegate int HashingAlgorithm(K key, int size);
        private HashingAlgorithm _hash;

        public HashTable(int tableSize, HashingAlgorithm hashFunction, OrderingMode orderingMode = OrderingMode.Descending)
        {
            _size = tableSize;
            table = new SortedLinkedList<HashTableElement>[tableSize];
            _hash = hashFunction;

            for (int i = 0; i < _size; i++)
            {
                table[i] = new SortedLinkedList<HashTableElement>(orderingMode);
            }
        }
    
        public void Add(TContent content, K key)
        {
            int index = _hash(key, _size);
            table[index].Add(new HashTableElement(content, key));
        }

        public void Remove(TContent content, K key)
        {
            int index = _hash(key, _size);
            table[index].Remove(new HashTableElement(content, key));
        }

        public TContent Search(K key)
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

        public TContent this[K key]
        {
            get { return Search(key); }
            set { Add(value, key); }
        }
    }
}