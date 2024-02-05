using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;
using UnityEngine.UI;
using TMPro;
  
public class ButtonAction : MonoBehaviour  
{  
    public GameManager GM;
    //[SerializeField] GameManager GM;
     
    public TextMeshProUGUI myText;

      public void OnButtonPress(){
        //Debug.Log(GM.turnNumber);
        
         GM.turnNumber++;
         //Debug.Log(GM.turnNumber);
         
         myText.text = "Day " + GM.turnNumber;  
         Debug.Log("Button clicked " + GM.turnNumber + " times.");
           
    }  
}  