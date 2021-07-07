using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Heaps.Exceptions
{
    public class HeapEmptyException : Exception
    {
        public HeapEmptyException() : base("Heap is empty!")
        {

        }
    }
}