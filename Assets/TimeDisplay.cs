using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeDisplay : MonoBehaviour
{
    public int stars = 0; // The variable to display
  public TMP_Text starText; // The TextMeshPro object to display

  // Update is called once per frame
  void Update()
  {
    starText.SetText("Stars: " + stars);
  }
}
