using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomObjects;
using CustomEnums;

public class KeyboardController : MonoBehaviour
{

    public bool ShiftEnabled = false;
    private Queue<KeyPressData> keyQueue;

    private DisplayController _displayController;

    void Start()
    {
        keyQueue = new Queue<KeyPressData>();
        _displayController = GameObject.FindGameObjectWithTag("DisplayController").GetComponent<DisplayController>();
        if (_displayController == null)
            throw new MissingComponentException("No game object with tag \"DisplayController\" found...");
    }

    void Update()
    {
        if (keyQueue.Count == 0) return;
        InterpretKeypress(keyQueue.Dequeue());
    }

    void InterpretKeypress(KeyPressData data)
    {
        if (data.KeyType == KeyType.Value)
            _displayController.AddText(data.Value);
        else if (data.Command == Commands.Tab)
            _displayController.AddText("    ");
        else if (data.Command == Commands.NewLine)
            _displayController.AddText("\n");
        else if (data.Command == Commands.Backspace || data.Command == Commands.Clear)
            _displayController.RemoveText(data.Command);
        else
            _displayController.ExecuteCommand(data.Command);
    }

    public void KeypressHandler(KeyPressData keyPressed)
    {
        keyQueue.Enqueue(keyPressed);
    }
}
