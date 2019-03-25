using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class KeyBehavior : MonoBehaviour
{
    public string valueUnshifted;
    public string valueShifted;
    public bool shiftable;

    private Button _parentButton;
    private KeyboardController _keyboardController;

    void Start()
    {
        _parentButton = GameObject.Find(gameObject.name).GetComponent<Button>();
        if (_parentButton is null) throw new MissingComponentException($"No Button component on {gameObject.name}");

        _keyboardController = GameObject.FindGameObjectWithTag("KeyboardController").GetComponent<KeyboardController>();
        if (_keyboardController is null) throw new MissingComponentException($"Cant find KeyboardController in scene...");

        _parentButton.onClick.AddListener(TaskOnClick);
    }
    
    void TaskOnClick()
    {
        if (_keyboardController.ShiftClicked)
            _keyboardController.KeypressHandler(valueShifted);
        else
            _keyboardController.KeypressHandler(valueUnshifted);
    }
}
