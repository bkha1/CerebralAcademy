using UnityEngine;
using System.Collections;

public class InputHandler : MonoBehaviour {

    public string cognitivDisappearKey;

    public string cognitivLiftKey;

	public string cogntivPushKey;

	public string cognitivLeftKey;
    public string cognitivRightKey;

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
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(pauseKey))
        {
            // NOTE: This can actually just be "OnPause" and let the handler manage the state.
			/*if (!GameState.Instance.isPaused()) {
                NotificationCenter.DefaultCenter.PostNotification(this, "OnPauseEvent");
				GameEventManager.TriggerPause();
			} else {
                NotificationCenter.DefaultCenter.PostNotification(this, "OnUnPauseEvent");
				GameEventManager.TriggerUnpause();
			}*/
            //NotificationCenter.DefaultCenter.PostNotification(this, "OnPauseEvent");

        }
        else if (Input.GetKeyUp(cognitivDisappearKey))
        {
            Hashtable param = new Hashtable();
            param.Add("skill", "disappear");
            param.Add("power", 6.4f);
            param.Add("time", Time.time);
            NotificationCenter.DefaultCenter.PostNotification(this, "OnCognitivEvent", param);
        }
        else if (Input.GetKeyUp(cognitivLiftKey))
        {
            Hashtable param = new Hashtable();
            param.Add("skill", "lift");
            param.Add("power", 6.4f);
            param.Add("time", Time.time);
            NotificationCenter.DefaultCenter.PostNotification(this, "OnCognitivEvent", param);
        }
        else if (Input.GetKeyUp(cognitivLeftKey))
        {
            Hashtable param = new Hashtable();
            param.Add("skill", "left");
            param.Add("power", 6.4f);
            param.Add("time", Time.time);
            NotificationCenter.DefaultCenter.PostNotification(this, "OnCognitivEvent", param);
        }
        else if (Input.GetKeyUp(cognitivRightKey))
        {
            Hashtable param = new Hashtable();
            param.Add("skill", "right");
            param.Add("power", 6.4f);
            param.Add("time", Time.time);
            NotificationCenter.DefaultCenter.PostNotification(this, "OnCognitivEvent", param);
        }
        else if (Input.GetKeyUp(cogntivPushKey))
        {
            Hashtable param = new Hashtable();
            param.Add("skill", "push");
            param.Add("power", 6.4f);
            param.Add("time", Time.time);
            NotificationCenter.DefaultCenter.PostNotification(this, "OnCognitivEvent", param);
        }


        else if (Input.GetKeyUp(debugKey))
        {
            GameObject gObj = GameState.Instance.getSelectedObject();

            if (gObj != null)
            {
                Hashtable param = new Hashtable();
                param.Add("gameObject", gObj);
                param.Add("text", "Debug String");
                param.Add("duration", 5.0f);
                NotificationCenter.DefaultCenter.PostNotification(this, "NewSpeechBubble", param);
            }
        }
	}

    void handleLeftEvent(object sender, float powerLevel)
    {
        Hashtable param = new Hashtable();
        param.Add("skill", "left");
        param.Add("power", powerLevel);
        param.Add("time", Time.time);
        NotificationCenter.DefaultCenter.PostNotification(this, "OnCognitivLeftEvent", param);
    }

    void handleRightEvent(object sender, float powerLevel)
    {
        Hashtable param = new Hashtable();
        param.Add("skill", "right");
        param.Add("power", powerLevel);
        param.Add("time", Time.time);
        NotificationCenter.DefaultCenter.PostNotification(this, "OnCognitivRightEvent", param);
    }

    void handleLiftEvent(object sender, float powerLevel)
    {
        Hashtable param = new Hashtable();
        param.Add("skill", "lift");
        param.Add("power", powerLevel);
        param.Add("time", Time.time);
        NotificationCenter.DefaultCenter.PostNotification(this, "OnCognitivLiftEvent", param);
    }

    void handlePushEvent(object sender, float powerLevel)
    {
        Hashtable param = new Hashtable();
        param.Add("skill", "push");
        param.Add("power", powerLevel);
        param.Add("time", Time.time);
        NotificationCenter.DefaultCenter.PostNotification(this, "OnCognitivPushEvent", param);
    }

    void handleDisappearEvent(object sender, float powerLevel)
    {
        Hashtable param = new Hashtable();
        param.Add("skill", "disappear");
        param.Add("power", powerLevel);
        param.Add("time", Time.time);
        NotificationCenter.DefaultCenter.PostNotification(this, "OnCognitivDisappearEvent", param);
    }


}
