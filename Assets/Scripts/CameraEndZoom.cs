using UnityEngine;
using System.Collections;

public class CameraEndZoom : MonoBehaviour {

	public GameObject player;
	public float playerDistance = 1200;
	public float playerTargetZ = 1100;
	
	public Vector3 initialPosition;
	public Vector3 targetPosition;

	public float playerDistanceAlongTrack;

	public Vector3 targetFocus;
	public Vector3 endFocus;

	public float initialFieldOfView;
	public float targetFieldOfView;

	private Camera camera;

	// Use this for initialization
	void Start () {

		camera = gameObject.GetComponent<Camera>();

	}
	
	// Update is called once per frame
	void Update () {

		playerDistanceAlongTrack = 1 - ((playerTargetZ - player.transform.position.z) / playerDistance);

		transform.position = initialPosition + (targetPosition - initialPosition) * playerDistanceAlongTrack;


		transform.LookAt(targetFocus - (targetFocus - endFocus) * playerDistanceAlongTrack );

		camera.fieldOfView = initialFieldOfView + (targetFieldOfView - initialFieldOfView)  * playerDistanceAlongTrack;
		if (camera.fieldOfView > 170) {
			camera.fieldOfView = 170;
		}
	}
}
