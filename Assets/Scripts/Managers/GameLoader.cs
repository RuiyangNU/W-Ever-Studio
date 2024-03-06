using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Planet;

public class GameLoader : MonoBehaviour
{
    private GameManager gameManager;
    private HexMapEditor hexMapEditor;
    private HexGrid hexGrid;

    private Dictionary<HexCoordinates, Owner> INITIAL_PLANETS = new Dictionary<HexCoordinates, Owner>
    {
        { new HexCoordinates(1, 2), Owner.PLAYER },
        { new HexCoordinates(1, 6), Owner.NONE },
        { new HexCoordinates(-2, 11), Owner.NONE },
        { new HexCoordinates(5, 5), Owner.NONE },
        { new HexCoordinates(10, 3), Owner.ENEMY },
        { new HexCoordinates(4, 10), Owner.ENEMY },
        { new HexCoordinates(9, 8), Owner.ENEMY },
        { new HexCoordinates(15, 4), Owner.ENEMY },
        { new HexCoordinates(10, 12), Owner.ENEMY },
    };

    private List<string> INITIAL_Name = new List<string>
    {
        "Chandra5-B",
        "Peno3-C",
        "Vega122-A",
        "Sirius6-D",
        "Lyra4-E",
        "Rigel1-B",
        "Altair7-C",
        "Deneb5-A",
        "Caligula",
    };

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        hexMapEditor = FindObjectOfType<HexMapEditor>();
        hexGrid = FindObjectOfType<HexGrid>();
    }

    private void Start()
    {
        int i = 0;
        foreach (HexCoordinates coords in INITIAL_PLANETS.Keys)
        {
            string name = INITIAL_Name[i];
            i++;
            //Planet planet = Instantiate(Planet.planetPrefab);
            //planet.SetProperties(
            //    INITIAL_PLANETS[coords],
            //    Random.Range(100, 200),
            //    3
            //    );

            //gameManager.AddPlanetToCell(hexGrid.GetCell(coords), planet);
            //gameManager.CreatePlanet()
            //Debug.Log(hexGrid);
            gameManager.CreatePlanet(hexGrid.GetCell(coords), INITIAL_PLANETS[coords], Random.Range(100, 200), 3, name);
        }    
    }
}
