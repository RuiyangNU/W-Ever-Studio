using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerManager;
using static Planet;

public static class PlanetSettings
{
    /*
     * Stats
     */
    public static readonly float DEFAULT_STEEL_PER_TICK = 1.0f;
    public static readonly float DEFAULT_METHANE_PER_TICK = 1.0f;

    public static readonly int DEFAULT_MAX_REFINERIES = 1;
    public static readonly int DEFAULT_MAX_SHIPYARDS = 1;

    public static readonly int DEFAULT_STARTING_REFINERIES = 0;
    public static readonly int DEFAULT_STARTING_SHIPYARDS = 0;

    public static readonly PlanetOwner DEFAULT_OWNER = PlanetOwner.NONE;

    public static readonly float DEFAULT_MAX_HEALTH = 150.0f;
    public static readonly int DEFAULT_CAPTURE_TIMER = 3;

    /*
     * Buildings
     */
    public static readonly Dictionary<PlayerResource, float> refineryCost = new Dictionary<PlayerResource, float>
    {
        { PlayerResource.METHANE, 10.0f },
        { PlayerResource.STEEL, 10.0f },
    };

    public static readonly Dictionary<PlayerResource, float> shipyardCost = new Dictionary<PlayerResource, float>
    {
        { PlayerResource.STEEL, 25.0f },
    };
}
