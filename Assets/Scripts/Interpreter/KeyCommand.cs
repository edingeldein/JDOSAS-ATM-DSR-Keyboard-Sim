using System;
using System.Text.RegularExpressions;
using DSR.Enums;

namespace DSR.Interpreter
{
    public class KeyCommand : KeyData
    {
        public CommandType Command { get; private set; }

        public KeyCommand(KeyType keyType, CommandType cmdType)
        {
            Command = cmdType;
        }

    }
}
