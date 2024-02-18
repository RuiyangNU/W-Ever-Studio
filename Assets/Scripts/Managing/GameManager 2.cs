using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerManager pm;
    private EnemyManager em;
    public int turnNumber = 0;
    private HexGrid hg;
    public GameState gameState;
    public enum GameState
    {
        WIN,
        LOSS,
        INPROGRESS

    }

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
        em = FindObjectOfType<EnemyManager>();
        hg = FindObjectOfType<HexGrid>();
        
    }
    private void Update()
    {
        CheckWin();
    }

    void CheckWin()
    {
        if (em.enemyControlledPlanets.Count == pm.playerControlledPlanets.Count ||
            em.enemyControlledPlanets.Count * pm.playerControlledPlanets.Count > 0)
        {

            gameState = GameState.INPROGRESS;
        }
        
        else if(em.enemyControlledPlanets.Count == 0)
        {
            gameState = GameState.WIN;
        }
        else if (pm.playerControlledPlanets.Count == 0)
        {
            gameState = GameState.LOSS;
        }
        
    }
}
