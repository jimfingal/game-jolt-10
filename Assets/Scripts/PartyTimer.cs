using UnityEngine;
using System.Collections;

public class PartyTimer : MonoBehaviour {


	private StatsTracker stats;

	void Awake () {
		stats = GameObject.FindGameObjectWithTag("StatsTracker").GetComponent<StatsTracker>();
	}

	void Start() {
		stats.startTimer();
	}

	void OnDestroy () {
		stats.stopTimer();
	}

}
