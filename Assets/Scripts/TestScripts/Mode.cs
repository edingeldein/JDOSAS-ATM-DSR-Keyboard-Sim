using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSR.Objects;
using DSR.Console;
using DSR.DsrLogic;

public abstract class Mode
{
    protected InterpreterController _interpreter;
    protected LineManagerController _lineManager;
    protected ConsoleController _console;
    protected DsrService _service;
    public Mode(MainController context)
    {
        _interpreter = context.Interpreter;
        _lineManager = context.LineManager;        
        _console = context.Console;
        _service = context.Service;
    }
    public abstract object ProcessKeyInput(KeyData data);
}
