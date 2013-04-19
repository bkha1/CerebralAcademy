using UnityEngine;
using System.Collections;

public class jumpHelper : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (this.rigidbody.position.y > 1.5){
			this.rigidbody.constraints = RigidbodyConstraints.FreezeAll;
		}	
	}
}
