using UnityEngine;
using System.Collections;

public class SpeechBubble
{
    public static float SHORT_DURATION = 1.0f;
    public static float LONG_DURATION = 3.0f;

    public Vector3 position;
    public GameObject gameObject = null;
    public float duration; // This is actually time of creation + duration (expiry time)
    public string text;
    public SpeechBubble(Vector3 pos, string txt, float expireDate)
    {
        position = pos;
        text = txt;
        duration = expireDate;

        // Set the GUIText text to our text

    }

    public void setText()
    {
        // This must be called after gameObject is instantiated
        //if (gameObject == null) Debug.Log("game object is still null!");
        GUIText guiText = gameObject.GetComponentInChildren<GUIText>() as GUIText;
        guiText.text = text;
    }
}