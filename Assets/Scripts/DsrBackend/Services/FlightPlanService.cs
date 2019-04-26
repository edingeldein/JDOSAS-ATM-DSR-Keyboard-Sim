using System;
using System.Collections.Generic;
using DsrBackend.DataAccess;
using DsrBackend.Utilities;

namespace DsrBackend.Services
{
    public class FlightPlanService : IActionService
    {

        private List<string> _flightPlans;

        public FlightPlanService(string fileContents)
        {
            _flightPlans = FileAccess.ParseFlightPlans(fileContents);
        }

        public List<string> GetActionList()
        {
            return _flightPlans;
        }

        public string GetRandomAction()
        {
            var rand = new Random();
            var index = rand.Next(_flightPlans.Count);
            return _flightPlans[index];
        }

        public ValidatedAction ValidateAction(string correctAction, string userAction)
        {
            var validatedAction = new ValidatedAction(correctAction, userAction);
            return validatedAction;
        }
    }
}
