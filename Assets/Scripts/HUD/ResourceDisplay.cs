using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Linq;

public class ResourceDisplay : MonoBehaviour
{
    private PlayerManager playerManager;
    
    [SerializeField]
    private List<TextMeshProUGUI> currencyText;

    [SerializeField]
    private List<TextMeshProUGUI> commodityText;

    void Awake() {
        playerManager = FindObjectOfType<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        int credit = playerManager.PlayerCredit;
        int research = playerManager.PlayerResearch;

        int cmaterial = playerManager.PlayerCMLevel;
        int alloy = playerManager.PlayerAlloyLevel;

        int cmaterialMilestone = playerManager.PlayerCMMilestone;
        int alloyMilestone = playerManager.PlayerAlloyMilestone;

        currencyText[0].text = credit.ToString() + " (+" + playerManager.GetCurrencyPerTick(Currency.CREDIT) + ")";
        currencyText[1].text = research.ToString() + " (+" + playerManager.GetCurrencyPerTick(Currency.RESEARCH) + ")";

        if (cmaterial == 0)
        {
            commodityText[0].text = "Lv0 (M0)";
        }
        else
        {
            commodityText[0].text = "Lv" + cmaterial.ToString() + " (M" + cmaterialMilestone + ")";
        }

        if (alloy == 0)
        {
            commodityText[1].text = "Lv0 (M0)";
        }
        else
        {
            commodityText[1].text = "Lv" + alloy.ToString() + " (M" + alloyMilestone + ")";
        }
    }
}
