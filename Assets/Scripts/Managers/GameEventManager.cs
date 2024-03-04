using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    public static Queue<GameEvent> gameEvents = new Queue<GameEvent>();
    public static List<GameEvent> potentialEvents = new List<GameEvent>();
    public GameEventManager gameEventManager = null;
    public EventUI eventUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (eventUI != null && !eventUI.isUiOpen)
        {
            while (gameEvents.Count > 0)
            {
                if (eventUI.isUiOpen)
                {
                    //break;
                }

                GameEvent gameEvent = gameEvents.Dequeue();


                //Link Event UI
                eventUI.OpenUI(gameEvent);
                break;

                //Process Event


            }
        }
        
        ScheduleAllEvent();
    }

    private void Awake()
    {
        gameEventManager = this;

        //Register All the Game Event:

    }

    public static GameEvent GetEventByID(string id)
    {
        foreach (GameEvent gameEvent in potentialEvents)
        {
            if(gameEvent.eventId == id)
            {
                return gameEvent;
            }
        }
        //throw new NullReferenceException("No ");
        return null;
    }

    public static void RegisterEvent(GameEvent gameEvent)
    {
        if (!potentialEvents.Contains(gameEvent) )
        {
            potentialEvents.Add(gameEvent);
        }
        //throw new NullReferenceException("No ");
        return;
    }

    public static void EnqueueEventByID(string id)
    {
        GameEvent gameEvent = GetEventByID(id);

        if (gameEvent != null)
        {
            gameEvents.Enqueue(gameEvent);
            potentialEvents.Remove(gameEvent);
        }
        else
        {
            Debug.LogError("Fail to Add Event: " + id);
        }
    }

    public void ScheduleAllEvent()
    {

        for (int i = potentialEvents.Count - 1; i >= 0; i--)
        {
            GameEvent gameEvent = potentialEvents[i];
            if (gameEvent.ProcessTriggers() && !gameEvent.isTriggeredOnly)
            {
                gameEvents.Enqueue(gameEvent);
                potentialEvents.RemoveAt(i);
            }
        }
        int it = 0;
    }
}
