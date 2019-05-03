using System;
using System.Text.RegularExpressions;
using DSR.Interpreter.Enums;

namespace DSR.Interpreter
{
    public class KeyValue : KeyData
    {
        public string Value { get; private set; }

        public KeyValue(KeyType type, string val)
        {
            Value = val;
        }
    }
}
