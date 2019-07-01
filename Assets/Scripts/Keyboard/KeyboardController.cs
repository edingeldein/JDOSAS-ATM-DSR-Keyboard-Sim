using System;
using System.Collections.Generic;
using UnityEngine;
using DSR.Keyboard.Interfaces;
using DSR.Interpreter;
using DSR.Interpreter.Interfaces;

namespace DSR.Keyboard
{
    public class OldKeyboardController : MonoBehaviour, IKeyboardController
    {        
        public bool Queued { get { return _keyQueue.Count > 0; } }
        public string Dequeue { get { return _keyQueue.Dequeue(); } }
        private Queue<string> _keyQueue;
        private bool _shifted;

        [SerializeField] private InterpreterController _interpreterController;

        void Start()
        {
            _keyQueue = new Queue<string>();
            _shifted = false;
        }

        // void Update()
        // {
        //     while(_keyQueue.Count > 0)
        //     {
        //         var keyVal = _keyQueue.Dequeue();
        //         _interpreterController.Interpret(keyVal);
        //     }
        // }

        public void QueueKeypress(string keypress)
        {
            keypress.Trim();
            _keyQueue.Enqueue(keypress);
        }

        public void SetShift(bool shift)
        {
            _shifted = shift;
        }

        public bool GetShift()
        {
            return _shifted;
        }
    }
}
