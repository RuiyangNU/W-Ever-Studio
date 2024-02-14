using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepotBuilding : Building
{
    public DepotBuilding(Planet homePlanet)
    {
        this.buildingID = BuildingID.DEPOT;
        this.homePlanet = homePlanet;
        this.level = 1;
    }   

    public override void UpdateTick()
    {

    }
}
