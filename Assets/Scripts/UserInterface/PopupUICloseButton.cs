using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupUICloseButton : MonoBehaviour, PopupUIElement
{
    private Image _image;
    private Button _button;

    // Start is called before the first frame update
    void Start()
    {
        _image = GetComponent<Image>();
        _button = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnUIClose()
    {
        _image.enabled = false;
        _button.enabled = false;
    }
    public void OnUIOpen()
    {
        _image.enabled = true;
        _button.enabled = true;
    }

}
