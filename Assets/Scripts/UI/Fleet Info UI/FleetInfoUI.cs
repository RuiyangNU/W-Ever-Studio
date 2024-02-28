using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FleetInfoUI : PopupUI
{
    public Fleet linkedFleet = null;

    public GameObject fleetPanel;

    public bool isUIOpen = false;

    public PlanetInfoUI planetInfoUI;

    public TextMeshProUGUI attackText;
    public TextMeshProUGUI actionPointText;
    public TextMeshProUGUI resistenceText;

    public GameObject damageIcon;
    public GameObject shipSprite;

    public GameObject hullBar;
    public GameObject shieldBar;

    public TextMeshProUGUI hullText;
    public TextMeshProUGUI shieldText;
    //public GameObject infoPanel;

    // Start is called before the first frame update
    protected void Start()
    {
        //base.Start();

        CloseUI();
    }

    protected void Update()
    {
        if (linkedFleet != null)
        {
            UpdateUI();
        }
    }

    /// <summary>
    /// Links the IClickableUI object to this UI. Will call the OnUIClose() method of the object when this UI is closed.
    /// </summary>
    public void Link(Fleet obj)
    {
        if (obj == null) { throw new NullReferenceException("Link was called but a null object was given."); }

        linkedFleet = obj;
        UpdateUI();
    }

    override public void OpenUI()
    {
        if (linkedFleet == null)
        {
            Debug.LogError("OpenUI was called, but no object was linked to " + this.name + ". Make sure to call Link from the object opening this UI first.");
            return;
        }

        if(planetInfoUI.isUIOpen)
        {
            planetInfoUI.CloseUI();
        }

        //_image.enabled = true;

        //foreach (PopupUIElement child in children)
        //{
        //    child.OnUIOpen();
        //}


        isUIOpen = true;
        fleetPanel.SetActive(true);
    }

    override public void CloseUI()
    {
        //_image.enabled = false;

        //foreach (PopupUIElement child in children)
        //{
        //    child.OnUIClose();
        //}

        if (linkedFleet != null)
        {
            linkedFleet.OnUIClose();
            linkedFleet = null;
        }

        isUIOpen = false;
        fleetPanel.SetActive(false);
        //UIManager.uiManager.CloseUI(gameObject);
    }

    private void CloseUIWithoutUnlink()
    {
        //_image.enabled = false;

        //foreach (PopupUIElement child in children)
        //{
        //    child.OnUIClose();
        //}

        isUIOpen = false;
    }

    public override void UpdateUI()
    {
        if (linkedFleet == null)
        {
            Debug.LogError("UpdateUI was called, but no object was linked to " + this.name + ". Make sure to call Link from the object opening this UI first.");
            return;
        }

        if (linkedFleet.IsPlayerOwned)
        {
            //GetTickCurrency();
            ChangeTextColor();
            attackText.text = linkedFleet.Damage.ToString();

            actionPointText.text = linkedFleet.ActionPoints.ToString() + "/" + linkedFleet.maxActionPoints;

            resistenceText.text = GenerateColoredText(linkedFleet.thermalRes.ToString(), "red") + "/" + GenerateColoredText(linkedFleet.kineticRes.ToString(), "white") + "/" + GenerateColoredText(linkedFleet.emRes.ToString(), "yellow");

            
        }
        else if (linkedFleet.owner == Owner.ENEMY)
        {
            ChangeTextColor(Color.red);

            attackText.text = linkedFleet.Damage.ToString();

            actionPointText.text = linkedFleet.ActionPoints.ToString() + "/" + linkedFleet.maxActionPoints;

            resistenceText.text = GenerateColoredText(linkedFleet.thermalRes.ToString(), "red") + "/" + GenerateColoredText(linkedFleet.kineticRes.ToString(), "white") + "/" + GenerateColoredText(linkedFleet.emRes.ToString(), "yellow");


        }


        //Update Health

        hullBar.GetComponent<Slider>().maxValue = linkedFleet.maxHull;
        hullBar.GetComponent<Slider>().minValue = 0;
        hullText.text = linkedFleet.Hull.ToString() + "/" + linkedFleet.maxHull.ToString();

        hullBar.GetComponent<Slider>().value = linkedFleet.Hull;


        shieldBar.GetComponent<Slider>().maxValue = linkedFleet.maxShield;
        shieldBar.GetComponent<Slider>().minValue = 0;

        shieldBar.GetComponent<Slider>().value = linkedFleet.Shield;
        shieldText.text = linkedFleet.Shield.ToString() + "/" + linkedFleet.maxShield.ToString();

        //CloseUIWithoutUnlink();
        //OpenUI();
    }

    public void ChangeTextColor()
    {
        //creditIncome.color = Color.green;
        //scienceIncome.color = Color.green;
        //buildingSlot.color = Color.green;
        //shipyardText.color = Color.green;
        ChangeTextColor(Color.green);
    }

    public void ChangeTextColor(Color color)
    {
        attackText.color = color;
        actionPointText.color = color;

    }

    public string GenerateColoredText(string text, string color)
    {
        return "<color=" + color + ">" + text + "</color>";
    }
}
