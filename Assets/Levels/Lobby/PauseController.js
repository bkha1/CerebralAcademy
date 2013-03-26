var gamePaused : boolean = false;

function Update()
{
	if(Input.GetKeyDown(KeyCode.Escape))//user its escape key
	{
		if(gamePaused)//if game is already paused, unpause the game
		{
			Time.timeScale = 1;
			gamePaused = false;
			gameObject.GetComponent(PauseMenu).enabled = false;
			gameObject.GetComponent(MouseLook).enabled = true;
			transform.parent.GetComponent(MouseLook).enabled = true;
			//NotificationCenter.DefaultCenter.PostNotification(this, "UnPauseEvent");
		}
		else//if game isnt paused, pause the game
		{
			Time.timeScale = 0;
			gamePaused = true;
			gameObject.GetComponent(PauseMenu).enabled = true;
			gameObject.GetComponent(MouseLook).enabled = false;
			transform.parent.GetComponent(MouseLook).enabled = false;
			//NotificationCenter.DefaultCenter.PostNotification(this, "PauseEvent");
		}
	}
}//end update

function setPaused( x : boolean)
{
	gamePaused = x;
}