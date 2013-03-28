using UnityEngine;
using System.Collections;

// TODO: Add a require CollisionTrigger component
public class DoorTrigger : MonoBehaviour {

    public string levelToLoad = null;
	
	private bool onTrigger = false;

    void Start()
    {
        if (levelToLoad == null)
        {
            Debug.LogError("Please input a level to load in the Editor!");
        }
    }
	
	void Update()
	{
		if(onTrigger && (Input.GetKeyDown(KeyCode.E)))
		{
			Hashtable param = new Hashtable();
            param.Add("gameObject", this.gameObject);
            param.Add("target", new Vector3(0.0f, 0.0f, 0.0f));
            param.Add("isLevel", true);
            param.Add("level", levelToLoad);
            NotificationCenter.DefaultCenter.PostNotification(this, "TeleportPlayerEvent", param);
		}
	}
	
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Create TeleportEvent passing gameObject (player), targetLocation, isLevel, level name
			/*
            Hashtable param = new Hashtable();
            param.Add("gameObject", other.gameObject);
            param.Add("target", new Vector3(0.0f, 0.0f, 0.0f));
            param.Add("isLevel", true);
            param.Add("level", levelToLoad);
            NotificationCenter.DefaultCenter.PostNotification(this, "TeleportPlayerEvent", param);
            */
			onTrigger = true;
        }
    }
	
	void OnTriggerExit(Collider other)
	{
		if(other.gameObject.tag == "Player")
			onTrigger = false;
	}
}
