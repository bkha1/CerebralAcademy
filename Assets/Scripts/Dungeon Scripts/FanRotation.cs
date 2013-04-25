using UnityEngine;
using System.Collections;

public class FanRotation : MonoBehaviour {
	
	public float rotation = -500.0f;
	
	// Update is called once per frame
	void Update () {		
		transform.Rotate(new Vector3(0, 0, rotation * Time.deltaTime));	
	}
}
