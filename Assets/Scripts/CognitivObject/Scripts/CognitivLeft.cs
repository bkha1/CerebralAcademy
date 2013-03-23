using UnityEngine;
using Emotiv;
using System.Collections;

public class CognitivLeft : MonoBehaviour {
	
	
	// Use this for initialization
	void Start () {
        //CognitvEventManager.LeftEvent += handle_leftEvent;
        NotificationCenter.DefaultCenter.AddObserver(this, "OnCognitivLeftEvent");
	}


    void OnCognitivLeftEvent(Notification notification)
    {
        GameObject gObj = GameState.Instance.getSelectedObject();

        if (gObj != null)
        {
            float powerLevel = (float) notification.data["power"];
            float amount = gObj.GetComponent<CognitivObject>().liftSensitivity * powerLevel;
            StartCoroutine(moveObject(gObj, Vector3.left, amount, 1.0f));
            //gObj.transform.Translate(Vector3.left * amount, Camera.main.transform);

            //Transform t = gObj.transform; t.transform.Translate(Vector3.left * amount, Camera.main.transform);
            //StartCoroutine(moveObject(gObj, t.transform.position, amount, 1.0f));
        }
    }

    IEnumerator moveObject(GameObject gObj, Vector3 target, float amount, float overTime)
    {
        Vector3 source = gObj.transform.position;
       // Vector3 target = source + alongAxis;

        float startTime = Time.time;
        while (Time.time < startTime + overTime)
        {
            gObj.transform.Translate(Vector3.left * ( (Time.time - startTime) * amount ), Camera.main.transform);
            //gObj.transform.position = Vector3.Lerp(source, target, (Time.time - startTime) / overTime);
            yield return null;
        }

        //gObj.transform.position = target;
    }
}
