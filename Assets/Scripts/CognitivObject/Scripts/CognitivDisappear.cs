using UnityEngine;
using Emotiv;
using System.Collections;

public class CognitivDisappear : MonoBehaviour {
	
	private bool disappear = false;
    private float disappearAmt = 0.0f;

    void Start() {
        CognitvEventManager.DisappearEvent += handle_disappearEvent;
    }

	void Update () {	
	}
	
	void modulateAlpha(GameObject gObj, float amount) {
		Color invisiColor = gObj.transform.renderer.material.color;
		
		// Use selectedObject's disappearSensitivity
        float valueToBeLerped = amount * gObj.GetComponent<CognitivObject>().disappearSensitivity;
		
		float decrementAmt = Mathf.Lerp(1.0f, 0.0f, valueToBeLerped);
        
        if (decrementAmt == 0) return;

		invisiColor.a -= decrementAmt * Time.deltaTime;
		gObj.transform.renderer.material.color = invisiColor;
	}

    void handle_disappearEvent(object sender, float powerLevel) {
        disappearAmt = powerLevel;

        if (disappear) return;

        GameObject gObj = GameState.Instance.getSelectedObject();

        if (gObj == null) return;

        if (!disappear)
        {
            modulateAlpha(gObj, disappearAmt);
            //currentTime = Time.time + 1.0f;
        }
        /*else if (Input.GetKeyUp(debugKey)) {
            modulateAlpha(gObj, incomingPower);
        }*/

        // Problem: If i just disable, obj will fall through world. 
        if (gObj.transform.renderer.material.color.a <= 0.0f)
        {
            disappear = true;
            gObj.transform.collider.enabled = false;

            // Start a 1 sec time until alpha is faded back to original state
            //currentTime = Time.time + 1.0f;
        }
        else
        {
            disappear = false;
            gObj.transform.collider.enabled = true;

        }



    }
}
