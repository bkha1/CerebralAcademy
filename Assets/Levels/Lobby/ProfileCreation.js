var areaHeight : float = 500;
var areaWidth : float = 500;
var buttonSpacing : float = 25;
var customSkin : GUISkin;
var layoutStyle : GUIStyle;
var textureTop : Texture2D;

var userName : String = "";
var isMale : boolean = false;
var isFemale : boolean = false;

function Start ()//pause game and enable profile gui
{
	
	gameObject.GetComponent(MouseLook).enabled = false;
	transform.parent.GetComponent(MouseLook).enabled = false;
	gameObject.GetComponent(PauseController).enabled = false;//so user cant pause during profile creation
	
	
	//enabled = true;

}

function OnGUI()
{ 
	Time.timeScale = 0;//this has to be here or else mana bar will fill for some reason

	areaHeight = Screen.height;
	
	GUI.skin = customSkin;
	GUILayout.BeginArea(Rect(Screen.width/2 - areaWidth/2, Screen.height/2 - areaHeight/2, areaWidth, areaHeight), layoutStyle);
	GUILayout.Space(50);
	//GUILayout.Label(textureTop);
	GUILayout.Label("Cerebral Academy Guest Book");
	GUILayout.Space(buttonSpacing);
	GUILayout.BeginHorizontal();
	GUILayout.Label("Name:");
	GUILayout.Space(10);
	userName = GUILayout.TextField(userName,25);
	GUILayout.EndHorizontal();
	GUILayout.Space(buttonSpacing);
	GUILayout.BeginHorizontal();
	GUILayout.Label("Gender:");
	GUILayout.Space(10);
	isMale = GUILayout.Toggle(isMale, "Male");
	GUILayout.Space(10);
	isFemale = GUILayout.Toggle(isFemale, "Female");
	GUILayout.Space(buttonSpacing);
	GUILayout.EndHorizontal();
	if(GUILayout.Button("Finish"))
	{
		Time.timeScale = 1;
		
		gameObject.GetComponent(MouseLook).enabled = true;
		transform.parent.GetComponent(MouseLook).enabled = true;
		gameObject.GetComponent(PauseController).enabled = true;
		enabled = false;
	}
	GUILayout.EndArea();
}