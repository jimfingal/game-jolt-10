using UnityEngine;
using System.Collections;

public class CameraEndZoom : MonoBehaviour {

	public GameObject player;
	public float playerDistance = 1200;
	public float cameraHeight = 40;

	public Vector3 initialPosition;
	public Vector3 targetPosition;

	public float initialX = 65;
	public float targetX = -500;

	public float initialZ = 1100;
	public float targetZ = 540;
	public float playerTargetZ = 1100;

	public float playerDistanceAlongTrack;

	public Vector3 targetFocus;
	public Vector3 endFocus;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		playerDistanceAlongTrack = 1 - ((playerTargetZ - player.transform.position.z) / 1200);

		transform.position = initialPosition + (targetPosition - initialPosition) * playerDistanceAlongTrack;


		transform.LookAt(targetFocus);
	
	}
}
