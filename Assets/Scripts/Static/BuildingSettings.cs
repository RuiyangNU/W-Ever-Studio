using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BuildingSettings
{
    public static int GetContribution(int buildingLevel)
    {
        if (buildingLevel == 1) { return 1; }

        int val = 2;
        for (int i = 0; i < buildingLevel - 1; i++)
        {
            val *= 2;
        }

        return val;
    }
}
