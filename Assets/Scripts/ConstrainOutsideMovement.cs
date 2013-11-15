using UnityEngine;
using System.Collections;

public class ConstrainOutsideMovement : MonoBehaviour {

	// Use this for initialization

	private float initialXPosition;
	
	void Start () {
		initialXPosition = transform.position.x;
	}
	
	// Update is called once per frame
	void Update () {

		transform.position = new Vector3(initialXPosition ,transform.position.y, transform.position.z);
	
	}
}
