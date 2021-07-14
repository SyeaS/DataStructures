using DataStructures.LinkedList;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Xunit;

namespace SortedPlayerQueue.Tests
{
    public class UniqueSortedLinkedListTests
    {
        UniqueSortedLinkedList<float> linkedList;

        [Theory]
        [InlineData(int.MaxValue)]
        [InlineData(1221)]
        [InlineData(213)]
        public void Add_ShouldWork(object obj)
        {
            linkedList = LoadLinkedList((OrderingMode)(new Random().Next(0, 2)));

            float[] floats = new float[2];
            foreach (float item in linkedList)
            {
                if (floats[0] == 0.00f)
                {
                    floats[0] = item;
                }
                else if (floats[1] == 0.00f)
                {
                    floats[1] = item;
                }
                else
                {
                    if (linkedList.OrderingMode == OrderingMode.Ascending)
                    {
                        float copy = floats[0];

                        floats[0] = floats[1];
                        floats[1] = copy;
                    }

                    if (floats[0] < floats[1])
                    {
                        Assert.True(false, linkedList.OrderingMode.ToString());
                    }

                    floats[0] = 0.00f;
                    floats[1] = 0.00f;
                }
            }
        }

        [Theory]
        [InlineData(212321)]
        [InlineData(342)]
        [InlineData(75)]
        public void Add_ShouldntWork(object obj)
        {
            UniqueSortedLinkedList<float> linkedList = new UniqueSortedLinkedList<float>(OrderingMode.Descending);
            List<float> numbers = new List<float>();
            int rnd = new Random().Next(0, byte.MaxValue / 8);

            for (int i = 0; i < rnd; i++)
            {
                numbers.Add(RandomGenerator());
                linkedList.Add(numbers[i]);
            }

            Assert.Throws<DuplicateListElementException>(() =>
            {
                linkedList.Add(numbers[new Random().Next(0, numbers.Count)]);
            });
        }

        [Theory]
        [InlineData(5453)]
        [InlineData(1231)]
        [InlineData(1271)]
        [InlineData(82)]
        [InlineData(52)]
        public void Remove_ShouldWork(object obj)
        {
            UniqueSortedLinkedList<float> linkedList = new UniqueSortedLinkedList<float>(OrderingMode.Descending);
            List<float> numbers = new List<float>();
            int rnd = new Random().Next(0, 32);

            for (int i = 0; i < rnd; i++)
            {
                numbers.Add(RandomGenerator());
                linkedList.Add(numbers[i]);
            }

            float[] values = new float[linkedList.Count];

            int index = 0;

            foreach (float item in linkedList)
            {
                values[index] = item;
                index++;
            }

            foreach (float item in values)
            {
                linkedList.Remove(item);
            }
            Assert.True(linkedList.IsEmpty);
        }

        [Theory]
        [InlineData(0.00f, 2.22f)]
        [InlineData(5.752f, 9.1f)]
        [InlineData(67.73353f, 2.56f)]
        [InlineData(3.85647f, 8.01f)]
        [InlineData(7.13f, 20.00001f)]
        public void Remove_ShouldntWork(params float[] content)
        {
            UniqueSortedLinkedList<float> linkedList = new UniqueSortedLinkedList<float>(OrderingMode.Descending);
            List<float> numbers = new List<float>();
            int rnd = new Random().Next(0, byte.MaxValue / 8);

            for (int i = 0; i < rnd; i++)
            {
                numbers.Add(RandomGenerator());
                linkedList.Add(numbers[i]);
            }

            foreach (float num in content)
            {
                Assert.Throws<ListElementNotFoundException>(() =>
                {
                    linkedList.Remove(num);
                });
            }
        }

        public static UniqueSortedLinkedList<float> LoadLinkedList(OrderingMode order)
        {
            UniqueSortedLinkedList<float> linkedList = new UniqueSortedLinkedList<float>(order);
            int rnd = new Random().Next(0, byte.MaxValue * 8);

            for (int i = 0; i < rnd; i++)
            {
                try
                {
                    linkedList.Add(RandomGenerator());
                }
                catch (Exception)
                {

                }
            }

            return linkedList;
        }

        private static float RandomGenerator()
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] bytes = new byte[1024];
            int number = 0;
            rng.GetBytes(bytes);

            foreach (byte value in bytes)
            {
                number += value;
            }

            return (float)number + ((float)number / 261120.0f);
        }
    }
}