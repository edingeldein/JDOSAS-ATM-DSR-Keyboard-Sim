using System.Collections;
using System.Collections.Generic;
using DSR.Objects;
using DSR.Enums;

public class PracticeMode : Mode
{
    private bool _validationState = false;

    public PracticeMode(MainController context) : base(context) { _validationState = false; }

    public override object ProcessKeyInput(KeyData data)
    {
        if(data.Command == CommandType.Enter && _validationState)
        {
            var line = _lineManager.ClearLines();
            _console.ClearLines();
            _validationState = false;
            return line;
        }
        
        // Give key input to line manager
        var  returnvalue = _lineManager.KeyInput(data);

        if (returnvalue.GetType() == typeof(Line))
        {
            var currentLine = (Line)returnvalue;
            _console.UpdateLine(currentLine.Text, currentLine.CursorText);
            return currentLine;
        }
        else if(returnvalue.GetType() == typeof(ReturnLine))
        {
            _validationState = true;
            var returnline = (ReturnLine)returnvalue;
            var validatedline = _service.Validate(returnline.LineToValidate);
            _console.DisplayValidation(validatedline);
        }
        return null;
    }

}
