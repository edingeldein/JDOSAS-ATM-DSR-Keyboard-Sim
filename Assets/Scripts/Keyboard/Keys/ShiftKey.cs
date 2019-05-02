using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DSR.Keyboard.Interfaces;

namespace DSR.Keyboard.Keys
{
    [RequireComponent(typeof(Toggle))]
    public class ShiftKey : MonoBehaviour, IToggleable
    {
        private IKeyboardController _keyboardController;
        private Toggle _toggle;

        private void Start()
        {
            _keyboardController = GameObject.Find("Keyboard").GetComponent<IKeyboardController>();
            _toggle = GetComponent<Toggle>();
            AddListener(OnToggle);
        }

        public void AddListener(UnityAction<bool> toggleHandler)
        {
            _toggle.onValueChanged.AddListener(toggleHandler);
        }

        public void OnToggle(bool value)
        {
            _keyboardController.SetShift(value);
        }

    }
}
