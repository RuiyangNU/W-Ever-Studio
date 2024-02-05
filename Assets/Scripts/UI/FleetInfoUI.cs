using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleetInfoUI : PopupUI
{
    private Fleet linkedObject = null;
    private FleetInfoDescription descriptionText;

    public bool isUIOpen = false;

    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();

        descriptionText = GetComponentInChildren<FleetInfoDescription>();
        
    }

    public void SetInfoText(string newText)
    {
        descriptionText.SetText(newText);
    }

    public void Link(Fleet obj)
    {
        linkedObject = obj;
    }

    private void UpdateInfo()
    {

    }

    override public void OpenUI()
    {
        if (linkedObject = null)
        {
            Debug.LogError("OpenUI was called, but no object was linked to " + this.name + ". Make sure to call Link from the object opening this UI first.");
        }

        UpdateInfo();

        _image.enabled = true;

        foreach (PopupUIElement child in children)
        {
            child.OnUIOpen();
        }

        isUIOpen = true;
    }

    override public void CloseUI()
    {
        _image.enabled = false;

        foreach (PopupUIElement child in children)
        {
            child.OnUIClose();
        }

        if (linkedObject != null)
        {
            linkedObject.OnUIClose();
            linkedObject = null;
        }

        isUIOpen = false;
    }
}
