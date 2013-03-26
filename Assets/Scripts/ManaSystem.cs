using UnityEngine;
using System.Collections;
using System;

public class ManaSystem : MonoBehaviour {

    public GUITexture ManabarTexture;
    public float ManabarGUIWidth = 1.4f;

    public float ManaRefillRate = 1.0f;
    public float minMana = 10.0f;
    public float maxMana = 100.0f;


    private float currentMana = 10.0f;
    private float currentTime;
    private float endTime;

	void Start () {
	    // Register to CognitivEvents (to make the manasystem the owner of events, we should have 
        // CognitivSkillEvent passed to here and have this post (if there is enough mana) the proper 
        // skill events.
        NotificationCenter.DefaultCenter.AddObserver(this, "OnCognitivLiftEvent");
        NotificationCenter.DefaultCenter.AddObserver(this, "OnCognitivLeftEvent");
        NotificationCenter.DefaultCenter.AddObserver(this, "OnCognitivRightEvent");
        NotificationCenter.DefaultCenter.AddObserver(this, "OnCognitivPushEvent");
        NotificationCenter.DefaultCenter.AddObserver(this, "OnCognitivDisappearEvent");

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

    void OnCognitivLiftEvent(Notification liftNotification)
    {
        GameObject gObj = GameState.Instance.getSelectedObject();

        if (gObj != null)
        {
            float powerLevel = (float)liftNotification.data["power"];
            if (powerLevel > 0)
            {
                updateMana(gObj.GetComponent<CognitivObject>().liftSensitivity, powerLevel, false);
            }
        }
    }


    void OnCognitivLeftEvent(Notification notification)
    {
        GameObject gObj = GameState.Instance.getSelectedObject();

        if (gObj != null)
        {
            float powerLevel = (float)notification.data["power"];
            if (powerLevel > 0)
            {
                updateMana(gObj.GetComponent<CognitivObject>().leftSensitivity, powerLevel, false);
            }
        }
    }

    void OnCognitivRightEvent(Notification notification)
    {
        GameObject gObj = GameState.Instance.getSelectedObject();

        if (gObj != null)
        {
            float powerLevel = (float)notification.data["power"];
            if (powerLevel > 0)
            {
                updateMana(gObj.GetComponent<CognitivObject>().rightSensitivity, powerLevel, false);
            }
        }
    }

    void OnCognitivPushEvent(Notification notification)
    {
        GameObject gObj = GameState.Instance.getSelectedObject();

        if (gObj != null)
        {
            float powerLevel = (float)notification.data["power"];
            if (powerLevel > 0)
            {
                updateMana(gObj.GetComponent<CognitivObject>().pushSensitivity, powerLevel, false);
            }
        }
    }

    void OnCognitivDisappearEvent(Notification notification)
    {
        GameObject gObj = GameState.Instance.getSelectedObject();

        if (gObj != null)
        {
            float powerLevel = (float)notification.data["power"];
            if (powerLevel > 0)
            {
                updateMana(gObj.GetComponent<CognitivObject>().disappearSensitivity, powerLevel, false);
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


   
    void updateMana(float objSensitivity, float powerLevel, bool recharge)
    {
        if (!recharge && (currentMana - (powerLevel / objSensitivity) < minMana))
        {
            Debug.Log("Not enough mana to perform skill.");
            return;
        }

        float endMana;
        if (recharge) endMana = (currentMana + powerLevel) / objSensitivity;
        else endMana = (currentMana - powerLevel) / objSensitivity;
        
        if (endMana < minMana)
        {
            endMana = minMana;
        }
        currentMana = endMana;
    }
}
