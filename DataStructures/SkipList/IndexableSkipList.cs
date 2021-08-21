using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.LinkedList
{
    public sealed class IndexableSkipList<T> : SkipList<T>
        where T : IComparable<T>
    {
        private class IndexedSkipListElement : IIndexedSkipListElement
        {
            public IIndexedSkipListElement[] Next { get; set; }
            private int[] Width { get; set; }
            public T Content { get; set; }

            protected IndexedSkipListElement(int size) : this(ref size)
            {

            }

            protected IndexedSkipListElement(ref int size)
            {
                Next = new IndexedSkipListElement[size];
                Width = new int[size - 1];
            }

            public IndexedSkipListElement(ref T content, int size) : this(ref size)
            {
                Content = content;
            }

            public IndexedSkipListElement(ref T content, ref int size) : this(ref size)
            {
                Content = content;
            }

            public virtual int this[int index]
            {
                get
                {
                    if (index == 0)
                    {
                        return 1;
                    }

                    return Width[index - 1];
                }
                set
                {
                    if (index == 0)
                    {
                        return;
                    }

                    Width[index - 1] = value;
                }
            }
        }

        private class SentinelIndexedSkipListElement : IndexedSkipListElement
        {
            public SentinelIndexedSkipListElement() : base(MAX_LEVEL)
            {

            }

            public override int this[int index]
            {
                get => 0;
                set { }
            }
        }

        private interface IIndexedSkipListElement
        {
            IIndexedSkipListElement[] Next { get; set; }
            T Content { get; set; }

            /// <summary>
            /// An indexer for getting the width at the specified index or setting the width.
            /// </summary>
            /// <param name="index"></param>
            /// <returns>The width.</returns>
            public int this[int index] { get; set; }
        }

        private IIndexedSkipListElement head;

        public IndexableSkipList() : base()
        {

        }

        public IndexableSkipList(GenerateRandom randomGenerator) : base(randomGenerator)
        {

        }

        public IndexableSkipList(IEnumerable<T> content) : base(content)
        {

        }

        public IndexableSkipList(IEnumerable<T> content, GenerateRandom randomGenerator) : base(content, randomGenerator)
        {

        }

        protected override void InternalAdd(ref T element)
        {
            int level = RandomLevel();
            IIndexedSkipListElement newElement = new IndexedSkipListElement(ref element, level + 1);

            if (head is null)
            {
                head = new SentinelIndexedSkipListElement();
            }

            IIndexedSkipListElement m = head;
            IIndexedSkipListElement prev = null;
            int width = 1;

            for (int i = Level - 1; i >= 0; i--)
            {
                for (; m.Next[i] is not null; m = m.Next[i])
                {
                    if (element.CompareTo(m.Next[i].Content) < 0)
                    {
                        if (i > 0 && m.Next[i] != prev)
                        {
                            m.Next[i][i]++;
                            prev = m.Next[i];
                        }
                        break;
                    }

                    width += m.Next[i][i];
                }

                if (level >= i)
                {
                    if (m.Next[i] is not null) // between two elements
                    {
                        newElement[i] = m.Next[i][i] - 1;
                        m.Next[i][i] = 1;
                    }
                    else
                    {
                        if (level < m.Next.Length - 1)
                        {
                            newElement[i] = width;
                        }
                    }
                    newElement.Next[i] = m.Next[i];
                    m.Next[i] = newElement;
                }

                width = 1;
            }
        }

        protected override void InternalRemove(ref T element)
        {
            IIndexedSkipListElement m = head;
            bool found = false;

            for (int i = Level - 1; i >= 0; i--)
            {
                int compare;

                for (; m.Next[i] is not null; m = m.Next[i])
                {
                    compare = element.CompareTo(m.Next[i].Content);

                    if (compare < 0)
                    {
                        break;
                    }
                    else if (compare == 0)
                    {
                        found = true;
                        m.Next[i] = m.Next[i].Next[i];
                        break;
                    }
                }
            }

            if (head.Next[0] is null)
            {
                head = null;
            }

            if (!found)
            {
                throw new ListElementNotFoundException();
            }
        }

        public override int IndexOf(T content)
        {
            throw new NotImplementedException();
        }

        public override void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        protected override IListElement SearchListElement(int index)
        {
            if (index >= Count || index < 0)
            {
                throw new IndexOutOfRangeException("Index is out of bounds of the linked list.");
            }

            IIndexedSkipListElement m = head;

            /*for (int i = Level - 1; i >= 0 && index == 1; i--)
            {
                for (; m.Next[i] is not null &&
                    index - m.Next[0].SkippingNodes >= 0; m = m.Next[i])
                {
                    index -= m.SkippingNodes;
                }

                m = m.Next[i] ?? m;
            }*/

            return new ListElement(m.Content);
        }

        public string[] Print()
        {
            string[] lines = new string[Level];
            IIndexedSkipListElement m = null;
            List<IIndexedSkipListElement>[] linesList = new List<IIndexedSkipListElement>[Level];

            for (int i = 0; i < Level; i++)
            {
                linesList[i] = new List<IIndexedSkipListElement>();
            }

            for (int i = 0; i < Level; i++)
            {
                m = head.Next[i];
                StringBuilder line = new StringBuilder();
                int stepped = 0;

                line.Append("[SENTINEL]");

                while (m is not null)
                {
                    try
                    {
                        int index = linesList[i - 1].IndexOf(m);
                        int j = index + (GetIndex(lines[i - 1], index + 1) - 2) - stepped;
                        int length = line.Length;

                        line.Append(' ');

                        for (int x = 0; x < j; x++)
                        {
                            line.Append('-');
                        }

                        line.Append($"> [{ m.Content } ({ m[i] })]");
                        stepped += line.Length - length;
                    }
                    catch (Exception)
                    {
                        line.Append($" ---> [{ m.Content } ({ m[i] })]");
                    }
                    finally
                    {
                        linesList[i].Add(m);
                        m = m.Next[i];
                    }
                }

                lines[i] = line.ToString();
            }

            return lines;
        }

        private int GetIndex(string line, int element)
        {
            int j = "[SENTINEL]".Length + 1;
            int z = 0;

            for (int i = 0; i < element; i++)
            {
                for (; line[j] != '['; j++)
                {
                    z++;
                }

                j++;
            }

            return z;
        }
    }
}