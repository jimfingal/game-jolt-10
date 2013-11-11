#pragma strict

import System.Collections.Generic;

var player : GameObject;
var dialog : GameObject;

private var enable_dialog_key : boolean = false;
var in_conversation : boolean = false;

private var dialog_component : MonoBehaviour;
private var player_movement : MonoBehaviour;
private var player_camera : MonoBehaviour;

private var player_movement_scripts : Component[];
var dialog_script : Component;


function OnTriggerEnter (other : Collider) {
		Debug.Log ("Entered Trigger");
		enable_dialog_key = true;
}

function OnTriggerExit (other : Collider) {
		Debug.Log ("Exited Trigger");
		enable_dialog_key = false;
}


function Start () {
	this.player_movement_scripts = player.GetComponents(MonoBehaviour);
	this.dialog_script = dialog.GetComponent(MonoBehaviour);
}

function Update () {

		if (this.enable_dialog_key && Input.GetButton("Talk") && !this.in_conversation) {

			Debug.Log("Pressed 'Talk' Button");
			this.in_conversation = true;
			
			for (var script : Component in this.player_movement_scripts) {
				(script as MonoBehaviour).enabled = false;
			 } 
			 
			(dialog_script as MonoBehaviour).enabled = true;
			 			
			player.transform.LookAt(new Vector3(transform.position.x, transform.position.y, transform.position.z));
			// TOOD: face the person
			
		}


		if (this.in_conversation && !(dialog_script as MonoBehaviour).enabled) {
			this.in_conversation = false;
			for (var script : Component in this.player_movement_scripts) {
				(script as MonoBehaviour).enabled = true;
			} 	
		} 

}