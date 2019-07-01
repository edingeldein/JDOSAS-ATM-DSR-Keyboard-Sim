using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSR.LineManager;
using DSR.Console;
using DSR.DsrLogic;
using DSR.Objects;

public class MainController : MonoBehaviour
{

    public KeyboardController Keyboard;
    public InterpreterController Interpreter;
    public LineManagerController LineManager;
    public DsrService Service;
    public ConsoleController Console;

    private Mode _executionMode;

    // Start is called before the first frame update
    void Start()
    {
        _executionMode = new PracticeMode(this);
    }

    // Update is called once per frame
    void Update()
    {
        while(Keyboard.Queued)
        {
            var keystring = Keyboard.Dequeue;
            var keydata = Interpreter.Interpret(keystring);
            var processdata = _executionMode.ProcessKeyInput(keydata);
            Debug.Log($"Keydata: {keydata.Action}, {keydata.Command}, {keydata.Value}, {keydata.Key}");
        }
        // Call executionMode method
        // execute process
        // print results to console
    }

    public void KeyInput(string input)
    {

    }

    public void SetExecutionMode(Mode executionMode) => _executionMode = executionMode;
    
}
