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
			NotificationCenter.DefaultCenter.PostNotification(this, "got"+name);
			Destroy(gameObject);
			
			Hashtable param = new Hashtable();
            param.Add("text", "You obtained the ability "+ name);
            param.Add("duration", 3.0f);
            NotificationCenter.DefaultCenter.PostNotification(this, "DisplayText", param);
		}
	}
}
