using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlanetInfoText : PopupUIElement
{
    private PlanetInfoUI planetInfoUI;

    private TextMeshProUGUI _text;

    private void Awake()
    {
        planetInfoUI = FindObjectOfType<PlanetInfoUI>();
        _text = GetComponent<TextMeshProUGUI>();
    }

    public void SetText(string text)
    {
        _text.text = text;
    }

    public override void OnUIClose()
    {
        _text.enabled = false;
    }

    public override void OnUIOpen()
    {
        Planet planet = planetInfoUI.linkedPlanet;

        string text = "Refineries: " + planet.numRefineries + ", Limit: " + planet.maxRefineries + "\n";
        text += "Shipyards: " + planet.numShipyards + ", Limit: " + planet.maxShipyards + "\n";

        if (planet.owner == Planet.PlanetOwner.NONE)
        {
            text += "\n";

            text += "Methane: ";
            if (planet.baseMethanePerTick <= 0)
            {
                text += "None\n";
            }
            else if (planet.baseMethanePerTick <= 2)
            {
                text += "Low\n";
            }
            else if (planet.baseMethanePerTick <= 5)
            {
                text += "Medium\n";
            }
            else
            {
                text += "High\n";
            }

            text += "Steel: ";
            if (planet.baseSteelPerTick <= 0)
            {
                text += "None\n";
            }
            else if (planet.baseSteelPerTick <= 2)
            {
                text += "Low\n";
            }
            else if (planet.baseSteelPerTick <= 5)
            {
                text += "Medium\n";
            }
            else
            {
                text += "High\n";
            }
        }

        SetText(text);

        _text.enabled = true;
    }
}
