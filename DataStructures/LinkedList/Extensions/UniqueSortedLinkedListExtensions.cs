using System;
using System.Collections.Generic;

namespace DataStructures.LinkedList
{
    public static class UniqueSortedLinkedListExtensions
    {
        public static List<T> ToList<T>(this UniqueSortedLinkedList<T> linkedList)
            where T : IComparable<T>
        {
            return new List<T>(linkedList as IEnumerable<T>);
        }

        public static T[] ToArray<T>(this UniqueSortedLinkedList<T> linkedList)
            where T : IComparable<T>
        {
            T[] array = new T[linkedList.Count];
            linkedList.CopyTo(array, 0);
            return array;
        }
    }
}