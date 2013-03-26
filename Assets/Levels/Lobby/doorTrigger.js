#pragma strict
var level : int;

function OnTriggerEnter(col : Collider) {
	if(col.gameObject.tag == "Player") {
		if(level == 1)
		{
			DoorOpener.toTrainingGrounds=true;
		}
	}
}

function OnTriggerExit(col : Collider) {
	if(col.gameObject.tag == "Player") {
	if(level == 1 )
	{
		DoorOpener.toTrainingGrounds=false;
	}
}
}