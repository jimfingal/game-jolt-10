using UnityEngine;
using System.Collections;

public class StartSongMidway : MonoBehaviour {

	private AudioSource source;
	void Start () {
		source = gameObject.GetComponent<AudioSource>();
		source.Stop ();
		source.time = 100;
		source.Play ();
	}
}
