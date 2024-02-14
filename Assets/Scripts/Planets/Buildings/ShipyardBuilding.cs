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
    }

    public override void UpdateTick()
    {

    }
}
