using UnityEngine;
using System.Collections;

public class drownScript : MonoBehaviour {

	
	void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.transform.position = new Vector3(5.0F,5.05F,15.0F);		
			EventFactory.FireDisplayTextEvent(this, "This water is too deep to wade and you haven't learned to swim yet", 1.0f);
        }
    }
}
