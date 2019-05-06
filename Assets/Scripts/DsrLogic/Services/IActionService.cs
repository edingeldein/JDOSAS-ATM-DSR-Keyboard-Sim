using System.Collections.Generic;
using DSR.DsrLogic.Utilities;

namespace DSR.DsrLogic.Services
{
    public interface IActionService
    {
        List<string> GetActionList();
        string GetRandomAction();
        ValidatedAction ValidateAction(string correctAction, string userAction);
        ValidatedAction ValidateAction(string searchAction);
    }
}
