using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Trees.BinaryTrees
{
    public interface IAVLTreeContent<T> : ITreeContent<T>
        where T : IComparable<T>
    {
        uint Height { get; set; }
    }
}