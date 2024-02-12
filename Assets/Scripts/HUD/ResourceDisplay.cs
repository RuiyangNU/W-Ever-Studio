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
        float meth = playerManager.PlayerMethane;
        float steel = playerManager.PlayerSteel;
        resourceText.text = "Methane:" + meth.ToString() + "\n Steel:"+ steel.ToString();
    }
}
