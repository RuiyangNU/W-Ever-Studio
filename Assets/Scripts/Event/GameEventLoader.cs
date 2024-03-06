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
        TutorialEvent3();
    }

    public void ExperimentalEvent()
    {
        GameEvent gameEvent = new GameEvent();
        gameEvent.title = "The Long Push Home";
        gameEvent.eventId = "opening.1";
        gameEvent.description = "<color=#E1D787>Legatus Aurelian:</color> Status Report, Centurian.\n\n" +
"<color=#7EC8E3>You:</color> Yes Commander, we have secured the colony of frontier world <color=orange>Chandra5-b</color>. It is a colonial world right before the disaster. The local authority and population have enough sustainability to survive the environment. We are welcomed by Governor Zhang and the local council, so we can continue our operation on the planet.\n\n" +
"<color=#E1D787>Legatus Aurelian:</color> Well done, Centurian. What is the situation of the <color=purple>Stargate</color> in the system? \n\n" +
"<color=#7EC8E3>You:</color> Just as we expected, the <color=purple>Stargate</color> in the <color=orange>Chandra5</color> system is offline. Fortunately, the internal structure remains intact, so we can reactivate it at any time. However, there is no communication from <color=orange>Kepler</color> or any department of <color=#adcae6>UGR</color> so far. The worst must have happened. I have a task force sent to examine what caused the <color=purple>Stargate</color> to close right now.\n\n" +
"<color=#E1D787>Legatus Aurelian:</color> Continue the infrastructure repair and exploration. Me and the main battle force of Legio XII will be waiting in Offshore to wait for further information. Right now, <color=yellow>Build a Depot</color> on Chandra-5b to allow further expansion of your operation.\n\n";


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
        gameEvent.description = "Commander, welcome to your first game in Remnant of Stars, a turn-based 4x strategy game in space. This is a short tutorial on your control and resources.\n\n" +
    "On the bottom right of the screen is the <color=green>Next Turn</color> button and <color=green>Turn Number Indicator</color>. Click on the button to progress the turn (<color=yellow>Don't do it right now.</color>)\n\n" +
    "You can use <color=green>WASD</color> to navigate through the map. Use the <color=green>Mouse's scroll</color> to zoom in and out. Use <color=green>Left Click</color> to select things on the map.\n\n" +
    "On the top left of the screen is your resource tab, faction flag, and technology menu. The top 2 resources are your currency, <color=green>Credit</color> and <color=green>Research</color>, which you can accumulate and consume globally. The little (+x) tells you your income per turn.\n\n" +
    "The bottom two resources are your Commodities, <color=green>Construction Material (CM)</color> and <color=green>Alloy</color>. Unlike currency, Commodities don't get used up in transactions; instead, you accumulate enough production points from building or events to unlock milestones, which in turn unlock new buildings and ships.\n\n" +
    "For example, you start with 0 CM and need CM Milestone 1 to build all other buildings other than the depot. Building a building which has a CM requirement will not consume your CM.\n\n" +
    "Below the resource tab is the tech menu entry button, which we will discuss in a later tutorial.\n\n" +
    "Commander, here is your first mission to progress the tutorial: <color=yellow>click on the map to select the planet you own.</color>";

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
        gameEvent.description = "Welcome to the Planet Info UI tutorial! This interface is your command center for managing the various aspects of your planet within the game. Let's break down each part of the UI and its functions:\r\n\r\nTop Menu Buttons:\r\n- Sum: Provides a summary of your planet's overall status and statistics (Not Implemented).\r\n- <color=green>Build</color>: Access this to construct new buildings or infrastructure.\r\n- <color=green>Shipyard</color>: Access the shipyard to build and manage your fleet (currently unavailable before building a shipyard).\r\n- <color=green>Close</color>: Closes the Planet Info UI.\r\n\r\nPlanet Status Indicators:\r\n- Building Slot 0/3: You have 0 out of 3 building slots filled.\r\n- Shipyard Status: The status of your shipyard, currently You do not have a shipyard.\r\n- Attack Indicator: Shows if a planet is under siege or not.\r\n\r\nEconomic Indicators:\r\n- Left One: <color=green>Credit</color> income per turn.\r\n- Right One: Research Income of the Planet.\r\n\r\nPlanet Display:\r\n- The image of the planet may change as you build or if under attack.\r\n\r\n To progress the tutorial, started by clicking on 'Build' to build a <color=green>Depot</color>\r\n";
        gameEvent.triggers.Add("hasFlag == tutorial_1_finished");
        gameEvent.triggers.Add("hasFlag == tutorial_enabled");
        gameEvent.isTriggeredOnly = false;
        gameEvent.imagePath = "Sprites/EventPicture/tutorial2";

        GameEventOption option = new GameEventOption();
        option.effectList = new List<string>();
        //option.effectList.Add("addCredits=2000");
        option.effectList.Add("setFlag=tutorial_2_enabled");
        option.description = "Continue to Part 2";

        //gameEvent.optionList.Add(option);
        gameEvent.optionList.Add(option);
        GameEventManager.RegisterEvent(gameEvent);
    }

    public void TutorialEvent3()
    {
        GameEvent gameEvent = new GameEvent();
        gameEvent.title = "Tutorial: Construction - Part 1";
        gameEvent.eventId = "tutorial.3";
        gameEvent.description = "Welcome to the Construction Tutorial! Long story short, the new panel which just shows up is the planet building display. Each slot will show a building's sprite and descprition if it has a building, or it will show the button to create a new building. <color=yellow>Click the Construction Button</color> after reading the tutorial.\n\n" +
            "A new window will popup, which will show you 5 buildings you will be able to construct in this game. Each building has it resource requirment to build, and the build button on that building will disappear if you don't meet the requirment. <color=yellow>Build a Depot</color> first, since it will provide you with 1 CM, which will unlock all other buildings.\n\n";
        gameEvent.isTriggeredOnly = false;
        gameEvent.triggers.Add("hasFlag == tutorial_2_finished");
        gameEvent.imagePath = "Sprites/EventPicture/tutorial1";


        GameEventOption option = new GameEventOption();
        option.effectList = new List<string>();
        //option.effectList.Add("addCredits=2000");
        option.description = "Continue to Part 3";

        //gameEvent.optionList.Add(option);
        gameEvent.optionList.Add(option);
        GameEventManager.RegisterEvent(gameEvent);
    }

    public void TutorialEvent4()
    {
        GameEvent gameEvent = new GameEvent();
        gameEvent.title = "Tutorial: Construction - Part 2";
        gameEvent.eventId = "tutorial.4";
        gameEvent.description = "Welcome to the Construction Tutorial! Long story short, the new panel which just shows up is the planet building display. Each slot will show a building's sprite and descprition if it has a building, or it will show the button to create a new building. <color=yellow>Click the Construction Button</color> after reading the tutorial.\n\n" +
            "A new window will popup, which will show you 5 buildings you will be able to construct in this game. Each building has it resource requirment to build, and the build button on that building will disappear if you don't meet the requirment. <color=yellow>Build a Depot</color> first, since it will provide you with 1 CM, which will unlock all other buildings.\n\n";
        gameEvent.isTriggeredOnly = false;
        gameEvent.triggers.Add("hasFlag == tutorial_2.5_finished");
        gameEvent.imagePath = "Sprites/EventPicture/tutorial1";


        GameEventOption option = new GameEventOption();
        option.effectList = new List<string>();
        //option.effectList.Add("addCredits=2000");
        option.description = "Continue to Part 3";

        //gameEvent.optionList.Add(option);
        gameEvent.optionList.Add(option);
        GameEventManager.RegisterEvent(gameEvent);
    }

    public void TutorialEvent5()
    {
        GameEvent gameEvent = new GameEvent();
        gameEvent.title = "Tutorial: Fleet Control";
        gameEvent.eventId = "tutorial.5";
        gameEvent.description = "";
        gameEvent.isTriggeredOnly = false;
        gameEvent.triggers.Add("hasFlag == tutorial_3_finished");
        gameEvent.imagePath = "Sprites/EventPicture/tutorial1";

        GameEventOption option = new GameEventOption();
        option.effectList = new List<string>();
        //option.effectList.Add("addCredits=2000");
        option.description = "Onward to the Sector (Tutorial Finished)";

        //gameEvent.optionList.Add(option);
        gameEvent.optionList.Add(option);
        GameEventManager.RegisterEvent(gameEvent);
    }

    public void EnemyEvent1()
    {
        GameEvent gameEvent = new GameEvent();
        gameEvent.title = "Enemy Unknown: Warlords of ";
        gameEvent.eventId = "tutorial.4";
        gameEvent.description = "";
        gameEvent.isTriggeredOnly = false;
        gameEvent.triggers.Add("hasFlag == tutorial_3_finished");
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
