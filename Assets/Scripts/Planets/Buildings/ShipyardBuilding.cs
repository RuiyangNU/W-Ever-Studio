 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipyardBuilding : Building
{
    [SerializeField]
    private int turnsLeft;

    private ShipID buildingShip;

    public ShipID BuildingShip { get => buildingShip; }

    public int TurnsLeft { get => turnsLeft; }

    public bool IsActive => turnsLeft != -1;

    public ShipyardBuilding(Planet homePlanet)
    {
        this.buildingID = BuildingID.SHIPYARD;
        this.homePlanet = homePlanet;
        this.level = 1;
        this.maxLevel = 2;
        this.turnsLeft = -1;

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
        turnsLeft = Mathf.Max(0, turnsLeft-1);

        if (turnsLeft == 0 && !(homePlanet.Occupied))
        {
            homePlanet.SpawnPlayerFleet(buildingShip);
            this.turnsLeft = -1;
        }
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

    public static int BuildTime(ShipID id)
    {

         switch (id)
        {
            case ShipID.MONO: return 3;
            case ShipID.FLARE: return 5;
            case ShipID.SPARK: return 5;
            case ShipID.PULSE: return 5;
            case ShipID.EMBER: return 8;
            case ShipID.VOLT: return 8;
            case ShipID.BLAST: return 8;

            default:
                Debug.LogError("Unknown ship ID.");
                return -1;
        }
    }

    public void BuildShip(ShipID id)
    {
        if (IsActive)
        {
            Debug.LogError("Tried to build a ship at " + homePlanet.name + ", but the shipyard is already building a ship.");
            return;
        }
        if (CanBuild(id) != true)
        {
            Debug.LogError("Cannot build" + id.ToString() + " ship with your current shipyard level.");
            return;
        }
        turnsLeft = BuildTime(id);
        buildingShip = id;

    }

    public bool CanBuild(ShipID id) {
        switch(id){
            case ShipID.MONO: return true;
            case ShipID.FLARE: return true;
            case ShipID.SPARK: return true;
            case ShipID.PULSE: return true;
            case ShipID.EMBER: return this.level >= 2;
            case ShipID.VOLT: return this.level >= 2;
            case ShipID.BLAST: return this.level >= 2;

            default:
                Debug.LogError("Unknown ship ID.");
                return false;
        }
    }
}
