using UnityEngine;
using DSR.Objects;
using DSR.DsrLogic;
using DSR.Enums;

public class LineManagerController : MonoBehaviour 
{

    private DsrService _dsrService;
    private Line _currentLine;

    private void Start() 
    {
        _currentLine = new Line();        
    }

    public object KeyInput(KeyData inputdata)
    {
        if(inputdata.Key == KeyType.Command) return CommandInput(inputdata);
        else if(inputdata.Key == KeyType.Action) return ActionInput(inputdata);
        else return ValueInput(inputdata);
    }

    public Line ClearLines()
    {
        _currentLine.ClearLine();
        return _currentLine;
    }

    public Line NewLine()
    {
        _currentLine = new Line();
        return _currentLine;
    }

    private object CommandInput(KeyData commandData)
    {
        var command = commandData.Command;

        switch (command)
        {
            case CommandType.Backspace:
                _currentLine.Backspace();
                return _currentLine;
            case CommandType.Clear:
                _currentLine.ClearLine();
                return _currentLine;
            case CommandType.Enter:
                var temp = _currentLine;
                _currentLine = new Line();
                return new ReturnLine(temp, _currentLine);
            case CommandType.Up:
                break;
            case CommandType.Down:
                break;
            case CommandType.Left:
                break;
            case CommandType.Right:
                break;
        }
        return null;
    }

    private object ActionInput(KeyData keydata)
    {
        _currentLine.StartAction(keydata.Action);
        return _currentLine;
    }

    private object ValueInput(KeyData keydata)
    {
        _currentLine.AddChar(keydata.Value);
        return _currentLine;
    }

    
}