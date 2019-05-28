﻿using UnityEngine;
using DSR.DsrLogic.Services;
using DSR.DsrLogic.Utilities;
using DSR.Objects;

namespace DSR.DsrLogic
{
    public class DsrService : MonoBehaviour
    {
        private DsrServiceDictionary _serviceDictionary;

        private const string FlightPlan = "FlightPlan";

        private void Start()
        {
            _serviceDictionary = new DsrServiceDictionary();

            var flightPlanText = GetFileContents("FlightPlans");
            _serviceDictionary.ConfigureService(FlightPlan, new FlightPlanService(flightPlanText));
        }

        private void Update()
        {
            
        }

        public ValidatedAction Validate(Line line)
        {
            var service = _serviceDictionary.Access(FlightPlan);
            return service.ValidateAction(line.Text);
        }

        private string GetFileContents(string filename)
        {
            var textAsset = (TextAsset)Resources.Load(filename, typeof(TextAsset));
            return textAsset.text;
        }
    }
}
