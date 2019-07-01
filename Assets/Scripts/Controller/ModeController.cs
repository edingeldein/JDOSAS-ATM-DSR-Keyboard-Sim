using System;
using UnityEngine;
using DSR.Keyboard.Interfaces;
using DSR.Interpreter.Interfaces;
using DSR.LineManager.Interfaces;
using DSR.Console.Interfaces;

namespace DSR.Controller
{
    public abstract class ModeController 
    {
        protected IKeyboardController _keyboardController;
        protected IInterpreterController _interpreterController;
        protected ILineManagerController _lineManagerController;
        protected IConsoleController _consoleController;

        public ModeController(GameObject mainControllerObj)
        {
            _keyboardController = mainControllerObj.GetComponent<IKeyboardController>();
            _interpreterController = mainControllerObj.GetComponent<IInterpreterController>();
            _lineManagerController = mainControllerObj.GetComponent<ILineManagerController>();
            _consoleController = mainControllerObj.GetComponent<IConsoleController>();
        }

        public abstract void Loop();
    }
}