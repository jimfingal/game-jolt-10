using UnityEngine;
using System.Collections;

public class Title : MonoBehaviour {


	public Texture smallTexture;
	public Texture mediumTexture;
	public Texture largeTexture;

	public float smallDelay = 0;
	public float mediumDelay = 1;
	public float largeDelay = 2;

	public bool smallEnabled;
	public bool mediumEnabled;
	public bool largeEnabled;
	private AudioSource sound;

	private Rect frame;

	public float dt;

	// Use this for initialization
	void Start () {

		smallEnabled = false;
		mediumEnabled = false;
		largeEnabled = false;

		frame = new Rect(0,0,Screen.width,Screen.height);
	
		sound = gameObject.GetComponent<AudioSource>();
		StartCoroutine("triggerAnimation");

	}

	void playSound() {
		sound.Stop();
		sound.Play();
	}

	IEnumerator triggerAnimation() {

		sound.loop = false;

		yield return new WaitForSeconds(smallDelay);

		smallEnabled = true;
		playSound();

		yield return new WaitForSeconds(mediumDelay);

		mediumEnabled = true;
		playSound();

		yield return new WaitForSeconds(largeDelay);

		largeEnabled = true;
		playSound();

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
