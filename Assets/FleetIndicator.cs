using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleetIndicator : MonoBehaviour
{
    public GameObject fleet;

    private Renderer rend;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fleet.GetComponent<Fleet>().owner == Owner.ENEMY)
        {
            rend.material.SetColor("_BaseColor", Color.red);
        }
        else
        {
            rend.material.SetColor("_BaseColor", Color.green);
        }
    }
}
