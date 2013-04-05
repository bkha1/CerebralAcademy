using UnityEngine;
using System.Collections;
using System;

public class ManaSystem : MonoBehaviour {

    public GUITexture ManabarTexture;
    public float ManabarGUIWidth = 1.4f;

    public float ManaRefillRate = 10.0f;
    public float minMana = 10.0f;
    public float maxMana = 100.0f;


    private float currentMana = 10.0f;
    private float currentTime;
    private float endTime;

	void Start () {
	    // Register to CognitivEvents (to make the manasystem the owner of events, we should have 
        // CognitivSkillEvent passed to here and have this post (if there is enough mana) the proper 
        // skill events.
        NotificationCenter.DefaultCenter.AddObserver(this, "OnCognitivEvent");
        NotificationCenter.DefaultCenter.AddObserver(this, "OnEmotionEvent");

        // Set ManabarTexture to correct size
        Rect textureRect = ManabarTexture.pixelInset;
        textureRect.xMax = ManabarTexture.pixelInset.xMin + ManabarGUIWidth * currentMana;
        ManabarTexture.pixelInset = textureRect;

        currentTime = Time.time;
        endTime = currentTime + 1.0f;
	}
	
	void Update () {

        currentTime += Time.deltaTime;
        if (currentMana < maxMana && currentTime >= endTime)
        {
            // Add ManaRefillRate mana
            if (currentMana < maxMana)
            {
                // Add 1 mana (default rate)
                currentMana += ManaRefillRate;

                if (currentMana > maxMana) currentMana = maxMana;

                Rect textureRect = ManabarTexture.pixelInset;
                textureRect.xMax = ManabarTexture.pixelInset.xMin + ManabarGUIWidth * currentMana;
                ManabarTexture.pixelInset = textureRect;
            }
            
            // Reset clock
            currentTime = Time.time;
            endTime = currentTime + 1.0f;
        }
	}

     void OnCognitivEvent(Notification notification)
    {
        GameObject gObj = GameState.Instance.getSelectedObject();

        if (gObj != null)
        {
            float powerLevel = (float)notification.data["power"];
            if (powerLevel > 0)
            {
                
                switch((string)notification.data["skill"]) 
                {
                    case ("lift"): 
                    {
                        if (updateMana(gObj.GetComponent<CognitivObject>().liftSensitivity, powerLevel, false))
                        {
                            NotificationCenter.DefaultCenter.PostNotification(this, "OnCognitivLiftEvent", notification.data);
                        }
                        break;
                    }
                    case ("disappear"):
                    {
                        if (updateMana(gObj.GetComponent<CognitivObject>().disappearSensitivity, powerLevel, false))
                        {
                            NotificationCenter.DefaultCenter.PostNotification(this, "OnCognitivDisappearEvent", notification.data);
                        }
                        break;
                    }
                    case ("right"):
                    {
                        if (updateMana(gObj.GetComponent<CognitivObject>().rightSensitivity, powerLevel, false))
                        {
                            NotificationCenter.DefaultCenter.PostNotification(this, "OnCognitivRightEvent", notification.data);
                        }
                        break;
                    }
                    case ("left"):
                    {
                        if (updateMana(gObj.GetComponent<CognitivObject>().leftSensitivity, powerLevel, false))
                        {
                            NotificationCenter.DefaultCenter.PostNotification(this, "OnCognitivLeftEvent", notification.data);
                        }
                        break;
                    }
                    case ("push"):
                    {
                        if (updateMana(gObj.GetComponent<CognitivObject>().liftSensitivity, powerLevel, false))
                        {
                            NotificationCenter.DefaultCenter.PostNotification(this, "OnCognitivPushEvent", notification.data);
                        }
                        break;
                    }
                }
            }
        }
    }
    

    void OnEmotionEvent(Notification notification)
    {
        // TODO: Fill this code out for the actual emotional state of user's brain.
        GameObject gObj = GameState.Instance.getSelectedObject();

        if (gObj != null)
        {
            float powerLevel = (float)notification.data["power"];
            if (powerLevel > 0)
            {
                updateMana(gObj.GetComponent<CognitivObject>().disappearSensitivity, powerLevel, true);
            }
        }
    }


   
    bool updateMana(float objSensitivity, float powerLevel, bool recharge)
    {
        if (!recharge && (currentMana - (powerLevel / objSensitivity) < minMana))
        {
            Debug.Log("Not enough mana to perform skill.");
            EventFactory.FireDisplayTextEvent(this, "Not enough mana to perform skill.", 0.5f);
            return false;
        }

        float endMana;
        if (recharge) endMana = (currentMana + powerLevel) / objSensitivity;
        else endMana = (currentMana - powerLevel) / objSensitivity;
        
        if (endMana < minMana)
        {
            endMana = minMana;
        }
        currentMana = endMana;

        return true;
    }
}
