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

            //gameObject.GetComponent<PauseMenu>().enabled = false;
            //gameObject.GetComponent<MouseLook>().enabled = true;
            //transform.parent.GetComponent<MouseLook>().enabled = true;

            // New code which allows PauseController to sit in the Managers Prefab
            player.GetComponentInChildren<PauseMenu>().enabled = false;
            playerCamera.GetComponent<MouseLook>().enabled = true;
            player.GetComponent<MouseLook>().enabled = true;
        }
        else
        {
            Time.timeScale = 0;
            Screen.lockCursor = false;
            Screen.showCursor = true;
            gamePaused = true;

            //gameObject.GetComponent<PauseMenu>().enabled = true;
            //gameObject.GetComponent<MouseLook>().enabled = false;
            //transform.parent.GetComponent<MouseLook>().enabled = false;

            // New code which allows PauseController to sit in the Managers Prefab
            player.GetComponentInChildren<PauseMenu>().enabled = true;
            playerCamera.GetComponent<MouseLook>().enabled = false;
            player.GetComponent<MouseLook>().enabled = false;
        }
    }

    void DisablePauseSettings(Notification notification)
    {
        player.GetComponentInChildren<PauseMenu>().enabled = false;
        playerCamera.GetComponent<MouseLook>().enabled = true;
        player.GetComponent<MouseLook>().enabled = true;
    }

    void EnablePauseSettings(Notification notification)
    {
        player.GetComponentInChildren<PauseMenu>().enabled = true;
        playerCamera.GetComponent<MouseLook>().enabled = false;
        player.GetComponent<MouseLook>().enabled = false;
    }
}
