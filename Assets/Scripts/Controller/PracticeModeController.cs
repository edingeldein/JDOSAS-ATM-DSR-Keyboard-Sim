using System;
using UnityEngine;
using DSR.Objects;
using DSR.Enums;

namespace DSR.Controller
{
    public class PracticeModeController : ModeController
    {

        public PracticeModeController(GameObject mainControllerObj) : base(mainControllerObj)
        {}

        public override void Loop()
        {
            while(_keyboardController.Queued)
            {
                var keypress = _keyboardController.Dequeue;
                var keydata = _interpreterController.Interpret(keypress);
                
                Line submittedLine;
                if(keydata.Key == KeyType.Command)
                    submittedLine = _lineManagerController.CommandInput(keydata);
                else if(keydata.Key == KeyType.Action)
                    _lineManagerController.ActionInput(keydata);
                else
                    _lineManagerController.KeyInput(keydata);
            }
        }
    }
}