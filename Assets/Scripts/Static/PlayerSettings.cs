using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerManager;

public static class PlayerSettings
{
    public static Dictionary<Currency, int> DEFAULT_STARTING_CURRENCY = new Dictionary<Currency, int>
    {
        { Currency.CREDIT, 1000 },
        { Currency.RESEARCH, 0 }
    };

}
