using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStructures.LinkedList;

namespace DataStructures.Trees.BinaryTrees
{
    public class BinarySearchTree<T> : AbstractBinarySearchTree<T, TreeContent<T>>
        where T : IComparable<T>
    {
        public BinarySearchTree()
        {

        }

        public BinarySearchTree(IEnumerable<T> content) : base(content)
        {

        }

        protected override T InternalPopMin()
        {
            TreeElement minimum = Minimum(root);

            RemoveTreeElement(ref minimum);

            return minimum.TreeContent.Content;
        }

        protected override T InternalPopMax()
        {
            TreeElement maximum = Maximum(root);

            RemoveTreeElement(ref maximum);

            return maximum.TreeContent.Content;
        }
    }
}