using System;
using System.Text.RegularExpressions;
using DSR.Interpreter.Enums;
using DSR.Exceptions;
using UnityEngine;

namespace DSR.Interpreter
{
    public class KeyData
    {
        public KeyType KeyType { get; private set; }

        public static KeyData NewKeyData(string key, string ser)
        {
            var type = (ser.Contains("Value")) ? KeyType.Value : KeyType.Command;
            if (type == KeyType.Value)
                return new KeyValue(type, GetValue(ser));

            try
            {
                return new KeyCommand(KeyType.Command, GetCommand(key));
            }
            catch(Exception ex)
            {
                Debug.LogError(ex.Message);
                return new KeyCommand(type, CommandType.Default);
            }
        }

        private static string GetValue(string toVal)
        {
            var rx = new Regex("\'([^\'])\'");
            var match = rx.Match(toVal);
            var ret = match.Value.Trim('\'');
            return ret;
        }

        private static CommandType GetCommand(string toCommand)
        {
            if (!Enum.TryParse<CommandType>(toCommand, out var result))
                throw new NoEnumException($"No CommandType enum value associated with the string {toCommand}");
            return result;
        }

    }
}
