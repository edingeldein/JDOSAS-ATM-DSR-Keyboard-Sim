using System;
using System.IO;
using System.Collections.Generic;
using CustomObjects.Actions;

public class Verifier
{
    private const string FlightPlansFilename = "./Assets/Scripts/Data/FlightPlans.txt";
    private List<string> FlightPlans;
    private string CurrentActionText;
    private IActionType CurrentAction;

    public Verifier()
    {
        var alltext = File.ReadAllText(FlightPlansFilename);
        FlightPlans = new List<string>(alltext.Split('\n'));
    }

    public void SetFlightPlanAction()
    {
        var rand = new Random();
        var fp = FlightPlans[rand.Next(FlightPlans.Count - 1)];
        FlightPlans.Remove(fp);
        CurrentActionText = fp;
        CurrentAction = new FlightPlan(fp);
    }

    public void ClearAction()
    {
        CurrentActionText = null;
        CurrentAction = null;
    }

    public string GetFlightPlan()
    {
        var rand = new Random();
        var fp = FlightPlans[rand.Next(FlightPlans.Count - 1)];
        FlightPlans.Remove(fp);
        return fp;
    }

    public bool CheckFlightPlan(string fp)
    {
        foreach(var flightPlan in FlightPlans)
        {
            if (fp.Equals(flightPlan))
                return true;
        }
        return false;
    }
}
