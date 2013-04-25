using UnityEngine;
using System.Collections;

public class WindShield : MonoBehaviour {

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "Player")
		{
        	Hashtable param = new Hashtable();
			param.Add ("isOn",true);
			NotificationCenter.DefaultCenter.PostNotification(this, "WindShield", param);
		}
	}
	
	void OnTriggerExit(Collider col) {
		if (col.gameObject.tag == "Player")
		{
        	Hashtable param = new Hashtable();
			param.Add ("isOn",false);
			NotificationCenter.DefaultCenter.PostNotification(this, "WindShield", param);
		}
	}
}
