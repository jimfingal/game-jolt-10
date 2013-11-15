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

		if (this.skin) {
			GUI.skin = this.skin;
		}

		this.skin.label.fontSize = mainFontSize;

		for (int i = 0; i < lines.Length; i++) {

			GUI.Label(new Rect (0,  50 + i * (mainFontSize * 2), Screen.width, 50), lines[i]);
		}

		this.skin.label.fontSize = otherFontSize;
	
		for (int i = 0; i < attributionLines.Length; i++) {

			GUI.Label(new Rect (0,  100 + Screen.height/3 + i * (otherFontSize * 2), Screen.width, 50), attributionLines[i]);
		}

		if (strobeEnabled && strobe) {
			GUI.DrawTexture(new Rect (Random.Range(0, Screen.width / 2),  Random.Range(0, Screen.height / 2), texture.width, texture.height), texture);

		}

	}




}
