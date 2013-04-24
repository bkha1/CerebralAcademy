using UnityEngine;
using System.Collections;
using System;

public class ManaSystem : MonoBehaviour {

    public GUITexture ManabarTexture = null;
    public float ManabarGUIWidth = 1.4f;

    public float ManaRefillRate = 10.0f;
    public float minMana = 10.0f;
    public float maxMana = 100.0f;
	public float cognitivSkillCost = 0.5f;


    private float currentMana = 10.0f;
    private float currentTime;
    private float endTime;
    private ManaSystem instance = null;

    private Player player = null;

	void Start () {


        if (ManabarTexture == null) Debug.LogError("Please assign a GUITexture for the mana bar in the Editor.");

        if (instance == null)
        {
            instance = GameObject.FindObjectOfType(typeof(ManaSystem)) as ManaSystem;

            if (instance == null)
            {
                GameObject obj = new GameObject("ManaManager");
                instance = obj.AddComponent<ManaSystem>();
                DontDestroyOnLoad(obj);
            }
        }
        
        

	    // Register to CognitivEvents (to make the manasystem the owner of events, we should have 
        // CognitivSkillEvent passed to here and have this post (if there is enough mana) the proper 
        // skill events.
        NotificationCenter.DefaultCenter.AddObserver(this, "OnCognitivEvent");
        NotificationCenter.DefaultCenter.AddObserver(this, "OnEmotionEvent");

        // Set currentMana to be whatever player has
        player = GameState.Instance.getCurrentPlayer();

        if (player != null)
            currentMana = player.Mana <= maxMana ? player.Mana : maxMana;
        else
        {
            currentMana = minMana;
        }

        // Set ManabarTexture to correct size
        Rect textureRect = ManabarTexture.pixelInset;
        textureRect.xMax = ManabarTexture.pixelInset.xMin + ManabarGUIWidth * currentMana;
        ManabarTexture.pixelInset = textureRect;
        Debug.Log("ManaSystem: textureRect.xMax = " + textureRect.xMax + " with Mana: " + currentMana);

        currentTime = Time.time;
        endTime = currentTime + 1.0f;
	}
	
	void Update () {
        
        currentTime += Time.deltaTime;

        #region TESTING
        // NOTE: This is just for starting the game. We can instead enable the mana system after profile creation.
        //if (player == null && GameState.Instance.DebugMode) player = new Player();
        //if (player == null) return;
        #endregion

        if (player.Mana < maxMana && currentTime >= endTime)
        {
            // Add ManaRefillRate mana
            if (player.Mana < maxMana)
            {
                // Add 1 mana (default rate)
                if (GameState.Instance.DebugMode)
                    player.Mana += ManaRefillRate;

                if (player.Mana > maxMana) player.Mana = maxMana;

                Rect textureRect = ManabarTexture.pixelInset;
                textureRect.xMax = ManabarTexture.pixelInset.xMin + ManabarGUIWidth * player.Mana;
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
                
                switch((CognitivSkill)notification.data["skill"]) 
                {
                    case (CognitivSkill.LIFT): 
                    {
                        if ((!player.hasLearnedLift && player.CurrentSkillEquipped == CognitivSkill.LIFT) || !GameState.Instance.DebugMode) EventFactory.FireDisplayTextEvent(this, "You first must learn lift.", 2.0f);
                        if (updateMana(cognitivSkillCost, powerLevel, false))
                        {
                            NotificationCenter.DefaultCenter.PostNotification(this, "OnCognitivLiftEvent", notification.data);
                        }
                        break;
                    }
                    case (CognitivSkill.DISAPPEAR):
                    {
                        if (updateMana(cognitivSkillCost, powerLevel, false))
                        {
                            NotificationCenter.DefaultCenter.PostNotification(this, "OnCognitivDisappearEvent", notification.data);
                        }
                        break;
                    }
                    case (CognitivSkill.RIGHT):
                    {
                        if (updateMana(cognitivSkillCost, powerLevel, false))
                        {
                            NotificationCenter.DefaultCenter.PostNotification(this, "OnCognitivRightEvent", notification.data);
                        }
                        break;
                    }
                    case (CognitivSkill.LEFT):
                    {
                        if (updateMana(cognitivSkillCost, powerLevel, false))
                        {
                            NotificationCenter.DefaultCenter.PostNotification(this, "OnCognitivLeftEvent", notification.data);
                        }
                        break;
                    }
                    case (CognitivSkill.PUSH):
                    {
                        if ((!player.hasLearnedPush && player.CurrentSkillEquipped == CognitivSkill.PUSH) || !GameState.Instance.DebugMode) EventFactory.FireDisplayTextEvent(this, "You first must learn push.", 2.0f);
                        if (updateMana(cognitivSkillCost, powerLevel, false))
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
        // NOTE: This code currently is very raw and instantly updates the mana. A better solution would 
        // be to use a 5 second rolling average and add that to the user's mana instead.
        GameObject gObj = GameState.Instance.getSelectedObject();


        if (gObj != null)
        {
            float powerLevel = (float)notification.data["power"];
            //Debug.Log("ManaSystem: OnEmotionEvent: " + powerLevel);
            if (powerLevel > 0)
            {
				//Debug.Log ("Mana updated via emotion event!");
                player.Mana += powerLevel * 2.5f;
            }
        }
    }


   
    bool updateMana(float objSensitivity, float powerLevel, bool recharge)
    {
        if (!recharge && (player.Mana - (powerLevel / objSensitivity) < minMana))
        {
            Debug.Log("Not enough mana to perform skill.");
            EventFactory.FireDisplayTextEvent(this, "Not enough mana to perform skill.", 0.5f);
            return false;
        }

        float endMana;
        if (recharge) endMana = (player.Mana + powerLevel) / objSensitivity;
        else endMana = (player.Mana - powerLevel) / objSensitivity;
        
        if (endMana < minMana)
        {
            endMana = minMana;
        }
        player.Mana = endMana;

        return true;
    }
}
