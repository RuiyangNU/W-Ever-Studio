using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static PlayerManager;
using static PlanetSettings;
using static Fleet;
using System.Text.RegularExpressions;
using Unity.VisualScripting;

public class Planet : MonoBehaviour, ISelectable
{
    [SerializeField]
    public static Planet planetPrefab;

    /*
     * References
     */
    private Renderer rend;

    private GameManager gameManager;
    private PlayerManager playerManager;
    private EnemyManager enemyManager;
    private PlanetInfoUI planetInfoUI;
    private HexGrid hexGrid;

    /*
     * Control
     */
    public Owner prevOwner = Owner.NONE;
    public Owner owner = DEFAULT_OWNER;

    public int captureTimer = DEFAULT_CAPTURE_TIMER;

    public int locationCellIndex = -1;

    /*
     * Stats
     */
    public int baseCreditsPerTick = DEFAULT_CREDIT_PER_TICK;
    public int buildingLimit = DEFAULT_BUILDING_LIMIT;

    /*
     * Buildings
     */
    public List<Building> buildings = new List<Building>();

    /*
     * Properties
     */
    public bool IsUIOpen => planetInfoUI.isUIOpen;

    public HexCell CurrentCell => hexGrid.GetCell(locationCellIndex);

    public bool Occupied => CurrentCell.fleet;

    public bool UnderAttack => CurrentCell.fleet && CurrentCell.fleet.owner != owner;

    public bool IsPlayerOwned => owner == Owner.PLAYER;

    public int NumBuildings => buildings.Count;

    public int TurnsUntilCaptured => captureTimer;

    /*************
     *  METHODS
     *************/

    /*
     * Initializers and Updaters
     */
    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>(); 
        playerManager = FindObjectOfType<PlayerManager>();
        enemyManager = FindObjectOfType<EnemyManager>();
        planetInfoUI = FindObjectOfType<PlanetInfoUI>();
        hexGrid = FindObjectOfType<HexGrid>();

        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        Color displayColor = new Color();

        if (owner == Owner.PLAYER){
            displayColor = Color.green * 0.5f;
        }
        if (owner == Owner.ENEMY) {
            displayColor = Color.red * 0.5f;
        }
        if (owner == Owner.NONE){
            displayColor = Color.grey * 0.5f;
        }

        if(planetInfoUI.linkedPlanet == this)
        {
            displayColor *= 2f;
        }

        displayColor.a = 1; // Correct alpha
        rend.material.SetColor("_BaseColor", displayColor);

        //Add to player list if planet is owned by player
        if (prevOwner != Owner.PLAYER && owner == Owner.PLAYER)
        {
            playerManager.AddPlanet(this);
        }
        if (prevOwner == Owner.PLAYER && owner != Owner.PLAYER)
        {
            playerManager.RemovePlanet(this);
        }

        //Add to enemy list if planet is owned by enemy
        if (prevOwner != Owner.ENEMY && owner == Owner.ENEMY)
        {
            enemyManager.AddPlanet(this);
        }
        if (prevOwner == Owner.ENEMY && owner != Owner.ENEMY)
        {
            enemyManager.RemovePlanet(this);
        }

        prevOwner = owner;
    }

    public void UpdateTick()
    {
        // Capturing
        Fleet occupyingFleet = CurrentCell.fleet;
        if (occupyingFleet && occupyingFleet.owner != owner)
        {
            captureTimer--;

            if (captureTimer == 0)
            {
                GetCaptured(occupyingFleet.owner);
            }
        }
        else
        {
            captureTimer = DEFAULT_CAPTURE_TIMER;
        }

        // Buildings
        foreach (Building building in buildings)
        {
            building.UpdateTick();
        }
    }

    public void SetProperties(Owner o)
    {
        this.owner = o;
    }

    public void SetProperties(Owner o, int baseCreditsPerTick, int buildingLimit)
    {
        this.owner = o;
        this.baseCreditsPerTick = baseCreditsPerTick;
        this.buildingLimit = buildingLimit;
    }

    public void SetLocation(int cellLocationIndex)
    {
        this.locationCellIndex = cellLocationIndex;
    }

    /*
     * Buildings
     */
    public void Build(BuildingID buildingID)
    {
        if (buildings.Count >= buildingLimit)
        {
            Debug.LogWarning("Tried to build a new building at " + this.name + ", which has the maximum number of buildings.");
            return;
        }

        if (HasBuilding(buildingID))
        {
            Debug.LogWarning("Tried to build a " + buildingID.ToString() + " at " + this.name + ", which already had one.");
            return;
        }

        int creditCost = Building.BuildCreditCost(buildingID);
        if (playerManager.PlayerCredit < creditCost)
        {
            Debug.LogWarning("Tried to build a " + buildingID.ToString() + " at " + this.name + ", but the player doesn't have enough credits.");
            return;
        }

        int commodityRequirement = Building.BuildCMRequirement(buildingID);
        if (!playerManager.QueryCommodityMilestone(Commodity.CONSTRUCTION, commodityRequirement))
        {
            Debug.LogWarning("Tried to build a " + buildingID.ToString() + " at " + this.name + ", but the player doesn't meet commodity requirements.");
            return;
        }

        playerManager.RemoveCurrencies(new() { { Currency.CREDIT, creditCost } });
        buildings.Add(Building.InitializeBuilding(buildingID, this));
        CustomEvent buildEvent = new CustomEvent();
        buildEvent.eventType = "Construction";
        buildEvent.message = "Build " + buildEvent.SetStringColor(buildingID.ToString(), Color.green);
        NotificationUI.notificationUI.AddNewItem(buildEvent);

    }

    public void UpgradeBuilding(BuildingID buildingID)
    {
        if (!HasBuilding(buildingID))
        {
            Debug.LogError("Tried to upgrade a " + buildingID.ToString() + " at " + this.name + ", which does not have this building.");
        }

        Building toUpgrade = GetBuilding(buildingID);

        int creditCost = toUpgrade.UpgradeCreditCost();
        if (playerManager.PlayerCredit < creditCost)
        {
            Debug.LogWarning("Tried to upgrade a " + buildingID.ToString() + " at " + this.name + ", but the player doesn't have enough credits.");
            return;
        }

        Dictionary<Commodity, int> commodityRequirement = toUpgrade.UpgradeCommodityRequirement();
        if (!playerManager.QueryCommodityMilestones(commodityRequirement))
        {
            Debug.LogWarning("Tried to upgrade a " + buildingID.ToString() + " at " + this.name + ", but the player doesn't meet commodity requirements.");
            return;
        }

        playerManager.RemoveCurrencies(new() { { Currency.CREDIT, creditCost } });
        toUpgrade.Upgrade();
    }

    /// <summary>
    /// Determines whether the player can build a specified building.
    /// </summary>
    /// <remarks>
    /// This function checks if the player meets all the necessary conditions to build a building.
    /// It returns 0 if the player can build the building. Otherwise, it returns an integer between 1 and 4,
    /// each representing a different reason why the building cannot be constructed.
    /// </remarks>
    /// <returns>
    /// An integer indicating the ability to build the building:
    /// <list type="bullet">
    /// <item>
    /// <description>0: The player can build the building.</description>
    /// </item>
    /// <item>
    /// <description>1: The player already has a building of this type.</description>
    /// </item>
    /// <item>
    /// <description>2: The planet has reached its building limit.</description>
    /// </item>
    /// <item>
    /// <description>3: The player does not have enough credits.</description>
    /// </item>
    /// <item>
    /// <description>4: The player does not meet the commodity requirements.</description>
    /// </item>
    /// </list>
    /// </returns>
    public int CanBuild(BuildingID buildingID)
    {
        if (HasBuilding(buildingID))
        {
            return 1;
        }
        else if (buildings.Count >= buildingLimit)
        {
            return 2;
        }
        else if (playerManager.PlayerCredit < Building.BuildCreditCost(buildingID))
        {
            return 3;
        }
        else if (!playerManager.QueryCommodityMilestone(Commodity.CONSTRUCTION, Building.BuildCMRequirement(buildingID)))
        {
            return 4;
        }
        return 0;
    }

    public static string GetBuildFailureReason(int reasonCode)
    {
        return reasonCode switch
        {
            0 => "Able to build",
            1 => "Already constructed",
            2 => "No slots available",
            3 => "Insufficient credits",
            4 => "Insufficient construction milestone",
            _ => "Unknown reason - report to devs!",
        };
    }

    public bool HasBuilding(BuildingID buildingID)
    {
        foreach (Building building in buildings)
        {
            if (building.ID == buildingID)
            {
                return true;
            }
        }

        return false;
    }

    public Building GetBuilding(BuildingID buildingID)
    {
        foreach (Building building in buildings)
        {
            if (building.ID == buildingID)
            {
                return building;
            }
        }

        return null;
    }

    public int GetBuildingLevel(BuildingID buildingID)
    {
        foreach (Building building in buildings)
        {
            if (building.ID == buildingID)
            {
                return building.Level;
            }
        }

        return -1;
    }

    /*
     * Shipyard
     */
    public void BuildShip(ShipID shipID)
    {
        if (!HasBuilding(BuildingID.SHIPYARD))
        {
            Debug.LogError("");
            return;
        }

        ShipyardBuilding shipyard = (ShipyardBuilding)GetBuilding(BuildingID.SHIPYARD);
        if (shipyard.IsActive)
        {
            Debug.LogError("");
            return;
        }
        else if (playerManager.PlayerCredit < Fleet.BuildCreditCost(shipID))
        {
            Debug.LogError("");
            return;
        }
        else if (!playerManager.QueryCommodityMilestone(Commodity.ALLOY, Fleet.BuildAlloyRequirement(shipID)))
        {
            Debug.LogError("");
            return;
        }
        else if (!shipyard.CanBuild(shipID))
        {
            Debug.LogError("");
            return;
        }

        playerManager.RemoveCurrencies(new Dictionary<Currency, int>() { { Currency.CREDIT, Fleet.BuildCreditCost(shipID) } });
        shipyard.BuildShip(shipID);
    }

    /// <summary>
    /// Determines whether the player can build a specified ship.
    /// </summary>
    /// <remarks>
    /// This function checks if the player meets all the necessary conditions to build a ship.
    /// It returns 0 if the player can build the ship. Otherwise, it returns an integer between 1 and 4,
    /// each representing a different reason why the ship cannot be constructed.
    /// </remarks>
    /// <returns>
    /// An integer indicating the ability to build the ship:
    /// <list type="bullet">
    /// <item>
    /// <description>0: The player can build the ship.</description>
    /// </item>
    /// <item>
    /// <description>1: The planet does not have a shipyard.</description>
    /// </item>
    /// <item>
    /// <description>2: The planet's shipyard is occupied.</description>
    /// </item>
    /// <item>
    /// <description>3: The player does not have enough credits.</description>
    /// </item>
    /// <item>
    /// <description>4: The player does not meet the commodity requirements.</description>
    /// </item>
    /// <item>
    /// <description>5: The player does not have the needed research.</description>
    /// </item>
    /// <item>
    /// <description>6: The shipyard's level is not high enough.</description>
    /// </item>
    /// </list>
    /// </returns>
    public int CanBuildShip(ShipID shipID)
    {
        if (!HasBuilding(BuildingID.SHIPYARD))
        {
            return 1;
        }

        ShipyardBuilding shipyard = (ShipyardBuilding)GetBuilding(BuildingID.SHIPYARD);
        if (shipyard.IsActive)
        {
            return 2;
        }
        else if (playerManager.PlayerCredit < Fleet.BuildCreditCost(shipID))
        {
            return 3;
        }
        else if (!playerManager.QueryCommodityMilestone(Commodity.ALLOY, Fleet.BuildAlloyRequirement(shipID)))
        {
            return 4;
        }
        else if (!playerManager.HasResearchForShip(shipID))
        {
            return 5;
        }
        else if (!shipyard.CanBuild(shipID))
        {
            return 6;
        }
        

        return 0;
    }

    /*
     * Controls
     */
    public void SpawnPlayerFleet(ShipID shipID)
    {
        if (owner != Owner.PLAYER)
        {
            Debug.LogWarning("Spawning a player controlled ship at a non-player controlled planet.");
        }

        gameManager.CreateFleet(CurrentCell, Owner.PLAYER, shipID);
    }

    /*
     * Resources
     */
    public Dictionary<Currency, int> GetTickCurrencies()
    {
        // Base generation
        Dictionary<Currency, int> generated = new()
        {
            { Currency.CREDIT, baseCreditsPerTick },
            { Currency.RESEARCH, 0 }
        };

        // Building generation
        foreach (Building building in buildings)
        {
            Dictionary<Currency, int> currencies = building.GetTickCurrencies();

            foreach (Currency c in currencies.Keys)
            {
                generated[c] += currencies[c];
            }
        }

        return generated;
    }

    public int GetTickCurrency(Currency c)
    {
        return GetTickCurrencies()[c];
    }

    public Dictionary<Commodity, int> GetAllCommodities()
    {
        Dictionary<Commodity, int> allCommodities = new()
        {
            { Commodity.CONSTRUCTION, 0 },
            { Commodity.ALLOY, 0 }
        };

        foreach (Building b in buildings)
        {
            Dictionary<Commodity, int> commodities = b.Commodities;

            foreach (Commodity c in commodities.Keys)
            {
                allCommodities[c] += commodities[c];
            }
        }

        return allCommodities;
    }

    /*
     * Combat
     */
    public void GetCaptured(Owner newOwner)
    {
        if (newOwner == this.owner)
        {
            Debug.LogError("GetCaptured was called on " + this.name + ", but the new owner was the same as its current owner.");
            return;
        }

        buildings = new List<Building>();

        this.owner = newOwner;
        if(newOwner == Owner.PLAYER)
        {
            hexGrid.IncreaseVisibility(CurrentCell, 2);
        }
        else
        {
            hexGrid.DecreaseVisibility(CurrentCell, 2);
        }
        
        this.captureTimer = DEFAULT_CAPTURE_TIMER;
    }

    /*
     * UI
     */
    public void OnSelect()
    {
        OpenUI();
    }

    public void OnDeselect() { }

    //TODO: REMOVE!!
    public void OnMouseDown()
    {
        //OpenUI();
    }

    public void OpenUI()
    {
        planetInfoUI.Link(this);
        planetInfoUI.OpenUI();
    }

    public void OnUIClose()
    {
        
    }
}
