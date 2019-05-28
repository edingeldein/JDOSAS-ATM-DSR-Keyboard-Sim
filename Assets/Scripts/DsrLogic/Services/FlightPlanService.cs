using System;
using System.Collections.Generic;
using DSR.DsrLogic.DataAccess;
using DSR.DsrLogic.Utilities;

namespace DSR.DsrLogic.Services
{
    public class FlightPlanService : IActionService
    {

        private Dictionary<string,string> _flightPlanDict;
        private List<string> _flightPlanList;

        public FlightPlanService(string fileContents)
        {
            _flightPlanDict = ParseFlightPlans(fileContents);
        }

        public List<string> GetActionList()
        {
            return _flightPlanList;
        }

        public string GetRandomAction()
        {
            var rand = new Random();
            var index = rand.Next(_flightPlanDict.Count);
            return _flightPlanList[index];
        }

        public ValidatedAction ValidateAction(string correctAction, string userAction)
        {
            var validatedAction = new ValidatedAction(correctAction, userAction);
            return validatedAction;
        }

        public ValidatedAction ValidateAction(string searchAction)
        {
            var actionSplit = searchAction.Split(' ');
            if (actionSplit.Length < 2) return new ValidatedAction("? ? ? ? ?", searchAction);
            var tailNum = actionSplit[1];

            var result = _flightPlanDict.TryGetValue(tailNum, out var correctAction);
            if (!result) return new ValidatedAction("? ? ? ? ?", searchAction);
            return new ValidatedAction(correctAction, searchAction);
        }

        private Dictionary<string,string> ParseFlightPlans(string contents)
        {
            var dict = new Dictionary<string, string>();
            _flightPlanList = new List<string>(contents.Split('\n'));
            foreach(var line in _flightPlanList)
            {
                var spl = line.Split(' ');
                var key = spl[1];
                dict.Add(key, line);
            }
            return dict;
        }
    }
}
