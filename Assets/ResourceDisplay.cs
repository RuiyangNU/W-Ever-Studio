using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    private TextMeshProUGUI resourceText;
    private PlayerManager PM;
    
    void Awake() {
        resourceText = GetComponent<TextMeshProUGUI>();
        PM = FindObjectOfType<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(PM.dictionary_resources[PlayerManager.PlayerResource.METHANE]);
        float meth = PM.playerResourcePool[PlayerManager.PlayerResource.METHANE];
        float steel = PM.playerResourcePool[PlayerManager.PlayerResource.STEEL];
        resourceText.text = "Methane:" + meth.ToString() + "\n Steel:"+ steel.ToString();
    }
}
