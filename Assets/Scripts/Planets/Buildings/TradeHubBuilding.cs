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
    }

    public override void UpdateTick()
    {

    }
}
