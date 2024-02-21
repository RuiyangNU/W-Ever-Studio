using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class PopupUI : MonoBehaviour
{
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
