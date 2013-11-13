using UnityEngine;
using System.Collections;

public class DrunkEffects : MonoBehaviour {

	public GameObject camera;
	public GameObject drunkStatContainer;
	public GameObject boombox;

	private PlayerStatBar drunkStat;
	private AudioReverbZone reverbZone;

	public float distortionThreshold = 30;
	public float wobbleThreshold = 30;

	private Vector3 rotation = Vector3.zero;
	private Quaternion initalRotation;

	public float frequency = 7;
	public float zIntensity = 2;
	public float xIntensity = 2;

	public float currentSinTranslation;
	public float lastSinTranslation = 0;	

	public float currentCosTranslation;
	public float lastCosTranslation = 0;	
	

	public float smooth = 2.0f;

	private float dt = 0;


	// Use this for initialization
	void Start () {
		this.drunkStat = drunkStatContainer.GetComponent<PlayerStatBar>();
		this.reverbZone = boombox.GetComponent<AudioReverbZone>();
		this.initalRotation = camera.transform.rotation;

	}
	
	// Update is called once per frame
	void Update () {
		
		if (this.drunkStat.getStatValue() < wobbleThreshold) {

			this.dt += Time.deltaTime * frequency;
			currentSinTranslation = Mathf.Sin(this.dt);
			float sinTranslationAmount = currentSinTranslation - lastSinTranslation;

			currentCosTranslation = Mathf.Sin(this.dt + 0.85f);
			float cosTranslationAmount = currentCosTranslation - lastCosTranslation;


			rotation.x = sinTranslationAmount * xIntensity;
			rotation.y = 0;
			rotation.z = cosTranslationAmount * zIntensity;
		
			camera.transform.Rotate(rotation);

		} 

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
