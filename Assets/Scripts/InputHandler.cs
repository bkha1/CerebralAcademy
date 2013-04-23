using UnityEngine;
using System.Collections;

public class InputHandler : MonoBehaviour {

    public string cognitivDisappearKey;

    public string cognitivLiftKey;

	public string cogntivPushKey;

	public string cognitivLeftKey;
    public string cognitivRightKey;

    public string emotivEngineToggleKey = "`";

    public string debugKey;

    public string pauseKey = "escape";
	public string tabKey = "tab";

    /**
     * This is the InputHandler. It is where all input into the game comes from. Due to the way 
     * NotificationCenter works, we cannot send Notifications from a non-Component (EmotivHandler). 
     * So, to work around this, InputHandler will listen for events from EmotivHandler and transform 
     * them into Notifications originating from this component. 
     * 
     * From another components view, this seems logical. There is no need for any other component to 
     * be aware of events from the EmotivHandler.
     */
    void Start()
    {
        CognitvEventManager.LeftEvent += handleLeftEvent;
        CognitvEventManager.RightEvent += handleRightEvent;
        CognitvEventManager.LiftEvent += handleLiftEvent;
        CognitvEventManager.PushEvent += handlePushEvent;
        CognitvEventManager.DisappearEvent += handleDisappearEvent;
        CognitvEventManager.EmotionEvent += handleEmotionEvent;
    }

	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(pauseKey))
        {
            EventFactory.FireOnPauseEvent(this);
        }
		else if(Input.GetKeyDown(tabKey))
		{
			EventFactory.FireOnSkillChoiceEvent(this);
		}
        else if (Input.GetKeyUp(cognitivDisappearKey))
        {
            EventFactory.FireOnCognitvEvent(this, CognitivSkill.DISAPPEAR, 6.4f);
        }
        else if (Input.GetKeyUp(cognitivLiftKey))
        {
            EventFactory.FireOnCognitvEvent(this, CognitivSkill.LIFT, 6.4f);
        }
        else if (Input.GetKeyUp(cognitivLeftKey))
        {
            EventFactory.FireOnCognitvEvent(this, CognitivSkill.LEFT, 6.4f);
        }
        else if (Input.GetKeyUp(cognitivRightKey))
        {
            EventFactory.FireOnCognitvEvent(this, CognitivSkill.RIGHT, 6.4f);
        }
        else if (Input.GetKeyUp(cogntivPushKey))
        {
            EventFactory.FireOnCognitvEvent(this, CognitivSkill.PUSH, 6.4f);
        }
        else if (Input.GetKeyUp(debugKey))
        {
            GameState.Instance.DebugMode = !GameState.Instance.DebugMode;
        }
        else if (Input.GetKeyUp(emotivEngineToggleKey))
        {
            if (EmotivHandler.Instance.isConnected())
            {
				Debug.Log("Disconnecting to EmotivHandler...");
                EmotivHandler.Instance.disconnect();
            }
            else
            {
				Debug.Log("Connecting to EmotivHandler...");
                EmotivHandler.Instance.connect();
            }
        }
	}

    void handleLeftEvent(object sender, float powerLevel)
    {
        EventFactory.FireOnCognitvEvent(this, CognitivSkill.LEFT, powerLevel);
    }

    void handleRightEvent(object sender, float powerLevel)
    {
        EventFactory.FireOnCognitvEvent(this, CognitivSkill.RIGHT, powerLevel);
    }

    void handleLiftEvent(object sender, float powerLevel)
    {
        EventFactory.FireOnCognitvEvent(this, CognitivSkill.LIFT, powerLevel);
    }

    void handlePushEvent(object sender, float powerLevel)
    {
        EventFactory.FireOnCognitvEvent(this, CognitivSkill.PUSH, powerLevel);
    }

    void handleDisappearEvent(object sender, float powerLevel)
    {
        EventFactory.FireOnCognitvEvent(this, CognitivSkill.DISAPPEAR, powerLevel);
    }

    void handleEmotionEvent(object sender, float powerLevel)
    {
        EventFactory.FireOnEmotionEvent(this, "meditation", powerLevel, 0.0f);
    }
}
