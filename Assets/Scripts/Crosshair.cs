using UnityEngine;
using System.Collections;

public class Crosshair : MonoBehaviour {

    public GUITexture texture = null;
    public bool lockCursor = false;

	// Use this for initialization
	void Start () {
        if (texture == null) Debug.LogError("GUITexture missing. Please assign a GUITexture in the Editor.");

        //NotificationCenter.DefaultCenter.AddObserver(this, "OnPauseEvent");
        Screen.lockCursor = lockCursor;
        Screen.showCursor = !lockCursor;
	}
	
	// Update is called once per frame
	void Update () {

        Screen.lockCursor = false;
        Screen.lockCursor = true;
	}


}
