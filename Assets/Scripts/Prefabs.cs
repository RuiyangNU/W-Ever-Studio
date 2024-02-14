using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Prefabs", menuName = "Inventory/Prefabs", order = 1)]
public class Prefabs : ScriptableObject
{
    [SerializeField] private static Fleet[] fleetPrefabs;

    public static Fleet Get(ShipID shipID)
    {
        foreach (Fleet fleet in fleetPrefabs)
        {
            if (fleet.shipID == shipID)
            {
                return fleet;
            }
        }

        Debug.LogWarning("The prefab for " + shipID.ToString() + " was not found.");
        return null;
    }
}
