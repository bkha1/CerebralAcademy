using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {

    // Stores <GUI NAME, GameObject> pairs for all objects already opened. This is for caching due to 
    // Find() being so expensive.
    private Hashtable guiScreens = new Hashtable();


	// Use this for initialization
	void Start () {

        NotificationCenter.GUICenter.AddObserver(this, "OpenGUI");
        NotificationCenter.GUICenter.AddObserver(this, "CloseGUI");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OpenGUI(Notification notification)
    {
        string guiName = notification.data["guiName"] as string;
        Debug.Log("Request to open GUI: " + guiName);

        if (guiScreens.Contains(guiName))
        {
            GameObject screen = guiScreens[guiScreens] as GameObject;
            screen.SetActive(true);
        }
        else
        {
            GameObject screen = GameObject.Find(guiName);
            if (screen == null) Debug.LogException(new UnityException("Cannot find GameObject: " + guiName));

            guiScreens.Add(guiName, screen);
            screen.SetActive(true);
        }
        
    }

    void CloseGUI(Notification notification)
    {
        string guiName = notification.data["guiName"] as string;
        Debug.Log("Request to close GUI: " + guiName);

        if (guiScreens.Contains(guiName))
        {
            GameObject screen = guiScreens[guiScreens] as GameObject;
            screen.SetActive(false);
        }
        else
        {
            GameObject screen = GameObject.Find(guiName);
            if (screen == null) Debug.LogException(new UnityException("Cannot find GameObject: " + guiName));

            guiScreens.Add(guiName, screen);
            screen.SetActive(false);
        }

    }
}
