using UnityEngine;
using System.Collections;


public class CognitivLift : MonoBehaviour {

    public float sensitivityReduction = 0.1f;

    private bool updating = false;
    private float movementAmount = 0.0f;

    private float endTime;
    private GameObject gObj;

	void Start() {
        NotificationCenter.DefaultCenter.AddObserver(this, "OnCognitivLiftEvent");
	}
	
	void Update () {

        if (updating)
        {
            if (Time.time >= endTime)
            {
                updating = false;
            }
            else
            {
                Vector3 source = gObj.transform.position;
                Vector3 target = source + Vector3.up;
                gObj.transform.position = Vector3.Lerp(source, target, Time.deltaTime * movementAmount);
            }
        }
	}

    void OnCognitivLiftEvent(Notification liftNotification)
    {
        gObj = GameState.Instance.getSelectedObject();

        if (gObj != null)
        {
            float powerLevel = (float)liftNotification.data["power"];

            if (powerLevel > 0.0f)
            {
                float amount = (gObj.GetComponent<CognitivObject>().liftSensitivity * powerLevel) * sensitivityReduction;
                movementAmount = amount;
                updating = true;
                endTime = Time.time + 0.5f;
            }
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
