using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomObjects.Actions
{
    public interface IActionType
    {

        bool VerifyActionFormat(string userString);
        string GetActionFormatReport();
    }
}
