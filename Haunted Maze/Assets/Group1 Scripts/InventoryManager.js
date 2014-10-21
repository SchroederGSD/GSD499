#pragma strict

var iMode = false; //flag for whether inventory is on or off
internal var controlCenter : GameObject;

function Start () {
	
camera.enabled = false; 
controlCenter = GameObject.Find("Control Center"); //access the control center
iMode = controlCenter.GetComponent(GameManager).iMode;

}

function Update () {
	
	if(Input.GetKeyDown("i")) ToggleMode(); //call function if i key is pressed

}

function ToggleMode(){
	
	if(iMode) { //if in inventory mode, turn it off
		camera.enabled = false; //turn off inventory camera
		
		iMode = false; //change flag
		controlCenter.GetComponent(GameManager).iMode = false; //inform the manager
		return;
	}
	
	else{ //if not in inventory mode turn it on
		camera.enabled = true; //turn on inventory camera
		
		iMode = true; // change flag
		controlCenter.GetComponent(GameManager).iMode = true; //inform the manager
		return;
	}

}