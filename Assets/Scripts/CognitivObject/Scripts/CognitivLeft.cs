using UnityEngine;
using Emotiv;
using System.Collections;

public class CognitivLeft : MonoBehaviour {
	
	
	// Use this for initialization
	void Start () {
        CognitvEventManager.LeftEvent += handle_leftEvent;
	}
	
	// Update is called once per frame
	void Update () {
	}

    void handle_leftEvent(object sender, float powerLevel)
    {
        GameObject gObj = GameState.Instance.getSelectedObject();

        if (gObj != null)
        {
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
