using UnityEngine.UI;

public class PlanetInfoCloseButton : PopupUIElement
{
    private Image _image;
    private Button _button;

    // Start is called before the first frame update
    void Start()
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
