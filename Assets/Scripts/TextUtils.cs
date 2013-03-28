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
    public int areaHeight = 60; // 40 supports 2 lines; 80 looks good for paragraph.
    public int yOffset = 40;
    public int xOffset = 40;

    private bool isVisible = false;

    private string text = "Debug Text";
    private float expireTime;

    private int NUM_PIXELS_PER_CHAR;
    private float prevHeight;

	// Use this for initialization
	void Start () {
        NotificationCenter.DefaultCenter.AddObserver(this, "DisplayText");

        NUM_PIXELS_PER_CHAR = areaWidth / 47;
        Debug.Log("Num of pixels per Char: " + NUM_PIXELS_PER_CHAR);

        prevHeight = areaHeight;

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

        // Handle scaling the width/height depending upon number of chars
        // Assume a char is 6 pixels wide

        int numOfCharHigh = areaHeight / NUM_PIXELS_PER_CHAR;
        Debug.Log("Num of Char High: " + numOfCharHigh);

        int heightPadding = text.Length / numOfCharHigh;

        Debug.Log("Height Padding: " + heightPadding);
        if (areaHeight < prevHeight + heightPadding)
        {
            areaHeight += heightPadding;
        }
        


        //layoutStyle.normal.background.height = layoutStyle.normal.background.height + (int)(1.4f / numOfCharHigh);

        expireTime = Time.time + (float)notification.data["duration"];
        isVisible = true;
    }

    void OnGUI()
    {
        if (!isVisible) return;

        GUI.skin = customSkin;
        //GUILayout.BeginArea(new Rect(Screen.width / 2 - areaWidth / 2, 40, areaWidth, areaHeight), layoutStyle);
        GUILayout.BeginArea(new Rect(Screen.width / 2 - areaWidth / 2, yOffset, areaWidth, areaHeight), layoutStyle);
        
        GUILayout.Label(text);

        GUILayout.EndArea();
    }
}
