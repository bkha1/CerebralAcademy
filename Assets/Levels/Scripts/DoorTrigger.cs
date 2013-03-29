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
		if(onTrigger)
		{
			if(Input.GetKeyDown(KeyCode.E))
			{
				if (levelToLoad == "Dungeon1" && !GameState.Instance.hasTrained) {
					Hashtable param = new Hashtable();
		            param.Add("text", "You have not been authorized to enter. Please visit the training grounds.");
		            param.Add("duration", 1.0f);
		            NotificationCenter.DefaultCenter.PostNotification(this, "DisplayText", param);
				} else {
				
					Hashtable param1 = new Hashtable();
		            param1.Add("gameObject", this.gameObject);
		            param1.Add("target", new Vector3(0.0f, 0.0f, 0.0f));
		            param1.Add("isLevel", true);
		            param1.Add("level", levelToLoad);
		            NotificationCenter.DefaultCenter.PostNotification(this, "TeleportPlayerEvent", param1);
				}
			}
			
		}
	}
	
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
			
            Hashtable param = new Hashtable();
            param.Add("text", "Press E Key");
            param.Add("duration", 1.0f);
            NotificationCenter.DefaultCenter.PostNotification(this, "DisplayText", param);
			
			onTrigger = true;
        }
    }
	
	void OnTriggerExit(Collider other)
	{
		
		
		if(other.gameObject.tag == "Player")
			onTrigger = false;
	}
}
