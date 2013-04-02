using UnityEngine;
using System.Collections;

public class LiftTest : MonoBehaviour {

    public int numberOfTimesToLift = 3;

    private int liftCounter = 0;

	void Start () {
		NotificationCenter.DefaultCenter.AddObserver(this, "OnCognitivLiftEvent");
	}
	
	void OnCognitivLiftEvent(Notification notification) 
	{
		liftCounter++;
		
		if (liftCounter == numberOfTimesToLift)
            {
                EventFactory.FireDisplayTextEvent(this, "Good job! You have learned Lift!", 5.0f);
                
                NotificationCenter.DefaultCenter.PostNotification(this, "LiftCompleted");
            }
	}
}
