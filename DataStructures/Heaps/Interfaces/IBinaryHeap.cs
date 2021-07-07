using DataStructures.Trees.BinaryTrees;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Heaps.Interfaces
{
    interface IBinaryHeap<T> : ICollection<T>, ICollection, IDisposable, IEquatable<IBinaryTree<T>>
        where T : IComparable<T>
    {

    }
}