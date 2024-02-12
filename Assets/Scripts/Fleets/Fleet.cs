using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static PlayerManager;
using static FleetSettings;

public class Fleet : MonoBehaviour, IClickableUI
{
    public enum ShipID
    {
        NONE,
        DESTROYER
    }

    // types of owners for a fleet
    public enum FleetOwner
    {
        PLAYER,
        ENEMY
    }

    // properties of an indiviual fleet
    private float health = 50.0f;
    private float damage = 50.0f;
    private float speed = 50.0f;
    private int actionPoints = 3;

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

    private PlayerManager playerManager;
    private FleetInfoUI fleetInfoUI;

    public PopupUI targetUI => fleetInfoUI;

    public bool IsUIOpen => fleetInfoUI.isUIOpen;

    private Renderer rend;

    void Awake()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        fleetInfoUI = FindObjectOfType<FleetInfoUI>();
        rend = GetComponent<Renderer>();
    }

    // set the intial properties of a fleet
    public void SetProperties(FleetOwner o, float health, float damage, float speed, int actionPoints)
    {
        this.owner = o;
        this.health = health;
        this.damage = damage;
        this.speed = speed;
        this.actionPoints = actionPoints;
    }

    // grid call travel(path) on HexUnit, Hexunit will determine an action type, if it is movement, retrieve coordinates and
    // pass those coordinates into MoveTo(vector 3 corrds)
    public void MoveTo(Vector3 hexUnitCoord)
    {
        this.transform.position = hexUnitCoord;
    }

    // function that takes the result of combat or movement and updates its parameters

    // function that makes a hex unit that represents itself

   

    public void UpdateTick()
    {
        actionPoints = 3;
        UpdateUIIfLinked();

        return;
    }

    public void DestroyFleet()
    {
        HexCell location = hexUnit.Grid.GetCell(hexUnit.locationCellIndex);
        location.fleet = null;
        hexUnit.Die();

        if (fleetInfoUI.linkedFleet == this)
        {
            fleetInfoUI.CloseUI();
        }

        Destroy(gameObject);
    }

    /*
     * UI
     */

    public void OnClick()
    {
        OpenUI();
    }

    public void OnMouseDown()
    {
        OnClick();
    }

    public void OpenUI()
    {
        if (!UpdateUIIfLinked())
        {
            fleetInfoUI.Link(this);
            fleetInfoUI.OpenUI();
        }
    }

    private bool UpdateUIIfLinked()
    {
        if (IsUIOpen && fleetInfoUI.linkedFleet == this)
        {
            fleetInfoUI.UpdateUI();
            return true;
        }

        return false;
    }

    public void OnUIClose()
    {

    }

    /*
     * Modifiers
     */

    public void AddHealth(float h)
    {
        this.health += h;
        UpdateUIIfLinked();
    }

    public void RemoveHealth(float h)
    {
        this.health -= h;
        UpdateUIIfLinked();
    }

    public void RemoveActionPoints(int ap)
    {
        this.actionPoints = Mathf.Max(0, this.actionPoints - ap);
        UpdateUIIfLinked();
    }
}
