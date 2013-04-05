using UnityEngine;
using System.Collections;

public class Crosshair : MonoBehaviour {

    public GUITexture texture = null;
    public bool lockCursor = false;

    private bool isEnabled = true;

	// Use this for initialization
	void Start () {
        if (texture == null) Debug.LogError("GUITexture missing. Please assign a GUITexture in the Editor.");

        NotificationCenter.DefaultCenter.AddObserver(this, "OnCrosshairOn");
        NotificationCenter.DefaultCenter.AddObserver(this, "OnCrosshairOff");

        Screen.lockCursor = lockCursor;
        Screen.showCursor = !lockCursor;
	}
	
	// Update is called once per frame
	void Update () {

        if (!isEnabled) return;

        Screen.lockCursor = false;
        Screen.lockCursor = lockCursor;
	}

    void OnCrosshairOn(Notification notification)
    {
        isEnabled = true;
        Screen.lockCursor = lockCursor;
        Screen.showCursor = false;
    }

    void OnCrosshairOff(Notification notification)
    {
        isEnabled = false;
        Screen.lockCursor = false;
        Screen.showCursor = true;
    }
}
