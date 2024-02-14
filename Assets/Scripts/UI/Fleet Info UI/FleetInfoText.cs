using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FleetInfoText : PopupUIElement
{
    private FleetInfoUI fleetInfoUI;

    private TextMeshProUGUI _text;

    private void Awake()
    {
        fleetInfoUI = FindObjectOfType<FleetInfoUI>();
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
        Fleet fleet = fleetInfoUI.linkedFleet;
        string text = "Hull: " + fleet.Hull + "\n";
        text += "ATK: " + fleet.Damage + "\n";
        text += "AP: " + fleet.ActionPoints;

        SetText(text);
        _text.enabled = true;
    }
}
