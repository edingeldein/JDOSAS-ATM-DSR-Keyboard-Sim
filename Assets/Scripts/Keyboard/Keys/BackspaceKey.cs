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
        [SerializeField] private KeyboardController _keyboardController;

        private Button _button;
        private bool _buttonDown;
        private const string _backspaceValue = "Backspace";

        void Start()
        {
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
            _keyboardController.QueueKeypress(_backspaceValue);
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
                _keyboardController.QueueKeypress(_backspaceValue);
                yield return new WaitForSeconds(.1f);
            }
        }
    }
}
