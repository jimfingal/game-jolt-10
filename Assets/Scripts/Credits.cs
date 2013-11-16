using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour {

	public GUISkin skin;

	public Texture texture;
	public int strobeEvery;
	public bool strobeEnabled = false;

	private bool strobe = false;
	private int frame = 1;

	public int mainFontSize = 20;
	public int otherFontSize = 12;

	public string[] lines;

	public string[] attributionLines;

	public SceneFader fader;

	void Start() {
		fader = GameObject.FindGameObjectWithTag("SceneFader").GetComponent<SceneFader>();
	}

	void Update() {

		if (strobeEnabled) {
			frame++;

			if (frame % strobeEvery == 0) {
				strobe = true;
			} else {
				strobe = false;
			}
		}

	}

	void OnGUI() {

		GUI.depth = 1;

		float lastY = 25;

		if (this.skin) {
			GUI.skin = this.skin;
		}

		this.skin.label.fontSize = mainFontSize;

		for (int i = 0; i < lines.Length; i++) {

			lastY = lastY + (mainFontSize * 2);
			GUI.Label(new Rect (0,  lastY, Screen.width, 50), lines[i]);
		
		}

		
		if (GUI.Button(new Rect(Screen.width/2 - 100, lastY + 50, 200, 30), "Play again")) {
			fader.triggerFadeOut("Startgame");
		}

		this.skin.label.fontSize = otherFontSize;
	
		lastY += 100;

		for (int i = 0; i < attributionLines.Length; i++) {

			lastY = lastY + (otherFontSize * 2);
			GUI.Label(new Rect (0,  lastY, Screen.width, 50), attributionLines[i]);
		}

		if (strobeEnabled && strobe) {
			GUI.DrawTexture(new Rect (Random.Range(0, Screen.width / 2),  Random.Range(0, Screen.height / 2), texture.width, texture.height), texture);
		}


	}




}
