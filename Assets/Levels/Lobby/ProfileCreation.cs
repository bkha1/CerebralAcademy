using UnityEngine;
using System.Collections;

public class ProfileCreation : MonoBehaviour {

    public float areaHeight = 500;
    public float areaWidth = 500;
    public float buttonSpacing = 25;
    public GUISkin customSkin;
    public GUIStyle layoutStyle;

    private Player player = null;
    private string userName = "";
    private bool isMale = false;
    private bool isFemale = false;

    private bool isVisible = false;

    private bool isError = false;
    private string errorMsg = "";

    private GameObject playerObject;
    private GameObject playerCamera;

	void Start () {

        /*Because the Enroll button is to start fresh, we will always create a new player object.
         * 
         * if (GameState.Instance.getCurrentPlayer() == null)
        {

            player = new Player();
        }
        else
        {
            player = GameState.Instance.getCurrentPlayer();
            userName = player.UserName;
            isMale = (player.Gender == "Male") ? true : false;
            isFemale = !isMale;
        }*/

        playerObject = GameObject.FindGameObjectWithTag("Player");
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera");

        if (GameState.Instance.getCurrentPlayer() == null || !GameState.Instance.getCurrentPlayer().hasLearnedLift)
        {
            NotificationCenter.DefaultCenter.AddObserver(this, "OpenProfileCreationGUI");
            NotificationCenter.DefaultCenter.PostNotification(this, "OpenProfileCreationGUI");
        }
        else
        {
            GameState.Instance.IsGuiOpen = false;
        }
	
	}

    void OpenProfileCreationGUI(Notification notification)
    {
        Debug.Log("Profile Creation GUI is opening...");

        if (isVisible == false)
        {
            isVisible = true;
            GameState.Instance.IsGuiOpen = isVisible;

            GameObject.Find("PauseManager").GetComponent<PauseController>().enabled = false;
            playerCamera.GetComponent<MouseLook>().enabled = false;
            playerObject.GetComponent<MouseLook>().enabled = false;
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
        GUILayout.Label("Cerebral Academy Guest Book");
        GUILayout.Space(buttonSpacing);
        
        #region Name Field
        GUILayout.BeginHorizontal();
        GUILayout.Label("Name:");
        GUILayout.Space(10);
        userName = GUILayout.TextField(userName, 25);
        GUILayout.EndHorizontal();
        #endregion 

        GUILayout.Space(buttonSpacing);

        #region Gender Field
        GUILayout.BeginHorizontal();
        GUILayout.Label("Gender:");
        GUILayout.Space(10);
        isMale = GUILayout.Toggle(isMale, "Male");
        GUILayout.Space(10);
        isFemale = GUILayout.Toggle(isFemale, "Female");
        GUILayout.EndHorizontal();
        #endregion 

        GUILayout.Space(buttonSpacing);

        #region Error Field
        if (isError) GUILayout.Label("Error: " + errorMsg, "Error");
        #endregion

        GUILayout.Space(buttonSpacing);

        if (GUILayout.Button("Finish"))
        {
            if (userName.Length == 0)
            {
                isError = true;
                errorMsg = "Please enter a username with at least one character.";
            }
            else
            {
                isError = false;
                player = GameState.Instance.getCurrentPlayer();
                player.UserName = userName;
                player.Gender = (isMale ? "Male" : "Female");

                EmotivHandler.Instance.LoadProfile(ref player, userName);
                GameState.Instance.setCurrentPlayer(player);

                NotificationCenter.DefaultCenter.PostNotification(this, "PlayerCreatedEvent");
                NotificationCenter.DefaultCenter.PostNotification(this, "OpenAfterProfileCreationMsg");

                this.enabled = false;
            }
        }
        GUILayout.EndArea();
    }

    
}
