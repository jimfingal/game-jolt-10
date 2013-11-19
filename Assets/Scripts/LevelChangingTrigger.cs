using UnityEngine;
using System.Collections;

public class LevelChangingTrigger : MonoBehaviour {

	public string levelToTransitionTo;

	private SceneFader fader;


	// Use this for initialization
	void Start () {
		this.fader = gameObject.GetComponent<SceneFader>();
	}
	

	void OnTriggerEnter (Collider other) {
		this.fader.triggerFadeOut(levelToTransitionTo);
		CursorLocker.setUnlockCursor(true);
	}


}

	