using UnityEngine;
using System.Collections;

public class InexorableMovement : MonoBehaviour {

	public GameObject movable;

	public Vector3 speed;

	// Update is called once per frame
	void Update () {

		if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") !=0) {
			movable.transform.Translate(speed * Time.deltaTime, Space.World);
		}
	}
}
