using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static PlayerManager;
using static FleetSettings;

public class Fleet : MonoBehaviour, IClickableUI
{
    // types of owners for a fleet
    public enum FleetOwner
    {
        PLAYER,
        ENEMY
    }

    // properties of an indiviual fleet
    public float health = 50.0f;
    public float damage = 50.0f;
    public float speed = 50.0f;
    public int actionPoints = 3;
    public FleetOwner owner;

    public static Fleet fleetPrefab;

    // getters and setters for stats
    public float Health
    {
        get => health;
        set => health = Mathf.Max(0, value);
    }

    public float Damage
    {
        get => damage;
        set => damage = Mathf.Max(0, value);
    }

    public float Speed
    {
        get => speed;
        set => speed = Mathf.Max(0, value);
    }

    public int ActionPoints
    {
        get => actionPoints;
        set => actionPoints = value;
    }

    // reference to the HexUnit class
    public HexUnit hexUnit;

    // fleet limiting per unit
    public int numFleets = DEFAULT_STARTING_FLEETS;
    public int maxFleets = 5;

    private PlayerManager playerManager;
    private FleetInfoUI fleetInfoUI;

    public PopupUI targetUI => fleetInfoUI;

    public bool IsUIOpen => fleetInfoUI.isUIOpen;

    void Awake()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        fleetInfoUI = FindObjectOfType<FleetInfoUI>();
    }

    // set the intial properties of a fleet
    public void SetProperties(FleetOwner o, float health, float damage, float speed, int maxFleets, int actionPoints)
    {
        this.owner = o;
        this.health = health;
        this.damage = damage;
        this.speed = speed;
        this.actionPoints = actionPoints;
        this.maxFleets = maxFleets;
    }

    // grid call travel(path) on HexUnit, Hexunit will determine an action type, if it is movement, retrieve coordinates and
    // pass those coordinates into MoveTo(vector 3 corrds)
    public void MoveTo(Vector3 hexUnitCoord)
    {
        this.transform.position = hexUnitCoord;
    }

    // function that takes the result of combat or movement and updates its parameters

    // function that makes a hex unit that represents itself

    // make a fleet
    public void BuildFleet()
    {
        if (numFleets >= maxFleets)
        {
            Debug.LogWarning("This cell is filled with fleets.");
            return;
        }

        Dictionary<PlayerResource, float> cost = FleetSettings.fleetCost;
        if (playerManager.QueryResources(cost))
        {
            playerManager.RemoveFromResourcePool(cost);

            // need to instantiate the fleet prefab here so it shows up on the map


            numFleets++;


            fleetInfoUI.UpdateUI();
        }

        return;
    }

    // maybe move this to game manager
    public void AttackFleet(Fleet yourFleet, Fleet targetFleet)
    {
        targetFleet.health -= yourFleet.damage;

        if (targetFleet.health <= 0)
        {
            targetFleet.DestroyFleet();
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
        if (health == 0)
        {
            DestroyFleet();
        }
        actionPoints = 3;
        // reset combat flag
        return;
    }

    public void DestroyFleet()
    {
        HexCell location = hexUnit.Grid.GetCell(hexUnit.locationCellIndex);
        location.fleet = null;
        hexUnit.Die();
        Destroy(gameObject);
    }

    public void OpenUI()
    {
        if (fleetInfoUI.isUIOpen)
        {
            return;
        }

        fleetInfoUI.Link(this);
        fleetInfoUI.OpenUI();
    }

    public void OnUIClose()
    {

    }
}
