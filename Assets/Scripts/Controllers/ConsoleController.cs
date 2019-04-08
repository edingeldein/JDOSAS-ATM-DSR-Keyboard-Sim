using System;
using System.Collections;
using System.Collections.Generic;
using CustomEnums;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{

    public class ConsoleController : MonoBehaviour
    {
        public RectTransform NewLinePrefab;
        public RectTransform KeyboardRectTransform;
        public float HeightDelta = 0.036f;
        public float padding = 5f;
        public float cursorBlinkTime = 0.3f;

        Transform ParentTransform;
        List<GameObject> Lines;

        CursorBuffers CursorBuf;
        Text CursorLineText;

        int CurrentLineNum = -1;

        Vector2 CurrentLineMin = new Vector2(0f, 1f);
        GameObject CurrentLine;
        Text CurrentLineText;

        void Start()
        {
            ParentTransform = gameObject.GetComponent<Transform>();

            var cursor = GetCursor();
            CursorBuf = new CursorBuffers(cursor);
            CursorLineText = GetCursorText(cursor);

            Lines = new List<GameObject>();
            var firstLine = NewLine();
            Lines.Add(firstLine);

            StartCoroutine(CursorTimer(cursorBlinkTime));
        }

        // Update is called once per frame
        void Update()
        {
            CursorBuf.Update(CurrentLineText.text, CurrentLine, CurrentLineNum);
            CursorLineText.text = CursorBuf.Display;

            Debug.Log($"KeyboardMax: {KeyboardRectTransform.anchorMax} --- CurrentLineMin: {CurrentLineMin}");
            if (KeyboardRectTransform.anchorMax.y > CurrentLineMin.y)
                ShiftLinesUp();
        }

        public void ShiftLinesUp()
        {
            foreach(var line in Lines)
            {
                var rectTrans = line.GetComponent<RectTransform>();
                ShiftUp(rectTrans, HeightDelta);
            }

            var newLineMin = Lines[Lines.Count - 1].GetComponent<RectTransform>();
            CurrentLineMin = newLineMin.anchorMin;

            var cursorRectTrans = CursorBuf.CursorObject.GetComponent<RectTransform>();
            ShiftUp(cursorRectTrans, HeightDelta);
        }

        private void ShiftUp(RectTransform rectTrans, float delta)
        {
            var max = rectTrans.anchorMax;
            var min = rectTrans.anchorMin;
            rectTrans.anchorMax = new Vector2(max.x, max.y + delta);
            rectTrans.anchorMin = new Vector2(min.x, min.y + delta);
        }

        public GameObject GetCursor()
        {
            var go = Instantiate(NewLinePrefab, ParentTransform).gameObject;

            var rt = go.GetComponent<RectTransform>();
            rt.anchorMin = new Vector2(CurrentLineMin.x, CurrentLineMin.y - HeightDelta);
            rt.anchorMax = new Vector2(1f, CurrentLineMin.y);

            return go;
        }

        private Text GetCursorText(GameObject cursor)
        {
            var t = cursor.GetComponent<Text>();
            t.text = CursorBuf.Display;
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
            var go = Instantiate(NewLinePrefab, ParentTransform).gameObject;
            CurrentLine = go;

            // Set Text and current editable line
            var t = go.GetComponent<Text>();
            t.text = startingText;
            CurrentLineText = t;

            // Set Rect transform of new object and update existing current line minimum anchor.
            var rt = go.GetComponent<RectTransform>();
            rt.anchorMin = new Vector2(CurrentLineMin.x, CurrentLineMin.y - HeightDelta);
            rt.anchorMax = new Vector2(1f, CurrentLineMin.y);
            CurrentLineMin = rt.anchorMin;
            CurrentLineNum++;

            return go;
        }

        internal void SubmitText()
        {
            var go = NewLine();
            Lines.Add(go);
        }

        internal void AddText(string value)
        {
            CurrentLineText.text += value;
        }

        internal void ExecuteCommand(Commands command)
        {
            CurrentLineText.text = $"> {command.ToString()} ";
        }

        internal void RemoveText(Commands command)
        {
            if (command == Commands.Backspace && CurrentLineText.text.Length > 2)
                CurrentLineText.text = CurrentLineText.text.Remove(CurrentLineText.text.Length - 1, 1);
            else if (command == Commands.Clear)
                CurrentLineText.text = "> ";
        }
        
        private IEnumerator CursorTimer(float waitTime)
        {
            while (true)
            {
                yield return new WaitForSeconds(waitTime);
                CursorBuf.Active = !CursorBuf.Active;
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