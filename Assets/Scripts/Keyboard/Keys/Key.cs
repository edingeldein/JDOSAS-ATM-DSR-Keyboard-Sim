using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DSR.Exceptions;
using DSR.Keyboard.Interfaces;
using DSR.Keyboard.Enums;

namespace DSR.Keyboard.Keys
{
    [RequireComponent(typeof(Button))]
    public class Key: MonoBehaviour, IClickable
    {
        public string value;
        public KeyboardController keyboardController;

        private Button _button;
        
        private void Start()
        {
            if (string.IsNullOrEmpty(value)) throw new NoButtonValueException($"Button {gameObject.name} has no assigned value.");
            if (keyboardController == null) throw new MissingComponentException($"Button {gameObject.name} missing reference to keyboard controller.");

            _button = GetComponent<Button>();
            AddListener(OnClick);
        }

        public void AddListener(UnityAction clickHandler)
        {
            _button.onClick.AddListener(clickHandler);
        }

        public void OnClick()
        {
            keyboardController.QueueKeypress(value);
        }
    }
}
