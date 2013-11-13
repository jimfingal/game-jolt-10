using UnityEngine;
using System.Collections;

public class BobWhenMove : MonoBehaviour {

	public GameObject camera;

	public float bobIntensity = 2;
	public float bobFrequency = 7;

	public float currentTranslation;
	public float lastTranslation = 0;

	private float initialY;
	private float dt = 0;

	// Use this for initialization
	void Start () {
		initialY = camera.transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetAxis ("Horizontal") != 0 || Input.GetAxis ("Vertical") != 0 ) {
			this.dt += Time.deltaTime * bobFrequency;

			currentTranslation = Mathf.Sin(this.dt) * bobIntensity;

			camera.transform.Translate(0,  currentTranslation - lastTranslation, 0);

			lastTranslation = currentTranslation;

		}
	}
}
