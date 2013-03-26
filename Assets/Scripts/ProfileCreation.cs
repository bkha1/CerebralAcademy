using UnityEngine;
using System.Collections;

public class ProfileCreation : MonoBehaviour {

    public float areaHeight = 500;
    public float areaWidth = 500;
    public float buttonSpacing = 25;
    public GUISkin customSkin;
    public GUIStyle layoutStyle;
    public Texture2D textureTop;

    private Player player = null;
    private string userName = "";
    private bool isMale = false;
    private bool isFemale = false;

    private bool isVisible = false;

	// Use this for initialization
	void Start () {

        if (GameState.Instance.getCurrentPlayer() == null) {

            player = new Player();
            //this.GetComponent<MouseLook>().enabled = false;
            //this.transform.parent.GetComponent<MouseLook>().enabled = false;
            //gameObject.GetComponent<PauseController>().enabled = false;//so user cant pause during profile creation
         }

        NotificationCenter.DefaultCenter.AddObserver(this, "OpenProfileCreationGUI");
	
	}

    void OpenProfileCreationGUI(Notification notification)
    {
        Debug.Log("Profile Creation GUI is opening...");
        //enabled = true;
        if (isVisible == false)
        {
            isVisible = true;


            this.GetComponent<MouseLook>().enabled = false;
            this.transform.parent.GetComponent<MouseLook>().enabled = false;
            gameObject.GetComponent<PauseController>().enabled = false;
        }
    }

    void OnGUI()
    {
        if (!isVisible) return;

        Time.timeScale = 0;//this has to be here or else mana bar will fill for some reason

        areaHeight = Screen.height;

        GUI.skin = customSkin;
        GUILayout.BeginArea(new Rect(Screen.width / 2 - areaWidth / 2, Screen.height / 2 - areaHeight / 2, areaWidth, areaHeight), layoutStyle);
        GUILayout.Space(50);
        //GUILayout.Label(textureTop);
        GUILayout.Label("Cerebral Academy Guest Book");
        GUILayout.Space(buttonSpacing);
        GUILayout.BeginHorizontal();
        GUILayout.Label("Name:");
        GUILayout.Space(10);
        userName = GUILayout.TextField(userName, 25);
        GUILayout.EndHorizontal();
        GUILayout.Space(buttonSpacing);
        GUILayout.BeginHorizontal();
        GUILayout.Label("Gender:");
        GUILayout.Space(10);
        isMale = GUILayout.Toggle(isMale, "Male");
        GUILayout.Space(10);
        isFemale = GUILayout.Toggle(isFemale, "Female");
        GUILayout.Space(buttonSpacing);
        GUILayout.EndHorizontal();
        if (GUILayout.Button("Finish"))
        {
            player.UserName = userName;
            player.Gender = (isMale ? "Male" : "Female");
            GameState.Instance.setCurrentPlayer(player);
            Time.timeScale = 1;

            this.GetComponent<MouseLook>().enabled = true;
            this.transform.parent.GetComponent<MouseLook>().enabled = true;
            gameObject.GetComponent<PauseController>().enabled = true;
            this.enabled = false;
        }
        GUILayout.EndArea();
    }
}
