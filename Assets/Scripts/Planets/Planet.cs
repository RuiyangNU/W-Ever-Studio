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

        if (planetInfoUI.linkedPlanet == this)
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

    //TODO: Add Resource costs
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

        buildings.Add(Building.InitializeBuilding(buildingID, this));
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
    public Dictionary<Currency, int> GetTickCurrency()
    {
        Dictionary<Currency, int> generated = new Dictionary<Currency, int>();

        generated.Add(Currency.CREDIT, baseCreditsPerTick);
        //TODO: Add research points

        return generated;
    }

    public Dictionary<Commodity, int> GetAllCommodities()
    {
        Dictionary<Commodity, int> allCommodities = new Dictionary<Commodity, int>();

        foreach (Building b in buildings)
        {
            Dictionary<Commodity, int> commodities = b.Commodities;

            foreach (Commodity c in commodities.Keys)
            {
                if (!allCommodities.ContainsKey(c))
                {
                    allCommodities.Add(c, commodities[c]);
                }
                else
                {
                    allCommodities[c] += commodities[c];
                }
            }
        }

        return allCommodities;
    }

    /*
     * Combat
     */
    public void GetCaptured(Owner newOwner)
    {
        if (newOwner == this.owner) { return; }

        buildings = new List<Building>();

        this.owner = newOwner;
        hexGrid.DecreaseVisibility(CurrentCell, 2);
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
        OpenUI();
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
