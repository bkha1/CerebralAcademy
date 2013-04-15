using UnityEngine;
using System.Collections;

/**
 * The Brain is the loader of the Managers and all required components per level. This will be run 
 * immidiately after NotificationCenter.
 */
public class TheBrain : MonoBehaviour {

    public GameObject managersPrefab = null;
    public GameObject manaPrefab = null;

    private GameObject instance = null;
    private GameObject manaGameObject = null;

	void Awake () {

        if (managersPrefab == null) Debug.LogError("Managers Prefab has not been set. Please attach in Editor Pane.");
        if (manaPrefab == null) Debug.LogError("Mana Prefab has not been set. Please attach in Editor Pane.");

        if (instance == null)
        {
            instance = GameObject.Find("Managers");

            if (instance == null)
            {
                instance = Instantiate(managersPrefab, new Vector3(), Quaternion.identity) as GameObject;
                instance.name = "Managers";

                manaGameObject = GameObject.Find("Mana");
                if (manaGameObject == null)
                {
                    manaGameObject = Instantiate(manaPrefab, new Vector3(), Quaternion.identity) as GameObject;
                    manaGameObject.name = "Mana";
                }

                ManaSystem ms = GameObject.FindObjectOfType(typeof(ManaSystem)) as ManaSystem;
                ms.ManabarTexture = manaGameObject.GetComponentInChildren<GUITexture>();
            }
        }

        DontDestroyOnLoad(instance);
        DontDestroyOnLoad(manaGameObject);
        DontDestroyOnLoad(this);
	    
	}
	
	
}
