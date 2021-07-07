using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using DataStructures.LinkedList;

namespace DataStructures.Tests
{
    public class StackTests
    {
        [Theory]
        [InlineData(0, 1, 2, 3, 4, 5, 6, 7, 8, 9)]
        public void AddTest(params int[] values)
        {
            LinkedList.Stack<int> stack = new LinkedList.Stack<int>(values);
            Assert.True(stack.Count == values.Length);
        }

        [Theory]
        [InlineData(0, 1, 2, 3, 4, 5, 6, 7, 8, 9)]
        public void RemoveTest(params int[] values)
        {
            LinkedList.Stack<int> stack = new DataStructures.LinkedList.Stack<int>(values);

            for (int i = values.Length - 1; i >= 0; i--)
            {
                if (stack.Pop() != values[i])
                {
                    Assert.True(false);
                }
            }

            Assert.True(stack.IsEmpty);
        }
    }
}