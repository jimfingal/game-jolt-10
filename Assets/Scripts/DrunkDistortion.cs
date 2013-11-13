using UnityEngine;
using System.Collections;

public class DrunkDistortion : MonoBehaviour {


	public GameObject drunkStatBucket;
	public float distortionThreshold = 30;

	private PlayerStatBar drunkStat;
	private AudioReverbZone reverbZone;

	// Use this for initialization
	void Start () {
		this.drunkStat = drunkStatBucket.GetComponent<PlayerStatBar>();
		this.reverbZone = gameObject.GetComponent<AudioReverbZone>();
	}
	
	// Update is called once per frame
	void Update () {
		if (this.drunkStat.getStatValue() < distortionThreshold) {
			this.reverbZone.enabled = false;
			this.reverbZone.reverbPreset = AudioReverbPreset.Drugged;
			this.reverbZone.enabled = true;
		} else {
	
			this.reverbZone.enabled = false;
			this.reverbZone.reverbPreset = AudioReverbPreset.Livingroom;
			this.reverbZone.enabled = true;

		}
	}
}
