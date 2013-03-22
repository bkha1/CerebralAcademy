using UnityEngine;
using System.Collections;

[ExecuteInEditMode] 
public class SpeechManager : MonoBehaviour {

    public GUISkin BubbleSkin;
    public Material BubbleMaterial;


    public static float SHORT_DURATION = 1.0f;
    public static float LONG_DURATION = 3.0f;

	// Use this for initialization
	void Start () {
        NotificationCenter.DefaultCenter.AddObserver(this, "NewSpeechBubble");
	    
	}
	

    // This code uses SpeechBubble2
    void NewSpeechBubble(Notification notification)
    {
        if (!notification.data.ContainsKey("gameObject"))
        {
            // TODO: Throw an exception
            return;
        }

        GameObject targetObject = (GameObject)notification.data["gameObject"];

        // Add the SpeechBubble2 Component to the target Object
        SpeechBubble targetScript = targetObject.AddComponent<SpeechBubble>();
        targetScript.GUIText = (string)notification.data["text"];
        targetScript.guiSkin = BubbleSkin;
        targetScript.mat = BubbleMaterial;

        Destroy(targetScript, (float)notification.data["duration"]);
    }

}


