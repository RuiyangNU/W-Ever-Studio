using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TechnologyUI : PopupUI
{
    public bool isUIOpen = false;
    public List<GameObject> techDisplay;
    public List<TextMeshProUGUI> techTextList;
    public Tech[] techOrder;
    public GameObject techPanel;

    public override void CloseUI()
    {
        techPanel.SetActive(false);
        isUIOpen = false;
    }

    public override void OpenUI()
    {
        techPanel.SetActive(true);
        isUIOpen=true;
    }

    public override void UpdateUI()
    {
        //throw new System.NotImplementedException();
        techTextList[0].text = techOrder[0].ToString() + " Current Level: " + PlayerManager.playerManager.PlayerShipTier + "./n Cost to Upgrade: " + PlayerManager.playerManager.GetResearchCost(Tech.SHIP_TIER);

        techTextList[1].text = techOrder[1].ToString() + " Current Level: " + PlayerManager.playerManager.PlayerDamageTier + "./n Cost to Upgrade: " + PlayerManager.playerManager.GetResearchCost(Tech.DAMAGE);

        techTextList[2].text = techOrder[2].ToString() + " Current Level: " + PlayerManager.playerManager.PlayerResistanceTier + "./n Cost to Upgrade: " + PlayerManager.playerManager.GetResearchCost(Tech.RESISTANCE);


    }

    public void ResearchShipType()
    {
        PlayerManager.playerManager.ResearchTech(Tech.SHIP_TIER);
    }

    public void ResearchDamage()
    {
        PlayerManager.playerManager.ResearchTech(Tech.DAMAGE);
    }

    public void ResearchResistence()
    {
        PlayerManager.playerManager.ResearchTech(Tech.RESISTANCE);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (isUIOpen)
        {
            UpdateUI();
        }
    }
}
