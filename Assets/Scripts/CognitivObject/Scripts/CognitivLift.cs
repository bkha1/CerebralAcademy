using UnityEngine;
using Emotiv;
using System.Collections;

public class CognitivLift : MonoBehaviour {
	
	void Start() {
        CognitvEventManager.LiftEvent += handle_liftEvent;
        NotificationCenter.DefaultCenter.AddObserver(this, "OnCognitivLiftEvent");
	}
	
	void Update () {
		
	}

    void OnCognitivLiftEvent(Notification liftNotification)
    {
        Debug.Log("NotificationCenter notified CognitivLift of lift event.");
        
        GameObject gObj = GameState.Instance.getSelectedObject();

        if (gObj != null)
        {
            float powerLevel = (float)liftNotification.data["power"];

            if (powerLevel > 0.0f)
            {
                float amount = gObj.GetComponent<CognitivObject>().liftSensitivity * powerLevel;
                StartCoroutine(liftObject(gObj, amount, 1.0f));
            }
        }
    }
	

    void handle_liftEvent(object sender, float powerLevel)
    {
        GameObject gObj = GameState.Instance.getSelectedObject();

        if (gObj != null)
        {
            float amount = gObj.GetComponent<CognitivObject>().liftSensitivity * powerLevel;
            StartCoroutine(liftObject(gObj, amount, 1.0f)); 
        }
    }

    IEnumerator liftObject(GameObject gObj, float amount, float overTime)
    {
        Vector3 source = gObj.transform.position;
        Vector3 target = source + Vector3.up;

        float startTime = Time.time;
        while (Time.time < startTime + overTime)
        {
            gObj.transform.position = Vector3.Lerp(source, target, (Time.time - startTime) * amount);
            yield return null;
        }

        gObj.transform.position = target;
    }
}
