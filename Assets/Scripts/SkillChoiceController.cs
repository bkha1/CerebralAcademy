using UnityEngine;
using System.Collections;

public class SkillChoiceController : MonoBehaviour {

    private bool gamePaused = false;

    private GameObject player;
    private GameObject playerCamera;

	void Start() 
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera");
        NotificationCenter.DefaultCenter.AddObserver(this, "SkillChoiceEvent");
        NotificationCenter.DefaultCenter.AddObserver(this, "DisablePauseSettings");
        NotificationCenter.DefaultCenter.AddObserver(this, "EnablePauseSettings");
    }

    void SkillChoiceEvent(Notification notification) 
    {
        if (gamePaused)
        {
            Time.timeScale = 1;
            Screen.lockCursor = true;
            Screen.showCursor = false;
            gamePaused = false;

            Debug.Log("Game was paused, now unpaused skillchoice");

            // New code which allows PauseController to sit in the Managers Prefab
            player.GetComponentInChildren<SkillChoiceMenu>().enabled = false;
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

            Debug.Log("Game was not paused, now paused skillchoice");

            // New code which allows PauseController to sit in the Managers Prefab
            player.GetComponentInChildren<SkillChoiceMenu>().enabled = true;
            playerCamera.GetComponent<MouseLook>().enabled = false;
            player.GetComponent<MouseLook>().enabled = false;

            NotificationCenter.DefaultCenter.PostNotification(this, "OnCrosshairOff");
        }
    }
	
	//Do these do anything?
	/*
    void DisablePauseSettings(Notification notification)
    {
        player.GetComponentInChildren<SkillChoiceMenu>().enabled = false;
        playerCamera.GetComponent<MouseLook>().enabled = true;
        player.GetComponent<MouseLook>().enabled = true;
    }

    void EnablePauseSettings(Notification notification)
    {
        player.GetComponentInChildren<SkillChoiceMenu>().enabled = true;
        playerCamera.GetComponent<MouseLook>().enabled = false;
        player.GetComponent<MouseLook>().enabled = false;
    }*/
}
