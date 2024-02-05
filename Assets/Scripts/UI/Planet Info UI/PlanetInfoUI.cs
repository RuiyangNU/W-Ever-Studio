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

    private Planet linkedPlanet = null;

    public bool isUIOpen = false;

    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();

        closeButton = FindObjectOfType<PlanetInfoCloseButton>();
        refineryButton = FindObjectOfType<PlanetInfoRefineryButton>();
        shipyardButton = FindObjectOfType<PlanetInfoShipyardButton>();

        CloseUI();
    }

    /// <summary>
    /// Links the IClickableUI object to this UI. Will call the OnUIClose() method of the object when this UI is closed.
    /// </summary>
    public void Link(Planet obj)
    {
        if (linkedPlanet == null) { throw new NullReferenceException("Link was called but a null object was given."); }

        linkedPlanet = obj;
    }

    private void UpdateProperties()
    {
        // refinery button
        refineryButton.showOnOpen = linkedPlanet.numRefineries == linkedPlanet.refineryLimit;
        
        // shipyard button
        shipyardButton.showOnOpen = linkedPlanet.numShipyards == linkedPlanet.shipyardLimit;
    }

    override public void OpenUI()
    {
        if (linkedPlanet == null)
        {
            Debug.LogError("OpenUI was called, but no object was linked to " + this.name + ". Make sure to call Link from the object opening this UI first.");
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

        if (linkedPlanet != null)
        {
            linkedPlanet.OnUIClose();
            linkedPlanet = null;
        }

        isUIOpen = false;
    }
}
