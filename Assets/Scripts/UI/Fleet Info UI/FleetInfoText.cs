using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FleetInfoText : PopupUIElement
{
    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    public void SetText(string text)
    {
        _text.text = text;
    }

    public override void OnUIClose()
    {
        _text.enabled = false;
    }

    public override void OnUIOpen()
    {
        _text.enabled = true;
    }
}
