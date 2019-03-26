using UnityEngine.UI;
using UnityEngine;
using CustomEnums;
using CustomObjects;

public class KeyBehavior : MonoBehaviour
{
    public KeyType keyType = KeyType.Value;
    public bool shiftable;
    public string valueUnshifted;
    public string valueShifted;
    public Commands command;

    private Button _parentButton;
    private KeyboardController _keyboardController;

    void Start()
    {
        _parentButton = GameObject.Find(gameObject.name).GetComponent<Button>();
        if (_parentButton is null) throw new MissingComponentException($"No Button component on {gameObject.name}");

        var keyboardControllers = GameObject.FindGameObjectsWithTag("KeyboardController");
        if (keyboardControllers.Length > 1)
            throw new UnityException("More than one keyboard controller found. Make sure there is only one GameObject with the tag \"KeyboardController\"");

        _keyboardController = keyboardControllers[0].GetComponent<KeyboardController>();

        if (_keyboardController is null) throw new MissingComponentException($"Cant find KeyboardController in scene...");

        _parentButton.onClick.AddListener(TaskOnClick);
    }
    
    void TaskOnClick()
    {
        string keyValue;
        if (shiftable)
            keyValue = (_keyboardController.ShiftEnabled) ? valueShifted : valueUnshifted;
        else
            keyValue = valueUnshifted;

        _keyboardController.KeypressHandler(new KeyPressData(keyType, keyValue, command));
    }
}