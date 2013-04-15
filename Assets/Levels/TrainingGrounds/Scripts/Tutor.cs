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
    private bool isActive = false;

    public int numberOfTimesToLift = 3;
    private int liftCounter = 0;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        NotificationCenter.DefaultCenter.AddObserver(this, "SelectionEvent");
        NotificationCenter.DefaultCenter.AddObserver(this, "TeleportPlayerEvent");
        NotificationCenter.DefaultCenter.AddObserver(this, "StartCogTutor");

        NotificationCenter.DefaultCenter.AddObserver(this, "LiftCompleted");
        NotificationCenter.DefaultCenter.AddObserver(this, "OnCognitivLiftEvent");
    }
	
	void Update () {
		
		if (movementFinished || !isActive) return;

        // If they have not moved yet, keep the into text up.
        if (movementCount == 0)
        {
            EventFactory.FireDisplayTextEvent(this, "Welcome, I will teach you to move. Use W, A, S, D, and SPACE to move. Go ahead, give it a try.", 5.0f);
        }
		
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

    void StartCogTutor(Notification notification)
    {
        isActive = true;
        EventFactory.FireDisplayTextEvent(this, "Welcome, I will teach you to move. Use W, A, S, D, and SPACE to move. Go ahead, give it a try.", 10.0f);
        
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
            EventFactory.FireDisplayTextEvent(this, "I think you should practice movement before continuing.", 1.0f);
		} else {
            EventFactory.FireDisplayTextEvent(this, "You're amazing! I think you are ready to step it up.", 5.0f);
            
            GameState.Instance.getCurrentPlayer().hasLearnedLift = true;

            // The tutor is done
            NotificationCenter.DefaultCenter.PostNotification(this, "CogTutorFinished");
			
		}
	}

    void OnCognitivLiftEvent(Notification notification)
    {
        liftCounter++;

        if (liftCounter == 1)
        {
            StartCoroutine(explainMana());
        } else if (liftCounter == numberOfTimesToLift)
        {
            EventFactory.FireDisplayTextEvent(this, "Good job! You have learned LIFT!", 5.0f);

            NotificationCenter.DefaultCenter.PostNotification(this, "LiftCompleted");
        }
    }

    IEnumerator explainMana() 
    {
        EventFactory.FireDisplayTextEvent(this, "Every time you use a Cognitive Skill, your mana decreases. Mana will passively refill depending on how relaxed you are. To learn about different relaxation techniques, talk with the tutor.", 5.0f);
        yield return new WaitForSeconds(10.0f);
    }
}
