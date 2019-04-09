using System;
using System.Collections.Generic;
using DsrBackend.DataAccess;
using DsrBackend.Utilities;

namespace DsrBackend.Services
{
    public class FlightPlanService : IActionService
    {

        private List<string> _flightPlans;
        private string _filepath;

        public FlightPlanService(string filepath)
        {
            _flightPlans = FileAccess.GetFlightPlans(filepath);
            Console.WriteLine(_flightPlans);
            _filepath = filepath;
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
