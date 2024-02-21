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
    private EnemyManager enemyManager;
    private FleetInfoUI fleetInfoUI;

    public bool IsUIOpen => fleetInfoUI.isUIOpen;


    public bool IsPlayerOwned => owner == Owner.PLAYER;
    /*
     * Stats
     */
    public float maxHull;
    public float maxShield;
    public int maxActionPoints;

    [SerializeField]
    protected float hull;
    [SerializeField]
    protected float shield;
    [SerializeField]
    protected int actionPoints;

    public DamageType damageType;
    [SerializeField]
    protected float damage;

    public int thermalRes;
    public int kineticRes;
    public int emRes;

    public float Hull { get => hull; }
    public float Shield { get => shield; }
    public float Damage { get => damage; }
    public float ActionPoints { get => actionPoints; }

    /*
     * Control
     */
    public EnemyAiTask enemyTask = null;

    public Owner owner;
    private Owner prevOwner = Owner.NONE;
    public ShipID shipID;

    /*
     * Methods
     */
    protected void InitializeReferences()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        enemyManager = FindObjectOfType<EnemyManager>();
        fleetInfoUI = FindObjectOfType<FleetInfoUI>();
    }

    void Update()
    {
        //Add to enemy list if fleet is owned by enemy
        if (prevOwner != Owner.ENEMY && owner == Owner.ENEMY)
        {
            enemyManager.AddFleet(this);
        }
        if (prevOwner == Owner.ENEMY && owner != Owner.ENEMY)
        {
            enemyManager.RemoveFleet(this);
        }
        prevOwner = owner;
    }

    public void UpdateTick()
    {
        RestoreActionPoints();

        // Regenerate Shields
        AddShield(maxShield * 0.2f);

        return;
    }

    public static int UpkeepCreditCost(ShipID id)
    {
        switch(id)
        {
            case ShipID.MONO: return 15;
            case ShipID.FLARE: return 30;
            case ShipID.SPARK: return 50;
            case ShipID.PULSE: return 50;
            case ShipID.EMBER: return 80;
            case ShipID.VOLT: return 120;
            case ShipID.BLAST: return 100;

            default:
                Debug.LogError("Unknown ship ID.");
                return -1;
        }
    }
    public static int BuildCreditCost(ShipID id)
    {
        switch (id)
        {
            case ShipID.MONO: return 150;
            case ShipID.FLARE: return 300;
            case ShipID.SPARK: return 500;
            case ShipID.PULSE: return 500;
            case ShipID.EMBER: return 800;
            case ShipID.VOLT: return 1200;
            case ShipID.BLAST: return 1000;

            default:
                Debug.LogError("Unknown ship ID.");
                return -1;
                
        }
    }
    public static int BuildAlloyRequirement(ShipID id)
    {
        switch (id)
        {
            case ShipID.MONO: return 0;
            case ShipID.FLARE: return 1;
            case ShipID.SPARK: return 1;
            case ShipID.PULSE: return 1;
            case ShipID.EMBER: return 2;
            case ShipID.VOLT: return 2;
            case ShipID.BLAST: return 2;

            default:
                Debug.LogError("Unknown ship ID.");
                return -1;
        }
    }

    /*
     * Control
     */

    // grid call travel(path) on HexUnit, Hexunit will determine an action type, if it is movement, retrieve coordinates and
    // pass those coordinates into MoveTo(vector 3 corrds)
    public void MoveTo(Vector3 hexUnitCoord)
    {
        this.transform.position = hexUnitCoord;
    }  

    public void DestroyFleet()
    {
        HexCell location = hexUnit.Grid.GetCell(hexUnit.locationCellIndex);
        location.fleet = null;
        hexUnit.Die();

        if (owner == Owner.ENEMY)
        {
            enemyManager.RemoveFleet(this);
        }

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
     * Stat Modifiers
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
    VOLT,
    BLAST
}
public enum DamageType
{
    KINETIC,
    THERMAL,
    EM
}
