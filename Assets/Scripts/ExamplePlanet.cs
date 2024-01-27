using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class ExamplePlanet : MonoBehaviour, IClickableUI
{
    [SerializeField]
    private PlanetInfoUI planetInfoUI;
    private string description;

    private bool isUIOpen;

    void Start()
    {
        planetInfoUI = FindObjectOfType<PlanetInfoUI>(); // Should be a singleton (only one planet info UI in the game)!
        description = "This is an example planet. It is quite uninteresting.";
    }

    void Update()
    {

    }

    private void OnMouseDown()
    {
        if (!isUIOpen)
        {
            OpenUI();
        }
    }

    public PopupUI targetUI
    {
        get { return planetInfoUI; }
    }

    public bool IsUIOpen
    {
        get { return isUIOpen; }
    }

    public void OpenUI()
    {
        planetInfoUI.Link(this); // Always call Link first so the UI can call OnUIClose() on this object when it closes!

        planetInfoUI.SetInfoText(description);
        planetInfoUI.OpenUI();
        isUIOpen = true;
    }

    public void OnUIClose()
    {
        isUIOpen = false;
    }
}
