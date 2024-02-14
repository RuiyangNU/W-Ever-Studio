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
        float credit = playerManager.PlayerCredit;
        float research = playerManager.PlayerResearch;
        resourceText.text = "Credits:" + Mathf.Floor(credit).ToString() + "\n Research:"+ Mathf.Floor(research).ToString();
    }
}
