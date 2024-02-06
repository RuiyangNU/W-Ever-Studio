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

    public PopupUI targetUI => null;

    public bool IsUIOpen => false; //TODO

    // Start is called before the first frame update
    void Start()
    {
        fleet = FindObjectOfType<Fleet>();
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
        actionPoints = 3;
    }

    public void DestroyFleet()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OpenUI()
    {
    }

    public void OnUIClose()
    {

    }
}
