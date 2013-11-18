using UnityEngine;
using System.Collections;

public class StatsTracker : MonoBehaviour {

	public float timeSpentAtParty = 0;
	public float playerMood;
	public float playerSobriety;

	private bool timerStarted = false;

	private static bool created = false;

	void Awake() {

		if (!created) {
			DontDestroyOnLoad(transform.gameObject);
			created = true;
		} else {
			Destroy(this.gameObject);
		} 

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
