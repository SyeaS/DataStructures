using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Trees.BinaryTrees
{
    public interface ITraversableTree<T> where T : IComparable<T>
    {
        public delegate void TraversaryDelegate(T content);

        public IEnumerable<T> InOrder();
        public IEnumerable<T> PreOrder();
        public IEnumerable<T> PostOrder();
        public IEnumerable<T> BreadthFirst();
        public IEnumerable<T> DepthFirst();

        public void InOrder(TraversaryDelegate method);
        public void PreOrder(TraversaryDelegate method);
        public void PostOrder(TraversaryDelegate method);
        public void BreadthFirst(TraversaryDelegate method);
        public void DepthFirst(TraversaryDelegate method);
    }
}