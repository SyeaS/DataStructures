using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Trees.BinaryTrees
{
    public class DuplicateTreeElementException : Exception
    {
        public DuplicateTreeElementException() : base("Binary Search Tree cannot contain duplicate elements!")
        {

        }

        public DuplicateTreeElementException(object obj)
            : base($"Binary Search Tree cannot contain multiple elements! (Inserted value: { obj })")
        {

        }

        public DuplicateTreeElementException(object insObj, object obj)
            : base($"Binary Search Tree cannot contain multiple elements! (Inserted value: { insObj } collided with { obj })")
        {

        }
    }
}