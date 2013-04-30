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
	
	public bool debugLearn = false;
	public bool learnedLift =false;
	public bool learnedPush =false;
	public bool learnedPull =false;

    private bool isVisible = false;

    void Start()
    {
		abilityList = new GUIContent[6];

		abilityList[0] = new GUIContent("LOCKED");
		abilityList[1] = new GUIContent("LOCKED");
		abilityList[2] = new GUIContent("LOCKED");
		abilityList[3] = new GUIContent("LOCKED");
		abilityList[4] = new GUIContent("LOCKED");
		abilityList[5] = new GUIContent("LOCKED");
		
		techList = new GUIContent[3];
		techList[0] = new GUIContent("Breath");
		techList[1] = new GUIContent("Count");
		techList[2] = new GUIContent("Hum");
		//techList[3] = new GUIContent("MEDITATE");
		//techList[4] = new GUIContent("MEDS");
		
		layoutStyle = new GUIStyle();
		layoutStyle.normal.textColor = Color.white;
		layoutStyle.onHover.background = layoutStyle.hover.background = new Texture2D(2,2);
		layoutStyle.padding.left = layoutStyle.padding.right = layoutStyle.padding.top = layoutStyle.padding.bottom = 4;
		/*
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
		layoutStyle.padding.left = layoutStyle.padding.right = layoutStyle.padding.top = layoutStyle.padding.bottom = 4;*/
		
        // A Notification handler for toggling the menu's visibility
        NotificationCenter.DefaultCenter.AddObserver(this, "ToggleSkillChoiceMenuVisibility");
    }

    void OnGUI()
    {
        if (!isVisible) return;
		
		if(!debugLearn)
		{
			learnedLift = GameState.Instance.getCurrentPlayer().hasLearnedLift;
			learnedPush = GameState.Instance.getCurrentPlayer().hasLearnedPush;
			learnedPull = GameState.Instance.getCurrentPlayer().hasLearnedPull;
		}
		
		if(learnedLift)//GameState.Instance.getCurrentPlayer().hasLearnedLift)
		{
			abilityList[0] = new GUIContent("Lift");
			Debug.Log("Cognitive Skill LIFT Unlocked");
		}
		if(learnedPush)//GameState.Instance.getCurrentPlayer().hasLearnedPush)
		{
			abilityList[1] = new GUIContent("Push");
			Debug.Log("Cognitive Skill PUSH Unlocked");
		}
		if(learnedPull)//GameState.Instance.getCurrentPlayer().hasLearnedPull)
		{
			abilityList[2] = new GUIContent("Pull");
			Debug.Log("Cognitive Skill PULL Unlocked");
		}

		abilityCombo.List(new Rect(50, 100, 100, 20), new GUIContent("Abilities"), abilityList, layoutStyle);
		//checks and sets skills
		switch(abilityCombo.GetSelectedItemIndex())
		{
			case 0:
				if(learnedLift)//GameState.Instance.getCurrentPlayer().hasLearnedLift)
				{
					GameState.Instance.getCurrentPlayer().CurrentSkillEquipped = CognitivSkill.LIFT;
					Debug.Log("Cognitive Skill LIFT Set");
				}
				else
				{
					Debug.Log("Cognitive Skill LIFT LOCKED");
				}
				break;
			case 1:
			if(learnedPush)//GameState.Instance.getCurrentPlayer().hasLearnedPush)
			{
				GameState.Instance.getCurrentPlayer().CurrentSkillEquipped = CognitivSkill.PUSH;
				Debug.Log("Cognitive Skill PUSH Set");
			}
			else
			{
				Debug.Log("Cognitive Skill PUSH LOCKED");
			}
				break;
			case 2:
			if(learnedPull)//GameState.Instance.getCurrentPlayer().hasLearnedPull)
			{
				GameState.Instance.getCurrentPlayer().CurrentSkillEquipped = CognitivSkill.PULL;
				Debug.Log("Cognitive Skill PULL Set");
			}
			else
			{
				Debug.Log("Cognitive Skill Pull LOCKED");
			}
				break;
			// JVM: NOTE: These skills are not supported in the game.
			/*case 3:
				GameState.Instance.getCurrentPlayer().CurrentSkillEquipped = CognitivSkill.DISAPPEAR;
				Debug.Log("Cognitive Skill DISAPPEAR Set");
				break;
			case 4:
				GameState.Instance.getCurrentPlayer().CurrentSkillEquipped = CognitivSkill.LEFT;
				Debug.Log("Cognitive Skill LEFT Set");
				break;
			case 5:
				GameState.Instance.getCurrentPlayer().CurrentSkillEquipped = CognitivSkill.RIGHT;
				Debug.Log("Cognitive Skill PUSH Set");
				break;*/
			default:
				GameState.Instance.getCurrentPlayer().CurrentSkillEquipped = CognitivSkill.LIFT;
				Debug.Log("Cognitive Skill Default LIFT Set");
				break;
		}//end ability switchcase
		GUI.Label(new Rect(50, 70, 400, 20), "You picked " + abilityList[abilityCombo.GetSelectedItemIndex()].text + "!");
		
		techCombo.List(new Rect(200, 100, 100, 20), new GUIContent("Techniques"), techList, layoutStyle);
		//checks and sets techniques
		switch(techCombo.GetSelectedItemIndex())
		{
			case 0:
				GameState.Instance.getCurrentPlayer().CurrentRelaxationEquipped = RelaxationTechnique.BREATHE;
				Debug.Log("Relax Tech BREATHE Set");
				break;
			case 1:
				GameState.Instance.getCurrentPlayer().CurrentRelaxationEquipped = RelaxationTechnique.COUNT;
				Debug.Log("Relax Tech COUNT Set");
				break;
			case 2:
				GameState.Instance.getCurrentPlayer().CurrentRelaxationEquipped = RelaxationTechnique.HUM;
				Debug.Log("Relax Tech HUM Set");
				break;
			default:
				GameState.Instance.getCurrentPlayer().CurrentRelaxationEquipped = RelaxationTechnique.BREATHE;
				Debug.Log("Relax Tech default BREATHE Set");
				break;
		}//end technique switchcase
		GUI.Label(new Rect(200, 70, 400, 20), "You picked " + techList[techCombo.GetSelectedItemIndex()].text +"!");//.ToString() + "!");
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


    void ToggleSkillChoiceMenuVisibility(Notification notification)
    {
        
        if (this.isVisible) this.isVisible = false;
        else this.isVisible = true;
        //this.isVisible = !this.isVisible; // why does this not work?

        Debug.Log("Menu Visibility: " + this.isVisible);
    }
}
