using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static PlayerManager;

public class Planet : MonoBehaviour, IClickableUI
{

    public enum PlanetOwner
    {
        NONE,
        PLAYER,
        ENEMY
    }

    public float baseSteelPerTick = 0.0f;
    public float baseMethanePerTick = 0.0f;

    public int refineryLimit = 0;
    public int shipyardLimit = 0;

    public int numRefineries = 0;
    public int numShipyards = 0;

    public PlanetOwner owner = PlanetOwner.NONE;

    private PlanetInfoUI planetInfoUI;

    public PopupUI targetUI => planetInfoUI;

    public bool IsUIOpen => planetInfoUI.isUIOpen;

    // Start is called before the first frame update
    void Start()
    {
        planetInfoUI = FindObjectOfType<PlanetInfoUI>();
    }

    public void SetProperties(PlanetOwner o, float baseSteelPerTick, float baseMethanePerTick, int refineryLimit = 0, int shipyardLimit = 0)
    {
        this.owner = o;
        this.baseSteelPerTick = baseSteelPerTick;
        this.baseMethanePerTick = baseMethanePerTick;
        this.refineryLimit = refineryLimit;
        this.shipyardLimit = shipyardLimit;
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

    public Dictionary<PlayerResource, float> GetResources()
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