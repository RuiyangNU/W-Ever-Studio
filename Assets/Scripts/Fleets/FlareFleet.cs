using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlareFleet : Fleet
{
    void Awake()
    {
        InitializeReferences();

        this.damage = 25;
        this.hull = 75;
        this.shield = 25;

        this.maxHull = 75;
        this.maxShield = 25;

        this.damageType = DamageType.THERMAL;

        this.actionPoints = 3;
        this.maxActionPoints = 3;

        this.thermalRes = 0;
        this.kineticRes = 0;
        this.emRes = 0;
    }
}
