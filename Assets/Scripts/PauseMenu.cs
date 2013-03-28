using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

    public float areaHeight = 500;
    public float areaWidth = 500;
    public string mainMenu = "Main Menu";
    public float buttonSpacing = 25;
    public GUISkin customSkin;
    public GUIStyle layoutStyle;
    public Texture2D textureTop;

    void OnGUI()
    {
        areaHeight = Screen.height;
        //areaWidth = Screen.width;
        GUI.skin = customSkin;
        GUILayout.BeginArea(new Rect(Screen.width / 2 - areaWidth / 2, Screen.height / 2 - areaHeight / 2, areaWidth, areaHeight), layoutStyle);
        GUILayout.Space(50);
        GUILayout.Label(textureTop);
        GUILayout.Space(buttonSpacing);
        if (GUILayout.Button("Resume Game"))
        {
            Time.timeScale = 1;

            NotificationCenter.DefaultCenter.PostNotification(this, "OnPauseEvent");
            enabled = false;
            gameObject.GetComponent<MouseLook>().enabled = true;
            transform.parent.GetComponent<MouseLook>().enabled = true;
        }
        GUILayout.Space(buttonSpacing);
        if (GUILayout.Button("Statistics"))
        {
        }
        GUILayout.Space(buttonSpacing);
		GUILayout.Label("Volume");
        AudioListener.volume = GUILayout.HorizontalSlider(AudioListener.volume, 0, 1);
        GUILayout.Space(buttonSpacing);
        if (GUILayout.Button("Main Menu"))
        {
            Time.timeScale = 1;
			
            Hashtable param = new Hashtable();
            param.Add("gameObject", this.gameObject);
            param.Add("target", new Vector3(0.0f, 0.0f, 0.0f));
            param.Add("isLevel", true);
            param.Add("level", mainMenu);
            NotificationCenter.DefaultCenter.PostNotification(this, "TeleportPlayerEvent", param);
            
			//Application.LoadLevel ("Main Menu");
        }

        GUILayout.Space(buttonSpacing);
        if (GUILayout.Button("Quit To Desktop"))
        {
            Application.Quit();
        }
        GUILayout.Space(buttonSpacing);
        GUILayout.EndArea();
    }//end OnGUI
}
