using UnityEngine;
using System.Collections;

public class Playlist : MonoBehaviour {

	public AudioSource source;
	public AudioClip[] songs;
	public bool loop = true;
	public float delayBetweenTracks = 2f;

	private int index = 0;

	void Start () {

		source.loop = false;

		if (songs.Length > 0) {
			playClip(index);
		}
	
	}

	void playClip(int index) {

		source.Stop();

		AudioClip nextSong = songs[index];

		source.clip = nextSong;

		float trackLength = nextSong.length;
	
		source.Play();
	
		index++;

		// If we're at the end of the playlist
		if (index >= songs.Length) {

			// If we loop, queue the first song back up
			if (loop) {
				index = 0;
				StartCoroutine("queueNextSong", trackLength + delayBetweenTracks);
			} else {
				// We end here
			}
		} else {

			// Otherwise just queue up the next song.
			StartCoroutine("queueNextSong", trackLength + delayBetweenTracks);
		}
	
	}

	// Wait a certain amount of time then play the track
	IEnumerator queueNextSong(int waitTime) {

		yield return new WaitForSeconds(waitTime);
		playClip(index);

	}

}