using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public List<Planet> playerControlledPlanets = new List<Planet>();

    public Dictionary<Currency, int> playerCurrency;

    public Dictionary<Commodity, int> playerCommodity;

    public float PlayerCredit => playerCurrency[Currency.CREDIT];
    public float PlayerResearch => playerCurrency[Currency.RESEARCH];
    public float PlayerConstruction => playerCommodity[Commodity.CONSTRUCTION];
    public float PlayerAlloy => playerCommodity[Commodity.ALLOY];

    /*
     * Initialization
     */
    void Awake()
    {
        playerCurrency = new Dictionary<Currency, int>(PlayerSettings.DEFAULT_STARTING_CURRENCY);
    }

    /*
     * Tick
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
            if (!(playerCurrency.ContainsKey(c)))
            {
                playerCurrency.Add(c, currency[c]);
            }

            else
            {
                playerCurrency[c] += currency[c];
            }
        }
    }

    public void RemoveCurrency(Dictionary<Currency, int> currency)
    {
        foreach (Currency c in currency.Keys)
        {
            if (!(playerCurrency.ContainsKey(c)))
            {
                playerCurrency.Add(c, currency[c]);
            }

            else
            {
                playerCurrency[c] -= currency[c];
            }
        }
    }

    public bool QueryCurrency(Dictionary<Currency, int> currency)
    {
        foreach (Currency c in currency.Keys)
        {
            if (!(playerCurrency.ContainsKey(c)) || playerCurrency[c] < currency[c])
            {
                return false;
            }
        }

        return true;
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
