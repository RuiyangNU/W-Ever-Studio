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

    public Dictionary<PlayerResource, float> playerResourcePool;

    public float PlayerMethane => playerResourcePool[PlayerResource.METHANE];
    public float PlayerSteel => playerResourcePool[PlayerResource.STEEL];

    void Awake()
    {
        playerResourcePool = PlayerDefaultSettings.DEFAULT_STARTING_RESOURCES;
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
        if (playerControlledPlanets.Contains(p))
        {
            Debug.LogWarning("Tried to add " + p.name + " to PlayerManager, but " + p.name + " was already in the list.");
            return;
        }
        playerControlledPlanets.Add(p);
    }

    public void RemovePlanet(Planet p)
    {
        if (!(playerControlledPlanets.Remove(p)))
        {
            Debug.LogWarning("Tried to remove " + p.name + " from PlayerManager, but " + p.name + " was not in the list.");
        }
    }

    public void AddToResourcePool(Dictionary<PlayerResource, float> resources)
    {
        foreach (PlayerResource res in resources.Keys)
        {
            if (!(playerResourcePool.ContainsKey(res)))
            {
                playerResourcePool.Add(res, resources[res]);
            }

            else
            {
                playerResourcePool[res] += resources[res];
            }
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

            else
            {
                playerResourcePool[res] -= resources[res];
            }
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
