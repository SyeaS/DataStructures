using DataStructures.LinkedList;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DataStructures.Tests
{
    public class NonUniqueSortedLinkedListTests
    {
        static Comparison<User> comparer = new Comparison<User>(Comparer);

        private static int Comparer(User x, User y)
        {
            if (x.Priority == y.Priority && x.SID == y.SID)
            {
                return 0;
            }
            else if (x.Priority > y.Priority)
            {
                return 1;
            }

            return -1;
        }

        [Theory]
        [InlineData(0f, 0f, 10f, 5f, 2f, 7f, 2f, 0f, 61f, 1f, 6f, 2f)]
        public void Add_ShouldWork(params float[] values)
        {
            SortedLinkedList<User> linkedList = new SortedLinkedList<User>(UserFactory(values), comparer, (OrderingMode)new Random().Next(0, 2));

            float[] floats = new float[2] { float.NaN, float.NaN };
            foreach (User item in linkedList)
            {
                if (floats[0] == float.NaN)
                {
                    floats[0] = item.Priority;
                }
                else if (floats[1] == float.NaN)
                {
                    floats[1] = item.Priority;
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

                    floats[0] = float.NaN;
                    floats[1] = float.NaN;
                }
            }
        }

        [Theory]
        [InlineData(0f, 0f, 10f, 5f, 2f, 7f, 2f, 0f, 61f, 1f, 6f, 2f)]
        public void Remove_ShouldWork(params float[] values)
        {
            IEnumerable<User> users = UserFactory(values);
            SortedLinkedList<User> linkedList = new SortedLinkedList<User>(users, comparer, (OrderingMode)new Random().Next(0, 2));

            foreach (User item in users)
            {
                linkedList.Remove(item);

                if (linkedList.Contains(item))
                {
                    Assert.True(false);
                }
            }

            Assert.True(linkedList.IsEmpty);
        }

        [Theory]
        [InlineData(0f, 0f, 10f, 5f, 2f, 7f, 2f, 0f, 61f, 1f, 6f, 2f)]
        public void Remove_ShouldntWork(params float[] values)
        {
            SortedLinkedList<User> linkedList = new SortedLinkedList<User>(UserFactory(values), comparer, (OrderingMode)new Random().Next(0, 2)); ;

            foreach (float value in values)
            {
                Assert.Throws<ListElementNotFoundException>(() =>
                {
                    linkedList.Remove(User.CreateUser(value));
                });
            }
        }

        private IEnumerable<User> UserFactory(float[] values)
        {
            User[] users = new User[values.Length];

            for (int i = 0; i < users.Length; i++)
            {
                users[i] = User.CreateUser(values[i]);
            }

            return users;
        }
    }

    public class User : IComparable<User>
    {
        public int SID { get; }
        public float Priority { get; }

        public User(int SID, float priority)
        {
            this.SID = SID;
            Priority = priority;
        }

        public static User CreateUser(float priority)
        {
            return new User(new Random().Next(0, int.MaxValue), priority);
        }

        public static User CreateUser(int SID, float priority)
        {
            return new User(SID, priority);
        }

        public int CompareTo(User other)
        {
            if (this.Priority == other.Priority)
            {
                return 0;
            }
            else if (this.Priority > other.Priority)
            {
                return 1;
            }

            return -1;
        }
    }
}