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

	var chest1b = GameObject.Find("Treasure room/Chest1b");
	RendererAndColliderOff(chest1b);
	var chest2b = GameObject.Find("Treasure room/Chest2b");
	RendererAndColliderOff(chest2b);
	var push = GameObject.Find("Treasure room/Push");
	RendererAndColliderOff(push);
	var pull = GameObject.Find("Treasure room/Pull");
	RendererAndColliderOff(pull);

}
function OnTriggerEnter(otherObj : Collider) {

	if(otherObj.gameObject.tag == "Player") {
	
		var chest1 = GameObject.Find("Treasure room/Chest1");
		RendererAndColliderOff(chest1);
		var chest2 = GameObject.Find("Treasure room/Chest2");
		RendererAndColliderOff(chest2);
		
		var fence1 = GameObject.Find("Treasure room/Fence(Left)");
		RendererAndColliderOff(fence1);
			fence1 = GameObject.Find("Treasure room/Fence(Left)/Fence0");
			RendererAndColliderOff(fence1);
			fence1 = GameObject.Find("Treasure room/Fence(Left)/Fence1");
			RendererAndColliderOff(fence1); 
		var fence2 = GameObject.Find("Treasure room/Fence(Right)");
		RendererAndColliderOff(fence2);
			fence2 = GameObject.Find("Treasure room/Fence(Right)/Fence0");
			RendererAndColliderOff(fence2);
			fence2 = GameObject.Find("Treasure room/Fence(Right)/Fence1");
			RendererAndColliderOff(fence2); 
			
		var chest1b = GameObject.Find("Treasure room/Chest1b");
		RendererAndColliderOn(chest1b);
		var chest2b = GameObject.Find("Treasure room/Chest2b");
		RendererAndColliderOn(chest2b);
		
		var push = GameObject.Find("Treasure room/Push");
		RendererAndColliderOn(push);
		var pull = GameObject.Find("Treasure room/Pull");
		RendererAndColliderOn(pull);
	
		
	}
}