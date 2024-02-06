using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static PlayerManager;
using static HexUnit;

public class Fleet : MonoBehaviour, IClickableUI
{
    private Fleet fleet;
    private HexUnit hexUnit;

    public enum FleetOwner
    {
        PLAYER,
        ENEMY
    }

    public float health = 50.0f;
    public float damage = 50.0f;
    public float speed = 50.0f;
    public int actionPoints = 3;
    public FleetOwner owner;

    private FleetInfoUI fleetInfoUI;

    public PopupUI targetUI => fleetInfoUI;

    public bool IsUIOpen => fleetInfoUI.isUIOpen;

    // Start is called before the first frame update
    void Start()
    {
        hexUnit = GetComponent<HexUnit>();
        if (hexUnit == null)
        {
            Debug.LogError("HexUnit not found");
        }
        fleet = FindObjectOfType<Fleet>();
        fleetInfoUI = FindObjectOfType<FleetInfoUI>();
    }

    public void SetProperties(FleetOwner o, float health, float damage, float speed, int actionPoints)
    {
        this.owner = o;
        this.health = health;
        this.damage = damage;
        this.speed = speed;
        this.actionPoints = actionPoints;
    }

    public void OnClick()
    {
        OpenUI();
    }

    public void UpdateTick()
    {
        // check if health is depleted
        if (health <= 0)
        {
            DestroyFleet();
        }
        if (actionPoints == 0)
        {
            actionPoints = 3;
        }
        else
        {
            actionPoints += 3;
        }
    }

    public void DestroyFleet()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTick();
    }

    public void OpenUI()
    {
        fleetInfoUI.Link(this);
        fleetInfoUI.OpenUI();
    }

    public void OnUIClose()
    {

    }
}
