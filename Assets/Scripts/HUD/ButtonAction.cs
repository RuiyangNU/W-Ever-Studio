using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;
using UnityEngine.UI;
using TMPro;
  
public class ButtonAction : MonoBehaviour  
{  
    int n;  
    public TextMeshProUGUI myText;

      public void OnButtonPress(){  
         n++;
         myText.text = "Day " + n;  
         Debug.Log("Button clicked " + n + " times.");  
    }  
}  