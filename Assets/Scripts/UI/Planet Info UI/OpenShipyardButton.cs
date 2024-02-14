using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OpenShipyardButton : PopupUIElement
{
    private PlanetInfoUI planetInfoUI;

    private Image _image;
    private Button _button;

    private TextMeshProUGUI buttonText;

    private bool showOnOpen = false;

    // Start is called before the first frame update
    void Start()
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
        showOnOpen = planet.owner == Owner.PLAYER && planet.HasBuilding(BuildingID.SHIPYARD);

        if (showOnOpen)
        {
            _image.enabled = true;
            _button.enabled = true;
            buttonText.enabled = true;
        }
    }
}
