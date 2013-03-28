using UnityEngine;
using System.Collections;

public class LiftTest : MonoBehaviour {

    public int numberOfTimesToLift = 3;

    private int liftCounter = 0;

	// Use this for initialization
	void Start () {
	
	}
	

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CognitivObject")
        {
            Debug.Log("User has demonstrated lift skill.");
            liftCounter++;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "CognitivObject")
        {
            if (liftCounter == numberOfTimesToLift)
            {
                Debug.Log("User, good job! You have learned lift!");
                NotificationCenter.DefaultCenter.PostNotification(this, "LiftCompleted");
            }
        }
    }
}
