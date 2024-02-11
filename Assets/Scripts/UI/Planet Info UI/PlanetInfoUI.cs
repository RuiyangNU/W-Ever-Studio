using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlanetInfoUI : PopupUI
{
    private PlanetInfoCloseButton closeButton;
    private PlanetInfoRefineryButton refineryButton;
    private PlanetInfoShipyardButton shipyardButton;
    private PlanetInfoText infoText;

    private Planet linkedPlanet = null;

    public bool isUIOpen = false;

    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();

        closeButton = FindObjectOfType<PlanetInfoCloseButton>();
        refineryButton = FindObjectOfType<PlanetInfoRefineryButton>();
        shipyardButton = FindObjectOfType<PlanetInfoShipyardButton>();
        infoText = FindObjectOfType<PlanetInfoText>();

        CloseUI();
    }

    /// <summary>
    /// Links the IClickableUI object to this UI. Will call the OnUIClose() method of the object when this UI is closed.
    /// </summary>
    public void Link(Planet obj)
    {
        if (obj == null) { throw new NullReferenceException("Link was called but a null object was given."); }

        linkedPlanet = obj;
    }


    private void UpdateProperties()
    {
        // refinery button
        refineryButton.showOnOpen = linkedPlanet.numRefineries < linkedPlanet.maxRefineries;
        
        // shipyard button
        shipyardButton.showOnOpen = linkedPlanet.numShipyards < linkedPlanet.maxShipyards;

        // info text
        string text = "Refineries: " + linkedPlanet.numRefineries + ", Limit: " + linkedPlanet.maxRefineries + "\n";
        text += "Shipyards: " + linkedPlanet.numShipyards + ", Limit: " + linkedPlanet.maxShipyards + "\n";
        infoText.SetText(text);
    }

    public void BuildRefinery()
    {
        if (linkedPlanet == null)
        {
            Debug.LogError("BuildRefinery was called, but no object was linked to " + this.name + ". Make sure to call Link from the object opening this UI first.");
            return;
        }

        linkedPlanet.BuildRefinery();
    }

    public void BuildShipyard()
    {
        if (linkedPlanet == null)
        {
            Debug.LogError("BuildShipyard was called, but no object was linked to " + this.name + ". Make sure to call Link from the object opening this UI first.");
            return;
        }

        linkedPlanet.BuildShipyard();
    }

    override public void OpenUI()
    {
        if (linkedPlanet == null)
        {
            Debug.LogError("OpenUI was called, but no object was linked to " + this.name + ". Make sure to call Link from the object opening this UI first.");
            return;
        }

        UpdateProperties();

        _image.enabled = true;

        foreach (PopupUIElement child in children)
        {
            //Debug.Log(child);
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

        if (linkedPlanet != null)
        {
            linkedPlanet.OnUIClose();
            linkedPlanet = null;
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
        if (linkedPlanet == null)
        {
            Debug.LogError("UpdateUI was called, but no object was linked to " + this.name + ". Make sure to call Link from the object opening this UI first.");
            return;
        }

        CloseUIWithoutUnlink();
        OpenUI();
    }
}
