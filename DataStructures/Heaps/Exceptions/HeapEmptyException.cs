using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresAndAlgorithms.Heaps.Exceptions
{
    public class HeapEmptyException : Exception
    {
        public HeapEmptyException() : base("Heap is empty!")
        {

        }
    }
}