using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Trees.BinaryTrees
{
    public class TreeContent<T> : ITreeContent<T>
        where T : IComparable<T>
    {
        public T Content { get; set; }

        public TreeContent()
        {

        }

        public TreeContent(T content)
        {
            Content = content;
        }
    }
}