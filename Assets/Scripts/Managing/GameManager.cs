using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerManager pm;
    public int turnNumber = 0;
    private HexGrid hg;


    public void UpdateTick(){
        turnNumber ++;
        pm.UpdateTick();
        hg.UpdateTick();
    }

    public void HandleCombat(Fleet attacker, Fleet defender)
    {
        // Record stats
        float attackerDamage = attacker.Damage;
        float defenderDamage = defender.Damage;
        float attackerHealth = attacker.Health;
        float defenderHealth = defender.Health;

        defender.Health -= attackerDamage;
        attacker.Health -= defenderDamage;
    }

    void Awake() {
        pm = FindObjectOfType<PlayerManager>();
        hg = FindObjectOfType<HexGrid>();
    }
}
