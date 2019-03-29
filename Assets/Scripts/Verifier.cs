using System;
using System.IO;
using System.Collections.Generic;

public class Verifier
{
    private const string FlightPlansFilename = "./Assets/Scripts/Data/FlightPlans.txt";
    private List<string> FlightPlans;

    public Verifier()
    {
        var alltext = File.ReadAllText(FlightPlansFilename);
        FlightPlans = new List<string>(alltext.Split('\n'));
    }

    public string GetFlightPlan()
    {
        var rand = new Random();
        var fp = FlightPlans[rand.Next(FlightPlans.Count - 1)];
        FlightPlans.Remove(fp);
        return fp;
    }
}
