using UnityEngine;
using System.Collections;

public class PlayerStatBar : MonoBehaviour {

	public string label;
	public float maxValue = 100;
	public float currentValue = 100;
	public float fadeSpeed = 0.1f;

	private bool toggleOscillate = false;
	private float oscillationStartTime;

	private float oscillationStart = -1;

		
	// Update is called once per frame
	void Update () {

		testFade();

		testOscillate(5);

	}

	public void testFade() {

		if (Input.GetButtonDown("Test1")) {

			Debug.Log("Pressed the test1 button");

			fadeBar(-10);

		}

		if (currentValue <= 5) {
			currentValue = 5;
		}
	}

	public void testOscillate(float amount) {

		if (Input.GetButtonDown("Test2")) {

			Debug.Log("Pressed the test2 button");

			if (oscillationStart > 0) {
				currentValue = oscillationStart;
			}
			toggleOscillate = !toggleOscillate;
			oscillationStart = currentValue;
			oscillationStartTime = Time.time;
		}

		if (toggleOscillate) {

			float oscillationAmount = Mathf.Sin(Time.time - oscillationStartTime) * amount;
			currentValue = oscillationStart + oscillationAmount;

		}
	}

	public float getStatValue() {
		
		return currentValue;
		
	}

	public float getStatPercentage() {
	
		return currentValue / maxValue;
	
	}

	public void oscillateHealthBar(float amount) {
		
		StartCoroutine("Fade", amount);
		
	}


	public void fadeBar(int amount) {

		StartCoroutine("Fade", amount);

	}

	IEnumerator Fade(int amount) {

		// Debug.Log("In Coroutine. My current health is: " + currentHealth + " and target amount is " + amount);

		float target = currentValue + amount;

		if (amount > 0) {
			
			for (float f = currentValue; f <= target; f += fadeSpeed) {
				currentValue += fadeSpeed;
				//Debug.Log("Setting current health to " + f);
				yield return new WaitForSeconds(1 / 60);
			}
			
			
		} else {
			
			for (float f = currentValue; f >= target; f -= fadeSpeed) {
				currentValue -= fadeSpeed;
				//Debug.Log("Setting current health to " + f);
				yield return new WaitForSeconds(1 / 60);
			}
			
		}
		
	}

}
