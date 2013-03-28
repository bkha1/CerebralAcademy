using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(Halo))]
public class Selector : MonoBehaviour {
	
	public Material highlightMaterial;
    public bool highlightEffect = false;
    
	private RaycastHit hit = new RaycastHit();
	private Ray lookAtRay = new Ray();
	private GameObject prevSelected;
	private Material prevMaterial;
	private Material[] prevMaterials;
    private GameObject defaultObject;


    void Start()
    {
        defaultObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        defaultObject.AddComponent<Rigidbody>();
        CognitivObject cogObjScript = defaultObject.AddComponent<CognitivObject>();
        cogObjScript.liftSensitivity = 0.0f;
        cogObjScript.disappearSensitivity = 0.0f;
        cogObjScript.leftSensitivity = 0.0f;
        cogObjScript.rightSensitivity = 0.0f;
        cogObjScript.pushSensitivity = 0.0f;
        defaultObject.tag = "CognitivObject";
        defaultObject.transform.position = new Vector3(1000.0f, 1000.0f, 1000.0f);
        defaultObject.name = "Default Selector Object";

    }
	
	// Update is called once per frame
	void Update () {
		
		lookAtRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		
		if (Physics.Raycast(lookAtRay, out hit, 100.0f ) && Input.GetMouseButtonDown(0)) {
			// if hit is a CognitivObject
            if (hit.collider.gameObject.tag == "CognitivObject") {
				GameObject selectedObject = hit.collider.gameObject;
				
				Hashtable param = new Hashtable();
            	param.Add("gameObject", selectedObject);
				NotificationCenter.DefaultCenter.PostNotification(this, "SelectionEvent", param);

                swapSelectedObjects(selectedObject);

			} else {
                swapSelectedObjects(defaultObject);
			}
		}
	}

    private void swapSelectedObjects(GameObject selectedObject)
    {
        if (prevSelected == null)
        { // On first run
            GameState.Instance.setSelectedObject(selectedObject);
            prevSelected = selectedObject;
        }
        else if (prevSelected != selectedObject)
        {
            GameState.Instance.setSelectedObject(selectedObject);
            if (highlightEffect)
            {
                prevSelected.renderer.materials = prevMaterials;
            }
            prevSelected = selectedObject;
        }

        if (highlightEffect)
        {
            prevMaterials = selectedObject.renderer.materials;

            Material[] selectedMaterials = new Material[selectedObject.renderer.materials.Length + 1];
            selectedObject.renderer.materials.CopyTo(selectedMaterials, 0);
            selectedMaterials[selectedMaterials.Length - 1] = highlightMaterial;
            selectedObject.renderer.materials = selectedMaterials;
        }
    }
}
