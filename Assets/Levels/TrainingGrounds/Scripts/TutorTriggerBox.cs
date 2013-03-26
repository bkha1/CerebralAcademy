using UnityEngine;
using System.Collections;

public class TutorTriggerBox : MonoBehaviour {


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something entered trigger box!");
        //if (other.tag == "Player")
        //{
            NotificationCenter.DefaultCenter.PostNotification(this, "OnTutorTriggerEnter");
        //}
    }

    void OnTriggerExit(Collider other)
    {
        //if (other.tag == "Player")
        //{
            NotificationCenter.DefaultCenter.PostNotification(this, "OnTutorTriggerExit");
        //}
    }
}
