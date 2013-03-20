using UnityEngine;
using System.Collections;
using System;

public class ManaSystem : MonoBehaviour {

    public GUITexture ManabarTexture;

    public float liftScaleFactor = 1.0f;
    public float ManaRefillRate = 1.0f;

    private float currentMana = 10.0f;
    //private object _lock = new object(); // This lock is for currentMana to ensure no concurrent modifications are made
    private float currentTime;
    private float endTime;

	// Use this for initialization
	void Start () {
	    // Register to CognitivEvents
        CognitvEventManager.LiftEvent += handle_liftEvent;
        NotificationCenter.DefaultCenter.AddObserver(this, "OnCognitivLiftEvent");
        currentTime = Time.time;
        endTime = currentTime + 1.0f;
	}
	
	// Update is called once per frame
	void Update () {

        currentTime += Time.deltaTime;
        if (currentMana < 100.0f && currentTime >= endTime)
        {
            // Add 1 mana (default rate)
            currentMana += ManaRefillRate;
            // Reset clock
            currentTime = Time.time;
            endTime = currentTime + 1.0f;
            Debug.Log("Current Mana is " + currentMana);
        }

        Color tempColor = ManabarTexture.color;
        if (currentMana < 45.0f)
        {
            tempColor.r = 1.0f;
        }
        else if (currentMana >= 80.0f)
        {
            tempColor.r = 0.0f;
            tempColor.b = 1.0f;
        }

        ManabarTexture.color = tempColor;
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
                StartCoroutine(decrementMana(amount, 1.0f));
            }
        }
    }

    void handle_liftEvent(object sender, float powerLevel)
    {
        GameObject gObj = GameState.Instance.getSelectedObject();

        if (gObj != null)
        {
            if (powerLevel > 0)
            {
                float amount = gObj.GetComponent<CognitivObject>().liftSensitivity / powerLevel * liftScaleFactor;
                // subtract current mana by amount.
                StartCoroutine(decrementMana(amount, 1.0f));
            }
        }
    }


    IEnumerator decrementMana(float amount, float overTime)
    {
        Debug.Log("Current Mana is " + currentMana + " and will loose " + amount);
        float startMana = currentMana;
        float endMana = currentMana + amount;

        float startTime = Time.time;
        while (Time.time < startTime + overTime)
        {
            currentMana = Mathf.Lerp(startMana, endMana, (Time.time - startTime) * amount);
            yield return null;
        }
    }
}
