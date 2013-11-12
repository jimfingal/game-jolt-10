using UnityEngine;
using System.Collections;

public class PlayerHealthBar : MonoBehaviour {

	public float totalHealth = 100;
	public float currentHealth = 100;

	public float fadeSpeed = 0.1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		testFade();

	}

	public void testFade() {

		if (Input.GetButton("Test")) {

			Debug.Log("Pressed the test button");

			fadeHealthBar(10);

		}

		if (currentHealth <= 5) {
			currentHealth = 5;
		}
	}

	public float getHealthPercentage() {
		return currentHealth / totalHealth;
	}


	public void fadeHealthBar(int amount) {

		StartCoroutine("Fade", amount);

	}

	IEnumerator Fade(int amount) {

		Debug.Log("In Coroutine. My current health is: " + currentHealth + " and target amount is " + amount);

		float target = currentHealth + amount;

		if (amount > 0) {
			
			for (float f = currentHealth; f <= target; f += fadeSpeed) {
				currentHealth = f;
				Debug.Log("Setting current health to " + f);
				yield return new WaitForSeconds(1 / 60);
			}
			
			
		} else {
			
			for (float f = currentHealth; f >= target; f -= fadeSpeed) {
				currentHealth = f;
				Debug.Log("Setting current health to " + f);
				yield return new WaitForSeconds(1 / 60);
			}
			
		}
		
	}

}
