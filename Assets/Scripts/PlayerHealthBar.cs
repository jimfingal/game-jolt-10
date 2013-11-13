using UnityEngine;
using System.Collections;

public class PlayerHealthBar : MonoBehaviour {

	public float totalHealth = 100;
	public float currentHealth = 100;
	public float fadeSpeed = 0.1f;

	private bool toggleOscillate = false;
	private float oscillationStartTime;


	private float oscillationStart = -1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		testFade();

		testOscillate(5);

	}

	public void testFade() {

		if (Input.GetButtonDown("Test1")) {

			Debug.Log("Pressed the test1 button");

			fadeHealthBar(-10);

		}

		if (currentHealth <= 5) {
			currentHealth = 5;
		}
	}

	public void testOscillate(float amount) {

		if (Input.GetButtonDown("Test2")) {
			Debug.Log("Pressed the test2 button");

			if (oscillationStart > 0) {
				currentHealth = oscillationStart;
			}
			toggleOscillate = !toggleOscillate;
			oscillationStart = currentHealth;
			oscillationStartTime = Time.time;
		}

		if (toggleOscillate) {

			float oscillationAmount = Mathf.Sin(Time.time - oscillationStartTime) * amount;
			currentHealth = oscillationStart + oscillationAmount;

		}
	}

	public float getHealthPercentage() {
	
		return currentHealth / totalHealth;
	
	}

	public void oscillateHealthBar(float amount) {
		
		StartCoroutine("Fade", amount);
		
	}


	public void fadeHealthBar(int amount) {

		StartCoroutine("Fade", amount);

	}

	IEnumerator Fade(int amount) {

		// Debug.Log("In Coroutine. My current health is: " + currentHealth + " and target amount is " + amount);

		float target = currentHealth + amount;

		if (amount > 0) {
			
			for (float f = currentHealth; f <= target; f += fadeSpeed) {
				currentHealth += fadeSpeed;
				//Debug.Log("Setting current health to " + f);
				yield return new WaitForSeconds(1 / 60);
			}
			
			
		} else {
			
			for (float f = currentHealth; f >= target; f -= fadeSpeed) {
				currentHealth -= fadeSpeed;
				//Debug.Log("Setting current health to " + f);
				yield return new WaitForSeconds(1 / 60);
			}
			
		}
		
	}

}
