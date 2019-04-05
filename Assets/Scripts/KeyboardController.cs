using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controllers;
using CustomObjects;
using CustomEnums;

public class KeyboardController : MonoBehaviour
{

    public bool ShiftEnabled = false;
    private Queue<KeyPressData> keyQueue;

    private ConsoleController _consoleController;

    void Start()
    {
        keyQueue = new Queue<KeyPressData>();
        _consoleController = GameObject.FindGameObjectWithTag("ConsoleController").GetComponent<ConsoleController>();
        if (_consoleController == null)
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
            _consoleController.AddText(data.Value);
        else if (data.Command == Commands.Tab)
            _consoleController.AddText("    ");
        else if (data.Command == Commands.NewLine)
        {
            _consoleController.SubmitText();
            _consoleController.AddText("\n");
        }            
        else if (data.Command == Commands.Backspace || data.Command == Commands.Clear)
            _consoleController.RemoveText(data.Command);
        else
            _consoleController.ExecuteCommand(data.Command);
    }

    public void KeypressHandler(KeyPressData keyPressed)
    {
        keyQueue.Enqueue(keyPressed);
    }
}
