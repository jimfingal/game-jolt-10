using UnityEngine;
using System.Collections;

public class CameraOutsideZoom : MonoBehaviour {

	public GameObject player;
	public float playerDistance = 1200;
	public float cameraHeight = 70;

	public float initialX = -200;
	public float targetX = -500;

	public float initialZ = 1100;
	public float targetZ = 540;
	public float playerTargetZ = -100;

	public float playerDistanceAlongTrack;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		playerDistanceAlongTrack = (1200 - (player.transform.position.z - playerTargetZ)) / 1200;

		transform.position = new Vector3(initialX + (targetX - initialX) * playerDistanceAlongTrack, 
		                             cameraHeight, 
		                             initialZ + (targetZ - initialZ) * playerDistanceAlongTrack);
	
	}
}
