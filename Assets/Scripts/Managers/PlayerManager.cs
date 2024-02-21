using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public List<Planet> playerControlledPlanets = new List<Planet>();

    public Dictionary<Currency, int> playerCurrencies;

    public Dictionary<Commodity, int> playerCommodities;

    public float PlayerCredit => playerCurrencies[Currency.CREDIT];
    public float PlayerResearch => playerCurrencies[Currency.RESEARCH];
    public Dictionary<Commodity, int> PlayerCommodityMilestones => GetCommodityMilestones();

    /*
     * Initialization
     */
    void Awake()
    {
        playerCurrencies = new Dictionary<Currency, int>(PlayerSettings.DEFAULT_STARTING_CURRENCY);
        playerCommodities = new()
        {
            { Commodity.CONSTRUCTION, 0 },
            { Commodity.ALLOY, 0 }
        };
    }

    /*
     * Tick and update
     */
    public void UpdateTick()
    {
        foreach (Planet p in playerControlledPlanets)
        {
            AddCurrency(p.GetTickCurrency());
        }
    }

    /*
     * Control
     */
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

    public void AddCurrency(Dictionary<Currency, int> currency)
    {
        foreach (Currency c in currency.Keys)
        {
            if (!(playerCurrencies.ContainsKey(c)))
            {
                playerCurrencies.Add(c, currency[c]);
            }

            else
            {
                playerCurrencies[c] += currency[c];
            }
        }
    }

    public void RemoveCurrency(Dictionary<Currency, int> currency)
    {
        foreach (Currency c in currency.Keys)
        {
            if (!(playerCurrencies.ContainsKey(c)))
            {
                playerCurrencies.Add(c, currency[c]);
            }

            else
            {
                playerCurrencies[c] -= currency[c];
            }
        }
    }

    public bool QueryCurrency(Dictionary<Currency, int> currency)
    {
        foreach (Currency c in currency.Keys)
        {
            if (!(playerCurrencies.ContainsKey(c)) || playerCurrencies[c] < currency[c])
            {
                return false;
            }
        }

        return true;
    }

    private Dictionary<Commodity, int> GetCommodityMilestones()
    {
        Dictionary<Commodity, int> milestones = new()
        {
            { Commodity.CONSTRUCTION, 0 },
            { Commodity.ALLOY, 0 }
        };

        ResetCommodities();
        AddAllCommodities();

        foreach (Commodity c in playerCommodities.Keys)
        {
            int total = playerCommodities[c];

            if (total >= 7)
            {
                milestones[c] = 3;
            }
            else if (total >= 3)
            {
                milestones[c] = 2;
            }
            else if (total >= 1)
            {
                milestones[c] = 1;
            }
        }

        return milestones;
    }

    public int GetCommodityMilestone(Commodity c)
    {

        Dictionary<Commodity, int> milestone = GetCommodityMilestones();

        return milestone[c];

    }


    private void AddAllCommodities()
    {
        foreach (Planet p in playerControlledPlanets)
        {
            AddCommodities(p.GetAllCommodities());
        }
    }

    private void AddCommodities(Dictionary<Commodity, int> commodities)
    {
        foreach (Commodity c in commodities.Keys)
        {
            if (!(playerCommodities.ContainsKey(c)))
            {
                playerCommodities.Add(c, commodities[c]);
            }

            else
            {
                playerCommodities[c] += commodities[c];
            }
        }
    }

    private void ResetCommodities()
    {
        foreach (Commodity c in playerCommodities.Keys)
        {
            playerCommodities[c] = 0;
        }
    }
}
public enum Currency
{
    CREDIT,
    RESEARCH,
}

public enum Commodity
{
    CONSTRUCTION,
    ALLOY,
}
