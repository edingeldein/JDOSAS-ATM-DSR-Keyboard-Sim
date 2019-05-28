using System;
using System.Text.RegularExpressions;
using DSR.Enums;
using DSR.Exceptions;
using UnityEngine;

namespace DSR.Objects
{
    public class KeyData
    {
        public KeyType Key { get; private set; }
        public CommandType Command { get; private set; }
        public ActionType Action { get; private set; }
        public string Value { get; private set; }

        public KeyData(string key, string serialized)
        {
            if (serialized.Contains("Command"))
            {
                Key = KeyType.Command;
                Action = ActionType.None;
                Command = GetCommand(key);
            }
            else if (serialized.Contains("Action"))
            {
                Key = KeyType.Action;
                Command = CommandType.None;
                Action = GetAction(key);
            }
            else
            {
                Key = KeyType.Value;
                Command = CommandType.None;
                Action = ActionType.None;
            }
            Value = GetValue(serialized);
        }

        private string GetValue(string toVal)
        {
            var rx = new Regex("\'([^\']*)\'");
            var match = rx.Match(toVal);
            var ret = match.Value.Trim('\'');
            return ret;
        }

        private CommandType GetCommand(string toCommand)
        {
            if (!Enum.TryParse<CommandType>(toCommand, out var result))
                throw new NoEnumException($"No CommandType enum value associated with the string {toCommand}");
            return result;
        }

        private ActionType GetAction(string toAction)
        {
            if (!Enum.TryParse<ActionType>(toAction, out var result))
                throw new NoEnumException($"No ActionType enum value associated with the string {toAction}");
            return result;
        }

    }
}
