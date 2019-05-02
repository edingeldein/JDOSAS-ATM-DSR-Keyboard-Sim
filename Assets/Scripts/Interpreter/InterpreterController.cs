using System.Collections.Generic;
using UnityEngine;
using DSR.Interpreter.Interfaces;
using DSR.Interpreter.Enums;
using DSR.LineManager.Interfaces;

namespace DSR.Interpreter
{
    public class InterpreterController : MonoBehaviour, IInterpreterController
    {
        private Translator _translator;
        private Queue<KeyData> _keyQueue;
        private ILineManagerController _lineManagerController;
        private bool bam = false;

        private void Start()
        {
            _translator = new Translator();
            _keyQueue = new Queue<KeyData>();
            _lineManagerController = GameObject.Find("LineManager").GetComponent<ILineManagerController>();
        }

        // Command switch
        private void Update()
        {
            while(_keyQueue.Count > 0)
            {
                var keyData = _keyQueue.Dequeue();
                Debug.Log($"keyData({keyData.KeyType})");
                if (keyData.KeyType == KeyType.Value)
                    _lineManagerController.KeyValueInput(keyData);
            }
        }

        public void Interpret(string key)
        {
            var keyData = _translator.Translate(key);

            if (keyData.KeyType == KeyType.Command) _keyQueue.Enqueue(keyData);
            else _keyQueue.Enqueue(keyData);
            bam = true;
        }
    }
}
