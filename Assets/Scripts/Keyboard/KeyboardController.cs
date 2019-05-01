using System;
using System.Collections.Generic;
using UnityEngine;
using DSR.Keyboard.Interfaces;

namespace DSR.Keyboard
{
    public class KeyboardController : MonoBehaviour, IKeyboardController
    {
        private Queue<string> _keyQueue;
        private bool _shifted;
        // Interpreter

        void Start()
        {
            _keyQueue = new Queue<string>();
            _shifted = false;
        }

        void Update()
        {
            if (_keyQueue.Count == 0) return;
            var keyVal = _keyQueue.Dequeue();
            // TODO something with the key
            Debug.Log(keyVal);
        }

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
