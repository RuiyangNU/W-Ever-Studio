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
    public bool isShipyardOpen = false;

    public TextMeshProUGUI buildingSlot;
    public TextMeshProUGUI creditIncome;
    public TextMeshProUGUI scienceIncome;
    public TextMeshProUGUI shipyardText;
    public TextMeshProUGUI planetName;
    public TextMeshProUGUI captureText;

    public GameObject infoPanel;

    public BuildingInfoUI buildingInfoUI;

    public GameObject shipyardPanel;

    public List<GameObject> shipConstructionDisplay;

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
            if(isShipyardOpen)
            {
                UpdateShipyardUI();
            }
        }
    }
    /// <summary>
    /// Links the IClickableUI object to this UI. Will call the OnUIClose() method of the object when this UI is closed.
    /// </summary>
    public void Link(Planet obj)
    {
        if (obj == null) { throw new NullReferenceException("Link was called but a null object was given."); }

        linkedPlanet = obj;
        buildingInfoUI.CloseUI();
        CloseShipyardUI();
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
        FleetInfoUI f = FindObjectOfType<FleetInfoUI>();
        if (f.isUIOpen)
        {
            f.CloseUI();
        }

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
        CloseShipyardUI();
        infoPanel.SetActive(false);

    }

    public void OpenShipyardUI()
    {
        if (linkedPlanet.HasBuilding(BuildingID.SHIPYARD))
        {
            shipyardPanel.SetActive(true);
            isShipyardOpen = true;
        }
        else
        {
            return;
        }
    }

    public void CloseShipyardUI()
    {
        shipyardPanel.SetActive(false);
        isShipyardOpen = false;
    }

    public void ChangeShipyardUI()
    {
        if(isShipyardOpen)
        {
            CloseShipyardUI();
            
        }
        else
        {
            OpenShipyardUI();
        }
    }

    public void UpdateShipyardUI()
    {
        if(linkedPlanet.CanBuildShip(ShipID.MONO) == 0)
        {
            shipConstructionDisplay[0].transform.GetChild(3).gameObject.SetActive(true);
        }
        else
        {
            shipConstructionDisplay[0].transform.GetChild(3).gameObject.SetActive(false);
        }


    }

    public void BuildMono()
    {
        linkedPlanet.BuildShip(ShipID.MONO);
        CloseShipyardUI();
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
                ShipyardBuilding shipyard = (ShipyardBuilding)linkedPlanet.GetBuilding(BuildingID.SHIPYARD);

                if (shipyard.IsActive)
                {
                    shipyardText.text = "Busy: " + ((ShipyardBuilding)linkedPlanet.GetBuilding(BuildingID.SHIPYARD)).TurnsLeft.ToString();
                }
                else
                {
                    shipyardText.text = "Shipyard Idle";
                }
            }
            else
            {
                shipyardText.text = "No Shipyard";
            }

            if (linkedPlanet.UnderAttack)
            {
                captureText.text = linkedPlanet.TurnsUntilCaptured.ToString() + "Turns Until Capture";
            }
            else
            {
                captureText.text = "Not Under Attack";
            }
        }
        else if (linkedPlanet.owner != Owner.PLAYER)
        {
            ChangeTextColor(Color.red);
            creditIncome.text = "???";


            scienceIncome.text = "???";

            buildingSlot.text = linkedPlanet.buildings.Count.ToString() + "/" + linkedPlanet.buildingLimit.ToString();

            shipyardText.text = "???";

            if (linkedPlanet.UnderAttack)
            {
                captureText.text = linkedPlanet.TurnsUntilCaptured.ToString() + "Turns Until Capture";
            }
            else
            {
                captureText.text = "Not Under Attack";
            }
        }


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
        captureText.color = color;
    }
}
