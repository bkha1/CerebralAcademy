using UnityEngine;
using System.Collections;

public class TeleportManager : MonoBehaviour {

    private GameObject teleportManager = null;

	// Use this for initialization
	void Awake () {

        teleportManager = GameObject.Find("TeleportManager");

        if (teleportManager == null)
        {
            teleportManager = new GameObject("TeleportManager");
            teleportManager.AddComponent<TeleportManager>();
        }

        NotificationCenter.DefaultCenter.AddObserver(this, "TeleportPlayerEvent");
	}

    void TeleportPlayerEvent(Notification notification)
    {
        if ((bool)notification.data["isLevel"] == true)
        {
            Debug.Log("Loading level: " + (string)notification.data["level"] + "...");

            if (((string)notification.data["level"]) == "MainMenu")
            {
                // Handle business here
                //GameState.Instance.hasTrained = false; // This breaks main menu

            }

            Application.LoadLevel((string)notification.data["level"]);
        }
        else
        {
            GameObject gameObject = notification.data["gameObject"] as GameObject;
            gameObject.transform.position = (Vector3) notification.data["target"];
        }
        
    }
}
