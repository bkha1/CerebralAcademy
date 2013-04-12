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
        else if (Input.GetKeyUp(cognitivDisappearKey))
        {
            EventFactory.FireOnCognitvEvent(this, "disappear", 6.4f, Time.time);
        }
        else if (Input.GetKeyUp(cognitivLiftKey))
        {
            EventFactory.FireOnCognitvEvent(this, "lift", 6.4f, Time.time);
        }
        else if (Input.GetKeyUp(cognitivLeftKey))
        {
            EventFactory.FireOnCognitvEvent(this, "left", 6.4f, Time.time);
        }
        else if (Input.GetKeyUp(cognitivRightKey))
        {
            EventFactory.FireOnCognitvEvent(this, "right", 6.4f, Time.time);
        }
        else if (Input.GetKeyUp(cogntivPushKey))
        {
            EventFactory.FireOnCognitvEvent(this, "push", 6.4f, Time.time);
        }
        else if (Input.GetKeyUp(debugKey))
        {
            EventFactory.FireDisplayTextEvent(this, "Would you like some debug text with that?", 5.0f);

            EventFactory.FireTeleportPlayerEvent(this, GameObject.FindGameObjectWithTag("Player").gameObject, GameState.Instance.getSelectedObject().transform.position, false, "");
        }
        else if (Input.GetKeyUp(emotivEngineToggleKey))
        {
            if (EmotivHandler.Instance.isConnected())
            {
                EmotivHandler.Instance.disconnect();
            }
            else
            {
                EmotivHandler.Instance.connect();
            }
        }
	}

    void handleLeftEvent(object sender, float powerLevel)
    {
        EventFactory.FireOnCognitvEvent(this, "left", powerLevel, Time.time);
    }

    void handleRightEvent(object sender, float powerLevel)
    {
        EventFactory.FireOnCognitvEvent(this, "right", powerLevel, Time.time);
    }

    void handleLiftEvent(object sender, float powerLevel)
    {
        //Debug.Log("Lift Event of Power: " + powerLevel);
        //EventFactory.FireOnCognitvEvent(this, "lift", powerLevel, Time.time);
        EventFactory.FireOnCognitvEvent(this, "lift", powerLevel, 0.0f);
    }

    void handlePushEvent(object sender, float powerLevel)
    {
        EventFactory.FireOnCognitvEvent(this, "push", powerLevel, Time.time);
    }

    void handleDisappearEvent(object sender, float powerLevel)
    {
        EventFactory.FireOnCognitvEvent(this, "disappear", powerLevel, Time.time);
    }

    void handleEmotionEvent(object sender, float powerLevel)
    {
        //EventFactory.FireOnEmotionEvent(this, "meditation", powerLevel, Time.time);
        EventFactory.FireOnEmotionEvent(this, "meditation", powerLevel, 0.0f);
    }
}
