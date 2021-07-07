using DataStructures.HashTable;
using DataStructures.LinkedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SortedPlayerQueue.Tests.HashTable
{
    public class HashTableTests
    {
        private static int Hash(float key, int size)
        {
            int value = (int)key / 10;

            if (value > 9)
            {
                value = 9;
            }
            else if (value < 0)
            {
                value = 0;
            }

            return value;
        }

        private static HashTable<float, float> CreateHashTable()
        {
            return new HashTable<float, float>(10,
                new HashTable<float, float>.HashingAlgorithm(Hash));
        }

        [Theory]
        [InlineData(55.01252f, 35.012f, 00.11f, 11.00f, 9.99f, 99.99f, 100.00f, 50.00f, 40.00f, 11.11f, 10.11f, 25.623542523f, 71.42f, 72.567f, 21.667f, 74.0543f)]
        public void AddShouldWork(params float[] keys)
        {
            HashTable<float, float> table = CreateHashTable();
            int number = keys.Length;

            for (int i = 0; i < number; i++)
            {
                table.Add(keys[i], keys[i]);
            }
        }

        [Theory]
        [InlineData(55.01252f, 35.012f, 00.11f, 11.00f, 9.99f, 99.99f, 100.00f, 50.00f, 40.00f, 11.11f, 10.11f, 25.623542523f, 71.42f, 72.567f, 21.667f, 74.0543f)]
        public void AddShouldntWork(params float[] keys)
        {
            HashTable<float, float> table = CreateHashTable();

            for (int i = 0; i < keys.Length; i++)
            {
                table.Add(keys[i], keys[i]);

                Assert.Throws<DuplicateListElementException>(() =>
                table.Add(keys[i], keys[i]));
            }
        }
    }
}