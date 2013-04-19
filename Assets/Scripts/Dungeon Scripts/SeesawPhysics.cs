using UnityEngine;
using System.Collections;

public class SeesawPhysics : MonoBehaviour {
	public string ally = null; //name of where the force is being transfered to
	public bool useMass = false; //where or not mass affects force
	private GameObject heavyObject = null;

	// Use this for initialization
	void Start () {		
		NotificationCenter.DefaultCenter.AddObserver(this, "Seesaw");	
	}
	
	// Update is called once per frame
	void Update () {
	
	}	
	
	void OnTriggerEnter(Collider col){ //detect how muych force is applied to sensor A

		heavyObject = col.gameObject; //heavy object is what steps on the sensor
		float mass = heavyObject.rigidbody.mass;		
		float velocity = heavyObject.rigidbody.velocity.y;
		if (velocity < -3)
		{
			if (useMass){
				velocity = mass*velocity;
			}
			Hashtable param = new Hashtable();
			param.Add ("force",velocity);
			NotificationCenter.DefaultCenter.PostNotification(this, "Seesaw", param);
		}
	}
	
	 void OnTriggerExit (Collider col) {
        heavyObject = null;
    }
	
	void Seesaw (Notification notification){ //sensor B returns the force
		if (ally == notification.sender.name){
			float force = -1f * float.Parse(""+notification.data["force"]);
			if (useMass){
				force = force / heavyObject.rigidbody.mass;
			}
			if (heavyObject.name == "Player"){
				Hashtable param = new Hashtable();
				param.Add ("force",force);
				NotificationCenter.DefaultCenter.PostNotification(this, "Jump", param);	
			}
			else{			
			heavyObject.rigidbody.AddForce(0,force,0,ForceMode.VelocityChange);
			}
		}
	}
}
