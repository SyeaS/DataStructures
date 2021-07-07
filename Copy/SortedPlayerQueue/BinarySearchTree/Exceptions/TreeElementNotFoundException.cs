using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortedPlayerQueue
{
    public class TreeElementNotFoundException : Exception
    {
        public TreeElementNotFoundException() : base("The specified tree element wasn't found!")
        {

        }

        public TreeElementNotFoundException(object obj)
            : base($"The specified tree element ({ obj }) wasn't found!")
        {

        }
    }
}