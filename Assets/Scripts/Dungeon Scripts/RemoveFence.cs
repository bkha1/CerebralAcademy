using UnityEngine;
using System.Collections;

public class RemoveFence : MonoBehaviour {
	
	
	private int inventory = 0;
	public int itemsNeeded = 1;
	public bool Notify = false;
	public string notification = null;
	
	// Use this for initialization
	void Start () {
		
		NotificationCenter.DefaultCenter.AddObserver(this, "ItemPickup");
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if (inventory == itemsNeeded) {
			Destroy (this.gameObject);
			if (Notify) NotificationCenter.DefaultCenter.PostNotification(this, notification);
		}
	
	}
	
	void ItemPickup(Notification notification) 
	{
		string item = notification.data["name"] as string;		
		Debug.Log ("You picked up: " + item);
		inventory++;
	}
}
