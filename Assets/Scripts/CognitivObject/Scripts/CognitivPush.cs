using UnityEngine;
using Emotiv;
using System.Collections;

public class CognitivPush : MonoBehaviour {

	private Ray lookAtRay;
	
	// Use this for initialization
	void Start () {
        NotificationCenter.DefaultCenter.AddObserver(this, "OnCognitivPushEvent");
	}

    void OnCognitivPushEvent(Notification notification)
    {
        GameObject gObj = GameState.Instance.getSelectedObject();

        if (gObj != null)
        {
            float powerLevel = (float)notification.data["power"];
            float amount = gObj.GetComponent<CognitivObject>().pushSensitivity * powerLevel;
            //StartCoroutine(pushObject(gObj, amount, 1.0f));
            push(amount);
        }
    }

	
	private void push(float amount) {
		lookAtRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		GameObject gObj = GameState.Instance.getSelectedObject();
		
		if (gObj != null && gObj.rigidbody != null) {
			gObj.rigidbody.AddForce(lookAtRay.direction * amount, ForceMode.Impulse);
		}
	}
}
