using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseFleet : Fleet
{
    void Awake()
    {
        InitializeReferences();

        this.damage = 25;
        this.hull = 150;
        this.shield = 10;

        this.maxHull = 150;
        this.maxShield = 10;

        this.damageType = DamageType.KINETIC;

        this.actionPoints = 2;
        this.maxActionPoints = 2;

        this.thermalRes = 0;
        this.kineticRes = 0;
        this.emRes = 0;
    }
}
