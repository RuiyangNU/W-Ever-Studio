using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerManager;
using static Fleet;

public static class FleetSettings
{
    public static readonly int DEFAULT_STARTING_FLEETS = 0;

    public static readonly float DEFAULT_METHANE_PER_TICK = 1.0f;

    public static readonly Dictionary<PlayerResource, float> fleetCost = new Dictionary<PlayerResource, float>
    {
        { PlayerResource.METHANE, 5.0f },
        { PlayerResource.STEEL, 20.0f },
    };
}