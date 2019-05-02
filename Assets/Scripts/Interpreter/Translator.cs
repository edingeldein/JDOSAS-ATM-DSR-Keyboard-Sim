using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DSR.Exceptions;

namespace DSR.Interpreter
{
    public class Translator
    {
        private const string _dictionaryName = "InterpreterDictionary";
        private Dictionary<string, KeyData> _dictionary;

        public Translator()
        {
            LoadDictionary();
        }

        private void LoadDictionary()
        {
            var dictFile = (TextAsset)Resources.Load(_dictionaryName, typeof(TextAsset));
            _dictionary = new Dictionary<string, KeyData>();

            var fileLines = dictFile.text.Split('\n');

            foreach(var line in fileLines)
            {
                var keyval = line.Split('~');
                var key = keyval[0];
                var val = new KeyData(keyval[1]);
                _dictionary.Add(key, val);
            }

        }

        public KeyData Translate(string key)
        {
            var success = _dictionary.TryGetValue(key, out var value);
            if (!success) throw new MissingDictionaryValueException($"No value associated with key '{key}'");
            return value;
        }
    }
}
