using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    public static Queue<GameEvent> gameEvents = new Queue<GameEvent>();
    public static List<GameEvent> potentialEvents = new List<GameEvent>();
    public GameEventManager gameEventManager = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        while (gameEvents.Count > 0)
        {
            GameEvent gameEvent = gameEvents.Dequeue();

            //Link Event UI

            //Process Event
            

        }
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

    public static void RegisterEventByID(string id)
    {
        GameEvent gameEvent = GetEventByID(id);

        if (gameEvent != null)
        {
            potentialEvents.Add(gameEvent);
        }
        else
        {
            Debug.LogError("Fail to Add Event: " + id);
        }
    }
}
