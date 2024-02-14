using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchLabBuilding : Building
{
    public ResearchLabBuilding(Planet homePlanet)
    {
        this.buildingID = BuildingID.LAB;
        this.homePlanet = homePlanet;
        this.level = 1;
    }

    public override void UpdateTick()
    {

    }
}
