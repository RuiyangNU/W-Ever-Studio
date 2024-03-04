using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventUI : PopupUI
{

    public bool isUiOpen;
    public GameEvent linkedEvent = null;
    public GameObject eventPannel;
    public GameObject optionPrefab;
    public GameObject optionScrollViewContent;
    public GameObject eventDescText;
    public override void CloseUI()
    {
        linkedEvent = null;
        //foreach
        while (optionScrollViewContent.transform.childCount > 0)
        {
            DestroyImmediate(optionScrollViewContent.transform.GetChild(0).gameObject);
        }
        GameObject image = eventPannel.transform.Find("Event Image").gameObject;
        //image.SetActive(true);
        image.GetComponent<Image>().sprite = null;
        eventPannel.SetActive(false);
        isUiOpen = false;
    }

    //Should not Use this one without linking the gameEvent
    public override void OpenUI()
    {

        eventPannel.SetActive(true);
        isUiOpen = true;
    }

    public void OpenUI(GameEvent gameEvent)
    {

        eventPannel.SetActive(true);
        linkGameEvent(gameEvent);
        isUiOpen = true;
    }

    public void linkGameEvent(GameEvent gameEvent)
    {
        linkedEvent = gameEvent;
        UpdateUI();
    }

    public override void UpdateUI()
    {
        if (linkedEvent == null)
        {
            CloseUI();
            return;
        }
        //Update Title and Description
        GameObject title = eventPannel.transform.Find("Event Title").gameObject;
        title.GetComponent<TextMeshProUGUI>().text = linkedEvent.title;

        //GameObject desc = eventPannel.transform.Find("Event Text").gameObject;
        eventDescText.GetComponent<TextMeshProUGUI>().text = linkedEvent.description;

        //Update Event Picture if there is any
        if (linkedEvent.imagePath != null)
        {
            Sprite loadedSprite = Resources.Load<Sprite>(linkedEvent.imagePath);
            Debug.Log(linkedEvent.imagePath);
            if (loadedSprite != null)
            {
                GameObject image = eventPannel.transform.Find("Event Image").gameObject;
                //image.SetActive(true);
                image.GetComponent<Image>().sprite = loadedSprite;
            }
            else
            {
                GameObject image = eventPannel.transform.Find("Event Image").gameObject;
                //image.SetActive(false);
                image.GetComponent<Image>().sprite = null;
            }
        }
        else
        {
            GameObject image = eventPannel.transform.Find("Event Image").gameObject;
            image.GetComponent<Image>().sprite = null;
        }

        //Generate Option
        foreach(GameEventOption option in linkedEvent.optionList)
        {
            GenerateOption(option);
        }
    }

    public void GenerateOption(GameEventOption option)
    {
        if ( option == null)
        {
            return;
        }

        GameObject optionButton = Instantiate(optionPrefab, optionScrollViewContent.transform);

        optionButton.transform.Find("Content").gameObject.GetComponent<TextMeshProUGUI>().text = option.description;
        optionButton.GetComponent<Button>().onClick.AddListener(() => linkedEvent.ProcessEffect(option));
        optionButton.GetComponent<Button>().onClick.AddListener(() => CloseUI());
        //optionButto
    }

    // Start is called before the first frame update
    void Start()
    {
        CloseUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
