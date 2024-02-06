using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Start is called before the first frame update
    public enum PlayerResource
    {
        METHANE,
        STEEL,
    }

    public List<Planet> playerControlledPlanets = new List<Planet>();

    public Dictionary<PlayerResource, float> playerResourcePool = 
        new Dictionary<PlayerResource, float>(){
                    {PlayerResource.METHANE, 0},
                    {PlayerResource.STEEL, 0}};
        

    public void UpdateTickResources(Dictionary<PlayerResource, float> resources) {
        // add resources from this function to the resource pool
        playerResourcePool[PlayerResource.METHANE] += resources[PlayerResource.METHANE];
        playerResourcePool[PlayerResource.STEEL] += resources[PlayerResource.STEEL];
    }


    void Start()
    {
        playerResourcePool = new Dictionary<PlayerResource, float>()
        {
             {PlayerResource.METHANE, 0},
             {PlayerResource.STEEL, 0}
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateTick()
    {
        foreach (Planet p in playerControlledPlanets)
        {
            AddToResourcePool(p.GetTickResources());
        }
    }

    public void AddPlanet(Planet p)
    {
        playerControlledPlanets.Add(p);
    }

    public void RemovePlanet(Planet p)
    {
        playerControlledPlanets.Remove(p);
    }

    public void AddToResourcePool(Dictionary<PlayerResource, float> resources)
    {
        foreach (PlayerResource res in resources.Keys)
        {
            if (!(playerResourcePool.ContainsKey(res)))
            {
                playerResourcePool.Add(res, resources[res]);
            }

            playerResourcePool[res] += resources[res];
        }
    }

    public void RemoveFromResourcePool(Dictionary<PlayerResource, float> resources)
    {
        foreach (PlayerResource res in resources.Keys)
        {
            if (!(playerResourcePool.ContainsKey(res)))
            {
                playerResourcePool.Add(res, resources[res]);
            }

            playerResourcePool[res] -= resources[res];
        }
    }

    public bool QueryResources(Dictionary<PlayerResource, float> resources)
    {
        foreach (PlayerResource res in resources.Keys)
        {
            if (!(playerResourcePool.ContainsKey(res)) || playerResourcePool[res] < resources[res])
            {
                return false;
            }
        }

        return true;
    }

}
