using System.Collections;
using System.Collections.Generic;
using CustomEnums;
using UnityEngine;
using UnityEngine.UI;

using DsrBackend;
using DsrBackend.Services;
using DsrBackend.Utilities;

namespace Controllers
{

    public class ConsoleController : MonoBehaviour
    {
        public RectTransform KeyboardRectTransform;

        ConsoleLineManager consoleLineManager;    
        Vector2 currentLineMin = new Vector2(0f, 1f);

        const string flightPlansFilePath = "Assets/Scripts/Data/FlightPlans.txt";
        DsrServiceDictionary serviceDictionary;
        string currentCorrectAction;

        #region Lifecycle

        // Start is called on startup
        void Start()
        {
            Debug.Log("ConsoleController Start()");
            consoleLineManager = gameObject.GetComponent<ConsoleLineManager>();

            serviceDictionary = new DsrServiceDictionary();
            serviceDictionary.ConfigureService("FlightPlan", new FlightPlanService(flightPlansFilePath));

            currentCorrectAction = serviceDictionary.Access("FlightPlan").GetRandomAction();

            NewLine($"> {currentCorrectAction}");
            NewLine();
        }

        // Update is called once per frame
        void Update()
        {
            consoleLineManager.UpdateCursor();

            if (KeyboardRectTransform.anchorMax.y > currentLineMin.y)
            {
                currentLineMin = consoleLineManager.ShiftLinesUp();
                consoleLineManager.ShiftCursorUp();
            }
        }

        #endregion Lifecycle

        #region ConsoleLineManager wrapper functions

        public void NewLine()
        {
            NewLine("> ");
        }

        public void NewLine(string startingText)
        {
            var nlo = consoleLineManager.NewLine(startingText);
            currentLineMin = nlo.GetComponent<RectTransform>().anchorMin;
        }

        public void VerificationLine(ValidatedAction action)
        {
            var vlo = consoleLineManager.GetVerifiedLine(action);
            currentLineMin = vlo.GetComponent<RectTransform>().anchorMin;
        }

        #endregion ConsoleLineManager wrapper functions

        #region Keyboard Interface

        internal void SubmitText()
        {
            var userInput = consoleLineManager.GetCurrentLineText();
            var verification = serviceDictionary.Access("FlightPlan").ValidateAction(currentCorrectAction, userInput);
            VerificationLine(verification);
            NewLine();
        }

        internal void AddText(string value)
        {
            consoleLineManager.Add(value);
        }

        internal void ExecuteCommand(Commands command)
        {
            consoleLineManager.Add($"{command.ToString()} ");
        }

        internal void RemoveText(Commands command)
        {
            if (command == Commands.Backspace)
                consoleLineManager.Backspace();
            else if (command == Commands.Clear)
                consoleLineManager.Clear();
        }

        #endregion Keyboard Interface

    }
}