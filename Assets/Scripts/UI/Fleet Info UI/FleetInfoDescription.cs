using TMPro;

public class FleetDescription : PopupUIElement
{
    private TextMeshProUGUI _text;

    void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    public void SetText(string newText)
    {
        _text.SetText(newText);
    }

    override public void OnUIClose()
    {
        _text.enabled = false;
    }

    override public void OnUIOpen()
    {
        _text.enabled = true;
    }
}
