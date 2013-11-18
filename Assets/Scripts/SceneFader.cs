using UnityEngine;
using System.Collections;

public class SceneFader : MonoBehaviour {


	public Texture2D blankScreenTexture;
	public float fadeLength = 3;

	private float startTime;

	private string loadThisLevel;
	private float fadeOutStart;

	public bool fadeIn = true;
	private bool fadeOut = false;


	public bool autoFadeOut = false;
	public float autoFadeOutTimer = 10;
	public string autoTransitionTo;

	private StatsTracker stats;


	public float spy;

	// Use this for initialization
	void Start () {
		startTime = Time.time;

		if (fadeIn) {
			AudioListener.volume = 0;
		} else {
			AudioListener.volume = 1;
		}

		if ( GameObject.FindGameObjectWithTag("StatsTracker")) {
			stats = GameObject.FindGameObjectWithTag("StatsTracker").GetComponent<StatsTracker>();
		} else {
			Debug.Log("Stats Tracker not set, stats will not be saved.");
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (autoFadeOut && Time.time - startTime > autoFadeOutTimer) {
			triggerFadeOut(autoTransitionTo);
			autoFadeOut = false;
		}

	}


	void OnGUI() {

		GUI.depth = 0;

		if (inFadeIn()) {
			drawFadeIn();
		}

		if (inFadeOut()) {

			float percentageDone = drawFadeOut();

			if (percentageDone > 1) {
				Application.LoadLevel(loadThisLevel);
			}
		}

	}
	
	public void triggerFadeOut(string level) {

		fadeOut = true;
		loadThisLevel = level;
		fadeOutStart = Time.time;

		if (stats) {
			stats.save();
		}
	}

	private bool inFadeIn() {
		return fadeIn && (Time.time - startTime < fadeLength);
	}

	
	private bool inFadeOut() {
		return fadeOut;
	}

	private float drawFadeIn() {
		float percentageDone = (Time.time - startTime) / fadeLength;
		float alphaBlend = (255 - (255 * percentageDone)) / 255;

		GUI.color = new Color(0, 0, 0, alphaBlend);
		GUI.DrawTexture(new Rect( 0, 0, Screen.width, Screen.height ), this.blankScreenTexture, ScaleMode.StretchToFill, true);

		AudioListener.volume = percentageDone;

		spy = percentageDone;

		return percentageDone;
	}

	private float drawFadeOut() {

		float percentageDone = (Time.time - fadeOutStart) / fadeLength;
		float alphaBlend = (255 * percentageDone) / 255;

		GUI.color = new Color(0, 0, 0, alphaBlend);

		GUI.DrawTexture(new Rect( 0, 0, Screen.width, Screen.height ), this.blankScreenTexture, ScaleMode.StretchToFill, true);

		AudioListener.volume = 1 - percentageDone;

		spy = percentageDone;

		return percentageDone;
	}


}
