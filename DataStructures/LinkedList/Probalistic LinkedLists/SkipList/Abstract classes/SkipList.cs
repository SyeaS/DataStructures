using DataStructures.LinkedList.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.LinkedList
{
    public abstract class SkipList<T> : LinkedList<T>
        where T : IComparable<T>
    {
        protected class SkipListElement : ListElement, ISkipListElement
        {
            private ISkipListElement _next;
            public ISkipListElement Next
            {
                get => _next;
                set
                {
                    _next = value;
                    if (value is not SentinelSkipListElement)
                    {
                        base.Next = value as IListElement;
                    }
                }
            }
            public ISkipListElement Previous { get; set; }
            public ISkipListElement Top { get; set; }
            public ISkipListElement Bottom { get; set; }

            protected SkipListElement()
            {

            }

            public SkipListElement(ref T content) : base(ref content)
            {

            }
        }

        protected class SentinelSkipListElement : SkipListElement
        {
            public SentinelSkipListElement()
            {

            }
        }

        protected interface ISkipListElement : IListElement
        {
            ISkipListElement Next { get; set; }
            ISkipListElement Previous { get; set; }
            ISkipListElement Top { get; set; }
            ISkipListElement Bottom { get; set; }
        }

        private const int MAX_LEVEL = 32;
        private const float PROBABILITY = 0.5f;
        private const int MAX_RANDOM = int.MaxValue;

        private int Level { get; set; }
        protected override IListElement Head => firstHead?.Next;
        protected ISkipListElement sentinelHead; // Sentinel node!
        protected ISkipListElement firstHead; // For enumerator!

        private static Random random;
        private GenerateRandom GetRandom { get; }

        public SkipList() : this(RandomGenerator)
        {
            random = new Random();
        }

        public SkipList(GenerateRandom randomGenerator)
        {
            GetRandom = randomGenerator;
        }

        public SkipList(IEnumerable<T> content) : this()
        {
            base.CreateFromIEnumerable(ref content);
        }

        public SkipList(IEnumerable<T> content, GenerateRandom randomGenerator) : this(randomGenerator)
        {
            base.CreateFromIEnumerable(ref content);
        }

        private static int RandomGenerator()
        {
            return random.Next(int.MaxValue);
        }

        private static int RandomLevel()
        {
            int lvl = 0;

            while ((((float)RandomGenerator()) / MAX_RANDOM) < PROBABILITY &&
                lvl < MAX_LEVEL)
            {
                ++lvl;
            }

            return lvl;
        }

        protected override void InternalAdd(ref T element)
        {
            ISkipListElement newElement = new SkipListElement(ref element);
            Stack<ISkipListElement> levels = null;

            try
            {
                Search(ref element, out levels);
            }
            catch (InternalListElementNotFoundException e)
            {
                ISkipListElement listElement = e.ListElement as ISkipListElement;
                AddElement(ref newElement, ref listElement);
            }
            catch (NullReferenceException)
            {
                CreateLevel(ref newElement);
                return;
            }

            int randomLevel = RandomLevel();
            ISkipListElement previous = newElement;

            for (int level = 0; level < randomLevel; level++)
            {
                ISkipListElement current = new SkipListElement(ref element);

                try
                {
                    ISkipListElement levelElement = levels.Pop();

                    AddElement(ref current, ref levelElement);
                }
                catch (EmptyLinkedListException)
                {
                    CreateLevel(ref current);
                }

                ConnectElement(ref current, ref previous);

                previous = current;
            }
        }

        protected void AddElement(ref ISkipListElement element, ref ISkipListElement from)
        {
            element.Next = from.Next;
            element.Previous = from;
            from.Next.Previous = element;
            from.Next = element;
        }

        protected void ConnectElement(ref ISkipListElement top, ref ISkipListElement bottom)
        {
            bottom.Top = top;
            top.Bottom = bottom;
        }

        protected override void InternalRemove(ref T element)
        {
            ISkipListElement listElement = Search(ref element);
            this.InternalRemove(ref listElement);
        }

        protected void InternalRemove(ref ISkipListElement listElement)
        {
            while (listElement != null)
            {
                listElement.Next.Previous = listElement.Previous;
                listElement.Previous.Next = listElement.Next;
                listElement.Top = null;

                if (IsSentinel(listElement.Previous) && IsSentinel(listElement.Next))
                {
                    RemoveLastLevel();
                }

                listElement = listElement.Bottom;
            }
        }

        protected override T Search(T content)
        {
            return this.Search(ref content).Content;
        }

        public override bool Contains(T item)
        {
            try
            {
                Search(item);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private ISkipListElement Search(ref T content)
        {
            ISkipListElement m = sentinelHead;
            int compare = content.CompareTo(m.Next.Content);

            while (compare != 0)
            {
                if (compare < 0 || IsSentinel(m.Next))
                {
                    m = m.Bottom;

                    if (m is null)
                    {
                        throw new ListElementNotFoundException();
                    }
                }
                else if (compare > 0)
                {
                    m = m.Next;
                }

                compare = content.CompareTo(m.Next.Content);
            }

            return m.Next;
        }

        private ISkipListElement Search(ref T content, out Stack<ISkipListElement> levels)
        {
            ISkipListElement m = sentinelHead;
            levels = new Stack<ISkipListElement>();
            int compare = content.CompareTo(m.Next.Content);

            while (compare != 0)
            {
                if (compare < 0 || IsSentinel(m.Next))
                {
                    if (m.Bottom is null)
                    {
                        throw new InternalListElementNotFoundException(m);
                    }

                    levels.Push(m);
                    m = m.Bottom;
                }
                else if (compare > 0)
                {
                    m = m.Next;
                }

                compare = content.CompareTo(m.Next.Content);
            }

            throw new DuplicateListElementException();
        }

        public T Maximum()
        {
            ISkipListElement max = sentinelHead.Previous.Top;

            return max.Content;
        }

        public T PopMax()
        {
            ISkipListElement max = sentinelHead.Previous.Top;
            this.InternalRemove(ref max);
            return max.Content;
        }

        public T Minimum()
        {
            ISkipListElement min = firstHead;

            return min.Content;
        }

        public T PopMin()
        {
            ISkipListElement min = firstHead;
            this.InternalRemove(ref min);
            return min.Content;
        }

        private void CreateLevel(ref ISkipListElement element)
        {
            SentinelSkipListElement sentinelHead = new SentinelSkipListElement();
            SentinelSkipListElement sentinelTail = new SentinelSkipListElement();
            sentinelHead.Previous = sentinelTail;

            sentinelHead.Next = element;
            sentinelTail.Previous = element;

            element.Previous = sentinelHead;
            element.Next = sentinelTail;

            if (this.firstHead is null)
            {
                this.firstHead = sentinelHead;
            }
            else
            {
                ISkipListElement previousSentinelHead = this.sentinelHead;
                sentinelHead.Bottom = previousSentinelHead;
                previousSentinelHead.Top = sentinelHead;

                previousSentinelHead.Previous.Top = sentinelTail;
                sentinelTail.Bottom = previousSentinelHead.Previous;

                sentinelHead.Top = firstHead; // iter
                sentinelTail.Top = firstHead;
            }

            this.sentinelHead = sentinelHead;

            Level++;
        }

        private void RemoveLastLevel()
        {
            if (--Level == 0)
            {
                firstHead = null;
                sentinelHead = null;
                return;
            }

            sentinelHead.Bottom.Top = null;
            sentinelHead.Next.Bottom.Top = null;
            sentinelHead = sentinelHead.Bottom;
        }

        private bool IsSentinel(ISkipListElement element)
        {
            return element is SentinelSkipListElement;
        }

        private bool IsSentinel(ref ISkipListElement element)
        {
            return element is SentinelSkipListElement;
        }

        public IEnumerable<int> GetLevels()
        {
            int[] levels = new int[Level];
            Queue<ISkipListElement> queue = new Queue<ISkipListElement>();
            ISkipListElement m = sentinelHead;

            while (m is not null)
            {
                queue.Enqueue(m.Next);
                m = m.Bottom;
            }

            for (int i = Level - 1; i >= 0; i--)
            {
                int j = 0;
                m = queue.Dequeue();

                while (!IsSentinel(ref m))
                {
                    j++;
                    m = m.Next;
                }

                levels[i] = j;
            }

            return levels;
        }
    }
}