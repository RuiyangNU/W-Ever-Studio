using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Fleet;

public class ShipyardInfoText : PopupUIElement
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
        if (planet.shipyardFleet == ShipID.NONE)
        {
            text = "Idle";
        }
        else
        {
            if (planet.UnderAttack)
            {
                text += "Sabotaged";
            }
            else if (planet.Occupied)
            {
                text += "Overcrowded";
            }
            else
            {
                text = "Turns left: ";
                text += (planet.shipyardTurnsLeft + 1).ToString();
            }
        }

        SetText(text);
        _text.enabled = true;
    }
}
