using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Trees.BinaryTrees
{
    public interface ITreeContent<T>
        where T : IComparable<T>
    {
        T Content { get; set; }
    }
}