using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using CustomObjects;
using CustomEnums;
using System;

public class DisplayController : MonoBehaviour
{

    TextMeshProUGUI _textDisplay;
    string _textToDisplay;
    string[] _textBuffers = new string[2];
    int strPos = 0;
    int tick = 0;
    bool alive = true;

    void Start()
    {
        _textDisplay = gameObject.GetComponentInChildren<TextMeshProUGUI>();

        _textBuffers[0] = "";
        _textBuffers[1] = "_";
        _textToDisplay = _textBuffers[0];

        var cursorCoroutine = Cursor(0.6f);
        StartCoroutine(cursorCoroutine);
    }

    void Update()
    {
        if (NeedsUpdate())
            _textToDisplay = _textBuffers[tick % 2];
        _textDisplay.text = _textToDisplay;
    }

    bool NeedsUpdate()
    {
        var tbzClean = _textToDisplay.Equals(_textBuffers[0]);
        var tboClean = _textToDisplay.Equals(_textBuffers[1]);
        return !(tbzClean || tboClean);
    }

    public void AddText(string text)
    {
        _textBuffers[0] = _textBuffers[0].Insert(strPos, text);
        _textBuffers[1] = _textBuffers[1].Insert(strPos, text);
        strPos += text.Length;
    }

    public void RemoveText(Commands command)
    {
        if (command == Commands.Backspace)
        {
            if (strPos == 0) return;
            _textBuffers[0] = _textBuffers[0].Remove(strPos-1);
            _textBuffers[1] = _textBuffers[1].Substring(0, strPos-1) + "_";
            strPos--;
        }
        else if (command == Commands.Clear)
        {
            _textBuffers[0] = string.Empty;
            _textBuffers[1] = "_";
            strPos = 0;
        }

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

    private IEnumerator Cursor(float waitTime)
    {        
        while(alive)
        {
            yield return new WaitForSeconds(waitTime);
            _textToDisplay = _textBuffers[tick % 2];
            tick++;
        }
    }

    private void OnDestroy()
    {
        alive = false;
    }
}
