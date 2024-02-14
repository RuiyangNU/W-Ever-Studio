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

        SetText(text);
        _text.enabled = true;
    }
}
