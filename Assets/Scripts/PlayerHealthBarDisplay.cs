using UnityEngine;
using System.Collections;

public class PlayerHealthBarDisplay : MonoBehaviour
{

	//current progress
	public float barDisplay;


	public int boxPosX = 10;
	public int boxPosY = 500;

	public int boxSizeX = 250;
	public int boxSizeY = 50;

	private Vector2 pos;
	private Vector2 size;

	private PlayerHealthBar playerHealthBar;

	Texture progressBackground;	
	Texture progressForground;

	void OnGUI()
	{
		//draw the background:

		GUI.Box(new Rect(pos.x, pos.y, size.x, size.y), progressBackground);
		GUI.Box(new Rect(pos.x, pos.y, playerHealthBar.getHealthPercentage() * size.x, size.y), progressForground);
	}
	
	void Update()
	{
		//the player's health
		barDisplay = this.playerHealthBar.getHealthPercentage();
	}

	void Start() 
	{
		this.playerHealthBar = this.gameObject.GetComponent<PlayerHealthBar>();

		this.pos = new Vector2(10,500);
		this.size = new Vector2(250,50);
	}
}