using UnityEngine;
using System.Collections;

public class CursorLocker : MonoBehaviour {
	
	private static bool created = false;

	void Awake() {
		
		if (!created) {
			DontDestroyOnLoad(transform.gameObject);
			created = true;
		} else {
			Destroy(this.gameObject);
		} 
		
	}


	void DidLockCursor() {
		Debug.Log("Locking cursor");
	}
	void DidUnlockCursor() {
		Debug.Log("Unlocking cursor");
	}

	private bool wasLocked = false;

	void Start() {

		Screen.lockCursor = true;
		Screen.lockCursor = false;
		Screen.lockCursor = true;

		unlockCursor = true;
		unlockCursor = false;

	}

	public static bool unlockCursor = false;
	public bool unlockCursorWatcher = false;


	void Update() {
		unlockCursorWatcher = unlockCursor;

		if (!unlockCursor) {

			if (Input.GetAxis("Horizontal") !=0 || Input.GetAxis("Vertical") !=0) {
				Screen.lockCursor = true;
			}

		} else {
			Screen.lockCursor = false;
		}

	}
}
