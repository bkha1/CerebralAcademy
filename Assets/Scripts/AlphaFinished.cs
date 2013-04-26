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
		gameObject.GetComponent<MouseLook>().enabled = false;
        transform.parent.GetComponent<MouseLook>().enabled = false;
		gameObject.GetComponent<PauseController>().enabled = false;
		
		areaHeight = Screen.height;

        GUI.skin = customSkin;
        GUILayout.BeginArea(new Rect(Screen.width / 2 - areaWidth / 2, Screen.height / 2 - areaHeight / 2, areaWidth, areaHeight), layoutStyle);
        GUILayout.Space(50);
        GUILayout.Label(textureTop);
        GUILayout.Space(buttonSpacing);
		
		GUILayout.Label("Congratulations! You have gained the ability to push objects! Use the E Key to push objects! Please head on over to Test Area 2.");//, however, you'll have to wait until the beta to play with them. See you soon!");
		
        GUILayout.Space(buttonSpacing);
		if(GUILayout.Button("Okay"))
		{
			Time.timeScale = 1;

            gameObject.GetComponent<MouseLook>().enabled = true;
            transform.parent.GetComponent<MouseLook>().enabled = true;
            gameObject.GetComponent<PauseController>().enabled = true;
			//GameState.Instance.hasTrained = false;


            //EventFactory.FireTeleportPlayerEvent(this, player, new Vector3(), true, "Main Menu");
			isVisible = false;
			
			
			//this.enabled = false;
		}//end button
        GUILayout.EndArea();
        
	
	}
	
	void Dungeon1Complete(Notification notification) 
	{
		//Time.timeScale = 0;
		gameObject.GetComponent<MouseLook>().enabled = false;
        transform.parent.GetComponent<MouseLook>().enabled = false;
		gameObject.GetComponent<PauseController>().enabled = false;
		
		isVisible = true;
	}
}
