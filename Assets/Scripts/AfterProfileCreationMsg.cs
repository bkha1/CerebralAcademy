using UnityEngine;
using System.Collections;

public class AfterProfileCreationMsg : MonoBehaviour {

	public float areaHeight = 500;
    public float areaWidth = 500;
    public float buttonSpacing = 25;
    public GUISkin customSkin;
    public GUIStyle layoutStyle;
    public Texture2D textureTop;
	public bool turnOffToTrainingGrounds = false;

    private bool isVisible = false;
    
    private GameObject playerObject;
    private GameObject playerCamera;
	
	// Use this for initialization
	void Start () {

        playerObject = GameObject.FindGameObjectWithTag("Player");
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera");
        NotificationCenter.DefaultCenter.AddObserver(this, "OpenAfterProfileCreationMsg");
	
	}
	
	// Update is called once per frame
	void OnGUI () {

        if (!isVisible) return;
		
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


            playerObject.GetComponent<MouseLook>().enabled = true;
            playerCamera.GetComponent<MouseLook>().enabled = true;
            GameObject.Find("PauseManager").GetComponent<PauseController>().enabled = true;
			
			if(turnOffToTrainingGrounds == false)
			{
            	EventFactory.FireTeleportPlayerEvent(this, gameObject, new Vector3(), true, "TrainingGround");
			}
            isVisible = false;
			Screen.lockCursor = true;
            Screen.showCursor = false;
		}//end button
        GUILayout.EndArea();
        
	
	}

    void OpenAfterProfileCreationMsg(Notification notification)
    {
        isVisible = true;
    }
}
