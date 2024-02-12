using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Fleet;

public class BuildDestroyerButton : PopupUIElement
{
    public bool showOnOpen = true;

    private PlanetInfoUI planetInfoUI;

    private Image _image;
    private Button _button;

    private TextMeshProUGUI buttonText;

    void Awake()
    {
        planetInfoUI = FindObjectOfType<PlanetInfoUI>();
        _image = GetComponent<Image>();
        _button = GetComponent<Button>();
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public override void OnUIClose()
    {
        _image.enabled = false;
        _button.enabled = false;
        buttonText.enabled = false;
    }

    public override void OnUIOpen()
    {
        Planet planet = planetInfoUI.linkedPlanet;
        showOnOpen = planet.owner == Planet.PlanetOwner.PLAYER && planet.shipyardFleet == ShipID.NONE;

        if (showOnOpen)
        {
            _image.enabled = true;
            _button.enabled = true;
            buttonText.enabled = true;
        }
    }
}
