using UnityEngine;
using System.Collections;

public class GameStartup : MonoBehaviour {


	private StatsTracker stats;

	// Use this for initialization
	void Start () {

		if (Application.isWebPlayer) {
			Screen.SetResolution (960, 600, false, 60);
		}

		stats = GameObject.FindGameObjectWithTag("StatsTracker").GetComponent<StatsTracker>();

	}

	void OnEnabled() {
		stats.timeSpentAtParty = 0;
	}

	void Update() {

		// Hack 
		stats.timeSpentAtParty = 0;
	}

}
