using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventLoader : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameEvent> gameEvents = new List<GameEvent>();

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        
    }

    public void ExperimentalEvent()
    {
        GameEvent gameEvent = new GameEvent();
        gameEvent.title = "Test My Event";
        gameEvent.eventId = "test.1";
        gameEvent.description = "Description";

        GameEventOption option = new GameEventOption();
        option.effectList = new List<string>();
        option.effectList.Add("AddCredits=1000");
        option.description = "Option 1";

        gameEvent.optionList.Add(option);

        gameEvents.Add(gameEvent);
    }
}
