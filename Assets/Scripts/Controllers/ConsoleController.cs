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
        public float HeightDelta = 0.036f;
        public float padding = 5f;
        public float cursorBlinkTime = 0.3f;

        Transform ParentTransform;
        List<GameObject> Lines;

        GameObject Cursor;
        CursorBuffers CursorBuf;
        Text CursorLine;

        int CurrentLineNum = -1;
        bool TextChanged = false;
        Vector2 CurrentLineMin = new Vector2(0f, 1f);
        Text CurrentLine;

        void Start()
        {
            ParentTransform = gameObject.GetComponent<Transform>();

            CursorBuf = new CursorBuffers();
            Cursor = GetCursor();

            Lines = new List<GameObject>();
            var firstLine = NewLine();
            CurrentLine = firstLine.GetComponent<Text>();

            StartCoroutine(CursorTimer(cursorBlinkTime));
        }

        // Update is called once per frame
        void Update()
        {
            CursorBuf.Update(CurrentLine.text);
            CursorLine.text = CursorBuf.Display;
        }

        public GameObject GetCursor()
        {
            var go = Instantiate(NewLinePrefab, ParentTransform).gameObject;

            CursorBuf = new CursorBuffers();
            var t = go.GetComponent<Text>();
            t.text = CursorBuf.Display;
            t.fontStyle = FontStyle.Bold;
            CursorLine = t;

            var rt = go.GetComponent<RectTransform>();
            rt.anchorMin = new Vector2(CurrentLineMin.x, CurrentLineMin.y - HeightDelta);
            rt.anchorMax = new Vector2(1f, CurrentLineMin.y);

            return go;

        }

        public GameObject NewLine()
        {
            return NewLine("> ");
        }

        public GameObject NewLine(string startingText)
        {
            // Instantiate prefab as game object
            var go = Instantiate(NewLinePrefab, ParentTransform).gameObject;

            // Set Text and current editable line
            var t = go.GetComponent<Text>();
            t.text = startingText;
            CurrentLine = t;

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
            CurrentLine = Lines[CurrentLineNum].GetComponent<Text>();
        }

        internal void AddText(string value)
        {
            CurrentLine.text += value;
            TextChanged = true;
        }

        internal void ExecuteCommand(Commands command)
        {
            CurrentLine.text = command.ToString();
            TextChanged = true;
        }

        internal void RemoveText(Commands command)
        {
            if (command == Commands.Backspace && CurrentLine.text.Length > 2)
                CurrentLine.text = CurrentLine.text.Remove(CurrentLine.text.Length - 1, 1);
            else if (command == Commands.Clear)
                CurrentLine.text = "> ";

            TextChanged = true;
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
            public bool Active { get; set; }
            public string Cursor { get; set; }
            public string NoCursor { get; set; }
            public string Display { get; set; }
            public CursorBuffers()
            {
                Active = true;
                Cursor = "  _";
                NoCursor = "   ";
                Display = Cursor;
            }

            public void Navigate(int position)
            {
                var baseStr = new string(' ', position - 1) + "_";
                Cursor = baseStr + "_";
                NoCursor = baseStr + " ";
            }

            public void Update(string currentLine)
            {
                var len = currentLine.Length;
                Cursor = new string(' ', len) + "_";
                NoCursor = new string(' ', len + 1);
                Display = CursorBlink();
            }

            private string CursorBlink()
            {
                return Active ? Cursor : NoCursor;
            }
        }
    }
}