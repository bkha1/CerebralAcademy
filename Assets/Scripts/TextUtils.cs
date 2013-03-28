using UnityEngine;
using System.Collections;

/**
 * This class is a Utility Class which allows a GUIText to be displayed for a 
 * given amount of time.
 */
public class TextUtils : MonoBehaviour {

    public GUISkin customSkin;
    public GUIStyle layoutStyle;

    public int areaWidth = 300;
    public int areaHeight = 120;
    public int yOffset = 40;
    public int xOffset = 40;

    private bool isVisible = false;

    private string text = "Debug Text";
    private float expireTime;

	// Use this for initialization
	void Start () {
        NotificationCenter.DefaultCenter.AddObserver(this, "DisplayText");
	}

    void Update()
    {
        // If a message is on screen, check if timer has run out
        if (isVisible)
        {
            if (Time.time >= expireTime)
            {
                isVisible = false;
            }
        }
    }

    void DisplayText(Notification notification)
    {
        text = notification.data["text"] as string;

        expireTime = Time.time + (float)notification.data["duration"];
        isVisible = true;
    }

    void OnGUI()
    {
        if (!isVisible) return;

        GUI.skin = customSkin;
        //GUILayout.BeginArea(new Rect(Screen.width / 2 - areaWidth / 2, 40, areaWidth, areaHeight), layoutStyle);
        GUILayout.BeginArea(new Rect(Screen.width / 2 - areaWidth / 2, 40, areaWidth, areaHeight), layoutStyle);
        
        GUILayout.Label(text);

        GUILayout.EndArea();
    }
}
