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
        switch (this.level)
        {
            case 1:
                return new()
                {
                    { Commodity.CONSTRUCTION, 1 },
                    { Commodity.ALLOY, 0 }

                };
            case 2:
                return new()
                {
                    { Commodity.CONSTRUCTION, 2 },
                    { Commodity.ALLOY, 0 }

                };
            default:
                Debug.LogError("Unknown resource production for a level "+ this.level + " depot" );
                return null;

        }
        
    }

    public override Dictionary<Currency, int> GetTickCurrencies()
    {
        return new();
        /*
        switch (this.level)
        {
            case 1:
                return new()
                {
                    { Currency.CREDIT, 0 },
                    { Currency.RESEARCH, 0 }

                };
            case 2:
                return new()
                {
                    { Currency.CREDIT, 0 },
                    { Currency.RESEARCH, 0 }

                };
            default:
                Debug.LogError("Unknown currency production for a level " + this.level + " depot");
                return null;

        }
        */
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
