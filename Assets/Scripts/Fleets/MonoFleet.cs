using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoFleet : Fleet
{
    void Awake()
    {
        InitializeReferences();

        this.damage = 10;
        this.hull = 30;
        this.shield = 10;

        this.maxHull = 30;
        this.maxShield = 10;

        this.damageType = DamageType.THERMAL;

        this.actionPoints = 2;
        this.maxActionPoints = 2;

        this.thermalRes = 0;
        this.kineticRes = 0;
        this.emRes = 0;
    }
}
