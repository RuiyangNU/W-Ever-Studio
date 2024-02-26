using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NotificationUI : MonoBehaviour
{

    public ScrollRect scrollView;
    public GameObject contentPanel;

    public GameObject notificationPrefab;
    public List<GameObject> notificationList = new List<GameObject>();
    public List<GameObject> prevNotificationList = new List<GameObject>();
    public static NotificationUI notificationUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        NotificationUI.notificationUI = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateTick()
    {
        ClearItem();
    }

    public void AddNewItem(Event notificationEvent)
    {
        GameObject newItem = Instantiate(notificationPrefab, contentPanel.transform);
        // Optional: Initialize newItem with data
        TextMeshProUGUI type =  newItem.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI content = newItem.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        notificationList.Add(newItem);
        //To Change
        type.text = notificationEvent.eventType;
        content.text = notificationEvent.ToString();


        // Scroll to bottom
        Canvas.ForceUpdateCanvases(); // Refresh layout immediately
        scrollView.verticalNormalizedPosition = 0f; // Scrolls to bottom
    }

    public void ClearItem()
    {
        if(prevNotificationList != null)
        {
            foreach(GameObject item in prevNotificationList)
            {
                Destroy(item);
            }

        }
        prevNotificationList = notificationList;
        notificationList = new List<GameObject>();



        Canvas.ForceUpdateCanvases(); 
        scrollView.verticalNormalizedPosition = 0f; 
    }
}
