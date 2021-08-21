using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.LinkedList
{
    public delegate int GenerateRandom();

    public class SkipList<T> : LinkedList<T>
        where T : IComparable<T>
    {
        protected class SkipListElement : ISkipListElement
        {
            public ISkipListElement[] Next { get; set; }
            public T Content { get; set; }

            protected SkipListElement(int size) : this(ref size)
            {

            }

            protected SkipListElement(ref int size)
            {
                Next = new SkipListElement[size];
            }

            public SkipListElement(ref T content, int size) : this(ref size)
            {
                Content = content;
            }

            public SkipListElement(ref T content, ref int size) : this(ref size)
            {
                Content = content;
            }
        }

        protected class SentinelSkipListElement : SkipListElement
        {
            public SentinelSkipListElement(int size) : base(ref size)
            {

            }
        }

        protected interface ISkipListElement
        {
            ISkipListElement[] Next { get; set; }
            T Content { get; set; }
        }

        protected interface ISkipListElement<TSkipListElement>
        {
            TSkipListElement[] Next { get; set; }
            T Content { get; set; }
        }

        protected const int MAX_LEVEL = 32;
        private const float PROBABILITY = 0.5f;
        private const int MAX_RANDOM = int.MaxValue;

        protected int Level { get; set; } = 1;
        private ISkipListElement head;

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

        protected int RandomLevel()
        {
            int lvl = 0;

            while ((((float)GetRandom()) / MAX_RANDOM) < PROBABILITY &&
                lvl < Level)
            {
                ++lvl;
            }

            if (lvl == Level)
            {
                Level++;
            }

            return lvl;
        }

        private void Initialize()
        {
            head = new SentinelSkipListElement(MAX_LEVEL);
        }

        protected override void InternalAdd(ref T element)
        {
            int level = RandomLevel();
            SkipListElement newElement = new SkipListElement(ref element, level + 1);

            if (head is null)
            {
                Initialize();
            }

            ISkipListElement m = head;

            for (int i = Level - 1; i >= 0; i--)
            {
                for (; m.Next[i] is not null; m = m.Next[i])
                {
                    if (element.CompareTo(m.Next[i].Content) < 0)
                    {
                        break;
                    }
                }

                if (level >= i)
                {
                    newElement.Next[i] = m.Next[i];
                    m.Next[i] = newElement;
                }
            }
        }

        protected override void InternalRemove(ref T element)
        {
            ISkipListElement m = head;
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

        public override void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public override T Search(T content)
        {
            return this.Search(ref content).Next[0].Content;
        }

        private ISkipListElement Search(ref T content)
        {
            ISkipListElement m = head;
            int compare;

            for (int i = Level - 1; i >= 0; i--)
            {
                for (; m.Next[i] is not null; m = m.Next[i])
                {
                    compare = content.CompareTo(m.Next[i].Content);

                    if (compare < 0)
                    {
                        break;
                    }
                    else if (compare == 0)
                    {
                        return m;
                    }
                }
            }

            throw new ListElementNotFoundException();
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

        public override int IndexOf(T content)
        {
            throw new NotImplementedException();
        }

        public T Maximum()
        {
            ISkipListElement max = head.Next[Level - 1];

            for (int i = Level - 1; i != 0;)
            {
                while (max.Next[i] is not null)
                {
                    max = max.Next[i];
                }

                while (max.Next[i] is null && i != 0)
                {
                    i--;
                }

                max = max.Next[i];
            }

            return max.Next[0] is null ? max.Content : max.Next[0].Content;
        }

        public T PopMax()
        {
            T max = Maximum();
            this.InternalRemove(ref max);
            return max;
        }

        public T Minimum()
        {
            ISkipListElement min = head.Next[0];

            return min.Content;
        }

        public T PopMin()
        {
            T min = Minimum();
            this.InternalRemove(ref min);
            return min;
        }

        public IEnumerable<int> GetLevels()
        {
            int[] levels = new int[Level];

            for (int i = 0; i < Level; i++)
            {
                levels[i] = 1;
            }

            ISkipListElement m;

            for (int i = Level - 1; i >= 0; i--)
            {
                m = head.Next[i];

                while (m.Next[i] is not null)
                {
                    m = m.Next[i];
                    levels[i]++;
                }
            }

            return levels;
        }

        public override IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }

        struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
        {
            private ISkipListElement current;
            public T Current => current.Content;
            object IEnumerator.Current => Current as object;
            private readonly ISkipListElement head;

            public Enumerator(SkipList<T> skipList)
            {
                //SentinelSkipListElement sentinelHead = new SentinelSkipListElement(1);
                //sentinelHead.Next = skipList.head.Next;
                this.head = skipList.head;
                current = head;
            }

            public bool MoveNext()
            {
                current = current.Next[0];

                return current is not null;
            }

            public void Reset()
            {
                current = head;
            }

            public void Dispose()
            {
                current = null;
            }
        }
    }
}