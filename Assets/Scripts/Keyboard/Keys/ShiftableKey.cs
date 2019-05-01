using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using DSR.Exceptions;
using DSR.Keyboard.Interfaces;
using UnityEngine.Events;

namespace DSR.Keyboard.Keys
{
    [RequireComponent(typeof(Button))]
    class ShiftableKey : MonoBehaviour, IClickable
    {
        public string value;
        public string shiftedValue;
        public KeyboardController keyboardController;

        private Button _button;

        void Start()
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(shiftedValue))
                throw new NoButtonValueException($"Button {gameObject.name} has no assigned value.");
            if (keyboardController == null)
                throw new MissingComponentException($"Button {gameObject.name} is missing keyboard controller component.");

            _button = GetComponent<Button>();
            AddListener(OnClick);
        }

        public void AddListener(UnityAction clickHandler)
        {
            _button.onClick.AddListener(OnClick);
        }

        public void OnClick()
        {
            if (keyboardController.GetShift())
                keyboardController.QueueKeypress(shiftedValue);
            else
                keyboardController.QueueKeypress(value);
        }

    }
}
