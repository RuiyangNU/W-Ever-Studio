using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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
        enemyManager.UpdateTick();
        hexGrid.UpdateTick();
    }

    public void StartCombat(Fleet attacker, Fleet defender)
    {
        // Record stats
        float attackerDamageBeforeCombat = attacker.Damage;
        float defenderDamageBeforeCombat = defender.Damage;
        float attackerHullBeforeCombat = attacker.Hull;
        float defenderHullBeforeCombat = defender.Hull;

        defender.RemoveHull(attackerDamageBeforeCombat);

        if (Mathf.Abs(defender.Hull) <= Mathf.Epsilon && attacker != null) {

            //Temp solution of checking if it is removing player or enemy fleet
            enemyManager.RemoveFleet(attacker);
            enemyManager.RemoveFleet(defender);
            defender.DestroyFleet();
        }
        else
        {
            attacker.RemoveActionPoints(100);
        }
    }

    public void CreatePlanet(HexCell cell, Owner owner)
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

    public void CreateFleet(HexCell cell, Owner owner, ShipID shipID)
    {
        if (cell && cell.fleet)
        {
            Debug.LogWarning("Tried to spawn a fleet at an occupied cell, aborting...");
            return;
        }

        else if (cell && !cell.fleet)
        {
            HexUnit unit = Instantiate(HexUnit.unitPrefab);
            Fleet fleet = Prefabs.Get(shipID);

            // Link
            fleet.hexUnit = unit;
            unit.fleet = fleet;
            fleet.owner = owner;

            hexGrid.AddFleet(
                fleet, cell, Random.Range(0f, 360f)
            );

        }
    }


    public void AddPlanetToCell(HexCell cell, Planet planet)
    {
        if (cell && !cell.planet)
        {
            hexGrid.AddPlanet(planet, cell);

            planet.transform.localPosition = cell.Position;
            planet.SetLocation(cell.Index);
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

public enum GameState
{
    INPROGRESS,
    WIN,
    LOSE,
}

public enum Owner
{
    NONE,
    PLAYER,
    ENEMY
}