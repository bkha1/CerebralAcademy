using UnityEngine;
using System.Collections;

public class SkillChoiceMenu : MonoBehaviour {

    public float areaHeight = 500;
    public float areaWidth = 500;
    public string mainMenu = "Main Menu";
    public float buttonSpacing = 25;
    public GUISkin customSkin;
    public GUIStyle layoutStyle;
    public Texture2D textureTop;
	
	private bool showList = false;
	private int listEntry = 0;
	private bool picked = false;
	public ComboBox myCombo = new ComboBox();
	public GUIContent[] glist;

    void Start()
    {
		glist = new GUIContent[5];

		glist[0] = new GUIContent("Foo");
		glist[1] = new GUIContent("Bar");
		glist[2] = new GUIContent("Thing1");
		glist[3] = new GUIContent("Thing2");
		glist[4] = new GUIContent("Thing3");
		
		layoutStyle = new GUIStyle();
		layoutStyle.normal.textColor = Color.white;
		Texture2D tex = new Texture2D(2,2);
		Color[] colors = new Color[4];
		for(int i = 0; i < 4; i++)
		{
			colors[i] = Color.white;
		}
		tex.SetPixels(colors);
		tex.Apply();
		layoutStyle.hover.background = tex;
		layoutStyle.onHover.background = tex;
		layoutStyle.padding.left = layoutStyle.padding.right = layoutStyle.padding.top = layoutStyle.padding.bottom = 4;
		
    }

    void OnGUI()
    {
		myCombo.List(new Rect(50, 100, 100, 20), new GUIContent("Click me!"), glist, layoutStyle);
		GUI.Label(new Rect(50, 70, 400, 20), "You picked " + myCombo.GetSelectedItemIndex().ToString() + "!");
		//if(myCombo.List(new Rect(50, 100, 100, 20), showList, listEntry, new GUIContent("Click me!"), glist, layoutStyle))
		//{
		//	picked = true;
		//}
		//if (picked) {
		//GUI.Label (new Rect(50, 70, 400, 20), "You picked " + glist[listEntry].text + "!");
	//}
		//myCombo.List(new Rect(0,0,Screen.width/2,Screen.height/2), new GUIContent("string"), glist,layoutStyle);
		//myCombo.List(new Rect(0,0,Screen.width/2,Screen.height/2), new GUIContent("string"), new GUIContent[] {new GUIContent("fdsaf"),new GUIContent("gds")},layoutStyle);
        areaHeight = Screen.height;
        GUI.skin = customSkin;
        GUILayout.BeginArea(new Rect(Screen.width / 2 - areaWidth / 2, Screen.height / 2 - areaHeight / 2, areaWidth, areaHeight), layoutStyle);
        GUILayout.Space(50);
        GUILayout.Label(textureTop);
        GUILayout.Space(buttonSpacing);
		
		GUILayout.Space(buttonSpacing);
        if (GUILayout.Button("Okay"))
        {
            EventFactory.FireOnSkillChoiceEvent(this);
        }
        GUILayout.Space(buttonSpacing);

        GUILayout.EndArea();
    }//end OnGUI
}
