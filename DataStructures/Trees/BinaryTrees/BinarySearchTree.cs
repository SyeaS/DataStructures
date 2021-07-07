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

        public override T Pop(T content)
        {
            return InternalRemove(ref content).TreeContent.Content;
        }

        public override T PopMin()
        {
            TreeElement minimum = Minimum(root);

            RemoveTreeElement(ref minimum);

            return minimum.TreeContent.Content;
        }

        public override T PopMax()
        {
            TreeElement maximum = Maximum(root);

            RemoveTreeElement(ref maximum);

            return maximum.TreeContent.Content;
        }
    }
}