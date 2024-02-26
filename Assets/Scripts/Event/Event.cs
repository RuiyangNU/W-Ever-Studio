using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Event
{
    public string message;
    public string eventType;
    public override string ToString()
    {
        return message;
    }

    public virtual string ToSimpleString()
    {
        return message;
    }

    public string SetStringColor(string s, Color color)
    {
        return "<color=#" + ColorUtility.ToHtmlStringRGB(color) + ">" + s + "</color>";
    }
}
