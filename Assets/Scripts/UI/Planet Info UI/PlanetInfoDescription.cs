using TMPro;

public class PlanetDescription : PopupUIElement
{
    private TextMeshProUGUI _text;

    // Start is called before the first frame update
    void Start()
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
