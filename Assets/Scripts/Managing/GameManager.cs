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
    public HexGrid hexGrid;
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

    public void StartCombat(Fleet attacker, Fleet defender)
    {
        // Record stats
        float attackerDamage = attacker.Damage;
        float defenderDamage = defender.Damage;
        float attackerHealth = attacker.Health;
        float defenderHealth = defender.Health;

        defender.Health -= attackerDamage;
        attacker.Health -= defenderDamage;


        if (attackerHealth <= 0) {
            attacker.DestroyFleet();
        }

        if (defenderHealth <= 0.1 && attacker != null) {

            //Temp solution of checking if it is removing player or enemy fleet
            em.RemoveFleet(attacker);
            em.RemoveFleet(defender);
            defender.DestroyFleet();
        }
        else
        {
            attacker.actionPoints = 0;
        }
    }

    public void CreatePlanet(HexCell cell, Planet.PlanetOwner owner)
    {
        //HexCell cell = GetCellUnderCursor();
        if (cell && !cell.planet)
        {
            Planet planet = Instantiate(Planet.planetPrefab);
            //set default
            hexGrid.AddPlanet(
                planet, cell
            );

            planet.transform.localPosition = cell.Position;
            planet.SetProperties(owner);

        }
    }

    public void CreateFleet(HexCell cell, Fleet.FleetOwner owner)
    {
        //HexCell cell = GetCellUnderCursor();
        if (cell && !cell.fleet)
        {
            HexUnit unit = Instantiate(HexUnit.unitPrefab);
            Fleet fleet = Instantiate(Fleet.fleetPrefab);
            //set default
            fleet.hexUnit = unit;
            unit.fleet = fleet;
            fleet.owner = owner;
            //         hexGrid.AddUnit(
            //             unit, cell, Random.Range(0f, 360f)
            //);

            hexGrid.AddFleet(
                fleet, cell, Random.Range(0f, 360f)
            );

        }
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
