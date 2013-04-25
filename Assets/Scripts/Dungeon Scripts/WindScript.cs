using UnityEngine;
using System.Collections;

public class WindScript : MonoBehaviour {
	
	private GameObject player = null;
	private bool inZone = false;
	private int notShielded = 1;

	// Use this for initialization
	void Start () {
		NotificationCenter.DefaultCenter.AddObserver(this, "WindShield");	
	}
	
	// Update is called once per frame
	void Update () {	
		
		if (inZone == true){
			Debug.Log("Wind is pushing player");
			//strength ranges from 10 at the fan to 0 at the edge of the zone
			float strength = -.5F * Mathf.Min(0,player.gameObject.rigidbody.position.z);

			Vector3 playerPos = player.transform.position;
			playerPos += new Vector3 (0,0, strength * Time.deltaTime * notShielded);
			player.transform.position = playerPos;
			
		}
	
	}
	
	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "Player")
		{
        	player = col.gameObject;
			inZone = true;
		}
	}
	
	void OnTriggerExit(Collider col) {
		if (col.gameObject.tag == "Player")
		{
        	player = null;
			inZone = false;
		}
	}
	
	void WindShield (Notification notification){
		if (string.Compare(""+notification.data["isOn"],"True") == 0)
		{
			notShielded = 0;
		}
		else
		{
			notShielded = 1;
		}
	}
}
