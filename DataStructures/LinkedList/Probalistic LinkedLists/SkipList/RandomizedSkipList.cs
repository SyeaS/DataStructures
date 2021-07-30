using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.LinkedList
{
    public delegate int GenerateRandom();

    public class RandomizedSkipList<T> : SkipList<T>
        where T : IComparable<T>
    {
        public RandomizedSkipList()
        {

        }

        public RandomizedSkipList(IEnumerable<T> content) : base()
        {
            base.CreateFromIEnumerable(ref content);
        }

        public override void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public override int IndexOf(T content)
        {
            throw new NotImplementedException();
        }
    }
}