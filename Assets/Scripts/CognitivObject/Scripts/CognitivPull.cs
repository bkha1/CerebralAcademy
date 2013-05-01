using UnityEngine;
using Emotiv;
using System.Collections;

public class CognitivPull : MonoBehaviour {

	private Ray lookAtRay;
	
	// Use this for initialization
	void Start () {
        NotificationCenter.DefaultCenter.AddObserver(this, "OnCognitivPullEvent");
	}

    void OnCognitivPushEvent(Notification notification)
    {
        GameObject gObj = GameState.Instance.getSelectedObject();

        if (gObj != null)
        {
            float powerLevel = (float)notification.data["power"];
            float amount = gObj.GetComponent<CognitivObject>().pullSensitivity * powerLevel;
            pull(amount);
        }
    }

	
	private void pull(float amount) {
		lookAtRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		
		GameObject gObj = GameState.Instance.getSelectedObject();
		Vector3 direction = lookAtRay.direction;
		direction.x = direction.x * -1.0f;
		direction.z = direction.z * -1.0f;
		
		if (gObj != null && gObj.rigidbody != null) {
			gObj.rigidbody.AddForce(direction * amount, ForceMode.Impulse);
		}
	}
}
