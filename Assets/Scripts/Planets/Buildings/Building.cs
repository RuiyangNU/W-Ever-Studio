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
        switch (id)
        {
            case BuildingID.LAB: return 1000;
            case BuildingID.DEPOT: return 500;
            case BuildingID.SMELTERY: return 500;
            case BuildingID.TRADEHUB: return 1000;
            case BuildingID.SHIPYARD: return 1000;

            default:
                Debug.LogError("Unknown building id " + id.ToString() + ".");
                return -1;
        }
    }

    public static int BuildCMRequirement(BuildingID id)
    {
        switch (id)
        {
            case BuildingID.DEPOT: return 0;
            case BuildingID.SMELTERY: return 1;
            case BuildingID.TRADEHUB: return 1;
            case BuildingID.LAB: return 1;
            case BuildingID.SHIPYARD: return 1;
            default:
                Debug.LogError("Unknown building id " + id.ToString() + ".");
                return -1;
        }
    }

    public abstract int UpgradeCreditCost();

    public abstract Dictionary<Commodity, int> UpgradeCommodityRequirement();


    public static string GetDescription(BuildingID id)
    {
        switch (id)
        {
            case BuildingID.SHIPYARD:
                return "Constructs new fleets.";
            case BuildingID.LAB:
                return "Produces research points.";
            case BuildingID.DEPOT:
                return "Increases construction material level.";
            case BuildingID.TRADEHUB:
                return "Produces credits.";
            case BuildingID.SMELTERY:
                return "Increases alloy level.";
            default:
                Debug.LogError("Unknown Buiding ID " + id.ToString() + ".");
                return "";
        }
    }
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
