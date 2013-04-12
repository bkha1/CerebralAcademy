using UnityEngine;
using System.Collections;

public class PromptGUI : MonoBehaviour {

    private bool showWindow = false;
    private int promptWindow = 0;
    private int cogAbilityWindow = 1;
    private int trainingWindow = 2;
    private int relaxWindow = 3;

    private int activeWindow = 0;

    private int left = 0;
    private int top = 0;
    private int width = 80;
    private int height = 80;
    
    public int xOffset = 10;
    public int yOffset = 10;

    public string promptText = "What would you like to do?";


    private int navChoice = 0;

	// Use this for initialization
	void Start () {
        showWindow = true; // DEBUG

        width = Screen.width - 20;
        height = Screen.height / 6;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp("1"))
        {
            navChoice = 1;
        }
        else if (Input.GetKeyUp("2"))
        {
            navChoice = 2;
        }
        else if (Input.GetKeyUp("3"))
        {
            navChoice = 3;
        }
        else if (Input.GetKeyUp("4"))
        {
            navChoice = 4; // Close Menu
        }
        else
        {
            navChoice = 0;
        }
	}

    void OnGUI()
    {
        if (!showWindow) return;

        if (navChoice == 0)
        {
            GUI.Window(promptWindow, new Rect(left + xOffset, top - yOffset, width - xOffset, height), initWindow, "Prompt Window");
        }
        else if (navChoice == 1) // GUI for teaching about cog abilities and mana
        {
            activeWindow = 1;
        }
        else if (navChoice == 2) // GUI for training skills
        {
            activeWindow = 2;
        }
        else if (navChoice == 3) // GUI for teaching about relaxation techniques
        {
            activeWindow = 3;
        }
        else if (navChoice == 4) // close GUI and teleport player back to Lobby
        {
            activeWindow = 4;
            EventFactory.FireTeleportPlayerEvent(this, null, new Vector3(), true, "Lobby");
        }
    }

    void initWindow(int windowID)
    {
        if (windowID == promptWindow)
        {
            GUI.Label(new Rect(left + xOffset, top - yOffset, width - xOffset, 10), promptText);

            GUI.BeginGroup(new Rect(left + xOffset, height - 40, width - (xOffset*2), height/4));
            if(GUI.Button(new Rect(15, 20, 10, 10), "Learn about Cognitive Skills and Mana."))
            {
                Debug.Log("User want's to learn about skills and mana.");
            }

            /*if(GUI.Button(new Rect(30, 20, 10, 10), "Learn about Cognitive Skills and Mana."))
            {
                Debug.Log("User want's to learn about skills and mana.");
            }

            if(GUI.Button(new Rect(45, 20, 10, 10), "Learn about Cognitive Skills and Mana."))
            {
                Debug.Log("User want's to learn about skills and mana.");
            }*/

            GUI.EndGroup();
        }
    }
}
