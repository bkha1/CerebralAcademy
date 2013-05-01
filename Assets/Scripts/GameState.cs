using UnityEngine;
using System.Collections;

public class GameState {
	
	private static volatile GameState instance;
	private static object _lock = new object();
	
	private GameObject selectedObject;
	private Player currentPlayer;
    public bool DebugMode { get; set; }
    public bool IsPaused { get; set; }
    public bool IsGuiOpen { get; set; }
	public bool IsSkillMenuOpen { get; set; }
	public KeyCode SkillKey { get; set; }
	
    static GameState() {}
    private GameState() { DebugMode = false; SkillKey = KeyCode.Q; }
	
	public static GameState Instance
	{
		get
		{
			if (instance == null) {
				
				lock (_lock) {
					if (instance == null) {
						instance = new GameState();
					}
				}
			}
			
			return instance;
		}
	}
	
	public GameObject getSelectedObject() {
		return selectedObject;
	}
	
	public void setSelectedObject(GameObject selected) {
		selectedObject = selected;
	}
	
	public Player getCurrentPlayer() {
		return currentPlayer;
	}
	
	public void setCurrentPlayer(Player player) {
		currentPlayer = player;
	}
	
	
	
}
