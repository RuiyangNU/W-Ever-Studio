using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private PlayerManager playerManager;
    private EnemyManager enemyManager;
    private HexGrid hexGrid;
    [SerializeField] public List<Fleet> fleetPrefabs;
    [SerializeField] public List<GameObject> shipPrefabs;

    public int turnNumber = 0;
    public GameState gameState;
    public static GameManager gameManager;
    public List<string> gameFlags = new List<string>();
    public bool tutorial_fleet_status = false;

    /*
     * Static
     */
    public static Fleet GetShipByType(ShipID shipID)
    {
        foreach (Fleet fleet in gameManager.fleetPrefabs)
        {
            //Debug.Log(fleet);
            if (fleet.shipID == shipID)
            {
                return fleet;
            }
        }

        Debug.LogWarning("The prefab for " + shipID.ToString() + " was not found.");
        return null;
    }

    public static GameObject GetShipPrefab(ShipID shipID)
    {
        foreach (GameObject fleet in gameManager.shipPrefabs)
        {

            //Debug.Log(fleet);
            if (fleet.GetComponent<Fleet>().shipID == shipID)
            {
                return fleet;
            }
        }

        Debug.LogWarning("The prefab for " + shipID.ToString() + " was not found.");
        return null;
    }



    /// <summary>
    /// Get Variable By String, used for the event system.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public float GetVariableByName(string name)
    {
        switch (name.ToLower())
        {
            case "turn": 
                return gameManager.turnNumber;
            case "credit":
                return gameManager.playerManager.PlayerCredit;
            case "research":
                return gameManager.playerManager.PlayerResearch;
            default: break;
                
        }

        if(name == "alloy")
        {
            return playerManager.GetCommodityMilestone(Commodity.ALLOY);
        }

        if (name == "construction")
        {
            return playerManager.GetCommodityMilestone(Commodity.CONSTRUCTION);
        }


        return 0;
    }

    public bool HasFlag(string flag)
    {

        return gameFlags.Contains(flag);
    }

    public void AddFlag(string flag)
    {
        gameFlags.Add(flag);
    }

    public void RemoveFlag(string flag)
    {
        gameFlags.Remove(flag);
    }

    /*
     * Initializers and Updaters
     */
    private void Awake()
    {

        playerManager = FindObjectOfType<PlayerManager>();
        enemyManager = FindObjectOfType<EnemyManager>();
        hexGrid = FindObjectOfType<HexGrid>();
        //fleetPrefabs = new List<Fleet>();
        GameManager.gameManager = this;
    }

    private void Update()
    {
        CheckWin();
    }

    public void UpdateTick(){
        enemyManager.UpdateTick();
        hexGrid.UpdateTick();
        playerManager.UpdateTick();

        NotificationUI.notificationUI.UpdateTick();
        turnNumber ++;
    }

    /*
     * Combat Handling
     */
    public void StartCombat(Fleet attacker, Fleet defender)
    {
        // Stats
        float attackerDamage = attacker.Damage;
        DamageType attackerType = attacker.damageType;
        float defenderDamage = defender.Damage;
        DamageType defenderType = defender.damageType;
        CombatEvent combatEvent = new CombatEvent();


        combatEvent.attacker = attacker.owner;
        combatEvent.defender = defender.owner;


        int defenderRes = 0;
        switch (attackerType)
        {
            case DamageType.THERMAL:
                defenderRes = defender.thermalRes; break;
            case DamageType.KINETIC:
                defenderRes = defender.kineticRes; break;
            case DamageType.EM:
                defenderRes = defender.emRes; break;
            default:
                Debug.LogError("Unknown damage type for " + attacker.name + "."); break;
        }

        int attackerRes = 0;
        switch (defenderType)
        {
            case DamageType.THERMAL:
                attackerRes = attacker.thermalRes; break;
            case DamageType.KINETIC:
                attackerRes = attacker.kineticRes; break;
            case DamageType.EM:
                attackerRes = attacker.emRes; break;
            default:
                Debug.LogError("Unknown damage type for " + defender.name + "."); break;
        }

        /*
         * Attacker to Defender
         */
        float remainingRawDamage = attackerDamage;

        // Shields
        if (defender.Shield > 0)
        {
            int nativeRes = attackerType switch
            {
                DamageType.KINETIC => 20,
                DamageType.EM => -20,
                _ => 0,
            };

            float remainingEffectiveDamage = RawToEffective(remainingRawDamage, defenderRes + nativeRes);
            if (remainingEffectiveDamage > defender.Shield)
            {
                // Pierce shield
                remainingRawDamage -= EffectiveToRaw(defender.Shield, defenderRes + nativeRes);
                combatEvent.attackerShieldDamage = defender.Shield;
                defender.RemoveShield(defender.Shield);

            }
            else
            {
                // Blocked by shield
                remainingRawDamage = 0;
                combatEvent.attackerShieldDamage = remainingEffectiveDamage;
                defender.RemoveShield(remainingEffectiveDamage);
            }
        }

        // Hull
        if (remainingRawDamage > 0)
        {
            int nativeRes = attackerType switch
            {
                DamageType.KINETIC => 20,
                DamageType.EM => -20,
                _ => 0,
            };

            float remainingEffectiveDamage = RawToEffective(remainingRawDamage, defenderRes + nativeRes);
            combatEvent.attackerHullDamage = remainingEffectiveDamage;
            defender.RemoveHull(remainingEffectiveDamage);
        }

        /*
         * Defender to Attacker
         */
        remainingRawDamage = defenderDamage;

        // Shields
        if (attacker.Shield > 0)
        {
            int nativeRes = defenderType switch
            {
                DamageType.KINETIC => 20,
                DamageType.EM => -20,
                _ => 0,
            };

            float remainingEffectiveDamage = RawToEffective(remainingRawDamage, attackerRes + nativeRes);
            if (remainingEffectiveDamage > attacker.Shield)
            {
                // Pierce shield
                remainingRawDamage -= EffectiveToRaw(attacker.Shield, attackerRes + nativeRes);
                combatEvent.defenderShieldDamage = attacker.Shield;
                attacker.RemoveShield(attacker.Shield);
            }
            else
            {
                // Blocked by shield
                remainingRawDamage = 0;
                combatEvent.defenderShieldDamage = remainingEffectiveDamage;
                attacker.RemoveShield(remainingEffectiveDamage);
            }
        }

        // Hull
        if (remainingRawDamage > 0)
        {
            int nativeRes = attackerType switch
            {
                DamageType.KINETIC => 20,
                DamageType.EM => -20,
                _ => 0,
            };

            float remainingEffectiveDamage = RawToEffective(remainingRawDamage, attackerRes + nativeRes);
            combatEvent.defenderHullDamage = remainingEffectiveDamage;
            attacker.RemoveHull(remainingEffectiveDamage);
        }

        /*
         * Check for kills
         */
        if (defender.Hull == 0f)
        {
            combatEvent.isDefenderAlive = false;
            defender.DestroyFleet();
        }
        else
        {
            attacker.RemoveActionPoints(100);
        }

        if (attacker.Hull == 0f)
        {
            combatEvent.isAttackerAlive = false;
            attacker.DestroyFleet();
        }

        NotificationUI.notificationUI.AddNewItem(combatEvent);
    }

    private float RawToEffective(float rawDamage, int res)
    {
        return rawDamage * (100.0f / Mathf.Max(50, 100 + res));
    }

    private float EffectiveToRaw(float effectiveDamage, int res)
    {
        return effectiveDamage * (Mathf.Max(50, 100 + res) / 100.0f);
    }

    /*
     * Object Creation
     */
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
            //cell.DisablePlanetRender();
            if(planet.owner == Owner.PLAYER)
            {
                hexGrid.IncreaseVisibility(cell, 2);
            }
            if (!FindObjectOfType<HexMapEditor>().enabled)
            {
                cell.DisablePlanetRender();
            }
        }
    }

    public void CreatePlanet(HexCell cell, Owner owner, int baseCreditsPerTick, int buildingLimit)
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
            //cell.DisablePlanetRender();
            if (planet.owner == Owner.PLAYER)
            {
                hexGrid.IncreaseVisibility(cell, 2);
            }

            //Debug.Log(FindObjectOfType<HexMapEditor>());
            if (FindObjectOfType<HexMapEditor>() == null || !FindObjectOfType<HexMapEditor>().enabled)
            {
                cell.DisablePlanetRender();
            }
            //planet.SetLocation(cell.Index);

            //AddPlanetToCell(cell, planet);
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
            //HexUnit unit = Instantiate(HexUnit.unitPrefab);
            //Fleet fleet = Instantiate(GetShipByType(shipID));
            if (!tutorial_fleet_status)
            {
                GameManager.gameManager.AddFlag("tutorial_fleet_finished");
                tutorial_fleet_status = true;   
            }
            GameObject fleet = Instantiate(GetShipPrefab(shipID));

            //fleet.transform.position += new Vector3(0,5,0);
            // Link
            //fleet.fleet.hexUnit = unit;
            //fleet.unit.fleet = fleet;

            //GameObject createdFleet = Instantiate(GetShipPrefab(ShipID.MONO));

            fleet.GetComponent<Fleet>().owner = owner;

            hexGrid.AddFleet(
                fleet.GetComponent<Fleet>(), cell, Random.Range(0f, 360f)
            );

            if (FindObjectOfType<HexMapEditor>() == null || !FindObjectOfType<HexMapEditor>().enabled)
            {
                cell.DisableFleetRender();
            }

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

    /*
     * Victory and Defeat
     */
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