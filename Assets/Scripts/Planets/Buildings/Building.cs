using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building
{
    /*
     * Control
     */
    protected BuildingID buildingID;
    protected Planet homePlanet;

    protected int level;

    /*
     * Commodities
     */
    protected Dictionary<Commodity, int> commodities = new()
    {
        { Commodity.CONSTRUCTION, 0 },
        { Commodity.ALLOY, 0 }
    };

    /*
     * Properties
     */
    public int Level { get => level; }
    public BuildingID ID { get => buildingID; }
    public Dictionary<Commodity, int> Commodities { get => commodities; }

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

    /*
     * Methods
     */
    public abstract void UpdateTick();
    
    public void Upgrade()
    {
        this.level++;
    }
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
