using UnityEngine;
using System.Collections;

public class OrbEffects : MonoBehaviour {
	
	public float rotation = 100.0f;
	public string name;
	

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(new Vector3(0,rotation* Time.deltaTime ,0));
	}
	
	void OnTriggerEnter(Collider col){
		if(col.gameObject.tag == "Player"){
			Hashtable param = new Hashtable();
			param.Add ("name", name+"Orb");
			NotificationCenter.DefaultCenter.PostNotification(this, "ItemPickup", param);
			Destroy(gameObject);
			
			/*param = new Hashtable();
            param.Add("text", "You obtained the ability "+ name+"Orb.");
            param.Add("duration", 0.5f);
            NotificationCenter.DefaultCenter.PostNotification(this, "DisplayText", param);*/
		}
	}
}
