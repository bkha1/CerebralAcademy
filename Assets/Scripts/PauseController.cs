using UnityEngine;
using System.Collections;

public class PauseController : MonoBehaviour {

    private bool gamePaused = false;

    private GameObject player;
    private GameObject playerCamera;

	void Start() 
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera");
        NotificationCenter.DefaultCenter.AddObserver(this, "OnPauseEvent");
        NotificationCenter.DefaultCenter.AddObserver(this, "DisablePauseSettings");
        NotificationCenter.DefaultCenter.AddObserver(this, "EnablePauseSettings");
    }

    void OnPauseEvent(Notification notification) 
    {
        if (gamePaused)
        {
            Time.timeScale = 1;
            Screen.lockCursor = true;
            Screen.showCursor = false;
            gamePaused = false;

            Debug.Log("Game was paused, now unpaused");

            // New code which allows PauseController to sit in the Managers Prefab
            player.GetComponentInChildren<PauseMenu>().enabled = false;
            playerCamera.GetComponent<MouseLook>().enabled = true;
            player.GetComponent<MouseLook>().enabled = true;

            NotificationCenter.DefaultCenter.PostNotification(this, "OnCrosshairOn");
        }
        else
        {
            Time.timeScale = 0;
            Screen.lockCursor = false;
            Screen.showCursor = true;
            gamePaused = true;

            Debug.Log("Game was not paused, now paused");

            // New code which allows PauseController to sit in the Managers Prefab
            player.GetComponentInChildren<PauseMenu>().enabled = true;
            playerCamera.GetComponent<MouseLook>().enabled = false;
            player.GetComponent<MouseLook>().enabled = false;

            NotificationCenter.DefaultCenter.PostNotification(this, "OnCrosshairOff");
        }
    }
	
	//Do these two functions do anything?
    void DisablePauseSettings(Notification notification)
    {
		Time.timeScale = 1;
        Screen.lockCursor = true;
        Screen.showCursor = false;
		
        //player.GetComponentInChildren<PauseMenu>().enabled = false;
        playerCamera.GetComponent<MouseLook>().enabled = true;
        player.GetComponent<MouseLook>().enabled = true;
    }

    void EnablePauseSettings(Notification notification)
    {
		Time.timeScale = 0;
        Screen.lockCursor = false;
        Screen.showCursor = true;
		
        //player.GetComponentInChildren<PauseMenu>().enabled = true;
        playerCamera.GetComponent<MouseLook>().enabled = false;
        player.GetComponent<MouseLook>().enabled = false;
    }
    
}
