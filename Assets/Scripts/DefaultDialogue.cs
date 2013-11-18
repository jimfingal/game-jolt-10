using UnityEngine;
using System.Collections;

public class DefaultDialogue : MonoBehaviour {

	public float moodThreshold = 10;
	public float sobrietyThreshold = 20;

	public GameObject[] defaultSad;

	public GameObject[] defaultDrunk;


	private int mood_index = 0;
	private int sobriety_index = 0;

	private PlayerStatBar mood;
	private PlayerStatBar sobriety;

	// Use this for initialization
	void Start () {
		this.mood = GameObject.FindGameObjectWithTag("PlayerHealth").GetComponent<PlayerStatBar>();
		this.sobriety = GameObject.FindGameObjectWithTag("PlayerSobriety").GetComponent<PlayerStatBar>();

		mood_index = Random.Range(0, defaultSad.Length - 1);
		sobriety_index = Random.Range(0, defaultDrunk.Length - 1);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public MonoBehaviour getDefaultOption() {

		if (mood.currentValue < moodThreshold) {

			MonoBehaviour option = defaultSad[mood_index].GetComponent<MonoBehaviour>();
			mood_index++;
			if (mood_index >= defaultSad.Length) {
				mood_index = 0;
			}
			return option;
		} else if (sobriety.currentValue < sobrietyThreshold) {

			MonoBehaviour option = defaultDrunk[sobriety_index].GetComponent<MonoBehaviour>();
			sobriety_index++;
			if (sobriety_index >= defaultDrunk.Length) {
				sobriety_index = 0;
			}
			return option;
		}

		return null;
	}

}
