using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerManager;
using static Fleet;

public static class FleetSettings
{
    public static readonly int DEFAULT_STARTING_FLEETS = 0;

    public static readonly float DEFAULT_METHANE_PER_TICK = 1.0f;

    public static readonly Dictionary<PlayerResource, float> destroyerCost = new Dictionary<PlayerResource, float>
    {
        { PlayerResource.METHANE, 20.0f },
        { PlayerResource.STEEL, 5.0f },
    };

    public static int TurnsToBuild(ShipID shipID)
    {
        switch (shipID)
        {
            case ShipID.DESTROYER:
                return 3;

            default:
                return -1;
        }
    }
    public static Dictionary<PlayerResource, float> GetShipCost(ShipID shipID)
    {
        switch (shipID)
        {
            case ShipID.DESTROYER:
                return destroyerCost;

            default:
                return null;
        }
    }
}