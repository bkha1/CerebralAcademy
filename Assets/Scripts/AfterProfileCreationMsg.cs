using UnityEngine;
using System.Collections;

public class AfterProfileCreationMsg : MonoBehaviour {

	public float areaHeight = 500;
    public float areaWidth = 500;
    public float buttonSpacing = 25;
    public GUISkin customSkin;
    public GUIStyle layoutStyle;
    public Texture2D textureTop;
	
	
	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void OnGUI () {
		
		
		if (GameState.Instance.hasTrained) return;
		
		Time.timeScale = 0;
		
		areaHeight = Screen.height;

        GUI.skin = customSkin;
        GUILayout.BeginArea(new Rect(Screen.width / 2 - areaWidth / 2, Screen.height / 2 - areaHeight / 2, areaWidth, areaHeight), layoutStyle);
        GUILayout.Space(50);
        GUILayout.Label(textureTop);
        GUILayout.Space(buttonSpacing);
		
		GUILayout.Label("Thank you for enrolling. We will now take you to the school Training Grounds, where you will learn the basics of controlling your psychic abilities.");
		
        GUILayout.Space(buttonSpacing);
		if(GUILayout.Button("Continue"))
		{
			Time.timeScale = 1;

            this.GetComponent<MouseLook>().enabled = true;
            this.transform.parent.GetComponent<MouseLook>().enabled = true;
            gameObject.GetComponent<PauseController>().enabled = true;
			
			//this.GetComponent<ProfileCreation>().enabled = true;
			Hashtable param1 = new Hashtable();
            param1.Add("gameObject", this.gameObject);
            param1.Add("target", new Vector3(0.0f, 0.0f, 0.0f));
		    param1.Add("isLevel", true);
		    param1.Add("level", "TrainingGround");
		    NotificationCenter.DefaultCenter.PostNotification(this, "TeleportPlayerEvent", param1);
			this.enabled = false;
		}//end button
        GUILayout.EndArea();
        
	
	}
}
