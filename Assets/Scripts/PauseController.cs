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
		if(GameState.Instance.IsSkillMenuOpen) return;//checks to see if skill menu is opened
		
        if (gamePaused)
        {
            gamePaused = false;
            player.GetComponentInChildren<PauseMenu>().enabled = false;
            DisablePauseSettings(null);
        }
        else
        {
            gamePaused = true;
            player.GetComponentInChildren<PauseMenu>().enabled = true;
            EnablePauseSettings(null);
        }

        GameState.Instance.IsGuiOpen = gamePaused;
        GameState.Instance.IsPaused = gamePaused;
    }

    void DisablePauseSettings(Notification notification)
    {
        Time.timeScale = 1;
        Screen.lockCursor = true;
        Screen.showCursor = false;

        playerCamera.GetComponent<MouseLook>().enabled = true;
        player.GetComponent<MouseLook>().enabled = true;

        NotificationCenter.DefaultCenter.PostNotification(this, "OnCrosshairOn");
    }

    void EnablePauseSettings(Notification notification)
    {
        Time.timeScale = 0;
        Screen.lockCursor = false;
        Screen.showCursor = true;

        playerCamera.GetComponent<MouseLook>().enabled = false;
        player.GetComponent<MouseLook>().enabled = false;

        NotificationCenter.DefaultCenter.PostNotification(this, "OnCrosshairOff");
    }
}
