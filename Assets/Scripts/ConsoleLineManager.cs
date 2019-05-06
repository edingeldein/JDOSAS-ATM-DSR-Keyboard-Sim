using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DSR.DsrLogic.Utilities;

public class ConsoleLineManager : MonoBehaviour
{
    public RectTransform VerificationPanel;
    public RectTransform VerificationText;
    public RectTransform NewLinePrefab;
    public Vector2 CurrentMinimum = new Vector2(0f, 1f);
    [Range(0.036f,0.1f)]
    public float HeightDelta = 0.036f;
    public float CursorBlinkTime = 0.35f;

    public Color Correct;
    public Color Incorrect;

    //GameObject display;
    List<GameObject> lines;
    GameObject currentLine;
    Text currentLineText;

    CursorBuffers cursorBuf;
    Text cursorText;
    int currentLineNumber = -1;

    private void Awake()
    {
        lines = new List<GameObject>();
    }

    void Start()
    {        
        var cursor = GetCursor();
        cursorBuf = new CursorBuffers(cursor);

        StartCoroutine(CursorTimer(CursorBlinkTime));
    }

    #region Text Change methods

    public string GetCurrentLineText()
    {
        return currentLineText.text.Substring(2);
    }

    public void Add(string val)
    {
        currentLineText.text += val;
    }

    public void Backspace()
    {
        if(currentLineText.text.Length > 2)
            currentLineText.text = currentLineText.text.Remove(currentLineText.text.Length - 1, 1);
    }

    public void Clear()
    {
        currentLineText.text = "> ";
    }

    #endregion Text Change methods

    #region Line Creation Methods

    public GameObject NewLine()
    {
        return NewLine("> ");
    }

    public GameObject NewLine(string startingText)
    {
        //Instantiate prefab as game object
        var go = Instantiate(NewLinePrefab, gameObject.GetComponent<RectTransform>()).gameObject;

        // Set Text and current editable line
        var t = go.GetComponent<Text>();
        t.text = startingText;
        currentLineText = t;

        // Set Rect transform of new object and update existing current line minimum anchor.
        var rt = go.GetComponent<RectTransform>();
        rt.anchorMin = new Vector2(CurrentMinimum.x, CurrentMinimum.y - HeightDelta);
        rt.anchorMax = new Vector2(1f, CurrentMinimum.y);
        CurrentMinimum = rt.anchorMin;

        currentLine = go;

        return AddLine(go);
    }

    public GameObject GetVerifiedLine(ValidatedAction action)
    {
        var vfPanel = Instantiate(VerificationPanel, gameObject.GetComponent<RectTransform>()).gameObject;

        var vfrt = vfPanel.GetComponent<RectTransform>();
        vfrt.anchorMin = new Vector2(CurrentMinimum.x, CurrentMinimum.y - HeightDelta);
        vfrt.anchorMax = new Vector2(1f, CurrentMinimum.y);
        CurrentMinimum = vfrt.anchorMin;

        var lineCarrot = Instantiate(VerificationText, vfPanel.GetComponent<RectTransform>()).gameObject;
        lineCarrot.GetComponent<Text>().color = action.Correct ? Correct : Incorrect;

        var horizPos = 0.02f;
        foreach(var token in action.Result)
        {
            var tokenText = Instantiate(VerificationText, vfPanel.GetComponent<RectTransform>()).gameObject;
            var ttText = tokenText.GetComponent<Text>();
            var ttRectTransform = tokenText.GetComponent<RectTransform>();

            ttText.text = token.Section;
            ttText.color = token.Correct ? Correct : Incorrect;

            var width = (ttText.text.Length / 100f) + 0.01f;
            ttRectTransform.anchorMin = new Vector2(horizPos, 0f);
            ttRectTransform.anchorMax = new Vector2(horizPos + width, 1f);
            horizPos += width;
        }

        return AddLine(vfPanel);
    }

    private GameObject AddLine(GameObject line)
    {
        lines.Add(line);
        currentLineNumber = lines.Count;
        return line;
    }

    #endregion Line Creation Methods

    #region Cursor Methods

    private GameObject GetCursor()
    {
        var go = Instantiate(NewLinePrefab, gameObject.GetComponent<RectTransform>()).gameObject;

        var t = go.GetComponent<Text>();
        t.text = "  _";
        t.fontStyle = FontStyle.Bold;
        cursorText = t;

        var rt = go.GetComponent<RectTransform>();
        rt.anchorMin = new Vector2(CurrentMinimum.x, CurrentMinimum.y - HeightDelta);
        rt.anchorMax = new Vector2(1f, CurrentMinimum.y);

        return go;
    }

    public void UpdateCursor()
    {
        cursorBuf.Update(currentLineText.text, currentLine, currentLineNumber);
        cursorText.text = cursorBuf.Display;
    }

    private IEnumerator CursorTimer(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            cursorBuf.Active = !cursorBuf.Active;
        }
    }

    #endregion Cursor Methods

    #region Naviagation Methods

    public GameObject NavigateLine(int index)
    {
        if (index < 0) return lines[0];
        if (index > lines.Count - 1) return lines[lines.Count - 1];

        return lines[index];
    }

    public Vector2 ShiftLinesUp()
    {
        foreach(var line in lines)
        {
            var rectTrans = line.GetComponent<RectTransform>();
            var max = rectTrans.anchorMax;
            var min = rectTrans.anchorMin;
            rectTrans.anchorMax = new Vector2(max.x, max.y + HeightDelta);
            rectTrans.anchorMin = new Vector2(min.x, min.y + HeightDelta);
        }

        var newLineMin = lines[lines.Count - 1].GetComponent<RectTransform>();
        CurrentMinimum = newLineMin.anchorMin;

        return CurrentMinimum;
    }

    public void ShiftCursorUp()
    {
        var cursorRectTrans = cursorBuf.CursorObject.GetComponent<RectTransform>();
        var max = cursorRectTrans.anchorMax;
        var min = cursorRectTrans.anchorMin;
        cursorRectTrans.anchorMax = new Vector2(max.x, max.y + HeightDelta);
        cursorRectTrans.anchorMin = new Vector2(min.x, min.y + HeightDelta);
    }

    #endregion Naviagation Methods

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
