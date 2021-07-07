using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Trees.BinaryTrees
{
    public interface IRedBlackTreeContent<T> : ITreeContent<T>
        where T : IComparable<T>
    {
        bool IsBlack { get; set; }
    }
}