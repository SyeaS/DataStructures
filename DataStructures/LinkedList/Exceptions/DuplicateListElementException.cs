using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.LinkedList
{
    public class DuplicateListElementException : Exception
    {
        public DuplicateListElementException() : base("List cannot contain duplicate elements!")
        {

        }
    }
}