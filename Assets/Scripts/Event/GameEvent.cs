using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent : Event
{
    public string title;
    public string eventId;
    public string description;
    public List<GameEventOption> optionList;
    ///Change to JSON later

    /// <summary>
    /// Process all the registered effect in the option
    /// </summary>
    /// <param name="option"></param>
    public void ProcessEffect(GameEventOption option)
    {
        if(option.effectList != null)
        {
            foreach(string effect in option.effectList)
            {
                ProcessEffect(effect);
            }
        }
    }


    //Want to change to JSON later, place holder yet
    public void ProcessEffect(string s)
    {
        PlayerManager p = PlayerManager.playerManager;
        string[] subs = s.Split('=');
        int i = 0;

        switch (subs[0])
        {
            default:
                Debug.Log("Unkown Effect");
                return;
            case "addCredit":
                bool success = int.TryParse(subs[1], out i);
                if (success)
                {
                    Dictionary<Currency, int> credit = new Dictionary<Currency, int>();
                    credit.Add(Currency.CREDIT, i);
                    p.AddCurrencies(credit);
                }
                else
                {
                    Debug.Log("Wrong Argument: " + "addCredit");
                }
                break;
            case "addResearch":
                i = 0;
                success = int.TryParse(subs[1], out i);
                if (success)
                {
                    Dictionary<Currency, int> credit = new Dictionary<Currency, int>();
                    credit.Add(Currency.RESEARCH, i);
                    p.AddCurrencies(credit);
                }
                else
                {
                    Debug.Log("Wrong Argument: " + "addCredit");
                }
                break;
            case "addRelic":
                break;
            case "addTech":
                break;
            case "event":
                //GameEventManager.gameEvents.Enqueue;
                GameEventManager.RegisterEventByID(subs[1]);
                break;

        }


    }

    public bool ProcessTrigger(string condition)
    {
        string[] parts = condition.Split(' ');

        if (parts.Length != 3) return false;

        // Assume the first part is a variable name, the second is an operator, and the third is a value
        string variableName = parts[0];
        string operatorSymbol = parts[1];
        float value = float.Parse(parts[2]);

        // Retrieve the variable's value from your game (e.g., from a player stats manager)
        float variableValue = 0;

        switch (operatorSymbol)
        {
            case ">": return variableValue > value;
            case "<": return variableValue < value;
            case "==":
            {
                if(variableName.ToLower() == "has_flag"){

                    return variableValue == value;
                }
                else
                {
                    return variableValue == value;
                }
                break;
            }
            // Add more cases as necessary
            default: return false;
        }


    }


}

public class GameEventOption
{
    public string description;
    public List<string> effectList;
}
