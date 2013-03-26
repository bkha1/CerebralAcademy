#pragma strict
static var toTrainingGrounds : boolean = false;
static var toTestArea : boolean = false;
var levelToLoad : String;


function Update () {
	if(Input.GetKeyDown(KeyCode.E) && toTrainingGrounds==true)
	{
		Application.LoadLevel(levelToLoad);
	}
	else if(Input.GetKeyDown(KeyCode.E) && toTestArea==true)
	{
		Application.LoadLevel(levelToLoad);
	}
}