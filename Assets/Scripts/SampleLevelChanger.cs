using UnityEngine;
using System.Collections;

public class SampleLevelChanger : MonoBehaviour {


	private SceneFader sceneFader;

	// Use this for initialization
	void Start () {
		GameObject tmp = GameObject.FindWithTag("SceneFader");
		this.sceneFader = tmp.GetComponent<SceneFader>();
	}

	void OnTriggerEnter(Collider other) {

		Debug.Log("Hit Trigger, Changing Scenes");
		sceneFader.triggerFadeOut("3D Living Room");
	}
}
