#pragma strict
#pragma downcast

protected function RendererAndColliderOn(gameObject : GameObject) {
	if (gameObject.renderer) {
		gameObject.renderer.enabled = true;
	}
 
	if (gameObject.collider) {
		gameObject.collider.enabled = true;
	}
}	

protected function RendererAndColliderOff(gameObject : GameObject) {
	if (gameObject.renderer) {
		gameObject.renderer.enabled = false;
	}
 
	if (gameObject.collider) {
		gameObject.collider.enabled = false;
	}
}

function Start(){

	var chest = GameObject.Find("ChestB");
	RendererAndColliderOff(chest);
	var orb = GameObject.Find("Rotate");
	RendererAndColliderOff(orb);

}
function OnTriggerEnter(otherObj : Collider) {

	if(otherObj.gameObject.tag == "Player") {
	
		var chest = GameObject.Find("Chest");
		RendererAndColliderOff(chest);
			
		var chest2 = GameObject.Find("ChestB");
		RendererAndColliderOn(chest2);
		
		var orb = GameObject.Find("Rotate");
		RendererAndColliderOn(orb);		
	}
}