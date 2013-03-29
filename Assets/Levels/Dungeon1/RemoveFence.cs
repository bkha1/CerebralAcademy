using UnityEngine;
using System.Collections;

public class RemoveFence : MonoBehaviour {
	
	
	private int inventory = 0;
	
	// Use this for initialization
	void Start () {
		
		NotificationCenter.DefaultCenter.AddObserver(this, "ItemPickup");
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if (inventory == 2) {
			//Destroy (this);
			NotificationCenter.DefaultCenter.PostNotification(this, "Dungeon1Complete");
		}
	
	}
	
	void ItemPickup(Notification notification) 
	{
		string item = notification.data["name"] as string;
		
		Debug.Log ("You picked up: " + item);
		if (item == "PushOrb") {
			inventory++;
		} else if (item == "PullOrb") {
			inventory++;
		}
	}
}
