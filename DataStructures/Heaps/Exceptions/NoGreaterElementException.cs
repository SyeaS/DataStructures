using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Heaps.Exceptions
{
    internal class NoGreaterElementException : Exception
    {
        public NoGreaterElementException() : base("No greater element was found!")
        {

        }
    }
}