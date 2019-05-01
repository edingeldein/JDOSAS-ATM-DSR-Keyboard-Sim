using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DSR.Keyboard.Interfaces;

namespace DSR.Keyboard.Keys
{
    [RequireComponent(typeof(Toggle))]
    public class ShiftKey : MonoBehaviour, IToggleable
    {
        public KeyboardController keyboardController;

        private Toggle _toggle;

        private void Start()
        {
            if (keyboardController == null)
                throw new MissingComponentException($"Button {gameObject.name} is missing reference to keyboard controller component.");

            _toggle = GetComponent<Toggle>();
            AddListener(OnToggle);
        }

        public void AddListener(UnityAction<bool> toggleHandler)
        {
            _toggle.onValueChanged.AddListener(toggleHandler);
        }

        public void OnToggle(bool value)
        {
            keyboardController.SetShift(value);
        }

    }
}
