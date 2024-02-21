using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DepotBuilding : Building
{

    public DepotBuilding(Planet homePlanet)
    {
        this.buildingID = BuildingID.DEPOT;
        this.homePlanet = homePlanet;
        this.level = 1;
        this.maxLevel = 2;
    }

    public override Dictionary<Commodity, int> GetCommodities()
    {
        throw new System.NotImplementedException();
    }

    public override Dictionary<Currency, int> GetTickCurrencies()
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateTick()
    {

    }

    public override Dictionary<Commodity, int> UpgradeCommodityRequirement()
    {
        switch (this.level)
        {
            case 1:
                return new()
                {
                    { Commodity.CONSTRUCTION, 0 },
                    { Commodity.ALLOY, 0 }
                };
            default:
                Debug.LogError("Unknown commodity requirement to upgrade a level " +  this.level + " depot.");
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
                Debug.LogError("Unknown credit requirement to upgrade a level " + this.level + " depot.");
                return int.MaxValue;
        }
    }
}
