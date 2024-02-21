using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BuildingInfoUI : PopupUI
{
    public Planet linkedPlanet = null;

    public GameObject constructionPanel;
    public GameObject mainInfoPanel;
    public List<GameObject> buildingInfoPanel;
    public List<GameObject> constructBuildings;

    public List<BuildingID> buildingOrder = new List<BuildingID>(new BuildingID[] { BuildingID.SHIPYARD, BuildingID.DEPOT, BuildingID.LAB, BuildingID.SMELTERY, BuildingID.TRADEHUB });
    public bool isUIOpen = false;

    public bool isConstructionUIOpen = false;

    public void Link()
    {
        //if (obj == null) { throw new NullReferenceException("Link was called but a null object was given."); }

        //linkedPlanet = obj;
        linkedPlanet = FindObjectOfType<PlanetInfoUI>().linkedPlanet;
        UpdateUI();
    }

    public void ChangeUI()
    {
        if (isUIOpen)
        {
            CloseUI();
        }
        else
        {
            OpenUI();
        }
    }


    public override void CloseUI()
    {
        //if (linkedPlanet == null)
        //{
        //    Debug.LogError("OpenUI was called, but no object was linked to " + this.name + ". Make sure to call Link from the object opening this UI first.");
        //    return;
        //}

        //foreach (PopupUIElement child in children)
        //{
        //    child.OnUIOpen();
        //}
        if(linkedPlanet == null)
        {
            isUIOpen = false;
            mainInfoPanel.SetActive(false);
            return;
        }

        if (linkedPlanet.IsPlayerOwned)
        {
            isUIOpen = false;
            mainInfoPanel.SetActive(false);
            return;
        }
    }

    public override void OpenUI()
    {
        Link();
        if (linkedPlanet == null)
        {
            Debug.LogError("OpenUI was called, but no object was linked to " + this.name + ". Make sure to call Link from the object opening this UI first.");
            return;
        }

        //foreach (PopupUIElement child in children)
        //{
        //    child.OnUIOpen();
        //}

        if (linkedPlanet.IsPlayerOwned)
        {
            isUIOpen = true;
            mainInfoPanel.SetActive(true);
            CloseConstructionUI();
        }
    }

    public override void UpdateUI()
    {
        //Get Planet Building Status
        if(!linkedPlanet)
        {
            return;
        }
        List<Building> buildings = linkedPlanet.buildings;

        for(int i = 0; i < 5; i++)
        {
            if (i < linkedPlanet.buildingLimit)
            {

            }
            else
            {
                buildingInfoPanel[i].transform.GetChild(0).gameObject.SetActive(false);
                buildingInfoPanel[i].transform.GetChild(1).gameObject.SetActive(false);
            }
        }

        //Update Each Info Panel
        for (int i = 0; i < linkedPlanet.buildingLimit; i++)
        {
            if(i < buildings.Count)
            {
                Building building = buildings[i];
                buildingInfoPanel[i].transform.GetChild(0).gameObject.SetActive(false);
                buildingInfoPanel[i].transform.GetChild(1).gameObject.SetActive(true);
                UpdateBuildingDisplay(building, buildingInfoPanel[i].transform.GetChild(1).gameObject);
            }
            else
            {
                buildingInfoPanel[i].transform.GetChild(0).gameObject.SetActive(true);
                buildingInfoPanel[i].transform.GetChild(1).gameObject.SetActive(false);
            }
            

            


        }

    }

    public void ChangeConstructionUI()
    {
        if(linkedPlanet.IsPlayerOwned)
        {
            if (isConstructionUIOpen)
            {
                CloseConstructionUI();
            }
            else
            {
                OpenConstructionUI();
            }
        }
    }

    public void OpenConstructionUI()
    {
        if(linkedPlanet.IsPlayerOwned)
        {
            isConstructionUIOpen = true;
            constructionPanel.SetActive(true);
            HexMapCamera.Locked = true;
        }

        //Update Construction UI

    }

    public void UpdateConstructionUI() {
        int i = 0;
        //List<BuildingID> buildings = new List<BuildingID>(new BuildingID[] {BuildingID.SHIPYARD, BuildingID.DEPOT, BuildingID.LAB,BuildingID.SMELTERY, BuildingID.TRADEHUB});
        foreach (BuildingID id in buildingOrder)
        {
            if (linkedPlanet.CanBuild(id) != 0)
            {
                constructBuildings[i].transform.GetChild(3).gameObject.SetActive(false);
            }
            else
            {
                constructBuildings[i].transform.GetChild(3).gameObject.SetActive(true);
            }

            i++;
        }

    }

    public void CloseConstructionUI()
    {
        if (linkedPlanet.IsPlayerOwned)
        {
            isConstructionUIOpen = false;
            constructionPanel.SetActive(false);
            HexMapCamera.Locked = false;
        }
    }


    public void BuildLab()
    {
        linkedPlanet.Build(BuildingID.LAB);
        CloseConstructionUI() ;
    }

    public void BuildShipyard()
    {
        linkedPlanet.Build(BuildingID.SHIPYARD);
        CloseConstructionUI();
    }

    public void BuildRefinery()
    {
        linkedPlanet.Build(BuildingID.SMELTERY);
        CloseConstructionUI();
    }

    public void BuildTradeHub()
    {
        linkedPlanet.Build(BuildingID.TRADEHUB);
        CloseConstructionUI();
    }

    public void BuildingDepot()
    {
        linkedPlanet.Build(BuildingID.DEPOT);
        CloseConstructionUI();
    }

    public void UpdateBuildingDisplay(Building building, GameObject obj)
    {
        obj.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = building.ID.ToString();

        obj.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = Building.GetDescription(building.ID);

    }
    // Start is called before the first frame update
    void Start()
    {
        CloseUI();
    }

    // Update is called once per frame
    void Update()
    {
        if(linkedPlanet != null)
        {
            UpdateUI();
            if(isConstructionUIOpen)
            {
                UpdateConstructionUI();
            }
        }
    }
}
