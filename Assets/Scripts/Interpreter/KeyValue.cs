using System;
using System.Text.RegularExpressions;
using DSR.Interpreter.Enums;

namespace DSR.Interpreter
{
    public class KeyValue : KeyData
    {
        public string Value { get; private set; }

        public KeyValue(KeyType type, string serial)
        {
            Value = GetValue(serial);
        }

        private string GetValue(string toCommand)
        {
            var rx = new Regex("\'([^\'])\'");
            var match = rx.Match(toCommand);
            var ret = match.Value.Trim('\'');
            return ret;
        }
    }
}
