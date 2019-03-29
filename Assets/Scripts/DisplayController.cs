using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using CustomObjects;
using CustomObjects.Actions;
using CustomEnums;
using System;

public class DisplayController : MonoBehaviour
{

    public IActionType CurrentAction;

    TextMeshProUGUI textDisplay;
    TextMeshProUGUI verifyDisplay;
    Verifier verifier;

    string currentFlightPlan;
    string textToDisplay;
    string[] textBuffers = new string[2];

    int strPos = 0;
    int tick = 0;
    bool alive = true;

    #region Lifecycles

    void Start()
    {
        InitDisplays();
        
        verifier = new Verifier();
        currentFlightPlan = verifier.GetFlightPlan(); 

        textBuffers[0] = "";
        textBuffers[1] = "_";
        textToDisplay = textBuffers[0];

        var cursorCoroutine = Cursor(0.6f);
        StartCoroutine(cursorCoroutine);
    }
    
    void Update()
    {
        if (NeedsUpdate())
            textToDisplay = textBuffers[tick % 2];
        textDisplay.text = textToDisplay;
    }

    private void OnDestroy()
    {
        alive = false;
    }

    #endregion Lifecycles

    #region Keyhandlers

    public void AddText(string text)
    {
        textBuffers[0] = textBuffers[0].Insert(strPos, text);
        textBuffers[1] = textBuffers[1].Insert(strPos, text);
        strPos += text.Length;
    }

    public void RemoveText(Commands command)
    {
        if (command == Commands.Backspace)
        {
            if (strPos == 0) return;
            textBuffers[0] = textBuffers[0].Remove(strPos-1);
            textBuffers[1] = textBuffers[1].Substring(0, strPos-1) + "_";
            strPos--;
        }
        else if (command == Commands.Clear)
        {
            textBuffers[0] = string.Empty;
            textBuffers[1] = "_";
            strPos = 0;
        }

    }

    public void SubmitText()
    {
        var success = verifier.CheckFlightPlan(textBuffers[0]);
        if (success)
        {
            verifyDisplay.text = "SUCCESS!";
            verifyDisplay.faceColor = new Color(88f, 237f, 83f);
        }
        else
        {
            verifyDisplay.text = "Failure!";
            verifyDisplay.faceColor = new Color(237f, 108f, 83f);
        }
            
    }

    public void Navigate(KeyPressData data)
    {

    }

    public void ExecuteCommand(Commands command)
    {
        var commandText = command.ToString();
        AddText($"{commandText} ");
    }

    private IEnumerator Cursor(float waitTime)
    {        
        while(alive)
        {
            yield return new WaitForSeconds(waitTime);
            textToDisplay = textBuffers[tick % 2];
            tick++;
        }
    }

    #endregion Keyhandlers

    #region Helper functions

    void InitDisplays()
    {
        var components = gameObject.GetComponentsInChildren<TextMeshProUGUI>();
        textDisplay = GetDisplay(components, "DisplayText");
        verifyDisplay = GetDisplay(components, "VerifyText");
    }

    private TextMeshProUGUI GetDisplay(TextMeshProUGUI[] components, string v)
    {
        foreach (var comp in components)
        {
            if (comp.name.Equals(v))
                return comp;
        }

        throw new MissingComponentException($"Can't find component {v} on {gameObject.name}");
    }

    bool NeedsUpdate()
    {
        var tbzClean = textToDisplay.Equals(textBuffers[0]);
        var tboClean = textToDisplay.Equals(textBuffers[1]);
        return !(tbzClean || tboClean);
    }

    #endregion
}
