using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventUI : PopupUI
{

    public bool isUiOpen;
    public GameEvent linkedEvent = null;
    public override void CloseUI()
    {
        isUiOpen = false;
    }

    public override void OpenUI()
    {
        isUiOpen = true;
    }

    public override void UpdateUI()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
