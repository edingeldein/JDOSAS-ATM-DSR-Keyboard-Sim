using System.Collections.Generic;
using DsrBackend.Utilities;

namespace DsrBackend.Services
{
    public interface IActionService
    {
        List<string> GetActionList();
        string GetRandomAction();
        ValidatedAction ValidateAction(string correctAction, string userAction);
    }
}
