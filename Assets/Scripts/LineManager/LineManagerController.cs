using UnityEngine;
using DSR.LineManager.Interfaces;
using DSR.Interpreter;
using DSR.Enums;
using DSR.Console;
using DSR.Objects;
using DSR.DsrLogic;
using DSR.DsrLogic.Utilities;

namespace DSR.LineManager
{
    public class LineManagerController : MonoBehaviour, ILineManagerController
    {
        public Mode Mode;
        [SerializeField] private ConsoleController _consoleController;
        [SerializeField] private DsrService _dsrService;
        [SerializeField] private Line _currentLine;
        private bool _newVal;
        private bool _validationState;

        private void Start()
        {
            _currentLine = new Line();
            _newVal = false;
            _validationState = false;
        }

        private void Update()
        {
            if (_newVal)
            {
                // TODO post current line to console
                _consoleController.UpdateLine(_currentLine.Text, _currentLine.CursorText);
                _newVal = false;
            }
        }

        public void KeyInput(KeyData keyData)
        {
            if (keyData.Key == KeyType.Command) KeyCommandInput(keyData);
            else if (keyData.Key == KeyType.Action) KeyActionInput(keyData);
            KeyValueInput(keyData);
        }

        public Line CommandInput(KeyData keyData)
        {
            
            return _currentLine;
        }

        public void ActionInput(KeyData actionData)
        {

        }

        public void ValueInput(KeyData keyData)
        {

        }

        private void KeyValueInput(KeyData keyValue)
        {
            if (string.IsNullOrEmpty(keyValue.Value)) return;
            _currentLine.AddChar(keyValue.Value);
            _newVal = true;
        }

        private void KeyCommandInput(KeyData keyCommand)
        {
            var command = keyCommand.Command;

            switch (command)
            {
                case CommandType.Backspace:
                    _currentLine.Backspace();
                    break;
                case CommandType.Clear:
                    _currentLine.ClearLine();
                    break;
                case CommandType.Enter:
                    //if (Mode == Mode.Learn) ValidateLine();
                    if(true) ValidateLine();
                    else ProcessLine();
                    break;
                case CommandType.Up:
                    break;
                case CommandType.Down:
                    break;
                case CommandType.Left:
                    break;
                case CommandType.Right:
                    break;
            }

            _newVal = true;
        }

        private void KeyActionInput(KeyData keyAction)
        {
            _currentLine.ClearLine();
            _currentLine.StartAction(keyAction.Action);
        }

        private void HandleValidationState(KeyData key)
        {
            _validationState = false;
            var command = key.Command;
            if (command != CommandType.Enter) return;

            _consoleController.ClearLines();
        }

        //TODO Execute if in 'simulate' mode
        private void ProcessLine()
        {
            //TODO Extract command
            var command = _dsrService.GetCommand(_currentLine);
            //TODO Interpret command
            Debug.Log($"Command: {command}");
            //TODO Perform command
            
        }

        //TODO Execute if in 'practice' mode
        private void ValidateLine()
        {
            var validatedAction = _dsrService.Validate(_currentLine);
            _consoleController.DisplayValidation(validatedAction);            
        }
    }
}
