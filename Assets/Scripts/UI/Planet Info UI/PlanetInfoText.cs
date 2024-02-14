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

        string text = "";

        if (planet.owner == Owner.ENEMY)
        {
            text += "Enemy Controlled - No Information\n";
            text += "\n";
        }

        else
        {
            if (planet.owner == Owner.NONE)
            {
                
            }
        }

        if (planet.UnderAttack)
        {
            text += "Capturing! " + planet.captureTimer + " Turns Remaining";

            if (planet.owner != Owner.NONE)
            {
                text += "- Shipyard port sabotaged.";
            }
        }
        else if (planet.Occupied && planet.owner != Owner.NONE)
        {
            text += "Occupied - Shipyard port disabled.";
        }

        SetText(text);

        _text.enabled = true;
    }
}
