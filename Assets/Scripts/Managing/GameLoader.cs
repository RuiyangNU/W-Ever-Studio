using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Planet;

public class GameLoader : MonoBehaviour
{
    private GameManager gameManager;
    private HexMapEditor hexMapEditor;
    private HexGrid hexGrid;

    private Dictionary<HexCoordinates, PlanetOwner> INITIAL_PLANETS = new Dictionary<HexCoordinates, PlanetOwner>
    {
        { new HexCoordinates(1, 2), PlanetOwner.PLAYER },
        { new HexCoordinates(1, 6), PlanetOwner.NONE },
        { new HexCoordinates(-2, 11), PlanetOwner.NONE },
        { new HexCoordinates(5, 5), PlanetOwner.NONE },
        { new HexCoordinates(10, 3), PlanetOwner.ENEMY },
        { new HexCoordinates(4, 10), PlanetOwner.ENEMY },
        { new HexCoordinates(9, 8), PlanetOwner.ENEMY },
        { new HexCoordinates(15, 4), PlanetOwner.ENEMY },
        { new HexCoordinates(10, 12), PlanetOwner.ENEMY },
    };

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        hexMapEditor = FindObjectOfType<HexMapEditor>();
        hexGrid = FindObjectOfType<HexGrid>();
    }

    private void Start()
    {
        foreach (HexCoordinates coords in INITIAL_PLANETS.Keys)
        {
            Planet planet = Instantiate(Planet.planetPrefab);
            planet.SetProperties(
                INITIAL_PLANETS[coords],
                Random.Range(1.0f, 3.0f),
                Random.Range(1.0f, 3.0f),
                1,
                1
                );

            gameManager.AddPlanetToCell(hexGrid.GetCell(coords), planet);
        }    
    }
}
