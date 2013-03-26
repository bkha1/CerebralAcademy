using UnityEngine;
using System.Collections;

public class TeleportManager : MonoBehaviour {

    private GameObject teleportManager = null;

	// Use this for initialization
	void Awake () {

        teleportManager = GameObject.Find("Teleport Manager");

        if (teleportManager == null)
        {
            teleportManager = new GameObject("Teleport Manager");
            teleportManager.AddComponent<TeleportManager>();
        }

        NotificationCenter.DefaultCenter.AddObserver(this, "TeleportPlayerEvent");
	}

    void Start()
    {
        //NotificationCenter.DefaultCenter.AddObserver(this, "TeleportPlayerEvent");
    }

    void TeleportPlayerEvent(Notification notification)
    {
        // Check if 
        if ((bool) notification.data["isLevel"] == true)
        {
            Debug.Log("Loading level: " + (string)notification.data["level"] + "...");
            Application.LoadLevel((string)notification.data["level"]);
        }
        
    }
}
