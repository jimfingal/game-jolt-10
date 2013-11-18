using UnityEngine;
using System.Collections;

public class DefaultDialogue : MonoBehaviour {

	public float moodThreshold = 10;
	public float sobrietyThreshold = 20;

	public GameObject[] defaultOptions = new GameObject[2];

	private int MOOD_INDEX = 0;
	private int SOBRIETY_INDEX = 1;

	private PlayerStatBar mood;
	private PlayerStatBar sobriety;

	// Use this for initialization
	void Start () {
		this.mood = GameObject.FindGameObjectWithTag("PlayerHealth").GetComponent<PlayerStatBar>();
		this.sobriety = GameObject.FindGameObjectWithTag("PlayerSobriety").GetComponent<PlayerStatBar>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public MonoBehaviour getDefaultOption() {

		if (mood.currentValue < moodThreshold) {
			return defaultOptions[MOOD_INDEX].GetComponent<MonoBehaviour>();
		} else if (sobriety.currentValue < sobrietyThreshold) {
			return defaultOptions[SOBRIETY_INDEX].GetComponent<MonoBehaviour>();
		}

		return null;
	}

}
