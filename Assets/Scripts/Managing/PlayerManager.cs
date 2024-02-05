using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Start is called before the first frame update
    public enum PlayerResource
    {
        METHANE,
        STEEL,

    }

    Dictionary<PlayerResource, float> dictionary_resources = 
        new Dictionary<PlayerResource, float>(){
                    {PlayerResource.METHANE, 0},
                    {PlayerResource.STEEL, 0}};
        

    public void UpdateTickResources(Dictionary<PlayerResource, float> resources) {
        // add resources from this function to the resource pool
        dictionary_resources[PlayerResource.METHANE] += resources[PlayerResource.METHANE];
        dictionary_resources[PlayerResource.STEEL] += resources[PlayerResource.STEEL];
    }

    //Player's planets/claims
    public List<HexCoordinates> ListOfCoordinates = new List<HexCoordinates>();

    





    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
