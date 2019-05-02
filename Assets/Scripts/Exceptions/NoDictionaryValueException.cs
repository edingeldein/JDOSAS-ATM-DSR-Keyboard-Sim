using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSR.Exceptions
{
    public class MissingDictionaryValueException : Exception
    {
        public MissingDictionaryValueException()
        {
        }

        public MissingDictionaryValueException(string message)
            : base(message)
        {
        }

        public MissingDictionaryValueException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
