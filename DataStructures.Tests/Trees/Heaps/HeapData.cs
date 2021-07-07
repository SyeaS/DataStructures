using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Tests.Trees.Heaps
{
    public class HeapData : IComparable<HeapData>
    {
        public int Number { get; }

        public HeapData(int number)
        {
            Number = number;
        }

        public static IEnumerable<HeapData> CreateFromValues(IEnumerable<int> values)
        {
            List<HeapData> datas = new List<HeapData>();

            foreach (int item in values)
            {
                datas.Add(new HeapData(item));
            }

            return datas as IEnumerable<HeapData>;
        }

        public int CompareTo(HeapData other)
        {
            if (other != null)
            {
                return this.Number.CompareTo(other.Number);
            }

            return Number;
        }
    }
}