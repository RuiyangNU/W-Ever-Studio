using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupUIText : MonoBehaviour, PopupUIElement
{
    private TextMeshProUGUI _text;

    // Start is called before the first frame update
    void Start()
    {
        _text = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnUIClose()
    {
        _text.enabled = false;
    }

    public void OnUIOpen()
    {
        _text.enabled = true;
    }
}
