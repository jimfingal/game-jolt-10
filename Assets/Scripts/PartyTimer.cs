using UnityEngine;
using System.Collections;

public class PartyTimer : MonoBehaviour {


	private StatsTracker stats;

	void Awake () {
		if (GameObject.FindGameObjectWithTag("StatsTracker")) {
			stats = GameObject.FindGameObjectWithTag("StatsTracker").GetComponent<StatsTracker>();
		} else {
			Debug.Log("Stats Tracker not set, timer will not function.");
		}
	}

	void Start() {
		if (stats) {
			stats.startTimer();
		}
	}

	void OnDestroy () {
		if (stats) {
			stats.stopTimer();
		}
	}

}
