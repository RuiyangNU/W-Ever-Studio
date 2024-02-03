using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.VisualScripting.Metadata;

public class ExamplePopupUI : PopupUI
{
    override public void OpenUI()
    {
        _image.enabled = true;

        foreach (PopupUIElement child in children)
        {
            child.OnUIOpen();
        }
    }

    override public void CloseUI()
    {
        _image.enabled = false;
        Debug.Log("Check Close Popup");

        foreach (PopupUIElement child in children)
        {
            child.OnUIClose();
        }
    }
}
