using System.Collections.Generic;
using UnityEngine;
using DSR.Interpreter.Interfaces;
using DSR.LineManager;
using DSR.Objects;

namespace DSR.Interpreter
{
    public class InterpreterController : MonoBehaviour, IInterpreterController
    {
        public KeyData Interpret(string key) => _translator.Translate(key);
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
                _lineManagerController.KeyInput(keyData);
            }
        }
    }
}
