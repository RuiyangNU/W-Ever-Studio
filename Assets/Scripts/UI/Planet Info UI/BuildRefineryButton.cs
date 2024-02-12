using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildRefineryButton : PopupUIElement
{
    private PlanetInfoUI planetInfoUI;

    private Image _image;
    private Button _button;

    private TextMeshProUGUI buttonText;

    private bool showOnOpen = true;

    void Awake()
    {
        planetInfoUI = FindObjectOfType<PlanetInfoUI>();
        _image = GetComponent<Image>();
        _button = GetComponent<Button>();
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
    }

    override public void OnUIClose()
    {
        _image.enabled = false;
        _button.enabled = false;
        buttonText.enabled = false;
    }
    override public void OnUIOpen()
    {
        Planet planet = planetInfoUI.linkedPlanet;
        showOnOpen = planet.owner == Planet.PlanetOwner.PLAYER && planet.numRefineries < planet.maxRefineries;

        if (showOnOpen)
        {
            _image.enabled = true;
            _button.enabled = true;
            buttonText.enabled = true;
        }
    }
}
