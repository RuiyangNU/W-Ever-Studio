using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlanetInfoShipyardButton : PopupUIElement
{
    private Image _image;
    private Button _button;

    private TextMeshProUGUI buttonText;

    // Start is called before the first frame update
    void Start()
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
        _image.enabled = true;
        _button.enabled = true;
        buttonText.enabled = true;
    }
}
