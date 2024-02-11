using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // Start is called before the first frame update

    private GameManager GM;
    private PlayerManager PM;

    public List<Planet> enemyControlledPlanets = new List<Planet>();

    public List<Fleet> enemyControlledFleets = new List<Fleet>();



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
    }

    public void RemoveFleet(Fleet f)
    {
        if (!(enemyControlledFleets.Remove(f)))
        {
            Debug.LogWarning("Tried to remove " + f.name + " from EnemyManager, but " + f.name + " was not in the list.");
        }
    }

    public void SimpleAi()
    {

    }



}
