using UnityEngine;
using UnityEngine.UI;
using Controllers;

public class ToggleButton : MonoBehaviour
{
    Toggle _toggleButton;
    KeyboardController _keyboardController;

    ColorBlock toggledColor;
    ColorBlock untoggledColor;

    void Start()
    {
        _toggleButton = GameObject.Find(gameObject.name).GetComponent<Toggle>();
        _keyboardController = GameObject.FindGameObjectWithTag("KeyboardController").GetComponent<KeyboardController>();

        InitColors();

        _toggleButton.onValueChanged.AddListener(OnToggle);
    }

    public void OnToggle(bool value)
    {
        ColorBlock colors = _toggleButton.colors;
        colors.normalColor = Color.white;
        _toggleButton.colors = (value) ? untoggledColor : toggledColor;
        _keyboardController.ShiftEnabled = value;
    }

    private void InitColors()
    {
        toggledColor = _toggleButton.colors;
        untoggledColor = _toggleButton.colors;
        toggledColor.normalColor = Color.white;
        untoggledColor.normalColor = Color.gray;
    }
}
