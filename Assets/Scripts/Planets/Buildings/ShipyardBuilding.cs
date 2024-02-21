using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipyardBuilding : Building
{
    public ShipyardBuilding(Planet homePlanet)
    {
        this.buildingID = BuildingID.SHIPYARD;
        this.homePlanet = homePlanet;
        this.level = 1;
        this.maxLevel = 2;

    }

    public override Dictionary<Commodity, int> GetCommodities()
    {
        return new();
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
                    { Commodity.CONSTRUCTION, 1 },
                    { Commodity.ALLOY, 0 }
                };
            default:
                Debug.LogError("Unknown commodity requirement to upgrade a level " + this.level + " ship yard.");
                return null;
        }
    }

    public override int UpgradeCreditCost()
    {
        switch (this.level)
        {
            case 1:
                return 5000;
            default:
                Debug.LogError("Unknown credit requirement to upgrade a level " + this.level + " ship yard.");
                return int.MaxValue;
        }
    }
}
