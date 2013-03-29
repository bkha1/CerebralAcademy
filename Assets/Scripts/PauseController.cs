using UnityEngine;
using System.Collections;

public class PauseController : MonoBehaviour {

    private bool gamePaused = false;

	void Start() 
    {
        NotificationCenter.DefaultCenter.AddObserver(this, "OnPauseEvent");
    }

    void OnPauseEvent(Notification notification) 
    {
        if (gamePaused)
        {
            Time.timeScale = 1;
            gamePaused = false;
            gameObject.GetComponent<PauseMenu>().enabled = false;
            gameObject.GetComponent<MouseLook>().enabled = true;
            transform.parent.GetComponent<MouseLook>().enabled = true;
            
        }
        else
        {
            Time.timeScale = 0;
            gamePaused = true;
            gameObject.GetComponent<PauseMenu>().enabled = true;
            gameObject.GetComponent<MouseLook>().enabled = false;
            transform.parent.GetComponent<MouseLook>().enabled = false;
        }
    }
}
