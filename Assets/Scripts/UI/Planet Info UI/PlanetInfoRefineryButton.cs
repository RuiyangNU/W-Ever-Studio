using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlanetInfoRefineryButton : PopupUIElement
{
    public bool showOnOpen = true;

    private Image _image;
    private Button _button;

    private TextMeshProUGUI buttonText;

    void Awake()
    {
        _image = GetComponent<Image>();
        _button = GetComponent<Button>();
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
    }

    override public void OnUIClose()
    {
        _image.enabled = false;
        _button.enabled = false;
        buttonText.enabled = false;
    }
    override public void OnUIOpen()
    {
        if (showOnOpen)
        {
            _image.enabled = true;
            _button.enabled = true;
            buttonText.enabled = true;
        }
    }
}
