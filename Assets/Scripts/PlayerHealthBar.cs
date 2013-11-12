using UnityEngine;
using System.Collections;

public class PlayerHealthBar : MonoBehaviour {

	float totalHealth = 100;
	float currentHealth = 100;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public float getHealthPercentage() {
		return currentHealth / totalHealth;
	}
}
