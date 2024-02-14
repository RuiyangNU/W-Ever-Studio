using UnityEngine.UI;

public class FleetInfoCloseButton : PopupUIElement
{
    private Image _image;
    private Button _button;

    void Awake()
    {
        _image = GetComponent<Image>();
        _button = GetComponent<Button>();
    }

    override public void OnUIClose()
    {
        _image.enabled = false;
        _button.enabled = false;
    }
    override public void OnUIOpen()
    {
        _image.enabled = true;
        _button.enabled = true;
    }
}
