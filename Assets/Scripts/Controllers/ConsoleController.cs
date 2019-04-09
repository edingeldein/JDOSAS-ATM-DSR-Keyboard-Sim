using System.Collections;
using System.Collections.Generic;
using CustomEnums;
using UnityEngine;
using UnityEngine.UI;

using DsrBackend;
using DsrBackend.Services;

namespace Controllers
{

    public class ConsoleController : MonoBehaviour
    {
        public TextAsset FlightPlansFile;
        public RectTransform NewLinePrefab;
        public RectTransform KeyboardRectTransform;
        public float HeightDelta = 0.036f;
        public float Padding = 5f;
        public float CursorBlinkTime = 0.3f;
        public int MaxLength;

        Transform parentTransform;
        List<GameObject> lines;

        CursorBuffers cursorBuf;
        Text cursorLineText;

        int currentLineNum = -1;

        Vector2 currentLineMin = new Vector2(0f, 1f);
        GameObject currentLine;
        Text currentLineText;

        const string flightPlansFilePath = "Assets/Scripts/Data/FlightPlans.txt";
        DsrServiceDictionary serviceDictionary;
        string currentFlightPlan;

        #region Lifecycle

        // Start is called on startup
        void Start()
        {
            serviceDictionary = new DsrServiceDictionary();
            serviceDictionary.ConfigureService("FlightPlan", new FlightPlanService(flightPlansFilePath));

            parentTransform = gameObject.GetComponent<Transform>();

            var cursor = GetCursor();
            cursorBuf = new CursorBuffers(cursor);
            cursorLineText = GetCursorText(cursor);

            lines = new List<GameObject>();

            currentFlightPlan = serviceDictionary.Access("FlightPlan").GetRandomAction();

            var firstLine = NewLine(currentFlightPlan);
            lines.Add(firstLine);

            StartCoroutine(CursorTimer(CursorBlinkTime));
        }

        // Update is called once per frame
        void Update()
        {
            cursorBuf.Update(currentLineText.text, currentLine, currentLineNum);
            cursorLineText.text = cursorBuf.Display;

            if (KeyboardRectTransform.anchorMax.y > currentLineMin.y)
                ShiftLinesUp();
        }

        #endregion Lifecycle

        #region Object Creation

        public GameObject GetCursor()
        {
            var go = Instantiate(NewLinePrefab, parentTransform).gameObject;

            var rt = go.GetComponent<RectTransform>();
            rt.anchorMin = new Vector2(currentLineMin.x, currentLineMin.y - HeightDelta);
            rt.anchorMax = new Vector2(1f, currentLineMin.y);

            return go;
        }

        private Text GetCursorText(GameObject cursor)
        {
            var t = cursor.GetComponent<Text>();
            t.text = cursorBuf.Display;
            t.fontStyle = FontStyle.Bold;
            return t;
        }

        public GameObject NewLine()
        {
            return NewLine("> ");
        }

        public GameObject NewLine(string startingText)
        {
            // Instantiate prefab as game object
            var go = Instantiate(NewLinePrefab, parentTransform).gameObject;
            currentLine = go;

            // Set Text and current editable line
            var t = go.GetComponent<Text>();
            t.text = startingText;
            currentLineText = t;

            // Set Rect transform of new object and update existing current line minimum anchor.
            var rt = go.GetComponent<RectTransform>();
            rt.anchorMin = new Vector2(currentLineMin.x, currentLineMin.y - HeightDelta);
            rt.anchorMax = new Vector2(1f, currentLineMin.y);
            currentLineMin = rt.anchorMin;
            currentLineNum++;

            return go;
        }

        #endregion Object Creation

        #region Keyboard Interface

        internal void SubmitText()
        {
            var go = NewLine();
            lines.Add(go);
        }

        internal void AddText(string value)
        {
            currentLineText.text += value;
        }

        internal void ExecuteCommand(Commands command)
        {
            currentLineText.text = $"> {command.ToString()} ";
        }

        internal void RemoveText(Commands command)
        {
            if (command == Commands.Backspace && currentLineText.text.Length > 2)
                currentLineText.text = currentLineText.text.Remove(currentLineText.text.Length - 1, 1);
            else if (command == Commands.Clear)
                currentLineText.text = "> ";
        }

        #endregion Keyboard Interface

        private void ShiftLinesUp()
        {
            foreach (var line in lines)
            {
                var rectTrans = line.GetComponent<RectTransform>();
                ShiftUp(rectTrans, HeightDelta);
            }

            var newLineMin = lines[lines.Count - 1].GetComponent<RectTransform>();
            currentLineMin = newLineMin.anchorMin;

            var cursorRectTrans = cursorBuf.CursorObject.GetComponent<RectTransform>();
            ShiftUp(cursorRectTrans, HeightDelta);
        }

        private void ShiftUp(RectTransform rectTrans, float delta)
        {
            var max = rectTrans.anchorMax;
            var min = rectTrans.anchorMin;
            rectTrans.anchorMax = new Vector2(max.x, max.y + delta);
            rectTrans.anchorMin = new Vector2(min.x, min.y + delta);
        }

        private IEnumerator CursorTimer(float waitTime)
        {
            while (true)
            {
                yield return new WaitForSeconds(waitTime);
                cursorBuf.Active = !cursorBuf.Active;
            }
        }

        internal class CursorBuffers
        {
            private int currentVerticalPosition;
            public GameObject CursorObject { get; }
            public bool Active { get; set; }
            public string Cursor { get; set; }
            public string NoCursor { get; set; }
            public string Display { get; set; }

            public CursorBuffers(GameObject cursor)
            {
                CursorObject = cursor;
                currentVerticalPosition = 0;
                Active = true;
                Cursor = "  _";
                NoCursor = "   ";
                Display = Cursor;
            }

            public void Update(string currentLine, GameObject currentLineObj, int currentLineNum)
            {
                var len = currentLine.Length;
                Cursor = new string(' ', len) + "_";
                NoCursor = new string(' ', len + 1);
                Display = CursorBlink();

                if (currentLineNum != currentVerticalPosition)
                    UpdateVertical(currentLineObj, currentLineNum);
            }

            private void UpdateVertical(GameObject currentLine, int currentLineNum)
            {
                var currRectTrans = CursorObject.GetComponent<RectTransform>();
                var newRectTrans = currentLine.GetComponent<RectTransform>();
                currRectTrans.offsetMax = newRectTrans.offsetMax;
                currRectTrans.offsetMin = newRectTrans.offsetMin;
                currRectTrans.anchorMax = newRectTrans.anchorMax;
                currRectTrans.anchorMin = newRectTrans.anchorMin;

                currentVerticalPosition = currentLineNum;
            }

            public void Navigate(int position)
            {
                var baseStr = new string(' ', position - 1) + "_";
                Cursor = baseStr + "_";
                NoCursor = baseStr + " ";
            }

            private string CursorBlink()
            {
                return Active ? Cursor : NoCursor;
            }
        }
    }
}