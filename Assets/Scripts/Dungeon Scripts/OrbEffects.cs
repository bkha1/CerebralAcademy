using UnityEngine;
using System.Collections;

public class OrbEffects : MonoBehaviour {
	
	public float rotation = 100.0f;
	public float hoverSpeed = 120.0f;
	public float hoverHeight = 0.125f;
	public string name;
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
			param.Add ("name", name+"Orb");
			NotificationCenter.DefaultCenter.PostNotification(this, "ItemPickup", param);
			GameState.Instance.getCurrentPlayer().hasLearnedPush = true;
			GameState.Instance.getCurrentPlayer().hasLearnedPull = true;
			Destroy(gameObject);
		}
	}
}
