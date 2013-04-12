using UnityEngine;
using System.Collections;

public class KeepMeAlive : MonoBehaviour {

	void Awake () {
        DontDestroyOnLoad(this);
	}

}
