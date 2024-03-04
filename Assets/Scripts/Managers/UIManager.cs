using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{


    public PlanetInfoUI PlanetInfoUI;
    public GameObject PlanetBuildingUI;
    public FleetInfoUI FleetInfoUI;

    public static UIManager uiManager;

    //public GameObject PlanetConstructionUI;

    public void Awake()
    {
        uiManager = GetComponent<UIManager>();
    }

    public void CloseUI(GameObject UI)
    {
        //UI.CloseUI();
        //UI.SetActive(false);

    }

    public void OpenPlanetUI()
    {
        
        //UI.OpenUI();
        PlanetInfoUI.OpenUI();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
