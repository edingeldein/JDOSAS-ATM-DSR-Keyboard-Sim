using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DSR.Exceptions;
using DSR.Keyboard.Interfaces;

namespace DSR.Keyboard.Keys
{
    [RequireComponent(typeof(Button))]
    public class Key: MonoBehaviour, IClickable
    {
        public string value;

        [SerializeField] private KeyboardController _keyboardController;
        private Button _button;
        
        private void Start()
        {
            if (string.IsNullOrEmpty(value)) throw new NoButtonValueException($"Button {gameObject.name} has no assigned value.");

            _button = GetComponent<Button>();
            AddListener(OnClick);
        }

        public void AddListener(UnityAction clickHandler)
        {
            _button.onClick.AddListener(clickHandler);
        }

        public void OnClick()
        {
            _keyboardController.QueueKeypress(value);
        }
    }
}
