using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoltFleet : Fleet
{
    void Awake()
    {
        InitializeReferences();

        this.damage = 50;
        this.hull = 150;
        this.shield = 80;

        this.maxHull = 150;
        this.maxShield = 80;

        this.damageType = DamageType.EM;

        this.actionPoints = 4;
        this.maxActionPoints = 4;

        this.thermalRes = 0;
        this.kineticRes = 0;
        this.emRes = 0;
    }
}
