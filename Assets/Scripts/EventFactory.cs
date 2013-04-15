using UnityEngine;
using System.Collections;


static class EventFactory
{
    public static void FireDisplayTextEvent(Component comp, string text, float duration)
    {
        Hashtable param = new Hashtable();
        param.Add("text", text);
        param.Add("duration", duration);
        NotificationCenter.DefaultCenter.PostNotification(comp, "DisplayText", param);
    }

    public static void FireOnCognitvEvent(Component comp, CognitivSkill skill, float power)
    {
        Hashtable param = new Hashtable();
        param.Add("skill", skill);
        param.Add("power", power);
        NotificationCenter.DefaultCenter.PostNotification(comp, "OnCognitivEvent", param);
    }

    public static void FireSelectionEvent(Component comp, GameObject gameObject)
    {
        Hashtable param = new Hashtable();
        param.Add("gameObject", gameObject);
        NotificationCenter.DefaultCenter.PostNotification(comp, "SelectionEvent", param);
    }

    public static void FireTeleportPlayerEvent(Component comp, GameObject gameObject, Vector3 target, bool loadLevel, string levelID)
    {
        Hashtable param = new Hashtable();
        param.Add("gameObject", gameObject);
        param.Add("target", target);
        param.Add("isLevel", loadLevel);
        param.Add("level", levelID);
        NotificationCenter.DefaultCenter.PostNotification(comp, "TeleportPlayerEvent", param);
    }

    public static void FireOnEmotionEvent(Component comp, string emotion, float powerLevel, float time)
    {
        Hashtable param = new Hashtable();
        param.Add("emotion", emotion);
        param.Add("power", powerLevel);
        param.Add("time", time);
        NotificationCenter.DefaultCenter.PostNotification(comp, "OnEmotionEvent", param);
    }

    public static void FireOnCrosshairOnEvent(Component comp)
    {
        NotificationCenter.DefaultCenter.PostNotification(comp, "OnCrosshairOn");
    }

    public static void FireOnCrosshairOffEvent(Component comp)
    {
        NotificationCenter.DefaultCenter.PostNotification(comp, "OnCrosshairOff");
    }

    public static void FireOnPauseEvent(Component comp)
    {
        NotificationCenter.DefaultCenter.PostNotification(comp, "OnPauseEvent");
    }
	
	public static void FireOnSkillChoiceEvent(Component comp)
	{
		NotificationCenter.DefaultCenter.PostNotification(comp, "SkillChoiceEvent");
	}
}

