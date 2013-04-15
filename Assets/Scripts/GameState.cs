using UnityEngine;
using System.Collections;

public class GameState {
	
	private static volatile GameState instance;
	private static object _lock = new object();
	
	private GameObject selectedObject;
	private Player currentPlayer;
    public bool DebugMode { get; set; }

    static GameState() {}
    private GameState() { DebugMode = false; }
	
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
