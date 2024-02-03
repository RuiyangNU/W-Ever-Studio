using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetInfoUI : PopupUI
{
    private IClickableUI linkedObject = null;
    private PlanetInfoDescription descriptionText;

    public bool isUIOpen = false;

    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();

        descriptionText = GetComponentInChildren<PlanetInfoDescription>();
    }

    public void SetInfoText(string newText)
    {
        descriptionText.SetText(newText);
    }

    /// <summary>
    /// Links the IClickableUI object to this UI. Will call the OnUIClose() method of the object when this UI is closed.
    /// </summary>
    public void Link(IClickableUI obj)
    {
        linkedObject = obj;
    }

    override public void OpenUI()
    {
        if (linkedObject == null)
        {
            Debug.LogError("OpenUI was called, but no object was linked to " + this.name + ". Make sure to call Link from the object opening this UI first.");
        }

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
