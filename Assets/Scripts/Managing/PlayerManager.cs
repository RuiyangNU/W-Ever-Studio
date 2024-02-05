using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{


//Resource Management
    
    public enum PlayerResource
    {
        METHANE,
        STEEL,

    }

    public Dictionary<PlayerResource, float> dictionary_resources = 
        new Dictionary<PlayerResource, float>(){
                    {PlayerResource.METHANE, 0},
                    {PlayerResource.STEEL, 0}};
        


    public void UpdateTickResourcesSinglePlanet(Dictionary<PlayerResource, float> resources) {
        // add resources from this function to the resource pool
        dictionary_resources[PlayerResource.METHANE] += resources[PlayerResource.METHANE];
        dictionary_resources[PlayerResource.STEEL] += resources[PlayerResource.STEEL];
    }

        //Use when adding or removing resources outside of ticks, ex. purchasing a ship
    public void ChangeResource(PlayerResource resourceType, float Value){
        dictionary_resources[resourceType] += Value;

    }


//Player's planets/claims
    public List<HexCoordinates> ListOfCordinates = new List<HexCoordinates>();

    
    public void AddPlanet(HexCoordinates planetCord){
        ListOfCordinates.Add(planetCord);
    }

   //update resources based on claimed planets
   public void UpdateTickResourcesAllPlanets(){
        foreach(var Cord in ListOfCordinates){
            //Access planet and then its resources as a dictionary
            //Pass dictionary into UpdateTickResourcesSinglePlanet()

            return;
        }

   }



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
