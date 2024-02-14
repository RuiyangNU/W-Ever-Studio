using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour
{
    /*
     * Control
     */
    protected BuildingID buildingID;
    protected Planet homePlanet;

    protected int level;

    public int Level { get => level; }
    public BuildingID ID { get => buildingID; }

    public static Building InitializeBuilding(BuildingID buildingID, Planet homePlanet)
    {
        switch (buildingID)
        {
            case BuildingID.DEPOT: return new DepotBuilding(homePlanet);
            case BuildingID.SMELTERY: return new SmelteryBuilding(homePlanet);
            case BuildingID.TRADEHUB: return new TradeHubBuilding(homePlanet);
            case BuildingID.LAB: return new ResearchLabBuilding(homePlanet);
            case BuildingID.SHIPYARD: return new ShipyardBuilding(homePlanet);

            default:
                Debug.LogError("Unknown building ID.");
                return null;
        }
    }

    public abstract void UpdateTick();
}

public enum BuildingID
{
    // Commodities
    DEPOT,
    SMELTERY,

    // Currencies
    TRADEHUB,
    LAB,

    // Shipyards
    SHIPYARD
}
