using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerManager;
using static Fleet;

public static class FleetSettings
{
    private static readonly Dictionary<Currency, float> shipCosts = new Dictionary<Currency, float>
    {

    };

    public static int TurnsToBuild(ShipID shipID)
    {
        switch (shipID)
        {
            case ShipID.MONO:
                return 3;

            default:
                return -1;
        }
    }
    public static Dictionary<Currency, float> GetShipCost(ShipID shipID)
    {
        switch (shipID)
        {
            case ShipID.MONO:
                return null;

            default:
                return null;
        }
    }
}