using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresAndAlgorithms.Heaps.Exceptions
{
    internal class NoSmallerElementException : Exception
    {
        public NoSmallerElementException() : base("No smaller element was found!")
        {

        }
    }
}