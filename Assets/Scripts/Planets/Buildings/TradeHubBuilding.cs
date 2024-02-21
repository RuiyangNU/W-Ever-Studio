using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeHubBuilding : Building
{
    public TradeHubBuilding(Planet homePlanet)
    {
        this.buildingID = BuildingID.TRADEHUB;
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
        switch (this.level)
        {
            case 1:
                return new()
                {
                    { Currency.CREDIT, 100},
                    { Currency.RESEARCH, 0 }

                };
            case 2:
                return new()
                {
                    { Currency.CREDIT, 200 },
                    { Currency.RESEARCH, 0 }

                };
            default:
                Debug.LogError("Unknown currency production for a level " + this.level + " trade hub");
                return null;

        }
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
                Debug.LogError("Unknown commodity requirement to upgrade a level " + this.level + " trade Hub.");
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
                Debug.LogError("Unknown credit requirement to upgrade a level " + this.level + " trade hub.");
                return int.MaxValue;
        }
    }
}
