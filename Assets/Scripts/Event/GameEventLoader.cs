using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventLoader : MonoBehaviour
{
    // Start is called before the first frame update
    //public List<GameEvent> gameEvents = new List<GameEvent>();

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        //Register Game Event
        ExperimentalEvent();
        TutorialEvent();
        TutorialEvent2();
    }

    public void ExperimentalEvent()
    {
        GameEvent gameEvent = new GameEvent();
        gameEvent.title = "The Long Push Home";
        gameEvent.eventId = "opening.1";
        gameEvent.description = "Legatus Aurelian: Status Report, Centurian. \n" +
            "You: Yes Commander, we have secured the colony of frontier world Chandra5-b. It is a colonial world right before the disater, the local authority and population has enough sustainability to survive the environment. We are weclomed by Governor Zhang and the local concuil so we can continue our operation on the planet. \n" +
            "Legatus Aurelian: Well done centurian. What is the situation of Stargate in the system? \n" +
            "You: Just as we expected, the stargate in the Chandra5 system is also offline. Fortunately, the internal structure remain intact so we can reactivate it at anytime. However, there is no communication from Kepler or any department of UGR so far. The worst must have happened. I have a task force sent to examine what caused the stargate to close right now. \n" +
            "Legatus Aurelian: Continue the infrasture repair and exploration, me and the main battle force of Legio XII will be waiting in Offshore to wait for further information. Right now, build a <color=green>depot</color> on Chandra-5b to allow further expansion of your operation. \n";

        gameEvent.imagePath = "Sprites/EventPicture/hegemony07-tempEventPic";
        gameEvent.isTriggeredOnly = true;

        GameEventOption option = new GameEventOption();
        option.effectList = new List<string>();
        option.effectList.Add("event=tutorial.1");
        option.effectList.Add("setFlag=tutorial_enabled");
        option.description = "Copy That (Enable Tutorial)";

        GameEventOption option2 = new GameEventOption();
        option2.effectList = new List<string>();
        option2.effectList.Add("addCredits=2000");
        option2.description = "I will independently carry out the current task for best efficiency (Disable Tutorial)";

        gameEvent.optionList.Add(option);
        gameEvent.optionList.Add(option2);

        GameEventManager.RegisterEvent(gameEvent);

        GameEventManager.EnqueueEventByID(gameEvent.eventId);
    }


    //Register all the tutorial event
    public void TutorialEvent()
    {
        GameEvent gameEvent = new GameEvent();
        gameEvent.title = "Tutorial: Control and Resources - Part1";
        gameEvent.eventId = "tutorial.1";
        gameEvent.description = "Commander, welcome to you first game in Remnant of Stars. This is a short tutorial on the your resource.\n" + 
            "You can use <color=green>WASD</color> to navigate through the map. Use the <color=green>Mouse's scroll</color> to zoom in and zoom out. Use <color=green>Left Click</color> to select things on the map." +
            "On the top left of the screen is your resource tab, faction flag, and technology menu. The top 2 resources are your currency, <color=green>Credit</color> and <color=green>Research</color>, which you can accumulate and consume globally. The little (+x) tells you your income per turn. \n" +
            "The bottom two resources are your Commodity, <color=green>Construction Material (CM)</color> and <color=green>Alloy</color>. You don't get and use them as currency, you accumulate enough production point from building or event to unlock mileston, which will unlock you with new buildings and ships. \n" +
            "For example, you start with 0 CM and need CM Milestone 1 to build all other buildings other than depot. Building a building which has a CM requirement will not consume your CM. \n" +
            "Below the resource tab is the tech menu entry button, we will talk about that in the later tutorial.\n" +
            "Commander, here is your first mission: <color=yellow>click on the map to select the planet you own</color>";

        gameEvent.imagePath = "Sprites/EventPicture/tutorial1";
        gameEvent.isTriggeredOnly = true;
        GameEventOption option = new GameEventOption();
        option.effectList = new List<string>();
        //option.effectList.Add("addCredits=2000");
        option.description = "Continue";

        //gameEvent.optionList.Add(option);
        gameEvent.optionList.Add(option);
        GameEventManager.RegisterEvent(gameEvent);
    }

    public void TutorialEvent2()
    {
        GameEvent gameEvent = new GameEvent();
        gameEvent.title = "Tutorial: Resources - Part2";
        gameEvent.eventId = "tutorial.2";
        gameEvent.description = "Welcome to the Planet Info UI tutorial! This interface is your command center for managing the various aspects of your planet within the game. Let's break down each part of the UI and its functions:\r\n\r\nTop Menu Buttons:\r\n- Sum: Provides a summary of your planet's overall status and statistics.\r\n- Build: Access this to construct new buildings or infrastructure.\r\n- Shipyard: Access the shipyard to build and manage your fleet (currently unavailable).\r\n- Close: Closes the Planet Info UI.\r\n\r\nPlanet Status Indicators:\r\n- Building Slot 0/3: You have 0 out of 3 building slots filled. Time to build!\r\n- No Shipyard: You do not have a shipyard. Consider building one.\r\n- Not Under Attack: Your planet is at peace.\r\n\r\nEconomic Indicators:\r\n- $ 100/Turn: Your income of in-game currency per turn.\r\n- Fish Icon 0/Turn: Indicates production of a resource (currently none).\r\n\r\nPlanet Display:\r\n- The image of the planet may change as you build or if under attack.\r\n\r\nGet started by clicking on 'Build' to use a building slot and consider constructing a shipyard to enable fleet creation and resource production. Keep an eye on the 'Not Under Attack' indicator for changes in planet safety.\r\n\r\nManage your planet effectively and make strategic decisions for its prosperity and defense. Good luck!\r\n";
        gameEvent.triggers.Add("hasFlag == tutorial_1_finished");
        gameEvent.isTriggeredOnly = false;
        gameEvent.imagePath = "Sprites/EventPicture/tutorial1";

        GameEventOption option = new GameEventOption();
        option.effectList = new List<string>();
        //option.effectList.Add("addCredits=2000");
        option.description = "Continue to Part 2";

        //gameEvent.optionList.Add(option);
        gameEvent.optionList.Add(option);
        GameEventManager.RegisterEvent(gameEvent);
    }

    public void TutorialEvent3()
    {
        GameEvent gameEvent = new GameEvent();
        gameEvent.title = "Tutorial: Resources - Part2";
        gameEvent.eventId = "tutorial.2";
        gameEvent.description = "";

        gameEvent.imagePath = "Sprites/EventPicture/tutorial1";

        GameEventOption option = new GameEventOption();
        option.effectList = new List<string>();
        //option.effectList.Add("addCredits=2000");
        option.description = "Continue to Part 2";

        //gameEvent.optionList.Add(option);
        gameEvent.optionList.Add(option);
        GameEventManager.RegisterEvent(gameEvent);
    }

    public void TutorialEvent4()
    {
        GameEvent gameEvent = new GameEvent();
        gameEvent.title = "Tutorial: Resources - Part2";
        gameEvent.eventId = "tutorial.2";
        gameEvent.description = "";

        gameEvent.imagePath = "Sprites/EventPicture/tutorial1";

        GameEventOption option = new GameEventOption();
        option.effectList = new List<string>();
        //option.effectList.Add("addCredits=2000");
        option.description = "Continue to Part 2";

        //gameEvent.optionList.Add(option);
        gameEvent.optionList.Add(option);
        GameEventManager.RegisterEvent(gameEvent);
    }
}
