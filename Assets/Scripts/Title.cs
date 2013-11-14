using UnityEngine;
using System.Collections;

public class Title : MonoBehaviour {


	public Texture smallTexture;
	public Texture mediumTexture;
	public Texture largeTexture;

	private bool smallEnabled;
	private bool mediumEnabled;
	private bool largeEnabled;
	private AudioSource sound;

	private Rect frame;

	// Use this for initialization
	void Start () {

		smallEnabled = true;
		mediumEnabled = true;
		largeEnabled = true;

		frame = new Rect(0,0,Screen.width,Screen.height);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnGUI() {

		if (smallEnabled) {
			GUI.DrawTexture(frame, smallTexture, ScaleMode.ScaleToFit, true);
		}

		
		if (mediumEnabled) {
			GUI.DrawTexture(frame, mediumTexture, ScaleMode.ScaleToFit, true);
		}

		
		if (largeEnabled) {
			GUI.DrawTexture(frame, largeTexture, ScaleMode.ScaleToFit, true);
		}
	}

}
