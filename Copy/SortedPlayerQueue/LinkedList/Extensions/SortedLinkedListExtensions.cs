using System;
using System.Collections.Generic;

namespace SortedPlayerQueue.LinkedList.Extensions
{
    public static class SortedLinkedListExtensions
    {
        public static List<TContent> ToList<TContent>(this SortedLinkedList<TContent> linkedList)
            where TContent : IComparable<TContent>
        {
            return new List<TContent>(linkedList as IEnumerable<TContent>);
        }

        public static TContent[] ToArray<TContent>(this SortedLinkedList<TContent> linkedList)
            where TContent : IComparable<TContent>
        {
            TContent[] array = new TContent[linkedList.Count];
            linkedList.CopyTo(array, 0);
            return array;
        }
    }
}