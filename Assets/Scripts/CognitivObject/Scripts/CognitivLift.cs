using UnityEngine;
using System.Collections;


public class CognitivLift : MonoBehaviour {

    public float sensitivityReduction = 0.1f;

    private bool updating = false;
    private float movementAmount = 0.0f;

    private float endTime;
    private GameObject gObj;
	
	private bool yLock = false;
	private RigidbodyConstraints freezeStatus;
	private int freezeStatus2; // 2 types becasue the input exists in binary and enum states

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
		yLock = false;
		
		freezeStatus = gObj.rigidbody.constraints;
				
		if (freezeStatus == RigidbodyConstraints.FreezePositionY || freezeStatus == RigidbodyConstraints.FreezePosition ||
			freezeStatus == RigidbodyConstraints.FreezeAll)
		{
			yLock = true;
		}
		else if (freezeStatus == RigidbodyConstraints.FreezePositionX || freezeStatus == RigidbodyConstraints.FreezePositionZ ||
				freezeStatus == RigidbodyConstraints.FreezeRotationX || freezeStatus == RigidbodyConstraints.FreezeRotationY ||
				freezeStatus == RigidbodyConstraints.FreezeRotationZ || freezeStatus == RigidbodyConstraints.FreezeRotation ||
				freezeStatus == RigidbodyConstraints.None)
		{
			//do nothing	
		}
		else
		{
			freezeStatus2 = int.Parse(""+gObj.rigidbody.constraints);
			if( freezeStatus2 == 4 ||
				freezeStatus2 == 2 + 4 ||
				freezeStatus2 == 4 + 8 ||
				
				freezeStatus2 == 4 + 16 ||
				freezeStatus2 == 2 + 4 + 16 ||
				freezeStatus2 == 4 + 8 + 16 ||
				
				freezeStatus2 == 4 + 32 ||
				freezeStatus2 == 2 + 4 + 32 ||
				freezeStatus2 == 4 + 8 + 32 ||
				
				freezeStatus2 == 4 + 16 + 32 ||
				freezeStatus2 == 2 + 4 + 16 + 32 ||
				freezeStatus2 == 4 + 8 + 16 + 32 ||
				
				freezeStatus2 == 4 + 64 ||
				freezeStatus2 == 2 + 4 + 64 ||
				freezeStatus2 == 4 + 8 + 64 ||
				
				freezeStatus2 == 4 + 16 + 64 ||
				freezeStatus2 == 2 + 4 + 16 + 64 ||
				freezeStatus2 == 4 + 8 + 16 + 64 ||
				
				freezeStatus2 == 4 + 32 + 64 ||
				freezeStatus2 == 2 + 4 + 32 + 64 ||
				freezeStatus2 == 4 + 8 + 32 + 64 ||
				
				freezeStatus2 == 4 + 16 + 32 + 64 ||
				freezeStatus2 == 2 + 4 + 16 + 32 + 64 ||
				freezeStatus2 == 4 + 8 + 16 + 32 + 64)
			{
				yLock = true;
			}
		}
		
			
        if (gObj != null && !yLock)
        {
            float powerLevel = (float)liftNotification.data["power"];

            if (powerLevel > 0.0f)
            {
                float amount = (gObj.GetComponent<CognitivObject>().liftSensitivity * powerLevel) * sensitivityReduction;
                movementAmount = amount;
                updating = true;
                endTime = Time.time + 1.0f;
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
