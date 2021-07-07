using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Trees.BinaryTrees
{
    public class RedBlackTreeContent<T> : TreeContent<T>, IRedBlackTreeContent<T>
        where T : IComparable<T>
    {
        public bool IsBlack { get; set; }

        public RedBlackTreeContent()
        {

        }

        public RedBlackTreeContent(T content) : base(content)
        {

        }
    }
}