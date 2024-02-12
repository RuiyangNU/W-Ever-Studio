using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static PlayerManager;
using static PlanetSettings;
using static Fleet;

public class Planet : MonoBehaviour, IClickableUI
{
    [SerializeField]
    public static Planet planetPrefab;

    public enum PlanetOwner
    {
        NONE,
        PLAYER,
        ENEMY
    }

    /*
     * Control
     */
    private Renderer rend;

    private GameManager gameManager;
    private PlayerManager playerManager;
    private EnemyManager enemyManager;
    private PlanetInfoUI planetInfoUI;
    private HexGrid hexGrid;

    public PlanetOwner prevOwner = PlanetOwner.NONE;
    public PlanetOwner owner = DEFAULT_OWNER;

    public int locationCellIndex = -1;

    /*
     * Stats
     */
    public float baseSteelPerTick = DEFAULT_STEEL_PER_TICK;
    public float baseMethanePerTick = DEFAULT_METHANE_PER_TICK;

    public int maxRefineries = DEFAULT_MAX_REFINERIES;
    public int maxShipyards = DEFAULT_MAX_SHIPYARDS;

    public int numRefineries = DEFAULT_STARTING_REFINERIES;
    public int numShipyards = DEFAULT_STARTING_SHIPYARDS;

    /*
     * Shipyard
     */
    public ShipID shipyardFleet = ShipID.NONE;
    public int shipyardTurnsLeft = -1;

    public PopupUI targetUI => planetInfoUI;

    public bool IsUIOpen => planetInfoUI.isUIOpen;

    public HexCell CurrentCell => hexGrid.GetCell(locationCellIndex);


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

        if (owner == PlanetOwner.PLAYER){
            displayColor = Color.green * 0.5f;
        }
        if (owner == PlanetOwner.ENEMY) {
            displayColor = Color.red * 0.5f;
        }
        if (owner == PlanetOwner.NONE){
            displayColor = Color.grey * 0.5f;
        }

        if (planetInfoUI.linkedPlanet == this)
        {
            displayColor *= 2f;
        }

        displayColor.a = 1; // Correct alpha
        rend.material.SetColor("_BaseColor", displayColor);

        //Add to player list if planet is owned by player
        if (prevOwner != PlanetOwner.PLAYER && owner == PlanetOwner.PLAYER)
        {
            playerManager.AddPlanet(this);
        }
        if (prevOwner == PlanetOwner.PLAYER && owner != PlanetOwner.PLAYER)
        {
            playerManager.RemovePlanet(this);
        }

        //Add to enemy list if planet is owned by enemy
        if (prevOwner != PlanetOwner.ENEMY && owner == PlanetOwner.ENEMY)
        {
            enemyManager.AddPlanet(this);
        }
        if (prevOwner == PlanetOwner.ENEMY && owner != PlanetOwner.ENEMY)
        {
            enemyManager.RemovePlanet(this);
        }

        prevOwner = owner;
    }

    public void SetProperties(PlanetOwner o = PlanetOwner.NONE, float baseSteelPerTick = 1.0f, float baseMethanePerTick = 1.0f, int maxRefineries = 1, int maxShipyards = 1)
    {
        this.owner = o;
        this.baseSteelPerTick = baseSteelPerTick;
        this.baseMethanePerTick = baseMethanePerTick;
        this.maxRefineries = maxRefineries;
        this.maxShipyards = maxShipyards;
    }

    public void SetLocation(int cellLocationIndex)
    {
        this.locationCellIndex = cellLocationIndex;
    }

    /// <summary>
    /// Builds a refinery at this planet, if there are more spaces for refineries and if the player
    /// resource pool obtained from <c>PlayerManager</c> contains enough resources given the cost
    /// from <c>PlanetSettings</c>. Will remove the resources if the refinery was successfully built.
    /// </summary>
    public void BuildRefinery()
    {
        if (owner != PlanetOwner.PLAYER)
        {
            Debug.LogWarning("Tried to build a refinery, but the planet was not owned by the player.");
            return;
        }

        if (numRefineries >= maxRefineries)
        {
            Debug.LogWarning("Tried to build a refinery, but the planet already has the maximum number of refineries.");
            return;
        }

        Dictionary<PlayerResource, float> cost = PlanetSettings.refineryCost;
        if (playerManager.QueryResources(cost))
        {
            // Enough resources, build refinery
            playerManager.RemoveFromResourcePool(cost);
            numRefineries++;

            planetInfoUI.UpdateUI();
        }

        return;
    }

    /// <summary>
    /// Builds a shipyard at this planet, if there are more spaces for refineries and if the player
    /// resource pool obtained from <c>PlayerManager</c> contains enough resources given the cost
    /// from <c>PlanetSettings</c>. Will remove the resources if the shipyard was successfully built.
    /// </summary>
    public void BuildShipyard()
    {
        if (owner != PlanetOwner.PLAYER)
        {
            Debug.LogWarning("Tried to build a shipyard, but the planet was not owned by the player.");
            return;
        }

        if (numShipyards >= maxShipyards)
        {
            Debug.LogWarning("Tried to build a shipyard, but the planet already has the maximum number of shipyards.");
            return;
        }

        Dictionary<PlayerResource, float> cost = PlanetSettings.shipyardCost;
        if (playerManager.QueryResources(cost))
        {
            // Enough resources, build refinery
            playerManager.RemoveFromResourcePool(cost);
            numShipyards++;

            planetInfoUI.UpdateUI();
        }

        return;
    }

    public void BuildShip(ShipID shipID)
    {
        if (owner != PlanetOwner.PLAYER)
        {
            Debug.LogWarning("Tried to build a ship, but the planet was not owned by the player.");
            return;
        }

        if (numShipyards == 0)
        {
            Debug.LogWarning("Tried to build a ship, but the planet has no shipyards.");
            return;
        }

        if (shipyardFleet != ShipID.NONE)
        {
            return;
        }

        Dictionary<PlayerResource, float> cost = FleetSettings.GetShipCost(shipID);
        if (playerManager.QueryResources(cost))
        {
            // Enough resources, build ship
            playerManager.RemoveFromResourcePool(cost);
            shipyardFleet = shipID;
            shipyardTurnsLeft = FleetSettings.TurnsToBuild(shipID);

            planetInfoUI.UpdateUI();
        }
    }

    public void SpawnPlayerShip(ShipID shipID)
    {
        if (owner != PlanetOwner.PLAYER)
        {
            Debug.LogWarning("Spawning a player controlled ship at a non-player controlled planet.");
        }

        switch (shipID)
        {
            case ShipID.NONE:
                Debug.LogError("Tried to spawn a player controlled ship, but the ship type was NONE.");
                return;

            case ShipID.DESTROYER:
                gameManager.CreateFleet(CurrentCell, FleetOwner.PLAYER);
                break;
        }
    }

    public void OnClick()
    {
        OpenUI();
    }

    public void OnMouseDown()
    {
        OpenUI();
    }

    public void UpdateTick()
    {
        if (shipyardTurnsLeft > 0)
        {
            if (shipyardFleet == ShipID.NONE)
            {
                Debug.LogError("Shipyard timer was counting down, but the ship type is NONE.");
            }
            shipyardTurnsLeft--;
        }
        else if (shipyardTurnsLeft == 0)
        {
            if (shipyardFleet == ShipID.NONE)
            {
                Debug.LogError("Shipyard tried to spawn a ship, but the ship type is NONE.");
            }
            else
            {
                SpawnPlayerShip(shipyardFleet);
            }

            shipyardTurnsLeft = -1;
            shipyardFleet = ShipID.NONE;
        }

        if (planetInfoUI.linkedPlanet == this)
        {
            planetInfoUI.UpdateUI();
        }
    }

    public Dictionary<PlayerResource, float> GetTickResources()
    {
        Dictionary<PlayerResource, float> generatedResources = new Dictionary<PlayerResource, float>();
        generatedResources.Add(PlayerResource.METHANE, baseMethanePerTick * (1 + numRefineries));
        generatedResources.Add(PlayerResource.STEEL, baseSteelPerTick * (1 + numRefineries));

        return generatedResources;
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