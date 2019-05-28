using System;

namespace DSR.Exceptions
{
    public class NoButtonValueException : Exception
    {

        public NoButtonValueException()
        { 
        }

        public NoButtonValueException(string message) 
            : base(message)
        {
        }

        public NoButtonValueException(string message, Exception inner)
            : base(message,inner)
        {
        }

    }
}
