using UnityEngine;
using System.Collections;

public class Tutor : MonoBehaviour {

    private Transform transform; 
	// Use this for initialization
	void Start () {
        transform = this.GetComponent<Transform>();
        NotificationCenter.DefaultCenter.AddObserver(this, "OnTutorTriggerEnter");
        NotificationCenter.DefaultCenter.AddObserver(this, "OnTutorTriggerExit");
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    void OnTutorTriggerEnter(Notification notification)
    {
        Debug.Log("Notification arried at Tutor.");
        displayText("Hello player!", 1.0f);
    }

    void OnTutorTriggerExit(Notification notification)
    {

    }

    void displayText(string textMessage, float duration)
    {
        Hashtable param = new Hashtable();
        param.Add("gameObject", this.gameObject);
        param.Add("text", textMessage);
        param.Add("duration", duration);
        NotificationCenter.DefaultCenter.PostNotification(this, "NewSpeechBubble", param);
    }
}
