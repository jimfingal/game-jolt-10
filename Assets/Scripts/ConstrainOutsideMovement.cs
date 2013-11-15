using UnityEngine;
using System.Collections;

public class ConstrainOutsideMovement : MonoBehaviour {

	// Use this for initialization

	private float initialXPosition;

	private Quaternion initialRotation;


	void Start () {
		initialXPosition = transform.position.x;
		initialRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {

		transform.position = new Vector3(initialXPosition ,transform.position.y, transform.position.z);
		// transform.rotation = initialRotation;

	}
}
