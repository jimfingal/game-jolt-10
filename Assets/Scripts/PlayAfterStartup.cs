using UnityEngine;
using System.Collections;

public class PlayAfterStartup : MonoBehaviour {


	private AudioSource source;

	// Use this for initialization
	void Start () {
		source = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!source.isPlaying) {
			source.Play();
		}
	}
}
