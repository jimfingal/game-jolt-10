using UnityEngine;
using System.Collections;

public class ConstrainOutsideMovement : MonoBehaviour {

	// Use this for initialization

	private float initialXPosition;
	private float initialYRotation;


	void Start () {
		initialXPosition = transform.position.x;
		initialYRotation = transform.rotation.y;
	}
	
	// Update is called once per frame
	void Update () {

		transform.position = new Vector3(initialXPosition ,transform.position.y, transform.position.z);
		//transform.rotation = new Vector3(rotation.x,rotations.y,0);

	}
}
