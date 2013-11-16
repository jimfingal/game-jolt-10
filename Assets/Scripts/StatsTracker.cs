using UnityEngine;
using System.Collections;

public class StatsTracker : MonoBehaviour {

	public float timeSpentAtParty = 0;
	public float playerMood;
	public float playerSobriety;

	private bool timerStarted = false;

	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}

	// Update is called once per frame
	void Update () {

		if (timerStarted) {
			timeSpentAtParty += Time.deltaTime;
		}
	}

	public void startTimer() {
		timerStarted = true;
	}

	public void stopTimer() {
		timerStarted = false;
	}

	public void save() {

		if (GameObject.FindGameObjectWithTag("PlayerHealth")) {
			this.playerMood = GameObject.FindGameObjectWithTag("PlayerHealth").GetComponent<PlayerStatBar>().currentValue;
		}

		if (GameObject.FindGameObjectWithTag("PlayerSobriety")) {
			this.playerSobriety = GameObject.FindGameObjectWithTag("PlayerSobriety").GetComponent<PlayerStatBar>().currentValue;
		}
	}
}
