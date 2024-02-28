using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public List<Planet> playerControlledPlanets = new List<Planet>();

    public Dictionary<Currency, int> playerCurrencies;

    public Dictionary<Commodity, int> playerCommodities;

    public Dictionary<Tech, int> playerTech;

    public int PlayerCMLevel => playerCommodities[Commodity.CONSTRUCTION];
    public int PlayerAlloyLevel => playerCommodities[Commodity.ALLOY];
    public int PlayerCMMilestone => GetCommodityMilestone(Commodity.CONSTRUCTION);
    public int PlayerAlloyMilestone => GetCommodityMilestone(Commodity.ALLOY);
    public int PlayerShipTier => playerTech[Tech.SHIP_TIER];
    public int PlayerDamageTier => playerTech[Tech.DAMAGE];
    public int PlayerResistanceTier => playerTech[Tech.RESISTANCE];
    public int PlayerCredit => playerCurrencies[Currency.CREDIT];
    public int PlayerResearch => playerCurrencies[Currency.RESEARCH];
    public Dictionary<Commodity, int> PlayerCommodityMilestones => GetCommodityMilestones();

    public static PlayerManager playerManager;

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
        playerTech = new()
        {
            { Tech.SHIP_TIER, 0 },
            { Tech.DAMAGE, 0 },
            { Tech.RESISTANCE, 0 }
        };

        PlayerManager.playerManager = this;
    }

    /*
     * Tick and update
     */
    public void UpdateTick()
    {
        foreach (Planet p in playerControlledPlanets)
        {
            AddCurrencies(p.GetTickCurrencies());
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

    public void AddCurrency(Currency currency, int amount)
    {
        playerCurrencies[currency] += amount;
    }

    public void RemoveCurrency(Currency currency, int amount)
    {
        if (playerCurrencies[currency] < amount)
        {
            Debug.LogError("Tried to remove more " + currency.ToString() + " than the player has.");
            return;
        }
        playerCurrencies[currency] -= amount;
    }

    public void AddCurrencies(Dictionary<Currency, int> currency)
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

    public void RemoveCurrencies(Dictionary<Currency, int> currency)
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
    /*
     * Information
     */
    // Currency
    public Dictionary<Currency, int> GetCurrenciesPerTick()
    {
        Dictionary<Currency, int> currencies = new()
        {
            { Currency.CREDIT, 0 },
            { Currency.RESEARCH, 0 }
        };

        foreach (Planet p in playerControlledPlanets)
        {
            Dictionary<Currency, int> currency = p.GetTickCurrencies();
            foreach (Currency c in currency.Keys)
            {
                currencies[c] += currency[c];
            }
        }

        return currencies;
    }

    public int GetCurrencyPerTick(Currency c)
    {
        return GetCurrenciesPerTick()[c];
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

    // Commodity
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

    public bool QueryCommodityMilestone(Commodity c, int m)
    {

        Dictionary<Commodity, int> milestone = GetCommodityMilestones();

        return (milestone[c] >= m);

    }

    public bool QueryCommodityMilestones(Dictionary<Commodity, int> commodities)
    {

        Dictionary<Commodity, int> milestones = GetCommodityMilestones();


        foreach(Commodity c in commodities.Keys)
        {
            if (commodities[c] > milestones[c])
            {
                return false;
            }
        }

        return true;

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

        foreach (var c in playerCommodities.Keys.ToList())
        {
            playerCommodities[c] = 0;
        }
    }

    // Research
    public int GetResearchCost(Tech tech)
    {
        switch (tech)
        {
            case Tech.SHIP_TIER:
                return PlayerShipTier switch
                {
                    0 => 10,
                    1 => 30,
                    2 => 60,
                    _ => 999
                };

            case Tech.DAMAGE:
                return PlayerDamageTier switch
                {
                    0 => 3,
                    1 => 6,
                    2 => 12,
                    3 => 24,
                    4 => 48,
                    _ => 999
                };

            case Tech.RESISTANCE:
                return PlayerResistanceTier switch
                {
                    0 => 3,
                    1 => 6,
                    2 => 12,
                    3 => 24,
                    4 => 48,
                    _ => 999
                };
            default:
                Debug.LogError("Unknown Tech Type.");
                return -1;
        }
    }
    public bool CanResearchTech(Tech tech)
    {
        return playerCurrencies[Currency.RESEARCH] > GetResearchCost(tech);
    }
    public void ResearchTech(Tech tech)
    {
        playerCurrencies[Currency.RESEARCH] -= GetResearchCost(tech);
        playerTech[tech] += 1;
    }
    public bool HasResearchForShip(ShipID shipID)
    {
        return shipID switch
        {
            ShipID.MONO => true,
            ShipID.FLARE => PlayerShipTier >= 1,
            ShipID.SPARK => PlayerShipTier >= 1,
            ShipID.PULSE => PlayerShipTier >= 1,
            ShipID.EMBER => PlayerShipTier >= 2,
            ShipID.VOLT => PlayerShipTier >= 2,
            ShipID.BLAST => PlayerShipTier >= 2,
            _ => false
        };
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

public enum Tech
{
    SHIP_TIER,
    DAMAGE,
    RESISTANCE,
}
