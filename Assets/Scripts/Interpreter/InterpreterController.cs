using System.Collections.Generic;
using UnityEngine;
using DSR.Interpreter.Interfaces;
using DSR.Interpreter.Enums;
using DSR.LineManager.Interfaces;
using DSR.LineManager;

namespace DSR.Interpreter
{
    public class InterpreterController : MonoBehaviour, IInterpreterController
    {
        private Translator _translator;
        private Queue<KeyData> _keyQueue;

        [SerializeField] private LineManagerController _lineManagerController;

        private void Start()
        {
            _translator = new Translator();
            _keyQueue = new Queue<KeyData>();
        }

        // Command switch
        private void Update()
        {
            while(_keyQueue.Count > 0)
            {
                var keyData = _keyQueue.Dequeue();
                if (keyData.GetType() == typeof(KeyValue))
                    _lineManagerController.KeyValueInput(keyData as KeyValue);
                else
                    _lineManagerController.KeyCommandInput(keyData as KeyCommand);
            }
        }

        public void Interpret(string key)
        {
            var keyData = _translator.Translate(key);
            _keyQueue.Enqueue(keyData);
        }
    }
}
