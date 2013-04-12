using UnityEngine;
using System.Collections;

public class PromptGUI : MonoBehaviour {

    //private bool showWindow = false;
    private const int PROMPT_WIND = 0;
    private const int COG_ABILITY_WIND = 1;
    private const int TRAINING_WIND = 2;
    private const int RELAX_WIND = 3;
    private const int INVISIBLE_WIND = -1; // This stands for showing no window.

    private int activeWindow = 0;

    private int left = 0;
    private int top = 0;
    public int width = 500;
    public int height = 150;
    
    public int xOffset = 10;
    public int yOffset = 10;

    public string promptText = "What would you like to do?";
    //public string cogAbilityText = "What would you like to do?";
    private int navChoice = 0;



	// Use this for initialization
	void Start () {

        NotificationCenter.DefaultCenter.AddObserver(this, "CogTutorFinished");
	}
	
	// Update is called once per frame
	void Update () {

        if (activeWindow == PROMPT_WIND)
        {
            if (Input.GetKeyUp("1"))
            {
                //navChoice = 1;
                //activeWindow = COG_ABILITY_WIND;
                activeWindow = INVISIBLE_WIND;
                Debug.Log("User want's to learn about skills and mana.");
                NotificationCenter.DefaultCenter.PostNotification(this, "StartCogTutor");
            }
            else if (Input.GetKeyUp("2"))
            {
                //navChoice = 2;
                activeWindow = TRAINING_WIND;

            }
            else if (Input.GetKeyUp("3"))
            {
                //navChoice = 3;
                activeWindow = RELAX_WIND;
            }
            else if (Input.GetKeyUp("4"))
            {
                //navChoice = 4; // Close Menu
                activeWindow = PROMPT_WIND;
                EventFactory.FireTeleportPlayerEvent(this, null, new Vector3(), true, "Lobby");
            }
            else
            {
                //navChoice = 0;
                activeWindow = PROMPT_WIND;
            }
        }
        else if (activeWindow == COG_ABILITY_WIND)
        {
            //navChoice = INVISIBLE_WIND;
        }
        else if (activeWindow == TRAINING_WIND)
        {

        }
        else if (activeWindow == RELAX_WIND)
        {

        }
	}

    void OnGUI()
    {
        if ((activeWindow != INVISIBLE_WIND))
        {
            GUI.Window(activeWindow, new Rect(Screen.width / 2 - width / 2, 40, width, height), initWindow, "");
        }
    }

    void initWindow(int windowID)
    {
        GUILayout.BeginArea(new Rect(xOffset, yOffset, width, height));

        if (windowID == PROMPT_WIND)
        {
            GUILayout.Label(promptText);
            GUILayout.Space(1);

            GUILayout.Label("1. Learn about Cognitive Skills and Mana.");

            GUILayout.Label("2. I want to practice using my Cognitive Skills a bit.");

            GUILayout.Label("3. Learn about Relaxation Techniques.");

            GUILayout.Label("4. I would like to go back to the Lobby.");

        }
        else if (windowID == COG_ABILITY_WIND)
        {
            //GUILayout.Label(cogAbilityText);
            //GUILayout.Space(1);
        }
        else if (windowID == TRAINING_WIND)
        {

        }
        else if (windowID == RELAX_WIND)
        {

        }

        GUILayout.EndArea();
    }

    void CogTutorFinished(Notification notification)
    {
        activeWindow = PROMPT_WIND;
        navChoice = 0;
    }
}
