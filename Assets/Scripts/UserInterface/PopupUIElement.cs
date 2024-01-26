using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PopupUIElement : MonoBehaviour
{
    public abstract void OnUIClose();
    public abstract void OnUIOpen();

}
