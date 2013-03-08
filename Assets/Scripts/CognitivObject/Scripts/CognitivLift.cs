using UnityEngine;
using Emotiv;
using System.Collections;

public class CognitivLift : MonoBehaviour {
	
	void Start() {
        CognitvEventManager.LiftEvent += handle_liftEvent;
	}
	
	void Update () {
		
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
