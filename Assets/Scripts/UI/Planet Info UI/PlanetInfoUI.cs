using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Fleet;

public class PlanetInfoUI : PopupUI
{
    public Planet linkedPlanet = null;

    public bool isUIOpen = false;

    public TextMeshProUGUI buildingSlot;
    public TextMeshProUGUI creditIncome;
    public TextMeshProUGUI scienceIncome;
    public TextMeshProUGUI shipyardText;
    public TextMeshProUGUI planetName;

    public GameObject infoPanel;

    public BuildingInfoUI buildingInfoUI;

    // Start is called before the first frame update
    protected void Start()
    {
        //base.Start();
        CloseUI();
    }

    public void Update()
    {
        if (linkedPlanet != null)
        {
            UpdateUI();
        }
    }
    /// <summary>
    /// Links the IClickableUI object to this UI. Will call the OnUIClose() method of the object when this UI is closed.
    /// </summary>
    public void Link(Planet obj)
    {
        if (obj == null) { throw new NullReferenceException("Link was called but a null object was given."); }

        linkedPlanet = obj;
        UpdateUI();
    }

    override public void OpenUI()
    {
        if (linkedPlanet == null)
        {
            Debug.LogError("OpenUI was called, but no object was linked to " + this.name + ". Make sure to call Link from the object opening this UI first.");
            return;
        }

        //foreach (PopupUIElement child in children)
        //{
        //    child.OnUIOpen();
        //}

        isUIOpen = true;
        infoPanel.SetActive(true);
    }

    override public void CloseUI()
    {
        //_image.enabled = false;

        //foreach (PopupUIElement child in children)
        //{
        //    child.OnUIClose();
        //}

        if (linkedPlanet != null)
        {
            linkedPlanet.OnUIClose();
            linkedPlanet = null;
        }

        isUIOpen = false;
        buildingInfoUI.CloseUI();
        infoPanel.SetActive(false);
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
        if (linkedPlanet == null)
        {
            Debug.LogError("UpdateUI was called, but no object was linked to " + this.name + ". Make sure to call Link from the object opening this UI first.");
            return;
        }

        if (linkedPlanet.IsPlayerOwned)
        {
            //GetTickCurrency();
            ChangeTextColor();
            creditIncome.text = linkedPlanet.GetTickCurrency(Currency.CREDIT).ToString() + "/Turn";

            scienceIncome.text = linkedPlanet.GetTickCurrency(Currency.RESEARCH).ToString() + "/Turn";

            buildingSlot.text = linkedPlanet.buildings.Count.ToString() + "/" + linkedPlanet.buildingLimit.ToString();

            if (linkedPlanet.HasBuilding(BuildingID.SHIPYARD))
            {
                shipyardText.text = "Shipyard: T" + linkedPlanet.GetBuildingLevel(BuildingID.SHIPYARD).ToString();
            }
            else
            {
                shipyardText.text = "No Shipyard";
            }
        }
        else if (linkedPlanet.owner == Owner.ENEMY)
        {
            ChangeTextColor(Color.red);
            creditIncome.text = "???";


            scienceIncome.text = "???";

            buildingSlot.text = linkedPlanet.buildings.Count.ToString() + "/" + linkedPlanet.buildingLimit.ToString();

            shipyardText.text = "???";

            
        }

        //if (isUIOpen)
        //{
        //    CloseUIWithoutUnlink();
        //    OpenUI();
        //}

            //Update Name



            //Update Stat


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
        creditIncome.color = color;
        scienceIncome.color = color;
        buildingSlot.color = color;
        shipyardText.color = color;
    }
}
