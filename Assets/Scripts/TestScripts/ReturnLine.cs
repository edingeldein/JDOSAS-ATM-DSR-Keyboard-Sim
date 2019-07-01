using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSR.Objects;

public class ReturnLine
{
    public Line LineToValidate { get; private set; }
    public Line NewLine { get; private set; }

    public ReturnLine(Line ltv, Line nl)
    {
        LineToValidate = ltv;
        NewLine = nl;
    }
}
