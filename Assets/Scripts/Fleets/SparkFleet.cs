using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkFleet : Fleet
{
    void Awake()
    {
        InitializeReferences();

        this.damage = 20;
        this.hull = 100;
        this.shield = 40;

        this.maxHull = 100;
        this.maxShield = 40;

        this.damageType = DamageType.EM;

        this.actionPoints = 3;
        this.maxActionPoints = 3;

        this.thermalRes = 0;
        this.kineticRes = 0;
        this.emRes = 0;
    }
}
