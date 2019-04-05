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

        Transform ParentTransform;
        List<GameObject> Lines;

        GameObject Cursor;
        CursorBuffers CursorBuf;

        int CurrentLineNum = -1;
        Vector2 CurrentLineMin = new Vector2(0f, 1f);
        Text CurrentLine;
        Text CursorLine;

        void Start()
        {
            ParentTransform = gameObject.GetComponent<Transform>();
            CursorBuf = new CursorBuffers();
            Cursor = GetCursor();

            Lines = new List<GameObject>();
            var firstLine = NewLine();
            CurrentLine = firstLine.GetComponent<Text>();
                       
        }

        // Update is called once per frame
        void Update()
        {

        }

        public GameObject GetCursor()
        {
            var go = Instantiate(NewLinePrefab, ParentTransform).gameObject;

            CursorBuffers = new CursorBuffers();
            var t = go.GetComponent<Text>();
            t.text = CursorBuffers.Cursor;
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
        }

        internal void ExecuteCommand(Commands command)
        {
            CurrentLine.text = command.ToString();
        }

        internal void RemoveText(Commands command)
        {
            if (command == Commands.Backspace && CurrentLine.text.Length > 2)
                CurrentLine.text = CurrentLine.text.Remove(CurrentLine.text.Length - 1, 1);
            else if (command == Commands.Clear)
                CurrentLine.text = "> ";            
        }

        private IEnumerator ToggleCursor(float waitTime)
        {
            while (true)
            {
                yield return new WaitForSeconds(waitTime);
                CursorBuf.Display = CursorBuf.Active ? CursorBuf.Cursor : CursorBuf.NoCursor;
                CursorBuf.Active = !CursorBuf.Active;
            }
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
    }
}