using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static PlayerManager;
using static PlanetSettings;

public class Planet : MonoBehaviour, IClickableUI
{


    public static Planet planetPrefab;
    public enum PlanetOwner
    {
        NONE,
        PLAYER,
        ENEMY
    }

    public float baseSteelPerTick = DEFAULT_STEEL_PER_TICK;
    public float baseMethanePerTick = DEFAULT_METHANE_PER_TICK;

    public int maxRefineries = DEFAULT_MAX_REFINERIES;
    public int maxShipyards = DEFAULT_MAX_SHIPYARDS;

    public int numRefineries = DEFAULT_STARTING_REFINERIES;
    public int numShipyards = DEFAULT_STARTING_SHIPYARDS;

    public PlanetOwner prevOwner = PlanetOwner.NONE;
    public PlanetOwner owner = DEFAULT_OWNER;

    private PlayerManager playerManager;
    private PlanetInfoUI planetInfoUI;

    public PopupUI targetUI => planetInfoUI;

    public bool IsUIOpen => planetInfoUI.isUIOpen;

    void Awake()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        planetInfoUI = FindObjectOfType<PlanetInfoUI>();
    }

    void Update()
    {
        if (prevOwner != PlanetOwner.PLAYER && owner == PlanetOwner.PLAYER)
        {
            playerManager.AddPlanet(this);
        }
        if (prevOwner == PlanetOwner.PLAYER && owner != PlanetOwner.PLAYER)
        {
            playerManager.RemovePlanet(this);
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

    /// <summary>
    /// Builds a refinery at this planet, if there are more spaces for refineries and if the player
    /// resource pool obtained from <c>PlayerManager</c> contains enough resources given the cost
    /// from <c>PlanetSettings</c>. Will remove the resources if the refinery was successfully built.
    /// </summary>
    public void BuildRefinery()
    {
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
        return;
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
        if (planetInfoUI.isUIOpen)
        {
            return;
        }

        planetInfoUI.Link(this);
        planetInfoUI.OpenUI();
    }

    public void OnUIClose()
    {
        
    }
}