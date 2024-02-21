using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastFleet : Fleet
{
    void Awake()
    {
        InitializeReferences();

        this.damage = 50;
        this.hull = 250;
        this.shield = 25;

        this.maxHull = 250;
        this.maxShield = 25;

        this.damageType = DamageType.KINETIC;

        this.actionPoints = 3;
        this.maxActionPoints = 3;

        this.thermalRes = 0;
        this.kineticRes = 0;
        this.emRes = 0;
    }
}
