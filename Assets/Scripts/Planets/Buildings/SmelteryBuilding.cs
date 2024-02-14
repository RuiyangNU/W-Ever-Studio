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

    public override void UpdateTick()
    {

    }
}
