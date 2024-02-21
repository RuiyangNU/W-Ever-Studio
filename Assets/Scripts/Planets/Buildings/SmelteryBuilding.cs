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
        throw new System.NotImplementedException();
    }

    public override int UpgradeCreditCost()
    {
        throw new System.NotImplementedException();
    }
}
