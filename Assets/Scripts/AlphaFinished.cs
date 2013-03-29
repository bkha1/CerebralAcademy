using UnityEngine;
using System.Collections;

public class AlphaFinished : MonoBehaviour {

	public float areaHeight = 500;
    public float areaWidth = 500;
    public float buttonSpacing = 25;
    public GUISkin customSkin;
    public GUIStyle layoutStyle;
    public Texture2D textureTop;
	
	private bool isVisible = false;
	private GameObject player;
	
	void Start() {
		player = GameObject.FindGameObjectWithTag("Player");
		NotificationCenter.DefaultCenter.AddObserver(this, "Dungeon1Complete");
		
		
	}
	
	// Update is called once per frame
	void OnGUI () {
		
		
		if (!isVisible) return;
		
		Time.timeScale = 0;
		
		areaHeight = Screen.height;

        GUI.skin = customSkin;
        GUILayout.BeginArea(new Rect(Screen.width / 2 - areaWidth / 2, Screen.height / 2 - areaHeight / 2, areaWidth, areaHeight), layoutStyle);
        GUILayout.Space(50);
        GUILayout.Label(textureTop);
        GUILayout.Space(buttonSpacing);
		
		GUILayout.Label("Congratulations! You have gained the ability to push and pull objects, however, you'll have to wait until the beta to play with them. See you soon!");
		
        GUILayout.Space(buttonSpacing);
		if(GUILayout.Button("Okay"))
		{
			Time.timeScale = 1;

//            gameObject.GetComponent<MouseLook>().enabled = true;
//            transform.parent.GetComponent<MouseLook>().enabled = true;
//            gameObject.GetComponent<PauseController>().enabled = true;
			//GameState.Instance.hasTrained = false;
			
			
			Hashtable param1 = new Hashtable();
            param1.Add("gameObject", this.gameObject);
            param1.Add("target", new Vector3(0.0f, 0.0f, 0.0f));
		    param1.Add("isLevel", true);
		    param1.Add("level", "Main Menu");
		    NotificationCenter.DefaultCenter.PostNotification(this, "TeleportPlayerEvent", param1);
			isVisible = false;
			
			
			//this.enabled = false;
		}//end button
        GUILayout.EndArea();
        
	
	}
	
	void Dungeon1Complete(Notification notification) 
	{
		Time.timeScale = 0;
		gameObject.GetComponent<MouseLook>().enabled = false;
        transform.parent.GetComponent<MouseLook>().enabled = false;
		gameObject.GetComponent<PauseController>().enabled = false;
		
		isVisible = true;
	}
}
