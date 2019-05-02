using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DSR.Interpreter.Interfaces;
using DSR.Interpreter.Enums;

namespace DSR.Interpreter
{
    public class InterpreterController : MonoBehaviour, IInterpreterController
    {
        private Translator _translator;
        private Queue<KeyData> _consoleQueue;
        private Queue<KeyData> _commandQueue;
        private bool bam = false;

        private void Start()
        {
            _translator = new Translator();
            _consoleQueue = new Queue<KeyData>();
            _commandQueue = new Queue<KeyData>();
        }

        private void Update()
        {
            if(bam)
            {
                Debug.Log($"Console Queue: {_consoleQueue.Count}");
                Debug.Log($"Command Queue: {_commandQueue.Count}");
                bam = false;
            }            
        }

        public void Interpret(string key)
        {
            var keyData = _translator.Translate(key);

            if (keyData.KeyType == KeyType.Command) _commandQueue.Enqueue(keyData);
            else _consoleQueue.Enqueue(keyData);
            bam = true;
        }
    }
}
