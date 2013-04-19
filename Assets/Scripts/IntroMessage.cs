using UnityEngine;
using System.Collections;

public class IntroMessage : MonoBehaviour {
	
	public float areaHeight = 500;
    public float areaWidth = 500;
    public float buttonSpacing = 25;
    public GUISkin customSkin;
    public GUIStyle layoutStyle;
    public Texture2D textureTop;

    private bool isVisible = false;
	
	void Start () {

        // NOTE: At the start of the game, we create a player object.
        if (GameState.Instance.getCurrentPlayer() == null)
        {
            Debug.Log("Player was null.");
            Player player = new Player();
            GameState.Instance.setCurrentPlayer(player);
        }
		
		if (!GameState.Instance.getCurrentPlayer().hasLearnedLift)
		{
			Debug.Log("Player has not learned lift.");
            isVisible = true;
			Time.timeScale = 0;

	        gameObject.GetComponent<MouseLook>().enabled = false;
	        transform.parent.GetComponent<MouseLook>().enabled = false;
            GameObject.Find("PauseManager").GetComponent<PauseController>().enabled = false;	
			//GameObject.Find("SkillChoiceManager").GetComponent<SkillChoiceController>().enabled = false; // disable gameobject for bug workaround

		}
		else{
            isVisible = false;
			Time.timeScale = 1;
			gameObject.GetComponent<MouseLook>().enabled = true;
	        transform.parent.GetComponent<MouseLook>().enabled = true;
            GameObject.Find("PauseManager").GetComponent<PauseController>().enabled = true;
			//GameObject.Find("SkillChoiceManager").GetComponent<SkillChoiceController>().enabled = true;
			
		}

        GameState.Instance.IsGuiOpen = isVisible;
	
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
		
		GUILayout.Label("Welcome to Cerebral Academy. You have been chosen among thousands of others to be part of this school due to the inherent potential you possess. This marks the beginning of your initiation process. In order for the school to accept you as a student, you must prove that you have a good understanding of your psychic abilities. In the upcoming days you will be tested on your abilities to concentrate, relax, and use your mind to overcome obstacles. First thing's first: Please fill out this form to begin the initiation.");
		
        GUILayout.Space(buttonSpacing);
		if(GUILayout.Button("Continue"))
		{
			this.GetComponent<ProfileCreation>().enabled = true;
			this.enabled = false;
		}//end button
        GUILayout.EndArea();
	
	}
}
