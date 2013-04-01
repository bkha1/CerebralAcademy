using UnityEngine;
using System.Collections;

public class ThirdPersonPhysics : MonoBehaviour {
	
	private float weight;
	private float gravity = 15;
	private float yPositionOld;
	private float yPositionNew;
	
	void Start(){
		weight = this.gameObject.rigidbody.mass;
		yPositionOld = this.gameObject.rigidbody.position.y;
	}	
	
	void OnControllerColliderHit(ControllerColliderHit col){
        Rigidbody body = col.rigidbody;
        if (body == null || body.isKinematic)
		{
            return;
		}
		else{
			body.AddForceAtPosition(-Vector3.up * weight, col.point,ForceMode.Force);
		}
	}
	
	
}
