using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSR.Exceptions
{
    public class NoEnumException : Exception
    {
        public NoEnumException()
        {
        }

        public NoEnumException(string message)
            : base(message)
        {
        }

        public NoEnumException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
