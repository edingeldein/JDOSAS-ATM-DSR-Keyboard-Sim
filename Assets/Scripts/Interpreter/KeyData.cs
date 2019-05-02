using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DSR.Interpreter.Enums;
using UnityEngine;

namespace DSR.Interpreter
{
    public class KeyData
    {
        public string KeyVal { get; private set; }
        public KeyType KeyType { get; private set; }

        public KeyData(string fromSerial)
        {
            if (fromSerial.Contains("Value"))
                KeyType = KeyType.Value;             
            else
                KeyType = KeyType.Command;

            KeyVal = GetValue(fromSerial);
        }

        public KeyData(string keyVal, KeyType keyType)
        {
            KeyVal = keyVal;
            KeyType = KeyType;
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
