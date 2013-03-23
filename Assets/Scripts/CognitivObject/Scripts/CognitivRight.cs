using UnityEngine;
using Emotiv;
using System.Collections;

public class CognitivRight : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
        NotificationCenter.DefaultCenter.AddObserver(this, "OnCognitivRightEvent");
	}

    void OnCognitivRightEvent(Notification notification)
    {
        GameObject gObj = GameState.Instance.getSelectedObject();

        if (gObj != null)
        {
            float powerLevel = (float)notification.data["power"];

            if (powerLevel > 0.0f)
            {
                float amount = gObj.GetComponent<CognitivObject>().rightSensitivity * powerLevel; // this moves by x units
                translateRight(amount);
                //StartCoroutine(translateRight(gObj, amount, 1.0f)); // coroutines move by 1 unit
            }
        }
    }

    IEnumerator translateRight(GameObject gObj, float amount, float overTime)
    {
        Vector3 source = gObj.transform.position;
        Vector3 target = source + Vector3.right;
        Vector3 transform;

        float startTime = Time.time;
        while (Time.time < startTime + overTime)
        {
            transform = Vector3.Lerp(source, target, (Time.time - startTime) * amount);
            gObj.transform.Translate(transform, Camera.main.transform);
            //gObj.transform.position = Vector3.Lerp(source, target, (Time.time - startTime) * amount);
            yield return null;
        }

        gObj.transform.position = target;
    }
	
	private void translateRight(float amount) {
		GameObject gObj = GameState.Instance.getSelectedObject();
				
		if (gObj != null) {
			gObj.transform.Translate(Vector3.right * amount, Camera.main.transform);
		}
	}
}
