using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupUI : MonoBehaviour
{
    private List<PopupUIElement> children;
    private Image _image;

    // Start is called before the first frame update
    void Start()
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

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Opens the Popup UI. Calls OnUIOpen() on all its children.
    /// </summary>
    public void OpenUI()
    {
        _image.enabled = true;

        foreach (PopupUIElement child in children)
        {
            child.OnUIOpen();
        }
    }

    /// <summary>
    /// Closes the Popup UI. Calls OnUIClose() on all its children.
    /// </summary>
    public void CloseUI()
    {
        _image.enabled = false;

        foreach (PopupUIElement child in children)
        {
            child.OnUIClose();
        }
    }
}
