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
    protected int maxLevel;

    /*
     * Properties
     */
    public int Level { get => level; }
    public int MaxLevel { get => maxLevel; }
    public BuildingID ID { get => buildingID; }
    public Dictionary<Currency, int> Currencies {  get => GetTickCurrencies(); }
    public Dictionary<Commodity, int> Commodities { get => GetCommodities(); }

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

    public abstract Dictionary<Currency, int> GetTickCurrencies();

    public abstract Dictionary<Commodity, int> GetCommodities();

    public static int BuildCreditCost(BuildingID id)
    {
        throw new System.NotImplementedException();
    }

    public static Dictionary<Commodity, int> BuildCommodityRequirement(BuildingID id)
    {
        throw new System.NotImplementedException();
    }

    public abstract int UpgradeCreditCost();

    public abstract Dictionary<Commodity, int> UpgradeCommodityRequirement();

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
