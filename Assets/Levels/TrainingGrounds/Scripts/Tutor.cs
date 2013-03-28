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
	
	private bool movementFinished = false;
	
	
	// Use this for initialization
	void Start () {
        transform = this.GetComponent<Transform>();
		player = GameObject.FindGameObjectWithTag("Player");
		
		NotificationCenter.DefaultCenter.AddObserver(this, "SelectionEvent");

	}
	
	// Update is called once per frame
	void Update () {
		
		if (movementFinished) return;
		
		if (movementCount >= 5) { // 5 because of jump
			Hashtable param = new Hashtable();
            param.Add("text", "Good, it seems you understand basic movement. Why don't you click on that cube over there?");
            param.Add("duration", 6.0f);
            NotificationCenter.DefaultCenter.PostNotification(this, "DisplayText", param);
			movementFinished = true;
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

	void SelectionEvent(Notification notification) 
	{
		GameObject selectedObject = notification.data["gameObject"] as GameObject;
		
		if (selectedObject.tag == "CognitivObject") {
			Hashtable param = new Hashtable();
            param.Add("text", "Excellent! Now press 1 to lift the cube. Do this 3 times to continue.");
            param.Add("duration", 6.0f);
            NotificationCenter.DefaultCenter.PostNotification(this, "DisplayText", param);
		}
	}
    
}
