using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmelteryBuilding : Building
{
    public SmelteryBuilding(Planet homePlanet)
    {
        this.buildingID = BuildingID.SMELTERY;
        this.homePlanet = homePlanet;
        this.level = 1;
        this.maxLevel = 2;
    }

    public override Dictionary<Commodity, int> GetCommodities()
    {
        switch (this.level)
        {
            case 1:
                return new()
                {
                    { Commodity.CONSTRUCTION, 0 },
                    { Commodity.ALLOY, 1 }

                };
            case 2:
                return new()
                {
                    { Commodity.CONSTRUCTION, 0 },
                    { Commodity.ALLOY, 2 }

                };
            default:
                Debug.LogError("Unknown resource production for a level " + this.level + " smeltery");
                return null;

        }
    }

    public override Dictionary<Currency, int> GetTickCurrencies()
    {
        return new();
        
    }

    public override void UpdateTick()
    {
        return;
    }

    public override Dictionary<Commodity, int> UpgradeCommodityRequirement()
    {
        switch (this.level)
        {
            case 1:
                return new()
                {
                    { Commodity.CONSTRUCTION, 2 },
                    { Commodity.ALLOY, 0 }
                };
            default:
                Debug.LogError("Unknown commodity requirement to upgrade a level " + this.level + " smeltery.");
                return null;
        }
    }

    public override int UpgradeCreditCost()
    {
        switch (this.level)
        {
            case 1:
                return 2000;
            default:
                Debug.LogError("Unknown credit requirement to upgrade a level " + this.level + " smeltery.");
                return int.MaxValue;
        }
    }
}
