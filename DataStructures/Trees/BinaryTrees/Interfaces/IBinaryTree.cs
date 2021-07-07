using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Trees.BinaryTrees
{
    public interface IBinaryTree<T> : ICollection<T>, ICollection, IDisposable, IEquatable<IBinaryTree<T>>, ITraversableTree<T>
        where T : IComparable<T>
    {

    }
}