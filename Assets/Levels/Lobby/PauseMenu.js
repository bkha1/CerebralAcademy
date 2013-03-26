var areaHeight : float = 500;
var areaWidth : float = 500;
//var layoutCenter : float = 100;
//var firstLevel : String = "First";
var mainMenu : String = "Main Menu";
var buttonSpacing : float = 25;
var customSkin : GUISkin;
var layoutStyle : GUIStyle;
var textureTop : Texture2D;

function OnGUI()
{
	areaHeight = Screen.height;
	//areaWidth = Screen.width;
	GUI.skin = customSkin;
	GUILayout.BeginArea(Rect(Screen.width/2 - areaWidth/2, Screen.height/2 - areaHeight/2, areaWidth, areaHeight), layoutStyle);
	GUILayout.Space(50);
	GUILayout.Label(textureTop);
	GUILayout.Space(buttonSpacing);
	if(GUILayout.Button("Resume Game"))
	{
		Time.timeScale = 1;
		
		gameObject.GetComponent(PauseController).setPaused(false);
		//PauseController.gamePaused = false;
		enabled = false;
		gameObject.GetComponent(MouseLook).enabled = true;
		transform.parent.GetComponent(MouseLook).enabled = true;
	}
	GUILayout.Space(buttonSpacing);
	if(GUILayout.Button("Statistics"))
	{
	}
	GUILayout.Space(buttonSpacing);
	AudioListener.volume = GUILayout.HorizontalSlider(AudioListener.volume,0.0,1.0);
	GUILayout.Space(buttonSpacing);
	if(GUILayout.Button("Main Menu"))
	{
		//Application.LoadLevel(firstLevel);
		Time.timeScale = 1;
		Application.LoadLevel(mainMenu);
	}
	GUILayout.Space(buttonSpacing);
	if(GUILayout.Button("Quit To Desktop"))
	{
		Application.Quit();
	}
	GUILayout.Space(buttonSpacing);
	GUILayout.EndArea();
}//end OnGUI