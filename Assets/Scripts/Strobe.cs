using UnityEngine;
using System.Collections;

public class Strobe : MonoBehaviour {

	// Use this for initialization

	public int strobeEveryXFrames;
	private Light strobe;

	private int frame;

	void Start () {
		strobe = gameObject.GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
		frame++;

		if (frame % strobeEveryXFrames == 0) {
			strobe.enabled = !strobe.enabled;
		}

		if (frame % 1000000 == 0) {
			frame = 0;
		}
	
	}
}
