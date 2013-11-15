﻿using UnityEngine;
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


	// Use this for initialization
	void Start () {
		startTime = Time.time;

		if (!fadeIn) {
			AudioListener.volume = 1;
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

		GUI.depth = 0;
		GUI.color = new Color(0, 0, 0, alphaBlend);
		GUI.DrawTexture(new Rect( 0, 0, Screen.width, Screen.height ), this.blankScreenTexture, ScaleMode.StretchToFill, true);
		GUI.depth = 1;
		AudioListener.volume = percentageDone;

		return percentageDone;
	}

	private float drawFadeOut() {

		float percentageDone = (Time.time - fadeOutStart) / fadeLength;
		float alphaBlend = (255 * percentageDone) / 255;

		GUI.color = new Color(0, 0, 0, alphaBlend);
		GUI.DrawTexture(new Rect( 0, 0, Screen.width, Screen.height ), this.blankScreenTexture, ScaleMode.StretchToFill, true);

		AudioListener.volume = 1 - percentageDone;

		return percentageDone;
	}


}
