using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Event
{
    public string message;
    public override string ToString()
    {
        return "";
    }

    public virtual string ToSimpleString()
    {
        return "";
    }
}
