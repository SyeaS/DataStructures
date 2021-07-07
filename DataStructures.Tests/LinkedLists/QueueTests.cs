using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DataStructures.Tests
{
    public class QueueTests
    {
        [Theory]
        [InlineData(0, 1, 2, 3, 4, 5, 6, 7, 8, 9)]
        public void AddTest(params int[] values)
        {
            LinkedList.Queue<int> queue = new LinkedList.Queue<int>(values);
            Assert.True(queue.Count == values.Length);
        }

        [Theory]
        [InlineData(0, 1, 2, 3, 4, 5, 6, 7, 8, 9)]
        public void RemoveTest(params int[] values)
        {
            LinkedList.Queue<int> queue = new LinkedList.Queue<int>(values);

            for (int i = 0; i < values.Length; i++)
            {
                if (queue.Dequeue() != values[i])
                {
                    Assert.True(false);
                }
            }

            Assert.True(queue.Count == 0);
        }
    }
}