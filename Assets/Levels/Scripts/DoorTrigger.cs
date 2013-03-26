using UnityEngine;
using System.Collections;

// TODO: Add a require CollisionTrigger component
public class DoorTrigger : MonoBehaviour {

    public string levelToLoad = null;

    void Start()
    {
        if (levelToLoad == null)
        {
            Debug.LogError("Please input a level to load in the Editor!");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Create TeleportEvent passing gameObject (player), targetLocation, isLevel, level name
            Hashtable param = new Hashtable();
            param.Add("gameObject", other.gameObject);
            param.Add("target", new Vector3(0.0f, 0.0f, 0.0f));
            param.Add("isLevel", true);
            param.Add("level", levelToLoad);
            NotificationCenter.DefaultCenter.PostNotification(this, "TeleportPlayerEvent", param);
        }
    }
}
