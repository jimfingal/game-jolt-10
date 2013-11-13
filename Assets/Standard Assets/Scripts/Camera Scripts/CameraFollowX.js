#pragma strict

var target : Transform;
var CameraDistance : int=0;
var CameraHeight : int=0;

function Start () {

}

function Update () 
{
	transform.position = Vector3(-CameraDistance, CameraHeight, target.transform.position.z);
}