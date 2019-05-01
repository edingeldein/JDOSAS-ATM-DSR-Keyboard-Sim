using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DSR.Keyboard.Interfaces;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace DSR.Keyboard.Keys
{
    [RequireComponent(typeof(Button))]
    public class BackspaceKey : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public IKeyboardController keyboardController;

        private Button _button;
        private bool _buttonDown;
        private const string _backspaceValue = "Backspace";

        void Start()
        {
            if (keyboardController == null)
                throw new MissingComponentException($"Button {gameObject.name} is missing keyboard controller component.");
            _button = GetComponent<Button>();

            _buttonDown = false;
        }

        public void AddListener(UnityAction clickHandler)
        {
            _button.onClick.AddListener(clickHandler);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("PointerDown");
            keyboardController.QueueKeypress(_backspaceValue);
            StartCoroutine("BackspaceHoldHandler");
            _buttonDown = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Debug.Log("PointerUp");
            _buttonDown = false;
        }

        private IEnumerator BackspaceHoldHandler()
        {
            yield return new WaitForSeconds(.4f);
            while(_buttonDown)
            {
                keyboardController.QueueKeypress(_backspaceValue);
                yield return new WaitForSeconds(.1f);
            }
        }
    }
}
