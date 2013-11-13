using UnityEngine;
using System.Collections;

public class GameStartup : MonoBehaviour {

	// Use this for initialization
	void Start () {

		if (Application.isWebPlayer) {
			Screen.SetResolution (960, 600, false, 60);
		}
	}

}
