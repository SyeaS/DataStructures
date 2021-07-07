using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.LinkedList.Exceptions
{
    public class EmptyLinkedListException : Exception
    {
        public EmptyLinkedListException() : base("The linked list is empty.")
        {

        }
    }
}