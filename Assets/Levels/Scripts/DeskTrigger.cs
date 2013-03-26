using UnityEngine;
using System.Collections;

public class DeskTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Create TeleportEvent passing gameObject (player), targetLocation, isLevel, level name
            /*Hashtable param = new Hashtable();
            param.Add("gameObject", other.gameObject);
            param.Add("target", new Vector3(0.0f, 0.0f, 0.0f));
            param.Add("isLevel", true);
            param.Add("level", levelToLoad);
            NotificationCenter.DefaultCenter.PostNotification(this, "TeleportPlayerEvent", param);*/
            NotificationCenter.DefaultCenter.PostNotification(this, "OpenProfileCreationGUI");
        }
    }
}
