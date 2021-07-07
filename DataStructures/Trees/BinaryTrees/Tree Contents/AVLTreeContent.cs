using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Trees.BinaryTrees
{
    public class AVLTreeContent<T> : TreeContent<T>, IAVLTreeContent<T>
        where T : IComparable<T>
    {
        public uint Height { get; set; }

        public AVLTreeContent()
        {
            Height = 1;
        }
    }
}