using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DsrBackend.Utilities;

public class ConsoleLineManager : MonoBehaviour
{
    public GameObject Display;
    public RectTransform VerifiedLinePrefab;
    public RectTransform NewLinePrefab;
    public Vector2 CurrentMinimum = new Vector2(0f, 1f);
    [Range(0.036f,0.1f)]
    public float HeightDelta = 0.036f;

    List<GameObject> lines;

    public ConsoleLineManager()
    {
        lines = new List<GameObject>();
    }

    public GameObject GetVerifiedLine(ValidatedAction action)
    {
        var vfLine = Instantiate(VerifiedLinePrefab, Display.GetComponent<RectTransform>()).gameObject;
        return vfLine;
    }

    public GameObject NewLine()
    {
        return NewLine("> ");
    }

    public GameObject NewLine(string startingText)
    {
        //Instantiate prefab as game object
        var go = Instantiate(NewLinePrefab, Display.GetComponent<RectTransform>()).gameObject;

        // Set Text and current editable line
        var t = go.GetComponent<Text>();
        t.text = startingText;

        // Set Rect transform of new object and update existing current line minimum anchor.
        var rt = go.GetComponent<RectTransform>();
        rt.anchorMin = new Vector2(CurrentMinimum.x, CurrentMinimum.y - HeightDelta);
        rt.anchorMax = new Vector2(1f, CurrentMinimum.y);
        CurrentMinimum = rt.anchorMin;

        return AddLine(go);
    }

    private GameObject AddLine(GameObject line)
    {
        lines.Add(line);
        return line;
    }

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
}
