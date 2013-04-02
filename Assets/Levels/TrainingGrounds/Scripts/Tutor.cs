using UnityEngine;
using System.Collections;

public class Tutor : MonoBehaviour {
	
	
	public GameObject cube;
    public int maxLifts = 5;

	private GameObject player;
	
	private int movementCount = 0;
	
	private bool wPressed = false;
	private bool sPressed = false;
	private bool dPressed = false;
	private bool aPressed = false;
	private bool spacePressed = false;
	
	private bool movementFinished = false;

    private const int NUM_MOVEMENT_DIRECTIONS = 5;
	
	
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		
		NotificationCenter.DefaultCenter.AddObserver(this, "SelectionEvent");
		NotificationCenter.DefaultCenter.AddObserver(this, "LiftCompleted");
		NotificationCenter.DefaultCenter.AddObserver(this, "TeleportPlayerEvent");
		
        EventFactory.FireDisplayTextEvent(this, "Welcome, I will teach you to move. Use W, A, S, D, and Space to move. Go ahead, give it a try.", 6.0f);

	}
	
	// Update is called once per frame
	void Update () {
		
		if (movementFinished) return;
		
		if (movementCount >= NUM_MOVEMENT_DIRECTIONS) { // 5 because of jump

            EventFactory.FireDisplayTextEvent(this, "Good, it seems you understand basic movement. Why don't you click on that cube over there?", 6.0f);
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
            EventFactory.FireDisplayTextEvent(this, "Excellent! Now press Q to lift the cube. Do this 3 times to continue.", 6.0f);
		}
	}
	
	void LiftCompleted(Notification notification)
	{
		if (!movementFinished) 
		{
            EventFactory.FireDisplayTextEvent(this, "I think you should practice movement before continuing.", 3.0f);
		} else {
            EventFactory.FireDisplayTextEvent(this, "You're amazing! I think you are ready to step it up. Head over to the Test Area.", 6.0f);
			
			StartCoroutine(teleportBack(6.0f));
			
			GameState.Instance.hasTrained = true;
			
            EventFactory.FireTeleportPlayerEvent(this, GameObject.FindGameObjectWithTag("Player"), new Vector3(), true, "Lobby");
		}
	}
	
	IEnumerator teleportBack(float timeToWait) {
			
		yield return new WaitForSeconds(timeToWait); // This is NOT Working! BUG
	}
    
}
