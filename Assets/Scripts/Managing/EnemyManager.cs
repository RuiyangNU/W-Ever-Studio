using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAiTask
{
    public int id;
    public Planet targetPlanet = null;
    public Fleet targetFleet = null;
}

public class EnemyManager : MonoBehaviour
{
    // Start is called before the first frame update

    private GameManager gameManager;
    private PlayerManager playerManager;

    public HexGrid hexGrid;

    public List<Planet> enemyControlledPlanets = new List<Planet>();

    public List<Fleet> enemyControlledFleets = new List<Fleet>();

    public int spawnCoolDown = 0; //Placeholder for spawning

    //public enum AiTask
    //{
    //    IDLE,
    //}

    public void Awake()
    {
        hexGrid = FindObjectOfType<HexGrid>();
        gameManager = FindObjectOfType<GameManager>(); 
        playerManager = FindObjectOfType<PlayerManager>();
    }

    public void UpdateTick()
    {
        if (spawnCoolDown == 0)
        {
            SpawnEnemyFleet();
            spawnCoolDown = 5;
        }
        else
        {
            spawnCoolDown--;
        }

        Debug.Log(enemyControlledFleets.Count);


        foreach (Fleet fleet in enemyControlledFleets) {
            Debug.Log(fleet.enemyTask.id);
            //Hold
            if (fleet != null && fleet.enemyTask.id == 0)
            {

            }
            //Move to Enemy planet
            else if (fleet != null && fleet.enemyTask.id == 1)
            {
                if(fleet.enemyTask.targetPlanet != null)
                {
                    MoveAiFleetCell(fleet.enemyTask.targetPlanet.CurrentCell, fleet);
                }
                
            }

        }
    }

    /// <summary>
    /// Todo: fix spawning logic
    /// </summary>
    public void SpawnEnemyFleet()
    {
        int randomIndex = UnityEngine.Random.Range(0, enemyControlledPlanets.Count);
        Planet spawningPlanet = enemyControlledPlanets[randomIndex];
        if (spawningPlanet.CurrentCell.fleet == null && enemyControlledFleets.Count <= 5)
        {
            gameManager.CreateFleet(spawningPlanet.CurrentCell, Fleet.FleetOwner.ENEMY);
            AddFleet(spawningPlanet.CurrentCell.fleet);

            //Attack Force
            AssignAiTast(spawningPlanet.CurrentCell.fleet, 1);
        }
        else if(spawningPlanet.CurrentCell.fleet == null && enemyControlledFleets.Count > 5)
        {
            //Go to neighbors, don't generate fleet
            gameManager.CreateFleet(spawningPlanet.CurrentCell, Fleet.FleetOwner.ENEMY);
            AddFleet(spawningPlanet.CurrentCell.fleet);

            //Garrison
            AssignAiTast(spawningPlanet.CurrentCell.fleet, 0);
        }

    }

    public void AddPlanet(Planet p)
    {
       
        
        if (enemyControlledPlanets.Contains(p))
        {
            Debug.LogWarning("Tried to add " + p.name + " to EnemyManager, but " + p.name + " was already in the list.");
            return;
        }
        enemyControlledPlanets.Add(p);
    }

    public void RemovePlanet(Planet p)
    {
        if (!(enemyControlledPlanets.Remove(p)))
        {
            Debug.LogWarning("Tried to remove " + p.name + " from EnemyManager, but " + p.name + " was not in the list.");
        }
    }


    public void AddFleet(Fleet f)
    {


        if (enemyControlledFleets.Contains(f))
        {
            Debug.LogWarning("Tried to add " + f.name + " to EnemyManager, but " + f.name + " was already in the list.");
            return;
        }
        enemyControlledFleets.Add(f);
        AssignAiTast(f, 1);
    }

    public void RemoveFleet(Fleet f)
    {
        if (!(enemyControlledFleets.Remove(f)))
        {
            Debug.LogWarning("Tried to remove " + f.name + " from EnemyManager, but " + f.name + " was not in the list.");
        }
    }

    public void AssignAiTast(Fleet f, int taskId)
    {
        EnemyAiTask task = new EnemyAiTask();
        task.id = taskId;
        f.enemyTask = task;
        if (taskId == 1)
        {
            if (task != null && playerManager != null)
            {
                Planet p = FindCloestPlanet(f);
                
                //No reachable planet, change ai task, currently to idel
                if (p == null)
                {
                    //task.id = 0;
                    return;
                }

                task.targetPlanet = p;

            }
        }
    }

    /// <summary>
    /// Find the Cloest Planet to the Ai Unit
    /// </summary>
    /// <param name="fleet"></param>
    /// <returns></returns>
    public Planet FindCloestPlanet(Fleet fleet)
    {
        int distance = 10000;
        Planet planet = null;
        HexCell currentCell = hexGrid.GetCell(fleet.hexUnit.locationCellIndex);
        Debug.Log("If there is some planet" + playerManager.playerControlledPlanets.Count);

        foreach (Planet p in playerManager.playerControlledPlanets)
        {
            Debug.Log("Some unit" + fleet.hexUnit);

            hexGrid.FindPath(currentCell, p.CurrentCell, fleet.hexUnit);

            //Debug
            if (hexGrid.HasPath)
            {
                int d = hexGrid.GetPath().Count - 1;
                if (d < distance)
                {
                    planet = p;
                    distance = d;
                }
            }
        }


        return playerManager.playerControlledPlanets[0];
    }
    

    /// <summary>
    /// Move the enemy ai fleet toward target cell for 2 cell
    /// </summary>
    /// <param name="targetCell"></param>
    /// <param name="fleet"></param>
    public void MoveAiFleetCell(HexCell targetCell, Fleet fleet)
    {
        Debug.Log("Start Moving Fleet");
        hexGrid.FindPath(
                        fleet.hexUnit.Location,
                        targetCell,
        fleet.hexUnit);
        if (!hexGrid.HasPath)
        {
            if(fleet.hexUnit.IsValidCombat(targetCell))
            {
                AiFleetDoCombat(targetCell, fleet);
            }
        }
        else
        {
            fleet.hexUnit.TravelByStep(hexGrid.GetPath(), 2);
            fleet.RemoveActionPoints(100);
        }


        hexGrid.ClearPath();
    }



    /// <summary>
    /// Start Combat for enemy ai fleet toward player fleet
    /// </summary>
    /// <param name="targetCell"></param>
    /// <param name="fleet"></param>
    public void AiFleetDoCombat(HexCell targetCell, Fleet fleet)
    {
        gameManager.StartCombat(fleet, targetCell.fleet);

        if (targetCell.fleet == null && fleet != null)
        {
            hexGrid.FindPath(
                fleet.hexUnit.Location,
                targetCell,
                fleet.hexUnit);

            fleet.hexUnit.Travel(hexGrid.GetPath());
            fleet.RemoveActionPoints(100);
            hexGrid.ClearPath();
            return;

        }
    }


}
