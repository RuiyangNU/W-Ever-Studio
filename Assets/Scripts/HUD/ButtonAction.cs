using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;
using UnityEngine.UI;
using TMPro;
  
public class ButtonAction : MonoBehaviour  
{  
    public GameManager gm;
    public PlayerManager pm;
    //[SerializeField] GameManager GM;
    
    public TextMeshProUGUI myText;

    public void OnButtonPress(){
      
      //Debug.Log(GM.turnNumber);
        gm.UpdateTick();
        //gm.turnNumber++;
        //Debug.Log(GM.turnNumber);
        
        myText.text = "Turn " + gm.turnNumber;  
        Debug.Log("Button clicked " + gm.turnNumber + " times.");
           
    }  
}  