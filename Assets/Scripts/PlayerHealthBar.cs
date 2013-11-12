using UnityEngine;
using System.Collections;

public class PlayerHealthBar : MonoBehaviour {

	public float totalHealth = 100;
	public float currentHealth = 100;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		currentHealth = (Mathf.Sin(Time.time) * 47) + 50;
	
	}

	public float getHealthPercentage() {
		return currentHealth / totalHealth;
	}
}
