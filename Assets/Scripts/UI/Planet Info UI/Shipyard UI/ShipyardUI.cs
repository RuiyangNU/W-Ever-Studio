using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipyardUI : PopupUIElement
{
    private PlanetInfoUI planetInfoUI;

    private List<PopupUIElement> children;
    private Image _image;

    public bool show = false;

    private void Awake()
    {
        planetInfoUI = FindObjectOfType<PlanetInfoUI>();
        _image = GetComponent<Image>();

        // Find children
        children = new List<PopupUIElement>();
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            PopupUIElement childScript = child.GetComponent<PopupUIElement>();

            if (childScript != null)
            {
                children.Add(childScript);
            }
        }
    }

    public void Toggle()
    {
        show = !show;
        planetInfoUI.UpdateUI();
    }

    public override void OnUIClose()
    {
        _image.enabled = false;

        foreach (PopupUIElement child in children)
        {
            child.OnUIClose();
        }
    }

    public override void OnUIOpen()
    {
        Planet planet = planetInfoUI.linkedPlanet;
        if (show && planet.owner == Owner.PLAYER && planet.HasBuilding(BuildingID.SHIPYARD)) {
            _image.enabled = true;

            foreach (PopupUIElement child in children)
            {
                child.OnUIOpen();
            }
        }
    }

}
