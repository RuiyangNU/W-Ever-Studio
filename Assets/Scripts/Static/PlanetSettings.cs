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
    public static readonly int DEFAULT_CREDIT_PER_TICK = 100;
    public static readonly int DEFAULT_BUILDING_LIMIT = 3;

    public static readonly int DEFAULT_CAPTURE_TIMER = 3;

    public static readonly Owner DEFAULT_OWNER = Owner.NONE;
}
