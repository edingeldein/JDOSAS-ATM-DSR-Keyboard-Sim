using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DSR.Console.Interfaces;

namespace DSR.Console
{
    public class ConsoleController : MonoBehaviour, IConsoleController
    {
        public RectTransform LinePrefab;
        public Vector2 CurrentMinimum = new Vector2(0f, 1f);
        public float HeightDelta = 0.036f;
        public float CursorBlinkTime = 0.35f;

        [SerializeField] private RectTransform _displayCanvas;
        private List<GameObject> _displayedLines;
        private GameObject _currentLine;
        private GameObject _cursor;

        private void Start()
        {
            _cursor = CreateCursor();
            _currentLine = CreateNewLine();
            _displayedLines = new List<GameObject>();
            _displayedLines.Add(_currentLine);

            StartCoroutine(CursorTimer(CursorBlinkTime, _cursor));
        }

        private void Update()
        {
        
        }

        public void ClearLines()
        {
            throw new NotImplementedException();
        }

        public void NewLine(string line, string cursor)
        {
            //_currentLine.GetComponent<Text>().text = line;
        }

        public void UpdateLine(string line, string cursor)
        {
            _currentLine.GetComponent<Text>().text = $"> {line}";
            _cursor.GetComponent<Text>().text = $"  {cursor}";
        }

        private GameObject CreateCursor()
        {
            var go = Instantiate(LinePrefab, _displayCanvas).gameObject;

            var t = go.GetComponent<Text>();
            t.text = "  _";
            t.fontStyle = FontStyle.Bold;            

            var rt = go.GetComponent<RectTransform>();
            rt.anchorMin = new Vector2(CurrentMinimum.x, CurrentMinimum.y - HeightDelta);
            rt.anchorMax = new Vector2(1f, CurrentMinimum.y);

            return go;
        }

        private GameObject CreateNewLine()
        {
            return CreateNewLine("> ");
        }

        private GameObject CreateNewLine(string startingText)
        {
            var go = Instantiate(LinePrefab, _displayCanvas).gameObject;

            var t = go.GetComponent<Text>();
            t.text = startingText;

            var rt = go.GetComponent<RectTransform>();
            rt.anchorMin = new Vector2(CurrentMinimum.x, CurrentMinimum.y - HeightDelta);
            rt.anchorMax = new Vector2(1f, CurrentMinimum.y);
            CurrentMinimum = rt.anchorMin;

            return go;
        }

        private IEnumerator CursorTimer(float waitTime, GameObject cursor)
        {
            bool cursorActive = true;
            while(true)
            {
                yield return new WaitForSeconds(waitTime);
                cursorActive = !cursorActive;
                cursor.SetActive(cursorActive);
            }
        }
    }
}
