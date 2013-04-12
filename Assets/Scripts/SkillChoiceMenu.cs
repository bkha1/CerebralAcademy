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
	public ComboBox abilityCombo = new ComboBox();
	public GUIContent[] abilityList;
	
	public ComboBox techCombo = new ComboBox();
	public GUIContent[] techList;

    void Start()
    {
		abilityList = new GUIContent[5];

		abilityList[0] = new GUIContent("Lift");
		abilityList[1] = new GUIContent("Push");
		abilityList[2] = new GUIContent("Pull");
		abilityList[3] = new GUIContent("Rotate");
		abilityList[4] = new GUIContent("ARMAGEDDON");
		
		techList = new GUIContent[5];
		techList[0] = new GUIContent("Meditate");
		techList[1] = new GUIContent("MediTate");
		techList[2] = new GUIContent("meditate");
		techList[3] = new GUIContent("MEDITATE");
		techList[4] = new GUIContent("MEDS");
		
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
		abilityCombo.List(new Rect(50, 100, 100, 20), new GUIContent("Abilities"), abilityList, layoutStyle);
		GUI.Label(new Rect(50, 70, 400, 20), "You picked " + abilityCombo.GetSelectedItemIndex().ToString() + "!");
		
		techCombo.List(new Rect(200, 100, 100, 20), new GUIContent("Techniques"), techList, layoutStyle);
		GUI.Label(new Rect(200, 70, 400, 20), "You picked " + techCombo.GetSelectedItemIndex().ToString() + "!");
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
		if (GUILayout.Button("Okay"))
        {
            EventFactory.FireOnSkillChoiceEvent(this);
        }
		
        GUI.skin = customSkin;
        /*GUILayout.BeginArea(new Rect(Screen.width / 2 - areaWidth / 2, Screen.height / 2 - areaHeight / 2, areaWidth, areaHeight), layoutStyle);
        GUILayout.Space(50);
        GUILayout.Label(textureTop);
        GUILayout.Space(buttonSpacing);
		
		GUILayout.Space(buttonSpacing);
        if (GUILayout.Button("Okay"))
        {
            EventFactory.FireOnSkillChoiceEvent(this);
        }
        GUILayout.Space(buttonSpacing);

        GUILayout.EndArea();*/
    }//end OnGUI
}
