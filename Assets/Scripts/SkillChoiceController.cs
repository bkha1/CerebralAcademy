using UnityEngine;
using System.Collections;

public class SkillChoiceController : MonoBehaviour {

    private bool handleEvents = true;
    private bool menuOpen = false;

	void Start() 
    {
        NotificationCenter.DefaultCenter.AddObserver(this, "SkillChoiceEvent");
    }

    void SkillChoiceEvent(Notification notification) 
    {
        Debug.Log("SkillChoiceEvent handled!");

        if (GameState.Instance.IsGuiOpen) return;

        if (menuOpen)
        {
            NotificationCenter.DefaultCenter.PostNotification(this, "DisablePauseSettings");
            NotificationCenter.DefaultCenter.PostNotification(this, "ToggleSkillChoiceMenuVisibility");
			
			GameState.Instance.IsSkillMenuOpen = false;//IsGuiOpen = gamePaused;
            menuOpen = false;
        }
        else
        {
            NotificationCenter.DefaultCenter.PostNotification(this, "EnablePauseSettings");
            NotificationCenter.DefaultCenter.PostNotification(this, "ToggleSkillChoiceMenuVisibility");
			
			GameState.Instance.IsSkillMenuOpen = true;//IsGuiOpen = gamePaused;
            menuOpen = true;
        }
    }
}
