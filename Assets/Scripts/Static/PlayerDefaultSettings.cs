using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerManager;

public static class PlayerDefaultSettings
{
    public static Dictionary<PlayerResource, float> DEFAULT_STARTING_RESOURCES = new Dictionary<PlayerResource, float>
    {
        { PlayerResource.STEEL, 15.0f },
        { PlayerResource.METHANE, 15.0f }
    };

}
