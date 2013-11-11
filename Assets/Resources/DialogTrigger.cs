using UnityEngine;
using System.Collections;

public class DialogTrigger : MonoBehaviour {

	public GameObject player;
	public GameObject dialog;

	bool enable_dialog_key = false;
	bool in_dialog = false;

	bool just_was_talking = false;

	void OnTriggerEnter(Collider other) {
		Debug.Log("Object entered the trigger");
		enable_dialog_key = true;
	}

	void OnTriggerExit(Collider other) {
		Debug.Log("Object Exited the trigger");
		enable_dialog_key = false;	
	}

	void Start() {
		// this.dialog_script = dialog.GetComponent<DialogInstance>();
	}

	// Update is called once per frame
	void Update () {
	
		if (Input.GetButton("Talk")) {

			Debug.Log("Pressed 'Talk' Button");

			if (enable_dialog_key) {
				Debug.Log("We would enter dialog here");
				// this.dialog_script.enabled = true;
				player.GetComponent<CharacterController>().enabled = false;
				in_dialog = true;
				this.dialog.SetActive(true);
				just_was_talking = true;
			}
		}

		if (this.just_was_talking && !this.dialog.activeSelf) {

			this.just_was_talking = false;
			player.GetComponent<CharacterController>().enabled = true;
		
		}

	}
}
