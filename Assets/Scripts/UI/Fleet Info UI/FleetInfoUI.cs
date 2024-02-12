using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FleetInfoUI : PopupUI
{
    private FleetInfoCloseButton closeButton;
    private FleetInfoFleetButton fleetButton;
    private FleetInfoText infoText;

    private Fleet linkedFleet = null;

    public bool isUIOpen = false;

    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();

        closeButton = FindObjectOfType<FleetInfoCloseButton>();
        fleetButton = FindObjectOfType<FleetInfoFleetButton>();
        infoText = FindObjectOfType<FleetInfoText>();

        CloseUI();
    }

    /// <summary>
    /// Links the IClickableUI object to this UI. Will call the OnUIClose() method of the object when this UI is closed.
    /// </summary>
    public void Link(Fleet obj)
    {
        if (obj == null) { throw new NullReferenceException("Link was called but a null object was given."); }

        linkedFleet = obj;
    }

    private void UpdateProperties()
    {
        // info text
        string text = "Fleets: " + linkedFleet.numFleets + "\n" + "Health: " + linkedFleet.health + "\n" +
        "Damage: " + linkedFleet.damage + "\n" + "Speed: " + linkedFleet.speed + "\n" + "Action Points: "
        + linkedFleet.actionPoints;
        infoText.SetText(text);
    }

    override public void OpenUI()
    {
        if (linkedFleet == null)
        {
            Debug.LogError("OpenUI was called, but no object was linked to " + this.name + ". Make sure to call Link from the object opening this UI first.");
            return;
        }

        UpdateProperties();

        _image.enabled = true;

        foreach (PopupUIElement child in children)
        {
            child.OnUIOpen();
        }


        isUIOpen = true;
    }

    override public void CloseUI()
    {
        _image.enabled = false;

        foreach (PopupUIElement child in children)
        {
            child.OnUIClose();
        }

        if (linkedFleet != null)
        {
            linkedFleet.OnUIClose();
            linkedFleet = null;
        }

        isUIOpen = false;
    }

    private void CloseUIWithoutUnlink()
    {
        _image.enabled = false;

        foreach (PopupUIElement child in children)
        {
            child.OnUIClose();
        }

        isUIOpen = false;
    }

    public override void UpdateUI()
    {
        if (linkedFleet == null)
        {
            Debug.LogError("UpdateUI was called, but no object was linked to " + this.name + ". Make sure to call Link from the object opening this UI first.");
            return;
        }

        CloseUIWithoutUnlink();
        OpenUI();
    }
}
