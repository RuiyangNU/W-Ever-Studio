using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Serialization;
using UnityEngine;

public interface IClickableUI
{
    PopupUI targetUI { get; set; }
    bool IsUIOpen { get; }
    void OnClick();
    void OpenUI();
    void OnUIClose();
}
