using CustomEnums;

namespace CustomObjects
{
    public class KeyPressData
    {
        public KeyType KeyType { get; }
        public string Value { get; }
        public Commands Command { get; }

        public KeyPressData(KeyType type, string value, Commands command)
        {
            KeyType = type;
            Value = value;
            Command = command;
        }
    }
}
