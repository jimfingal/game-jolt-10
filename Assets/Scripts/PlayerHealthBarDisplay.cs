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

	//public Texture2D progressForeground;

	private Vector2 pos;
	private Vector2 size;

	private PlayerHealthBar playerHealthBar;

	public bool drawProgress = true;

	public GUISkin customSkin;

	void OnGUI()
	{
		//draw the background:
		if (drawProgress) {

			GUI.Box(new Rect (pos.x, pos.y, size.x, size.y), "");

			GUI.skin = customSkin;
			GUI.Box(new Rect(pos.x, pos.y, playerHealthBar.getHealthPercentage() * size.x, size.y), "");
			GUI.skin = null;

		}
	}
	
	void Update()
	{
		//the player's health
		barDisplay = this.playerHealthBar.getHealthPercentage();
	}

	void Start() 
	{
		this.playerHealthBar = this.gameObject.GetComponent<PlayerHealthBar>();

		this.pos = new Vector2(boxPosX,boxPosY);
		this.size = new Vector2(boxSizeX,boxSizeY);
	}
}