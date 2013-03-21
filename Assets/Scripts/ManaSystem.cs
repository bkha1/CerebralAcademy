using UnityEngine;
using System.Collections;
using System;

public class ManaSystem : MonoBehaviour {

    public GUITexture ManabarTexture;
    public float ManabarGUIWidth = 1.4f;

    public float liftScaleFactor = 1.0f;
    public float ManaRefillRate = 1.0f;

    public float minMana = 10.0f;
    public float maxMana = 100.0f;

    private float currentMana = 10.0f;
    private float currentTime;
    private float endTime;

	// Use this for initialization
	void Start () {
	    // Register to CognitivEvents
        NotificationCenter.DefaultCenter.AddObserver(this, "OnCognitivLiftEvent");


        // Set ManabarTexture to correct size
        Rect textureRect = ManabarTexture.pixelInset;
        textureRect.xMax = ManabarTexture.pixelInset.xMin + ManabarGUIWidth * currentMana;
        ManabarTexture.pixelInset = textureRect;

        currentTime = Time.time;
        endTime = currentTime + 1.0f;
	}
	
	// Update is called once per frame
	void Update () {

        currentTime += Time.deltaTime;
        if (currentMana < maxMana && currentTime >= endTime)
        {
            // Add 1 mana (default rate)
            currentMana += ManaRefillRate;
            
            // Reset clock
            currentTime = Time.time;
            endTime = currentTime + 1.0f;
            Debug.Log("Current Mana is " + currentMana);
        }


        Rect textureRect = ManabarTexture.pixelInset;
        textureRect.xMax = ManabarTexture.pixelInset.xMin + ManabarGUIWidth * currentMana;
        ManabarTexture.pixelInset = textureRect;

	}

    void OnCognitivLiftEvent(Notification liftNotification)
    {
        Debug.Log("NotificationCenter notified ManaSystem of lift event.");

        GameObject gObj = GameState.Instance.getSelectedObject();

        if (gObj != null)
        {
            float powerLevel = (float)liftNotification.data["power"];
            if (powerLevel > 0)
            {
                float amount = gObj.GetComponent<CognitivObject>().liftSensitivity / powerLevel * liftScaleFactor;
                // subtract current mana by amount.

                amount = 10.0f;
                float endMana = currentMana - amount;
                if (endMana < minMana)
                {
                    endMana = minMana;
                }
                currentMana = endMana;
            }
        }
    }

    // This is not the best method to do it with a GUIText object. 
    /*IEnumerator decrementMana(float amount, float overTime)
    {
        Debug.Log("Current Mana is " + currentMana + " and will loose " + amount);
        float startMana = currentMana;
        float endMana = currentMana - amount;
        if (endMana < minMana)
        {
            endMana = minMana;
        }

        float startTime = Time.time;
        while (Time.time < startTime + overTime)
        {
            currentMana = Mathf.Lerp(startMana, endMana, (Time.time - startTime) * amount);
            yield return null;
        }

        currentMana = endMana;
    }*/
}
