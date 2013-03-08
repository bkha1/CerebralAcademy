using UnityEngine;
using System.Collections;

public class InputHandler : MonoBehaviour {

    public string cognitivDisappearKey;

    public string cognitivLiftKey;

	public string cogntivPushKey;

	public string cognitivLeftKey;
    public string cognitivRightKey;
	//public string playerProfileKey;

    public string pauseKey = "escape";
	
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(pauseKey))
        {
			if (!GameState.Instance.isPaused()) {
				GameEventManager.TriggerPause();
			} else {
				GameEventManager.TriggerUnpause();
			}

        }
        else if (Input.GetKeyUp(cognitivDisappearKey))
        {
            CognitvEventManager.TriggerCognitivDisappear(null, 6.4f);
        }
        else if (Input.GetKeyUp(cognitivLiftKey))
        {
            CognitvEventManager.TriggerCognitivLift(null, 6.4f);
        }
        else if (Input.GetKeyUp(cognitivLeftKey))
        {
            CognitvEventManager.TriggerCognitivLeft(null, 6.4f);
        }
	}
}
