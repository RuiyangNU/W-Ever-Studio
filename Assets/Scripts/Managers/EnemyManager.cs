using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class EnemyAiTask
{
    public int id;
    public Planet targetPlanet = null;
    public Fleet targetFleet = null;
}

public class EnemyManager : MonoBehaviour
{
    /*
     * References
     */
    private GameManager gameManager;
    private PlayerManager playerManager;

    public HexGrid hexGrid;

    /*
     * Control
     */
    public List<Planet> enemyControlledPlanets = new List<Planet>();

    public List<Fleet> enemyControlledFleets = new List<Fleet>();

    private int TurnNumber => gameManager.turnNumber;

    private int NumAttackers => enemyControlledFleets.Count(fleet => fleet.enemyTask.id == 1);

    /*
     * Spawning
     */
    private int shipPoints;

    private ShipID intent;

    private Queue<ShipID> spawnQueue;

    private Dictionary<ShipID, int> shipCosts = new()
    {
        { ShipID.MONO, 10 },
        { ShipID.FLARE, 30 },
        { ShipID.SPARK, 30 },
        { ShipID.PULSE, 30 },
        { ShipID.EMBER, 90 },
        { ShipID.VOLT, 90 },
        { ShipID.BLAST, 90 }
    };

    /*
     * Initializers and Updaters 
     */
    public void Awake()
    {
        hexGrid = FindObjectOfType<HexGrid>();
        gameManager = FindObjectOfType<GameManager>(); 
        playerManager = FindObjectOfType<PlayerManager>();

        shipPoints = 0;
        intent = ShipID.MONO;
    }

    private void Update()
    {
        // Remove destroyed fleets
        enemyControlledFleets.RemoveAll(fleet => fleet == null);
    }

    public void UpdateTick()
    {
        // Perform Enemy Ship Tasks
        foreach (Fleet fleet in enemyControlledFleets)
        {
            if (fleet != null && fleet.enemyTask.id == 0)
            {

            }
            //Move to Enemy planet
            else if (fleet != null && fleet.enemyTask.id == 1)
            {


                if (fleet.enemyTask.targetPlanet != null)
                {

                    //Already Occupying, abort mission
                    if (fleet.enemyTask.targetPlanet.CurrentCell.fleet != null && fleet.enemyTask.targetPlanet.CurrentCell.fleet.owner == Owner.ENEMY)
                    {
                        AssignAiTask(fleet, 0);
                    }
                    else
                    {
                        MoveAiFleetCell(fleet.enemyTask.targetPlanet.CurrentCell, fleet);
                    }

                }

            }

        }

        // Gain ship points
        int pointsGained = 0;
        
        if (TurnNumber <= 5)
        {
            pointsGained = 0; // Turns 0-5
        }
        else if (TurnNumber <= 10)
        {
            pointsGained = 3; // Turns 6-10
        }
        else if (TurnNumber <= 15)
        {
            pointsGained = 4; // Turns 11-15
        }
        else if (TurnNumber <= 20)
        {
            pointsGained = 6; // Turns 16-20
        }
        else if (TurnNumber <= 25)
        {
            pointsGained = 11; // Turns 21-25
        }
        else if (TurnNumber <= 30)
        {
            pointsGained = 16; // Turns 26-30
        }
        else if (TurnNumber <= 35)
        {
            pointsGained = 25; // Turns 31-35
        }
        else
        {
            pointsGained = 35 + (int)(Mathf.Floor(Mathf.Pow(1.3f, 0.5f * (TurnNumber - 35)))); // Turns 36+
        }
        shipPoints += (int)(Mathf.Floor(Mathf.Pow(1.4f, 2.1f + 0.25f * (TurnNumber - 5))));

        // Queue intended ship and the planet to spawn them in
        if (shipPoints >= shipCosts[intent])
        {
            spawnQueue.Enqueue(intent);
            ChangeIntent();
        }

        // Spawn queued ships

        
    }

    /*
     * Control
     */

    /// <summary>
    /// Todo: fix spawning logic
    /// </summary>
    public void SpawnEnemyFleet(ShipID shipID)
    {
        int randomIndex = UnityEngine.Random.Range(0, enemyControlledPlanets.Count);
        Planet spawningPlanet = enemyControlledPlanets[randomIndex];

        if (spawningPlanet.CurrentCell.fleet == null && enemyControlledFleets.Count <= 5)
        {
            gameManager.CreateFleet(spawningPlanet.CurrentCell, Owner.ENEMY, shipID);
            AddFleet(spawningPlanet.CurrentCell.fleet);

            //Attack Force
            AssignAiTask(spawningPlanet.CurrentCell.fleet, 1);
        }
        else if(spawningPlanet.CurrentCell.fleet == null && enemyControlledFleets.Count > 5)
        {
            //Go to neighbors, don't generate fleet
            gameManager.CreateFleet(spawningPlanet.CurrentCell, Owner.ENEMY, shipID);
            AddFleet(spawningPlanet.CurrentCell.fleet);
            //Garrison
            AssignAiTask(spawningPlanet.CurrentCell.fleet, 0);
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
        AssignAiTask(f, 1);
    }

    public void RemoveFleet(Fleet f)
    {
        if (!(enemyControlledFleets.Remove(f)))
        {
            Debug.LogWarning("Tried to remove " + f.name + " from EnemyManager, but " + f.name + " was not in the list.");
            return;
        }
        enemyControlledFleets.Remove(f);
    }

    /*
     * AI
     */
    private void ChangeIntent()
    {
        System.Random rand = new();
        if (TurnNumber <= 15)
        {
            intent = ShipID.MONO;
        }
        else if (TurnNumber <= 30)
        {
            if (rand.Next(1, 101) >= (118 - 3 * TurnNumber))
            {
                // Spawn T1 ship
                switch (rand.Next(1, 4))
                {
                    case 1:
                        intent = ShipID.FLARE; break;
                    case 2:
                        intent = ShipID.SPARK; break;
                    case 3:
                        intent = ShipID.PULSE; break;
                    default:
                        Debug.LogError("Random out of range.");
                        intent = ShipID.FLARE; break;
                }
            }
            else
            {
                intent = ShipID.MONO;
            }
        }
        else
        {
            if (rand.Next(1, 101) >= (163 - 3 * TurnNumber))
            {
                // Spawn T2 ship
                switch (rand.Next(1, 4))
                {
                    case 1:
                        intent = ShipID.FLARE; break;
                    case 2:
                        intent = ShipID.SPARK; break;
                    case 3:
                        intent = ShipID.PULSE; break;
                    default:
                        Debug.LogError("Random out of range.");
                        intent = ShipID.FLARE; break;
                }
            }

            else
            {
                // Spawn T3 ship
                switch (rand.Next(1, 4))
                {
                    case 1:
                        intent = ShipID.EMBER; break;
                    case 2:
                        intent = ShipID.VOLT; break;
                    case 3:
                        intent = ShipID.BLAST; break;
                    default:
                        Debug.LogError("Random out of range.");
                        intent = ShipID.EMBER; break;
                }
            }
        }
    }

    public void AssignAiTask(Fleet f, int taskId)
    {
        EnemyAiTask task = new EnemyAiTask();
        task.id = taskId;
        f.enemyTask = task;
        if (taskId == 1)
        {
            if (task != null && playerManager != null)
            {
                Planet p = FindClosestPlanet(f);
                
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
    public Planet FindClosestPlanet(Fleet fleet)
    {
        int distance = 10000;
        Planet planet = null;
        HexCell currentCell = hexGrid.GetCell(fleet.hexUnit.locationCellIndex);
        //Debug.Log("If there is some planet: " + playerManager.playerControlledPlanets.Count);

        foreach (Planet p in playerManager.playerControlledPlanets)
        {
            //Debug.Log("Some unit: " + fleet.hexUnit);

            hexGrid.FindPathAi(currentCell, p.CurrentCell, fleet.hexUnit);

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


        return planet;
    }

    /// <summary>
    /// Move the enemy ai fleet toward target cell for 2 cell
    /// </summary>
    /// <param name="targetCell"></param>
    /// <param name="fleet"></param>
    public void MoveAiFleetCell(HexCell targetCell, Fleet fleet)
    {
        Debug.Log("Start Moving Fleet");
        hexGrid.FindPathAi(
                        fleet.hexUnit.Location,
                        targetCell,
        fleet.hexUnit);

        if (fleet.hexUnit.IsValidCombat(targetCell))
        {
            AiFleetDoCombat(targetCell, fleet);
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
            hexGrid.FindPathAi(
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
