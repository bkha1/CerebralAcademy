using UnityEngine;
using System.Collections;

public class OrbEffects : MonoBehaviour {
	
	public float rotation = 100.0f;
	public float hoverSpeed;
	public float hoverHeight;
	public string itemName;
	private float yCord;
	

	// Use this for initialization
	void Start () {
		yCord = transform.position.y;
	}
	
	// Update is called once per frame 
	void Update () {
		transform.Rotate(new Vector3(0,rotation* Time.deltaTime ,0)); //roatation
		Vector3 orbCord = transform.position;
		orbCord.y = yCord + hoverHeight*Mathf.Sin(hoverSpeed*Time.time*(Mathf.PI/180));
		transform.position = orbCord;
		
	}
	
	void OnTriggerEnter(Collider col){
		if(col.gameObject.tag == "Player"){
			Hashtable param = new Hashtable();
            param.Add("name", itemName + "Orb");
			NotificationCenter.DefaultCenter.PostNotification(this, "ItemPickup", param);
			Destroy(gameObject);

            /*param = new Hashtable();
            param.Add("text", "You obtained the ability "+ itemName+"Orb.");
            param.Add("duration", 0.5f);
            NotificationCenter.DefaultCenter.PostNotification(this, "DisplayText", param);*/
        }
	}
}
