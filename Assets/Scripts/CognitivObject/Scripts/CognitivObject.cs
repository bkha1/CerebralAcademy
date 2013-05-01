using UnityEngine;
using System.Collections;

public class CognitivObject : MonoBehaviour {

    public float liftSensitivity = 0.1f;
    public float disappearSensitivity = 0.1f;
    public float leftSensitivity = 0.1f;
    public float rightSensitivity = 0.1f;
    public float pushSensitivity = 0.1f;
	public float pullSensitivity = 0.1f;

	// Use this for initialization
	void Start () {
        transform.tag = "CognitivObject";

        if (liftSensitivity < 0.0f) liftSensitivity = 0.1f;
        if (disappearSensitivity < 0.0f) disappearSensitivity = 0.1f;
        if (leftSensitivity < 0.0f) leftSensitivity = 0.1f;
        if (rightSensitivity < 0.0f) rightSensitivity = 0.1f;
        if (pushSensitivity < 0.0f) pushSensitivity = 0.1f;
		if (pullSensitivity < 0.0f) pullSensitivity = 0.1f;
	}
}
