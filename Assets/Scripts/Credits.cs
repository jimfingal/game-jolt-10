using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour {

	public GUISkin skin;

	public string[] lines;


	void OnGUI() {

		if (this.skin) {
			GUI.skin = this.skin;
		}

		for (int i = 0; i < lines.Length; i++) {

			GUI.depth = 1;
			GUI.Label(new Rect (0,  i * (Screen.height / lines.Length), Screen.width, 50), lines[i]);
		}


	}
}
