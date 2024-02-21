using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmberFleet : Fleet
{
    void Awake()
    {
        InitializeReferences();

        this.damage = 40;
        this.hull = 175;
        this.shield = 50;

        this.maxHull = 175;
        this.maxShield = 50;

        this.damageType = DamageType.THERMAL;

        this.actionPoints = 3;
        this.maxActionPoints = 3;

        this.thermalRes = 0;
        this.kineticRes = 0;
        this.emRes = 0;
    }
}
