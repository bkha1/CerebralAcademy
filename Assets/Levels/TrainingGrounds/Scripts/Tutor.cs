using UnityEngine;
using System.Collections;

public class Tutor : MonoBehaviour {
	
	
	public GameObject cube;
	
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
		player = GameObject.FindGameObjectWithTag("Player");
		
		NotificationCenter.DefaultCenter.AddObserver(this, "SelectionEvent");
		NotificationCenter.DefaultCenter.AddObserver(this, "LiftCompleted");
		NotificationCenter.DefaultCenter.AddObserver(this, "TeleportPlayerEvent");
		
		Hashtable param = new Hashtable();
        param.Add("text", "Welcome, I will teach you to move. Use W, A, S, D, and Space to move. Go ahead, give it a try.");
        param.Add("duration", 6.0f);
        NotificationCenter.DefaultCenter.PostNotification(this, "DisplayText", param);

	}
	
	// Update is called once per frame
	void Update () {
		
		if (movementFinished) return;
		
		if (movementCount >= 5) { // 5 because of jump
			Hashtable param = new Hashtable();
            param.Add("text", "Good, it seems you understand basic movement. Why don't you click on that cube over there?");
            param.Add("duration", 6.0f);
            NotificationCenter.DefaultCenter.PostNotification(this, "DisplayText", param);
			
			cube.SetActive(true);
			
			movementFinished = true;
			
		}
		
		if (Input.GetKeyUp(KeyCode.W) && !wPressed) {
			movementCount++;
			wPressed = true;
		} else if (Input.GetKeyUp(KeyCode.A) && !aPressed) {
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
            param.Add("text", "Excellent! Now press Q to lift the cube. Do this 3 times to continue.");
            param.Add("duration", 6.0f);
            NotificationCenter.DefaultCenter.PostNotification(this, "DisplayText", param);
		}
	}
	
	void LiftCompleted(Notification notification)
	{
		if (!movementFinished) 
		{
			Hashtable param = new Hashtable();
            param.Add("text", "I think you should practice movement before continuing.");
            param.Add("duration", 3.0f);
            NotificationCenter.DefaultCenter.PostNotification(this, "DisplayText", param);
		} else {
			Hashtable param = new Hashtable();
            param.Add("text", "You're amazing! I think you are ready to step it up. Head over to the Test Area.");
            param.Add("duration", 3.0f);
            NotificationCenter.DefaultCenter.PostNotification(this, "DisplayText", param);
			
			StartCoroutine(teleportBack(6.0f));
			
			GameState.Instance.hasTrained = true;
			
			param = new Hashtable();
            param.Add("gameObject", GameObject.FindGameObjectWithTag("Player"));
            param.Add("target", new Vector3(0.0f, 0.0f, 0.0f));
            param.Add("isLevel", true);
            param.Add("level", "Lobby");
            NotificationCenter.DefaultCenter.PostNotification(this, "TeleportPlayerEvent", param);
		}
	}
	
	void TeleportPlayerEvent(Notification notification) 
	{
//		Hashtable param = new Hashtable();
//        param.Add("text", "Welcome, I will teach you to move. Use W, A, S, D, and Space to move. Go ahead, give it a try.");
//        param.Add("duration", 6.0f);
//        NotificationCenter.DefaultCenter.PostNotification(this, "DisplayText", param);
	}
	
	IEnumerator teleportBack(float timeToWait) {
			
		yield return new WaitForSeconds(timeToWait); // This is NOT Working! BUG
	}
    
}
