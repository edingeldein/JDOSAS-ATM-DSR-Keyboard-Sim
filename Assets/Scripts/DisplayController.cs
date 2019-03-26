using UnityEngine.UI;
using UnityEngine;
using TMPro;
using CustomObjects;
using CustomEnums;

public class DisplayController : MonoBehaviour
{

    private TextMeshProUGUI _textDisplay;
    private string _textToDisplay = "";

    void Start()
    {
        _textDisplay = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        Debug.Log(_textDisplay.text);
    }

    void Update()
    {
        _textDisplay.text = _textToDisplay;
    }

    public void AddText(string text)
    {
        _textToDisplay += text;
    }

    public void RemoveText(Commands command)
    {
        if (command == Commands.Backspace)
        {
            if (_textToDisplay.Length == 0) return;
            _textToDisplay = _textToDisplay.Substring(0, _textToDisplay.Length - 1);
        }
        else if (command == Commands.Clear)
            _textToDisplay = string.Empty;
    }

    public void Navigate(KeyPressData data)
    {

    }

    public void ExecuteCommand(Commands command)
    {
        var commandText = command.ToString();
        Debug.Log(command);
        AddText($"{commandText} ");
    }
}
