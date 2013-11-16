using UnityEngine;
using System.Collections;

public class DrunkEffects : MonoBehaviour {

	public GameObject camera;
	public GameObject boombox;

	private AudioReverbZone reverbZone;
	private AudioSource audioSource;

	public float dopplerThreshold = 20f;
	public float distortionThreshold = 30f;
	public float wobbleThreshold = 30f;

	public float dopplerAmount = 0.5f;

	private Vector3 rotation = Vector3.zero;

	public float frequency = 7f;
	public float zIntensity = 0.5f;
	public float xIntensity = 0.5f;

	public float smooth = 2.0f;

	private float dt = 0;


	public GameObject moodObject;
	public GameObject sobrietyObject;
	
	private PlayerStatBar mood;
	private PlayerStatBar sobriety;
	
	private float lastSobriety;
	
	public float oscillationThreshold = 40;
	public float oscillationAmount = 5;



	// Use this for initialization
	void Start () {

		
		mood = moodObject.GetComponent<PlayerStatBar>();
		sobriety = sobrietyObject.GetComponent<PlayerStatBar>();
		
		lastSobriety = sobriety.getStatValue();

		this.reverbZone = boombox.GetComponent<AudioReverbZone>();
		this.audioSource = boombox.GetComponent<AudioSource>();

	}
	
	// Update is called once per frame
	void Update () {

		// Oscillate mood once we pass a certain sobriety threshold
		if (lastSobriety >= oscillationThreshold && this.sobriety.getStatValue() < oscillationThreshold) {
			mood.oscillate(oscillationAmount);
		}


		// Add to mood whatever we lose in sobriety
		float diff = lastSobriety - this.sobriety.getStatValue();
		
		if (diff > 0) {
			mood.currentValue += diff / 3;
		}
		lastSobriety = this.sobriety.getStatValue();


		// Test for effects

		if (this.sobriety.getStatValue() < wobbleThreshold) {

			this.dt += Time.deltaTime * frequency;

			float sinTranslationAmount = Mathf.Sin(this.dt);;
			float cosTranslationAmount =  Mathf.Sin(this.dt + 0.85f);


			rotation.x = sinTranslationAmount * xIntensity;
			rotation.y = 0;
			rotation.z = cosTranslationAmount * zIntensity;
		
			camera.transform.Rotate(rotation);

		} 

		if (this.sobriety.getStatValue() < distortionThreshold) {
			this.reverbZone.enabled = false;
			this.reverbZone.reverbPreset = AudioReverbPreset.Drugged;
			this.reverbZone.enabled = true;
		} else {
			this.reverbZone.enabled = false;
			this.reverbZone.reverbPreset = AudioReverbPreset.Livingroom;
			this.reverbZone.enabled = true;
			
		}

		if (this.sobriety.getStatValue() < dopplerThreshold) {
			this.audioSource.dopplerLevel = dopplerAmount;
			this.reverbZone.enabled = false;
			this.reverbZone.reverbPreset = AudioReverbPreset.Psychotic;
			this.reverbZone.enabled = true;
		} else {
			this.audioSource.dopplerLevel = 0f;
		}
	}
}
