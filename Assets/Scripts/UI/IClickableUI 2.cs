using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Serialization;
using UnityEngine;

public interface IClickableUI
{
    PopupUI targetUI { get; }
    bool IsUIOpen { get; }
    void OpenUI();
    void OnUIClose();
}
