using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class PopupUI : MonoBehaviour
{
    public List<PopupUIElement> children;
    public Image _image;

    protected virtual void Start()
    {
        // Find children
        children = new List<PopupUIElement>();
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            PopupUIElement childScript = child.GetComponent<PopupUIElement>();

            if (childScript != null)
            {
                children.Add(childScript);
            }
        }

        _image = GetComponent<Image>();
    }

    /// <summary>
    /// Opens the Popup UI.
    /// </summary>
    public abstract void OpenUI();

    /// <summary>
    /// Closes the Popup UI.
    /// </summary>
    public abstract void CloseUI();

    /// <summary>
    /// Updates the PopupUI.
    /// </summary>
    public abstract void UpdateUI();
}
