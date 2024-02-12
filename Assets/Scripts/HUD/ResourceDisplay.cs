using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceDisplay : MonoBehaviour
{
    private PlayerManager playerManager;
    private TextMeshProUGUI resourceText;
    
    void Awake() {
        playerManager = FindObjectOfType<PlayerManager>();
        resourceText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        float meth = playerManager.playerResourcePool[PlayerManager.PlayerResource.METHANE];
        float steel = playerManager.playerResourcePool[PlayerManager.PlayerResource.STEEL];
        resourceText.text = "Methane:" + meth.ToString() + "\n Steel:"+ steel.ToString();
    }
}
