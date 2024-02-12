using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public enum GameState
    {
        INPROGRESS,
        WIN,
        LOSE,
    }

    private PlayerManager playerManager;
    private EnemyManager enemyManager;
    private HexGrid hexGrid;

    public int turnNumber = 0;
    public GameState gameState;

    private void Awake()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        enemyManager = FindObjectOfType<EnemyManager>();
        hexGrid = FindObjectOfType<HexGrid>();
    }

    private void Update()
    {
        CheckWin();
    }

    public void UpdateTick(){
        turnNumber ++;
        playerManager.UpdateTick();
        hexGrid.UpdateTick();
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

    void CheckWin()
    {
        if (enemyManager.enemyControlledPlanets.Count == playerManager.playerControlledPlanets.Count ||
            enemyManager.enemyControlledPlanets.Count * playerManager.playerControlledPlanets.Count > 0)
        {

            gameState = GameState.INPROGRESS;
        }
        
        else if(enemyManager.enemyControlledPlanets.Count == 0)
        {
            gameState = GameState.WIN;
        }
        else if (playerManager.playerControlledPlanets.Count == 0)
        {
            gameState = GameState.LOSE;
        }
        
    }
}
