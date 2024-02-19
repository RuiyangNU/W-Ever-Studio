using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceDisplay : MonoBehaviour
{
    private PlayerManager playerManager;
    private TextMeshProUGUI resourceText;
    
    
    [SerializeField]
    private List<TextMeshProUGUI> currencyText;



    [SerializeField]
    private List<TextMeshProUGUI> commodityText;

    void Awake() {
        playerManager = FindObjectOfType<PlayerManager>();
        resourceText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        float credit = playerManager.PlayerCredit;
        float research = playerManager.PlayerResearch;
        //foreach (TextMeshProUGUI text in currencyText)
        //{
        //    text.text = Mathf.Floor(credit).ToString();
        //}
        currencyText[0].text = Mathf.Floor(credit).ToString();
        currencyText[1].text = Mathf.Floor(research).ToString();
    }
}
