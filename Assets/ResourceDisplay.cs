using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    public int fuck = 10;
    [SerializeField] TextMeshProUGUI ResourceValue;
    [SerializeField] PlayerManager PM;
    

    // Update is called once per frame
    void Update()
    {
        
        //Debug.Log(PM.dictionary_resources[PlayerManager.PlayerResource.METHANE]);
        float meth = PM.dictionary_resources[PlayerManager.PlayerResource.METHANE];
        float steel = PM.dictionary_resources[PlayerManager.PlayerResource.STEEL];
        ResourceValue.text = "Methane:" + meth.ToString() + "\n Steel:"+ steel.ToString();
    }
}
