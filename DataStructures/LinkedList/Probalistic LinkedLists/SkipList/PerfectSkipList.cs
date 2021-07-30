using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.LinkedList
{
    public class PerfectSkipList<T> : SkipList<T>
        where T : IComparable<T>
    {
        public PerfectSkipList()
        {

        }

        public PerfectSkipList(IEnumerable<T> content)
        {
            base.CreateFromIEnumerable(ref content);
        }

        public override int IndexOf(T content)
        {
            throw new NotImplementedException();
        }

        public override void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        protected override void InternalAdd(ref T element)
        {
            throw new NotImplementedException();
        }

        protected override void InternalRemove(ref T element)
        {
            throw new NotImplementedException();
        }
    }
}