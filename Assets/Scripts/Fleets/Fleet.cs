using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static PlayerManager;
using static FleetSettings;

public abstract class Fleet : MonoBehaviour, ISelectable
{
    /*
     * References
     */
    public HexUnit hexUnit;

    private PlayerManager playerManager;
    private FleetInfoUI fleetInfoUI;

    public bool IsUIOpen => fleetInfoUI.isUIOpen;
    /*
     * Stats
     */
    public float maxHull;
    public float maxShield;
    public int maxActionPoints;

    [SerializeField]
    private float hull;
    [SerializeField]
    private float shield;
    [SerializeField]
    private int actionPoints;

    public DamageType damageType;
    [SerializeField]
    private float damage;

    public Owner owner;
    public ShipID shipID;

    public float Hull { get => hull; }
    public float Shield { get => shield; }
    public float Damage { get => damage; }
    public float ActionPoints { get => ActionPoints; }

    /*
     * Control
     */
    public EnemyAiTask enemyTask = null;

    /*
     * Methods
     */
    void Awake()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        fleetInfoUI = FindObjectOfType<FleetInfoUI>();
    }

    // grid call travel(path) on HexUnit, Hexunit will determine an action type, if it is movement, retrieve coordinates and
    // pass those coordinates into MoveTo(vector 3 corrds)
    public void MoveTo(Vector3 hexUnitCoord)
    {
        this.transform.position = hexUnitCoord;
    }  

    public void UpdateTick()
    {
        RestoreActionPoints();

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
    public void OnSelect()
    {
        OpenUI();
    }

    public void OnDeselect() { }

    // TODO: REMOVE!!
    public void OnMouseDown()
    {
        OnSelect();
    }

    public void OpenUI()
    {
        fleetInfoUI.Link(this);
        fleetInfoUI.OpenUI();
    }

    public void OnUIClose()
    {

    }

    /*
     * Modifiers
     */
    public void AddHull(float n)
    {
        this.hull = Mathf.Min(this.maxHull, this.hull + n);
    }

    public void RemoveHull(float n)
    { 
        this.hull = Mathf.Max(0, this.hull - n);
    }

    public void AddShield(float n)
    {
        this.shield = Mathf.Min(this.maxShield, this.shield + n);
    }

    public void RemoveShield(float n)
    {
        this.shield = Mathf.Max(0, this.shield - n);
    }

    public void AddActionPoints(int n)
    {
        Debug.LogWarning("Adding " + n.ToString() + " action points to " + this.name + ". Use RestoreActionPoints() instead to restore this unit's AP.");
        this.actionPoints = Mathf.Min(this.maxActionPoints, this.actionPoints + n);
    }

    public void RemoveActionPoints(int n)
    {
        this.actionPoints = Mathf.Max(0, this.actionPoints - n);
    }

    public void RestoreActionPoints()
    {
        this.actionPoints = this.maxActionPoints;
    }
}

public enum ShipID
{
    // Tech 0
    MONO,

    // Tech 1
    FLARE,
    SPARK,
    PULSE,

    // Tech 2
    EMBER,
    BOLT,
    BLAST
}
public enum DamageType
{
    KINETIC,
    THERMAL,
    EM
}
