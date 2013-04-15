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
				if (levelToLoad == "Dungeon1" && !GameState.Instance.getCurrentPlayer().hasLearnedLift) {
                    EventFactory.FireDisplayTextEvent(this, "You have not been authorized to enter. Please visit the training grounds.", 1.0f);
				} else {
                    EventFactory.FireTeleportPlayerEvent(this, GameObject.FindGameObjectWithTag("Player"), new Vector3(0.0f, 0.0f, 0.0f), true, levelToLoad);
				}
			}
			
		}
	}
	
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            EventFactory.FireDisplayTextEvent(this, "Press E Key", 1.0f);
			onTrigger = true;
        }
    }
	
	void OnTriggerExit(Collider other)
	{
		if(other.gameObject.tag == "Player")
			onTrigger = false;
	}
}
