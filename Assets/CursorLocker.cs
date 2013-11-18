using UnityEngine;
using System.Collections;

public class CursorLocker : MonoBehaviour {

	void DidLockCursor() {
		Debug.Log("Locking cursor");
	}
	void DidUnlockCursor() {
		Debug.Log("Unlocking cursor");
	}
	void OnMouseDown() {
		Screen.lockCursor = true;
	}

	private bool wasLocked = false;

	void Start() {
		Screen.lockCursor = true;
	}

	void Update() {

		if (Input.GetKeyDown("escape")) {
			Screen.lockCursor = false;
		}
		
		if (!Screen.lockCursor && wasLocked) {
			wasLocked = false;
		} else if (Screen.lockCursor && !wasLocked) {
			wasLocked = true;
		}

	}
}
