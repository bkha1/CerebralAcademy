using UnityEngine;
using System.Collections;

public class Tutor : MonoBehaviour {

    private Transform transform; 
	private GameObject player;
	
	private int movementCount = 0;
	
	private bool wPressed = false;
	private bool sPressed = false;
	private bool dPressed = false;
	private bool aPressed = false;
	private bool spacePressed = false;
	
	
	// Use this for initialization
	void Start () {
        transform = this.GetComponent<Transform>();
		player = GameObject.FindGameObjectWithTag("Player");

	}
	
	// Update is called once per frame
	void Update () {
		
		if (movementCount >= 5) { // 5 because of jump
			Hashtable param = new Hashtable();
            param.Add("text", "Good, it seems you understand basic movement. Why don't you click on that cube over there?");
            param.Add("duration", 6.0f);
            NotificationCenter.DefaultCenter.PostNotification(this, "DisplayText", param);
			this.enabled = false;
		}
		
		if (Input.GetKeyUp(KeyCode.W) && !wPressed) {
			// Congrats, you have moved forward, now try moving backward.
			/*Hashtable param = new Hashtable();
            param.Add("text", "Now try moving backward by pressing S.");
            param.Add("duration", 1.0f);
            NotificationCenter.DefaultCenter.PostNotification(this, "DisplayText", param);*/
			movementCount++;
			wPressed = true;
		} else if (Input.GetKeyUp(KeyCode.A) && !aPressed) {
			/*Hashtable param = new Hashtable();
            param.Add("text", "Now try strafing right by pressing D.");
            param.Add("duration", 1.0f);
            NotificationCenter.DefaultCenter.PostNotification(this, "DisplayText", param);*/
			movementCount++;
			aPressed = true;
		} else if (Input.GetKeyUp(KeyCode.S) && !sPressed) {
			movementCount++;
			sPressed = true;
		} else if (Input.GetKeyUp(KeyCode.D) && !dPressed) {
			movementCount++;
			dPressed = true;
		} else if (Input.GetKeyUp(KeyCode.Space) && !spacePressed) {
			movementCount++;
			spacePressed = true;
		}
	
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
